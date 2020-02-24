using MyCleanApp.Application.Common.Interfaces;
using MyCleanApp.Application.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanApp.Infrastructure.Services
{
    internal class LicensingService : ILicensingService
    {
        private string licenseServerURL = "";
        public LicensingService(string _licenseServerURL)
        {
            licenseServerURL = _licenseServerURL;
        }

        public Task<LicenseResult> GetLicense(string enproductID)
        {
            //throw new NotImplementedException();
            try
            {
                //code to call license server and get the details of product license
                var result = new LicenseResult()
                {
                    Status = Domain.Enums.LicenseStatus.Active,
                    ProductID = "",
                    MyKey = "mysamplekeyvalue",
                    ValidTill = DateTime.Now.AddDays(1),
                    Error = ""
                };

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var result = new LicenseResult() 
                {
                    Status = Domain.Enums.LicenseStatus.Unknown,
                    ProductID = "",
                    MyKey = "",
                    ValidTill = DateTime.Now,
                    Error = ex.Message
                };

                return Task.FromResult(result);
            }
        }



        private static async Task<LicenseResult> CallLicenseServerAsync(string licenseServerURL, string productID)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new LicenseResult() { ProductID = "ashdbas-akjsdhas-akjsdhaksd-asodjas-oashkas", Status = Domain.Enums.LicenseStatus.Active }), Encoding.UTF8, "application/json"); ;

                //client.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var response = await new HttpClient().PostAsync(licenseServerURL, content);

                var responseString = await response.Content.ReadAsStringAsync();
                ServiceResponse serviceresponse = JsonConvert.DeserializeObject<ServiceResponse>(responseString);
                return serviceresponse.data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    internal class ServiceResponse
    {
        internal LicenseResult data { get; set; }
    }

}
