using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using PangeaAssignmentTests.Models;

namespace PangeaAssignmentTests
{
    [TestClass]
    public class IntegrationTests
    {
        protected static string LeftURL = "https://localhost:44307/api/CompareInputs/Left"; //TestApi URL for first end point should go here 
        protected static string RightURL = "https://localhost:44307/api/CompareInputs/Right"; //TestApi URL for second end point should go here 
        protected static string CompareURL = "https://localhost:44307/api/CompareInputs/Compare"; //TestApi URL for last end point should go here 
        protected static HttpClient Client = new HttpClient();

        [TestMethod]
        public async Task Inputs_Shoud_Be_Equal()
        {
            var request = "\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\"";
            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response1 = await Client.PostAsync($"{LeftURL}", stringContent);
            var response2 = await Client.PostAsync($"{RightURL}", stringContent);
            var response3 = await Client.GetAsync($"{CompareURL}");

            string content1 = await response1.Content.ReadAsStringAsync();
            string content2 = await response2.Content.ReadAsStringAsync();
            string content3 = await response3.Content.ReadAsStringAsync();

            Assert.AreEqual("they are equal", content3);
        }

        [TestMethod]
        public async Task Inputs_Should_Be_Different_Size()
        {
            var request1 = "\"eyJpbnB1dCI6InRlc3RWYWx1ZSJ9\"";
            var request2 = "\"eyJpbnB1dCI6InRlc3RWYWx1ZTEifQ==\"";

            var stringContent1 = new StringContent(request1, Encoding.UTF8, "application/json");
            var stringContent2 = new StringContent(request2, Encoding.UTF8, "application/json");

            var response1 = await Client.PostAsync($"{LeftURL}", stringContent1);
            var response2 = await Client.PostAsync($"{RightURL}", stringContent2);
            var response3 = await Client.GetAsync($"{CompareURL}");

            string content1 = await response1.Content.ReadAsStringAsync();
            string content2 = await response2.Content.ReadAsStringAsync();
            string content3 = await response3.Content.ReadAsStringAsync();

            Assert.AreEqual("they are different in size", content3);
        }

        [TestMethod]
        public async Task Input_Same_Size_But_Different()
        {
            var request1 = "\"ewoiaW5wdXQiOiAidGVzdHZhbHVlMSIKfQ==\"";
            var request2 = "\"ewoiaW5wdXQiOiAidGVzdHZhbHVlMyIKfQ==\"";

            var stringContent1 = new StringContent(request1, Encoding.UTF8, "application/json");
            var stringContent2 = new StringContent(request2, Encoding.UTF8, "application/json");

            var response1 = await Client.PostAsync($"{LeftURL}", stringContent1);
            var response2 = await Client.PostAsync($"{RightURL}", stringContent2);
            var response3 = await Client.GetAsync($"{CompareURL}");

            string content1 = await response1.Content.ReadAsStringAsync();
            string content2 = await response2.Content.ReadAsStringAsync();
            string content3 = await response3.Content.ReadAsStringAsync();

            Assert.AreEqual("offset: testvalue1, size :1", content3);
        }

        [TestMethod]
        public async Task Pass_Empty_Json()
        {
            var request = "\"ewoiaW5wdXQiOiAiIgp9\"";

            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response1 = await Client.PostAsync($"{LeftURL}", stringContent);
            var response2 = await Client.PostAsync($"{RightURL}", stringContent);

            string contentString1 = await response1.Content.ReadAsStringAsync();
            string contentString2 = await response2.Content.ReadAsStringAsync();

            var contentModel1 = JsonConvert.DeserializeObject<ResponseContentModel>(contentString1);
            var contentModel2 = JsonConvert.DeserializeObject<ResponseContentModel>(contentString2);

            Assert.AreEqual("Value cannot be null. (Parameter 'Input Can not be null')", contentModel1.Message);
            Assert.AreEqual("Value cannot be null. (Parameter 'Input Can not be null')", contentModel2.Message);
        }

        [TestMethod]
        public async Task Pass_Incorect_Base64()
        {
            var request = "\"string\"";

            var stringContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response1 = await Client.PostAsync($"{LeftURL}", stringContent);
            var response2 = await Client.PostAsync($"{RightURL}", stringContent);

            string contentString1 = await response1.Content.ReadAsStringAsync();
            string contentString2 = await response2.Content.ReadAsStringAsync();

            var contentModel1 = JsonConvert.DeserializeObject<ResponseContentModel>(contentString1);
            var contentModel2 = JsonConvert.DeserializeObject<ResponseContentModel>(contentString2);

            Assert.AreEqual("Incorect base64 value", contentModel1.Message);
            Assert.AreEqual("Incorect base64 value", contentModel2.Message);
        }
    }
}