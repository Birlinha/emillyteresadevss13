using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace az204
{
    public class FnValidaCpf
    {
        private readonly ILogger<FnValidaCpf> _logger;

        public FnValidaCpf(ILogger<FnValidaCpf> logger)
        {
            _logger = logger;
        }

        [Function("FnValidaCpf")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            string finalMessage = "Iniciando Validação de CPF";

            _logger.LogInformation(finalMessage);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if (data == null)
            {
                return new BadRequestObjectResult("Por favor, informe o CPF.");
            }

            string cpf = data?.cpf ?? string.Empty;

            if (validaCPF(cpf) == false)
            {
                return new BadRequestObjectResult("CPF Inválido.");
            }

            finalMessage = "CPF Válido";

            return new OkObjectResult(finalMessage);
        }

        public static bool validaCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            int[] m1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCPF = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCPF[i].ToString()) * m1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCPF += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCPF[i].ToString()) * m2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
