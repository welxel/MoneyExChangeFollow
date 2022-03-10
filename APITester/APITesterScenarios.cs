using Newtonsoft.Json;
using NUnit.Framework;

using System.Data.Common;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XNUnitTest.Manager;
using XServiceUnitTest;

namespace RehberimApiTest {
    public class APITesterScenarios {
        private GeneralBusiness service;
        private HttpClient _localService;
        private HttpClient _httpService;
        public TestContext TestContext { get; set; }
        public APITesterScenarios() {
            _localService = ServiceScenariosBase.InstanceBase.CreateClient();
            _httpService = new HttpClient();
            service = new GeneralBusiness();
        }
        [Test]
        [Order(1)]
        public async Task GetTcmbData() {
            try
            {
                var response = await _httpService.GetAsync("https://www.tcmb.gov.tr/kurlar/today.xml");
                string xmlBody = await response.Content.ReadAsStringAsync();
                if (HttpStatusCode.OK==response.StatusCode)
                {
                    TestContext.WriteLine("Tcmbden get data. Test is success");
                }
                else
                {
                    TestContext.WriteLine("Tcmbden can't return data. Test is unsuccessful.");
                }
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                
            }
            catch (System.Exception)
            {
                TestContext.WriteLine("Https requestte an error occurred. Test is unsuccessful.");
            }
            
        }

        [Test]
        [Order(2)]
        public async Task GetCurrency()
        {
            try
            {
                var response = await _localService.GetAsync(ServiceScenariosBase.Get.GetCurrency());
                string responseBody = await response.Content.ReadAsStringAsync();

                if (HttpStatusCode.OK == response.StatusCode)
                {
                    TestContext.WriteLine("Currency methotdan data gelmiştir test başarılı.");
                }
                else
                {
                    TestContext.WriteLine("Currency Methodda sorun oluşmuştur.");
                }
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (System.Exception e)
            {
                TestContext.WriteLine("Birşeyler yanlış gitti. Detay: " + e.Message);
            }
        }

        [Test]
        [Order(3)]
        public async Task GetCurrencyDetail()
        {
            try
            {
                var content = new StringContent(service.GetJson(@$"\\GetCurrencyDetailRequest.json"), Encoding.UTF8, "application/json");
                var response = await _localService.PostAsync(ServiceScenariosBase.Post.GetCurrencyDetail(), content);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (HttpStatusCode.OK == response.StatusCode)
                {
                    TestContext.WriteLine("Currency methotdan data gelmiştir test başarılı.");
                }
                else
                {
                    TestContext.WriteLine("Currency Methodda sorun oluşmuştur.");
                }
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            
            catch (System.Exception e)
            {
                TestContext.WriteLine("Birşeyler yanlış gitti. Detay: " + e.Message);
            }

        }

    }

}
