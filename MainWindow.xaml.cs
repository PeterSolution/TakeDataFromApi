using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApiClient.model;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Threading;
using System.Windows.Threading;
using System.Data;
using System.Windows.Media.Animation;

namespace WpfApiClient
{
    public partial class MainWindow : Window
    {
        HttpClient webclient;
        int flagcheck = 0;
        public MainWindow()
        {
            InitializeComponent();
            webclient = new HttpClient();
            webclient.BaseAddress=new Uri("https://localhost:7157");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(boxnum.Text, out _))
                {
                    int jsonid = int.Parse(boxnum.Text);
                    Task.Run(async () => {
                        /*jsonbox.Items.Add(GetInfo(jsonid).ToString());
*/
                        var models = await GetInfo();
                        Dispatcher.Invoke(() => {
                            foreach (var model in models)
                            {
                                if (model.id == jsonid)
                                {
                                    jsonbox.Items.Add("ID: " + model.id + " question: " + model.question+" answer: "+model.answer+" value "+ model.value);
                                    flagcheck = 1;
                                }
                            }
                            if (flagcheck == 0)
                            {
                                jsonbox.Items.Add("Element " + jsonid + " do not exist");
                                
                            }
                            else
                            {

                                flagcheck = 0;
                            }



                        });
                    });
                    }
            }
            catch
            {

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
