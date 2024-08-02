using Microsoft.VisualBasic;
using OperationOnData.Models;
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
using System.Xml;

namespace OperationOnData
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Functions functions;
        int postflag = 0;
        public MainWindow()
        {
            InitializeComponent();
            functions = new Functions();
            Data.Items.Clear();
        }
        public async void getmodels_Click(object sender, RoutedEventArgs e)
        {

            Data.Items.Clear();
            string[] Elementlist;
            try
            {


                Elementlist = await functions.GetAllModels();
                foreach (string element in Elementlist)
                {
                    Data.Items.Add(element);
                }
            }
            catch
            {
                MessageBox.Show("Serwer error");
            }
        }

        private async void GetModelId_Click(object sender, RoutedEventArgs e)
        {
            string model;
            int id = int.Parse(Interaction.InputBox("What id of elelement you wan to select", "write a id of element"));
            model = await functions.Getmodelid(id);
            Data.Items.Clear();
            try
            {
                Data.Items.Add(model);
            }
            catch
            {
                Data.Items.Add("Element do not exist");
            }
        }
        
        private async Task Button_ClickAsync(HelpingDbModel model)
        {
            await functions.postmodel(model);
            /*
            if (postflag == 0)
            {
                Data.Items.Clear();
                string[] Elementlist;
                try
                {

                    Elementlist = await functions.GetAllModels();
                    foreach (string element in Elementlist)
                    {
                        Data.Items.Add(element);
                    }
                }
                catch
                {
                    MessageBox.Show("Serwer error");
                }
                postflag = 1;
                MessageBox.Show("Chose element from list");
            }
            else
            {
                if (Data.SelectedIndex != -1)
                {
                    string txt = Data.Items[Data.SelectedIndex].ToString();
                    int endOfId= txt.IndexOf("q");
                    string idOfTxt=txt.Substring(4,endOfId-4);
                    string newtxt = txt.Substring(endOfId);
                    string txtOfQuestion = newtxt.Substring(newtxt.IndexOf(":")+2, newtxt.IndexOf(":",2) -6);
                    string txt2 = txt.Substring(txt.IndexOf("question:"));
                    string txtOfAnwer = txt2.Substring(txt2.IndexOf(":")+2, txt2.IndexOf(":",4)-7);
                    string txt3 = txt2.Substring(txt2.IndexOf("answer:")+2);

                    string txtOfValue = txt3.Substring(txt3.IndexOf(":",2) + 2);
                    MessageBox.Show(idOfTxt);
                    MessageBox.Show(txtOfQuestion);
                    MessageBox.Show(txtOfAnwer);
                    MessageBox.Show(txtOfValue);
                    
                    *//*MessageBox.Show(newtxt2);*/
            /*MessageBox.Show(Data.Items[Data.SelectedIndex].ToString());*//*
        }
        else
        {
            MessageBox.Show("Choose position from list");
        }

    */
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Button_ClickAsync();*/
            string que = Interaction.InputBox("Question", "add question");
            string ans = Interaction.InputBox("Answer", "add answer");
            int val = int.Parse(Interaction.InputBox("Value", "add value"));
            HelpingDbModel model = new HelpingDbModel
            {
                question = que,
                answer = ans,
                value = val
            };
            Task.Run(async () =>
            {
                await Button_ClickAsync(model);
            });
        }

    }
}

