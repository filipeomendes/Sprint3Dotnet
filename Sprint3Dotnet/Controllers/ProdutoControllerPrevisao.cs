using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

namespace Sprint3Dotnet.Controllers
{
    public class DadosProduto
    {
        [LoadColumn(0)] public string NomeProduto { get; set; }
        [LoadColumn(1)] public string Categoria { get; set; }
        [LoadColumn(2)] public float Preco { get; set; }
    }

    public class PrevisaoNomeProduto
    {
        [ColumnName("PrevisaoProduto")]
        public string PrevisaoProduto { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoProdutoController : ControllerBase
    {
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloProduto.zip");
        private readonly string caminhoTreinamento = Path.Combine(Environment.CurrentDirectory, "Data", "dados_treinamento.csv");
        private readonly MLContext mlContext;

        public PrevisaoProdutoController()
        {
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento");
                TreinarModelo();
            }
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
                Console.WriteLine($"Diretório criado: {pastaModelo}");
            }

            IDataView dadosTreinamento = mlContext.Data.LoadFromTextFile<DadosProduto>(
                path: caminhoTreinamento, hasHeader: true, separatorChar: ',');

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(DadosProduto.NomeProduto))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("CategoriaEncoded", nameof(DadosProduto.Categoria)))
                .Append(mlContext.Transforms.Concatenate("Features", "CategoriaEncoded", nameof(DadosProduto.Preco)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PrevisaoProduto", "PredictedLabel"));

            var modelo = pipeline.Fit(dadosTreinamento);

            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }

        [HttpPost("prever")]
        public ActionResult<PrevisaoNomeProduto> PreverProduto([FromBody] DadosProduto dados)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            try
            {
                using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    modelo = mlContext.Model.Load(stream, out var modeloSchema);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao carregar o modelo: {ex.Message}");
            }

            var enginePrevisao = mlContext.Model.CreatePredictionEngine<DadosProduto, PrevisaoNomeProduto>(modelo);

            PrevisaoNomeProduto previsao;
            try
            {
                previsao = enginePrevisao.Predict(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao prever o produto: {ex.Message}");
            }

            if (previsao == null || string.IsNullOrEmpty(previsao.PrevisaoProduto))
            {
                return Ok(new PrevisaoNomeProduto { PrevisaoProduto = "Produto não encontrado" });
            }

            return Ok(previsao);
        }
    }
}
