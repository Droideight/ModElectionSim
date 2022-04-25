using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModElectionSim
{
    internal class SetupManager
    {
        public static void ChangeVariable(string type)
        {
            switch (type)
            {
                case "Name1":
                    Candidate1Name = enteredvaluecs;
                    break;
                case "Name2":
                    Candidate2Name = enteredvaluecs;
                    break;
                case "PVI":
                    PVI = enteredvaluecs;
                    break;
                case "Enthusiasm1":
                    Enthusiasm1 = enteredvaluecs;
                    break;
                case "Enthusiasm2":
                    Enthusiasm2 = enteredvaluecs;
                    break;
                case "Random":
                    Random = enteredvaluecs;
                    break;
                case "Runtime":
                    runtime = enteredvaluecs;
                    break;
                default:
                    break;
            }
            return;
        }
        private static string EnteredValueCs;
        private static string Can1Name = "Trump";
        private static string Can2Name = "Biden";
        public static string enteredvaluecs { get { return EnteredValueCs; } set { EnteredValueCs = value; } }
        public static string Candidate1Name {
            get { return Can1Name; } set { Can1Name = EnteredValueCs; } 
        }
        public static string Candidate2Name
        {
            get { return Can2Name; } set { Can2Name = EnteredValueCs; } 
        }
        private static string pvi = "0";
        private static string random = "0";
        private static string enthusiasm1 = "1";
        private static string enthusiasm2 = "1";
        public static string PVI { get { return pvi; } set { pvi = EnteredValueCs; } }
        public static string Random { get { return random; } set { random = EnteredValueCs; } }
        public static string Enthusiasm1 { get { return enthusiasm1; } set { enthusiasm1 = EnteredValueCs; } }
        public static string Enthusiasm2 { get { return enthusiasm2; } set { enthusiasm2 = EnteredValueCs;} }
        private static string runtime = "0";
        public static string RUNTIME { get { return runtime; } set { runtime = EnteredValueCs; } }
    }
}
