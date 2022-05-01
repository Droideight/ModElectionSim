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
        public static double inpct = 0.00;
        private static double inpcta = 0.00;
        private static double inpctb = 0.00;
        private static double thisbatcha = 0;
        private static double thisbatchb = 0;
        private static double thisbatchavote = 0;
        private static double thisbatchbvote = 0;
        public static double inavote = 0;
        public static double inbvote = 0;
        public static double inapct = 0.00;
        public static double inbpct = 0.00;
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
            CurrentlySetting.Content = "Setting PVI (-100~100):";
        }


        private void Edit_Enthusiasm1_Click(object sender, RoutedEventArgs e)
        {
            setting = "Enthusiasm1";
            CurrentlySetting.Content = "Setting Enthusiasm C1 (0~1.66):";
        }

        private void Edit_Enthusiasm2_Click(object sender, RoutedEventArgs e)
        {
            setting = "Enthusiasm2";
            CurrentlySetting.Content = "Setting Enthusiasm C2 (0~1.66):";
        }


        private void Edit_Random_Click(object sender, RoutedEventArgs e)
        {
            setting = "Random";
            CurrentlySetting.Content = "Setting Margin of Err. (0~100):";
        }

        private void ConfirmRuntime_Click(object sender, RoutedEventArgs e)
        {

        EnteredValueCs = (string)Runtime_Enter.Text;
            SetupManager.enteredvaluecs = EnteredValueCs.ToString();
            SetupManager.ChangeVariable("Runtime");
            TitleChange();
            StartSimulation.IsEnabled = true;
        }
        private void Population_Set_Click(object sender, RoutedEventArgs e)
        {
            setting = "Population";
            CurrentlySetting.Content = "Setting Population";
        }
        private void TitleChange()
        {
            SimulationCondition.Content = $"Population: {SetupManager.Population}; PVI: {SetupManager.PVI};\nEnthusiasm: {SetupManager.Enthusiasm1} to {SetupManager.Enthusiasm2};\nMOE: {SetupManager.Random}%; Runtime: {SetupManager.RUNTIME} time(s)";
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
                case "Population":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Population");
                    break;
                case "Speed":
                    SetupManager.enteredvaluecs = EnteredValueCs.ToString();
                    SetupManager.ChangeVariable("Speed");
                    break;
                default:
                    break;
            }
            TitleChange();
            StartSimulation.IsEnabled = true;
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            if (SetupManager.RUNTIME == "0")
            {
                inpct = 0.00;
                inpcta = 0.00;
                inpctb = 0.00;
                thisbatcha = 0;
                thisbatchb = 0;
                thisbatchavote = 0;
                thisbatchbvote = 0;
                inavote = 0;
                inbvote = 0;
                inapct = 0.00;
                inbpct = 0.00;
                double actualpctc1;
                Random rnda = new Random();
                double v1 = 1.0 - rnda.NextDouble();
                double v2 = 1.0 - rnda.NextDouble();
                double randStdNormala = Math.Sqrt(-2.0 * Math.Log(v1)) * Math.Sin(2.0 * Math.PI * v2); //random normal(0,1)
                double randNormala = ((0.5 + Double.Parse(SetupManager.PVI) / 200) + (Double.Parse(SetupManager.Random) * 0.005 * randStdNormala));
                if (Double.Parse(SetupManager.Random) != 0.0)
                {
                    if ((Math.Round(randNormala, 2) > 0.0))
                    {
                        if ((Math.Round(randNormala, 2) < 1.0))
                        {
                            actualpctc1 = Math.Round(randNormala, 2);
                        }
                        else actualpctc1 = 1.0;
                    }
                    else actualpctc1 = 0.0;
                    double actualvotes1 = (0.006 * Double.Parse(SetupManager.Enthusiasm1)) * actualpctc1 * Double.Parse(SetupManager.Population);
                    double actualvotes2 = (0.006 * Double.Parse(SetupManager.Enthusiasm2)) * (1 - actualpctc1) * Double.Parse(SetupManager.Population);
                    Liveupdatemanager(actualvotes1, actualvotes2);
                }
                if (Double.Parse(SetupManager.Random) == 0.0)
                {
                    if ((Math.Round((Double.Parse(SetupManager.PVI) / 200 + 0.50), 4, MidpointRounding.ToEven)) > 0.0)
                    {
                        if ((Math.Round((Double.Parse(SetupManager.PVI) / 200 + 0.50), 4, MidpointRounding.ToEven)) < 1.0)
                        { actualpctc1 = (Math.Round((Double.Parse(SetupManager.PVI) / 200 + 0.50), 4, MidpointRounding.ToEven)); }
                        else actualpctc1 = 1.0;
                    }
                    else actualpctc1 = 0.0;
                    double actualvotes1 = (0.006 * Double.Parse(SetupManager.Enthusiasm1)) * actualpctc1 * Double.Parse(SetupManager.Population);
                    double actualvotes2 = (0.006 * Double.Parse(SetupManager.Enthusiasm2)) * (1 - actualpctc1) * Double.Parse(SetupManager.Population);
                    Liveupdatemanager(actualvotes1,actualvotes2);
                };
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
                        double randNormal = ((50.000 + Double.Parse(SetupManager.PVI) / 2) * (Math.Pow((Double.Parse(SetupManager.Enthusiasm1) / 2 + 0.5), 2) / ((Double.Parse(SetupManager.Enthusiasm1) / 2 + 0.5) * (Double.Parse(SetupManager.Enthusiasm2) / 2 + 0.5))) + (Double.Parse(SetupManager.Random) * 0.5 * randStdNormal));
                        if (Double.Parse(SetupManager.Random) != 0.0)
                        {
                            if ((Math.Round(randNormal, 2) > 0.0))
                            {
                                if ((Math.Round(randNormal, 2) < 100.0))
                                {
                                    resultarray[i] = (Math.Round(randNormal, 2));
                                }
                                else resultarray[i] = 100.0;
                            }
                            else resultarray[i] = 0.0;
                        }
                        if (Double.Parse(SetupManager.Random) == 0.0)
                        {
                            if ((Math.Round((Double.Parse(SetupManager.PVI) / 2.0 + 50.0), 2, MidpointRounding.ToEven)) > 0.0)
                            {
                                if ((Math.Round((Double.Parse(SetupManager.PVI) / 2.0 + 50.0), 2, MidpointRounding.ToEven)) < 100.0)
                                { resultarray[i] = (Math.Round((Double.Parse(SetupManager.PVI) / 2.0 + 50.0), 2, MidpointRounding.ToEven)); }
                                else resultarray[i] = 100.0;
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
                    win = Math.Round(newlength / original * 100, 4);

                    SimulationResult.Content = $"{Can1Name.Content}: {l}~{m}% (Most Likely: {hsd}~{lsd})\n{Can2Name.Content}: {100 - m}~{100 - l}% (Most Likely: {100 - lsd}~{100 - hsd})\n\nAverage: {Can1Name.Content} {avg}%, {Can2Name.Content} {100 - avg}%\n{Can1Name.Content} wins in {win}% of tests;";
                    Array.Clear(resultarray, 0, resultarray.Length);
                    StartSimulation.IsEnabled = true;
                }
            }
        }
        public void Liveupdating(double lower1, double upper1, double lower2, double upper2, double votea1, double votea2)
        {
            Random LU = new Random();
            thisbatcha = 0.00001 * (double)LU.Next(100000 * (int)lower1, 100000 * (int)upper1);
            thisbatchb = 0.00001 * (double)LU.Next(100000 * (int)lower2, 100000 * (int)upper2);
            inpcta += thisbatcha;
            inpctb += thisbatchb;
            thisbatchavote = votea1 * thisbatcha;
            thisbatchbvote = votea2 * thisbatchb;
            inavote += thisbatchavote;
            inbvote += thisbatchbvote;
            inapct = inavote / (inavote + inbvote);
            inbpct = inbvote / (inbvote + inavote);
            inpct = (inavote + inbvote) / (votea1 + votea2);
            double inavoteu = inavote;
            double inbvoteu = inbvote;
            double inapctu = inapct;
            double inbpctu = inbpct;
            double inpctu = inpct;

            Can1VoteCount.Content = String.Format("{0:n0}", Math.Round(inavoteu, 0)); 
            Can2VoteCount.Content = String.Format("{0:n0}", Math.Round(inbvoteu, 0));
            Can2VotePCT.Content = $"{100* Math.Round(inbpctu, 4)}%";
            Can1VotePCT.Content = $"{100* Math.Round(inapctu, 4)}%";
            TurnoutCount.Content = $"{Math.Round(inpctu, 2)}%";
        }
        
        public async void Liveupdatemanager(double vote1, double vote2)
        {
            do
            {
                await Task.Delay(int.Parse(SetupManager.Speed));
                if (inpcta >= 98.00 && inpctb < 98.00) { Liveupdating(0.00, 0.10, 0.20, 1.00, vote1, vote2); }
                else if (inpctb >= 98.00 && inpcta < 98.00) { Liveupdating(0.20, 1.00, 0.00, 0.10, vote1, vote2); }
                else if (inpcta >= 98.00 && inpctb >= 98.00) { Liveupdating((100 - inpcta), (100 - inpcta), (100 - inpctb), (100 - inpctb), vote1, vote2); }
                else if ((inpcta - inpctb) > 8.00) { Liveupdating(0.10, 0.50, 0.20, 1.00, vote1, vote2); }
                else if ((inpctb - inpcta) > 8.00) { Liveupdating(0.20, 1.00, 0.10, 0.50, vote1, vote2); }
                else { Liveupdating(0.20, 1.00, 0.20, 1.00, vote1, vote2); }
            } while (inpcta < 100.0 || inpctb < 100.0);
        }

        private void Speed_Set_Click(object sender, RoutedEventArgs e)
        {
            setting = "Speed";
            CurrentlySetting.Content = "Setting Batch Speed (Recommend: 2s):";
        }
    }
}
