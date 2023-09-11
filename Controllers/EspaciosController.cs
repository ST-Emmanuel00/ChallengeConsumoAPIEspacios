using Espacios.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Espacios.Controllers

{
    public class EspaciosController : Controller
    {
        private readonly HttpClient httpClient;
        public EspaciosController()
        {

            httpClient = new HttpClient();

        }

        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://apptower-bk.onrender.com/api/espacios";
            var response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var espacios = JsonConvert.DeserializeObject<ApiResponse<MyEspacios>> (content);
                return View(espacios.Results);
            }

            return View(new List<MyEspacios>());
        }

        public class ApiResponse<T>
        {
            public Info Info { get; set; }
            public List<T> Results { get; set; }

        }

        public class Info
        {

            public int Count { get; set; }
            public int Pages { get; set; }
            public string Next { get; set; }
            public string Prev { get; set; }

        }
    }

    
}
