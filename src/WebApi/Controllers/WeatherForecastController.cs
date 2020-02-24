using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCleanApp.Application.Common.Models;
using MyCleanApp.Application.License.Queries.GetLicenseInfo;
using MyCleanApp.WebApi.Controllers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            string strenc = EncryptExt("Hello World");
            string strdec = DecryptExt(EncryptExt("Hello World"));


            Console.WriteLine(EncryptExt("Hello World"));
            Console.WriteLine(DecryptExt(EncryptExt("Hello World")));

            //string guidAndKey = Guid.NewGuid().ToString() + new Random().Next(1000, 10000000); 

            //string str = DecryptData("kzu8k0P2hO409y5z/Rmd7A==");

            //Console.WriteLine(str);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{productid}")]
        public async Task<ActionResult<LicenseResult>> Get(string productid)
        {
            return await Mediator.Send(new GetLicenseInfoQuery() { enProductID = productid });
        }

        private string DecryptData(string AuthorizationCode) {



            string keyString = "d5d91985-e3c4-4b1a-8256-2cb6a9d838ed6591724"; //replace with your key
            string ivString = "0000000000000000"; //replace with your iv

            byte[] key = Encoding.ASCII.GetBytes(keyString);
            byte[] iv = Encoding.ASCII.GetBytes(ivString);

            using (var rijndaelManaged =
                    new RijndaelManaged { Key = key, IV = iv, Mode = CipherMode.CBC })
            {
                
                rijndaelManaged.BlockSize = 128;
                rijndaelManaged.KeySize = 256;
                using (var memoryStream =
                       new MemoryStream(Convert.FromBase64String(AuthorizationCode)))
                using (var cryptoStream =
                       new CryptoStream(memoryStream,
                           rijndaelManaged.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                {
                    return new StreamReader(cryptoStream).ReadToEnd();
                }
            }
        }



        ////Another try for encryption and decryption
        ///
        public static string EncryptExt(string raw)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransformExt(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                string encrypted = Convert.ToBase64String(output);
                return encrypted;
            }
        }
        public static string DecryptExt(string encrypted)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransformExt(csp, false);
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }
        private static ICryptoTransform GetCryptoTransformExt(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var passWord = Convert.ToString("d5d91985-e3c4-4b1a-8256-2cb6a9d838ed");
            var salt = Convert.ToString("ABj4PQgf3j5gblQ0iDp0/Gb07ukQWo0a");
            String iv = Convert.ToString("0000000000000000");
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), Encoding.UTF8.GetBytes(salt), 65536);
            byte[] key = spec.GetBytes(32);
            csp.IV = Encoding.UTF8.GetBytes(iv);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }



    }
}
