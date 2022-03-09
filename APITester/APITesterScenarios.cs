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
        //private readonly IUserService _asda;
        private HttpClient _integrationService;
        public TestContext TestContext { get; set; }
        public APITesterScenarios() {
            _integrationService = ServiceScenariosBase.InstanceBase.CreateClient();
            service = new GeneralBusiness();
            //_asda = asda;
        }

        //[Test]
        //[Order(1)]
        //public async Task AddPerson_Ok() {
        //    var content = new StringContent(service.GetJson(@$"AddPerson_Ok.json"), Encoding.UTF8, "application/json");
        //    var response = await _integrationService.PostAsync(ServiceScenariosBase.Post.AddPerson(), content);
        //    var body = await response.Content.ReadAsStringAsync();
        //    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(body);
        //    ServiceScenariosBase.InstanceBase.UserId=myDeserializedClass.data.id;
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //}

        [Test]
        [Order(2)]
        public async Task AddPerson_DoesntHaveBody() {
            var content = new StringContent(service.GetJson(@$"AddPerson_DoesntHave.json"), Encoding.UTF8, "application/json");
            var response = await _integrationService.PostAsync(ServiceScenariosBase.Post.AddPerson(), content);
            var body = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        [Order(3)]
        public async Task GetPerson_Ok() {
            var response = await _integrationService.GetAsync(ServiceScenariosBase.Get.GetPerson());
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //[Test]
        //[Order(4)]
        //public async Task AddPersonInfo_Ok() {
        //    PersonelInfoRequestModel body = JsonConvert.DeserializeObject<PersonelInfoRequestModel>(service.GetJson(@$"AddPersonInfo_Ok.json"));
        //    body.userid = ServiceScenariosBase.InstanceBase.UserId;
        //    string contentbody = JsonConvert.SerializeObject(body);
        //    var content = new StringContent(contentbody, Encoding.UTF8, "application/json");
        //    var response = await _integrationService.PostAsync(ServiceScenariosBase.Post.AddPersonInfo(), content);
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        //}

        [Test]
        [Order(5)]
        public async Task GetUserById_Ok() {
            var response = await _integrationService.GetAsync(ServiceScenariosBase.Get.GetUserById(ServiceScenariosBase.InstanceBase.UserId));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        [Order(6)]
        public async Task DeletePersonelInfo_Ok() {
            var response = await _integrationService.DeleteAsync(ServiceScenariosBase.Delete.DeletePersonelInfo(ServiceScenariosBase.InstanceBase.UserId));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [Test]
        [Order(7)]
        public async Task DeletePersonel_Ok() {
            var response = await _integrationService.DeleteAsync(ServiceScenariosBase.Delete.DeletePersonel(ServiceScenariosBase.InstanceBase.UserId));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }

}
