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
            CurrentlySetting.Content = "Setting Cand.1 Name:";
        }

        private void EditCAN2Name_Click(object sender, RoutedEventArgs e)
        {
            setting = "Name2";
            CurrentlySetting.Content = "Setting Cand.2 Name:";
        }

        private void EditPVI_Click(object sender, RoutedEventArgs e)
        {
            setting = "PVI";
            CurrentlySetting.Content = "Setting PVI:";
        }


        private void Edit_Enthusiasm1_Click(object sender, RoutedEventArgs e)
        {
            setting = "Enthusiasm1";
            CurrentlySetting.Content = "Setting Enthusiasm C1";
        }

        private void Edit_Enthusiasm2_Click(object sender, RoutedEventArgs e)
        {
            setting = "Enthusiasm2";
            CurrentlySetting.Content = "Setting Enthusiasm C2";
        }


        private void Edit_Random_Click(object sender, RoutedEventArgs e)
        {
            setting = "Random";
            CurrentlySetting.Content = "Setting Margin of Err.:";
        }

        private void ConfirmRuntime_Click(object sender, RoutedEventArgs e)
        {
            EnteredValueCs = (string)Runtime_Enter.Text;
            SetupManager.enteredvaluecs = EnteredValueCs.ToString();
            SetupManager.ChangeVariable("Runtime");
            TitleChange();
            StartSimulation.IsEnabled = true;
        }
        private void TitleChange()
        {
            SimulationCondition.Content = $"PVI: {SetupManager.PVI};\nEnthusiasm of C1: {SetupManager.Enthusiasm1};\nEnthusiasm of C2: {SetupManager.Enthusiasm2};\nRandomness: {SetupManager.Random}; Runtime: {SetupManager.RUNTIME} time(s)";
        }
        private void WildCardSet_Click(object sender, RoutedEventArgs e)
        {
            EnteredValueCs = (string)WildCardEnterBox.Text;
            switch (setting)
            {
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
                case "Enthusiasm1":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Enthusiasm1");
                    break;
                case "Enthusiasm2":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Enthusiasm2");
                    break;
                case "Random":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Random");
                    break;
                default:
                    break;
            }
            TitleChange();
            StartSimulation.IsEnabled = true;
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (SetupManager.RUNTIME == "0") { };
            if (SetupManager.RUNTIME != "0")
            {
                StartSimulation.IsEnabled = false;
                double win;
                Random rnd = new Random();
                double[] resultarray = new double[Int32.Parse(SetupManager.RUNTIME)];
                for (int i = 0; i < resultarray.Length; i++)
                {
                    double u1 = 1.0 - rnd.NextDouble();
                    double u2 = 1.0 - rnd.NextDouble();
                    double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                    double randNormal = ((50.000 + Double.Parse(SetupManager.PVI) / 2)* Math.Pow(Double.Parse(SetupManager.Enthusiasm1), 2) / Double.Parse(SetupManager.Enthusiasm1) * Double.Parse(SetupManager.Enthusiasm2)) + (Double.Parse(SetupManager.Random) * 0.5  * randStdNormal);
                    if (Double.Parse(SetupManager.Random) != 0.0)
                    {
                        if ((Math.Round(randNormal, 2) > 0.0)) { resultarray[i] = (Math.Round(randNormal, 2)); }
                        else resultarray[i] = 0.0;
                    }
                    if (Double.Parse(SetupManager.Random) == 0.0)
                    {
                        if ((Math.Round((Double.Parse(SetupManager.PVI) / 2.0 + 50.0), 2, MidpointRounding.ToEven)) > 0.0)
                        {
                            resultarray[i] = (Math.Round((Double.Parse(SetupManager.PVI) / 2.0 + 50.0), 2, MidpointRounding.ToEven));
                        }
                        else resultarray[i] = 0.0;
                    }
                }
                    double m = Math.Round(resultarray.Max(), 2);
                    double l = Math.Round(resultarray.Min(), 2);
                    double avg = Math.Round(resultarray.Average(), 2);
                    double sumOfSquaresOfDifferences = resultarray.Select(val => (val - avg) * (val - avg)).Sum();
                    double sd = Math.Sqrt((double)sumOfSquaresOfDifferences / resultarray.Length);
                    double lsd = Math.Round((double)avg + sd, 2);
                    double hsd = Math.Round((double)avg - sd, 2);
                    if (lsd > 100) lsd = 100;
                    if (hsd < 0) hsd = 0;
                    double original = resultarray.Length;
                    Array.Sort(resultarray);
                    resultarray = Array.FindAll(resultarray, j => j > 50).ToArray();
                    double newlength = resultarray.Length;
                    win = Math.Round(newlength / original * 100, 2);

                    SimulationResult.Content = $"{Can1Name.Content}: {l}~{m}% (Most Likely: {hsd}~{lsd})\n{Can2Name.Content}: {100 - m}~{100 - l}% (Most Likely: {100 - lsd}~{100 - hsd})\n\nAverage: {Can1Name.Content} {avg}%, {Can2Name.Content} {100 - avg}%\n{Can1Name.Content} wins in {win}% of tests;";
                    Array.Clear(resultarray, 0, resultarray.Length);
                    StartSimulation.IsEnabled = true;
                }
            }

            }
    } 
