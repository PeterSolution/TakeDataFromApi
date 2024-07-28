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

namespace OperationOnData
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Functions functions;
        public MainWindow()
        {
            InitializeComponent();
            functions = new Functions();
            Data.Items.Clear();
        }
        public async void getmodels_Click(object sender, RoutedEventArgs e)
        {
            string[] Elementlist;
            Elementlist= await functions.GetAllModels();
            foreach (string element in Elementlist)
            {
                Data.Items.Add(element);
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
    }
}
