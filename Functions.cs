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
using System.Windows;

namespace OperationOnData
{
    public class Functions
    {

        HttpClient webclient = new HttpClient { BaseAddress = new Uri("https://localhost:7157") };
        public async Task<string[]> GetAllModels()
        {
            var models = await GetInfo();
            return models.Select(model =>
                $"ID: {model.id} question: {model.question} answer: {model.answer} value: {model.value}"
               
            ).ToArray();

        }
        public async Task<string> Getmodelid(int id)
        {
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

        /*public async Task postmodel(HelpingDbModel model)
        {
            string url = "https://localhost:7157/api/ApiRequest";
            try
            {
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await webclient.PostAsync(url, content);
                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show("Data posted successfully. Response: " + responseBody);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Request error: " + e.Message);
            }



        }*/
        public async Task postmodel(HelpingDbModel model)
        {
            string url = "https://localhost:7157/api/ApiRequest";
            try
            {
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json"); // Zmień typ zawartości
                HttpResponseMessage response = await webclient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Data posted successfully. Response: " + responseBody);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Server error: {response.StatusCode}. Details: {errorContent}");
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Request error: " + e.Message);
            }
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
