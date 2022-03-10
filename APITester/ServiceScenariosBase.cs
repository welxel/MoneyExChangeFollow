using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using MoneyExChangeFollowAPI;
using XNUnitTest.Base;

namespace XServiceUnitTest {
    public class ServiceScenariosBase
    {
        public static ServiceScenariosBase InstanceBase = new ServiceScenariosBase();
        public MyServer Instance = null;
        public HttpClient client;
        public int UserId { get; set; }

        public HttpClient CreateClient()
        {
            CreateServer();
            if (client!=null)
            {
                return client;
            }
             client = CreateServer().CreateClient();
             return client;
        }


        public MyServer CreateServer() {

            if (Instance != null) {
                return Instance;
            }

            var path = Assembly.GetAssembly(typeof(Startup))
                .Location;
            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .UseStartup<Startup>();
            Instance = new MyServer(hostBuilder);

            return Instance;
        }


        public static class Get {
            public static string GetCurrency() {
                return $"Current/getcurrency";
            }
        }

        public static class Post {
            public static string GetCurrencyDetail() {
                return $"Current/GetCurrencyCodeDetail";
            }
        }
    }
}

