using OperationOnData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Windows.Threading;
using System.Net;

namespace OperationOnData
{
    public class Functions
    {

        HttpClient webclient = new HttpClient();

        public async Task<string[]> GetAllModels()
        {
            webclient.BaseAddress = new Uri("https://localhost:7157");
            var models = await GetInfo();
            return models.Select(model =>
                $"ID: {model.id} question: {model.question} answer: {model.answer} value {model.value}"
            ).ToArray();

        }
        public async Task<string> Getmodelid(int id)
        {
            webclient.BaseAddress = new Uri("https://localhost:7157");
            var models = await GetInfo();
            string answer = "Do not exist";
            foreach (var model in models)
            {
                if (model.id == id)
                {
                    answer = $"ID: {model.id} question: {model.question} answer: {model.answer} value {model.value}";
                    
                }
            }

            return answer;


        }
        public async Task<List<DbModel>> GetInfo()
        {
            var apianswer = await webclient.GetAsync($"/api/ApiRequest");
            apianswer.EnsureSuccessStatusCode();
            var content = await apianswer.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DbModel>>(content);


        }
    }
}
