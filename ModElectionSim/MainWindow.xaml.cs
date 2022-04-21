using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ModElectionSim
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        static string setting = "nothing";
        string EnteredValueCs = "nothing";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EditCAN1Name_Click(object sender, RoutedEventArgs e)
        {
            setting = "Name1";
        }

        private void EditCAN2Name_Click(object sender, RoutedEventArgs e)
        {
            setting = "Name2";
        }
    
        private void EditPVI_Click(object sender, RoutedEventArgs e)
        {
            setting = "PVI";
        }
        

        private void Edit_Enthusiasm_Click(object sender, RoutedEventArgs e)
        {
            setting = "Enthusiasm";
        }


        private void Edit_Random_Click(object sender, RoutedEventArgs e)
        {
            setting = "Random";
        }

        private void ConfirmRuntime_Click(object sender, RoutedEventArgs e)
        {
            EnteredValueCs = (string)Runtime_Enter.Text;
            SetupManager.enteredvaluecs = EnteredValueCs.ToString();
            SetupManager.ChangeVariable("Runtime");
        }

        private void WildCardSet_Click(object sender, RoutedEventArgs e)
        {
            EnteredValueCs = (string)WildCardEnterBox.Text;
            switch (setting) {
                case "Name1":
                    Can1Name.Content = EnteredValueCs;
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Name1");
                    break;
                case "Name2":
                    Can2Name.Content = EnteredValueCs;
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Name2");
                    break;
                case "PVI":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("PVI");
                    break;
                case "Enthusiasm":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Enthusiasm");
                    break;
                case "Random":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Random");
                    break;
                default:
                    break;
            }
        }
      
    }
}
