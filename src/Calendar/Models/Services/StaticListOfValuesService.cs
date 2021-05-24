using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Helpers;
using System.Collections;

/* All the static List of Values will be provied by this Services */
namespace Calendar.Models.Services
{
    public class LOV
    {
        public string Function { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Filter { get; set; }

        public LOV (string pFunction, string pName, string pValue)
        {
            Function = pFunction;
            Name = pName;
            Value = pValue;
        }
        public LOV(string pFunction, string pName, string pValue, string pFilter)
        {
            Function = pFunction;
            Name = pName;
            Value = pValue;
            Filter = pFilter;
        }
    }

    public static class StaticListOfValuesService
    {
        const string RISK_VERYLOW  = "1";
        const string RISK_LOW      = "2";
        const string RISK_MODERATE = "3";
        const string RISK_HIGH     = "4";
        const string RISK_CRITICAL = "5";

        public static List<LOV> ListSeverities()
        {
            return new List<LOV>()
            {
                new LOV("severity", "High", "1"),
                new LOV("severity", "Medium","2"),
                new LOV("severity", "Low","3")
            };
        }

        public static List<string> ListColors()
        {
            return new List<string>() { "Blue", "Green", "Red", "Yellow" };
        }

        public static List<LOV> ListEnvironments()
        {
            return new List<LOV>()
            {
                new LOV("Environment", "PROD", "PROD"),
            };
        }

        public static List<LOV> ListLikelihoods()
        {
            return new List<LOV>()
            {
                new LOV("Likelihood", "Rare", "1"),
                new LOV("Likelihood", "Unlikely","2"),
                new LOV("Likelihood", "Possible","3"),
                new LOV("Likelihood", "Likely","4"),
                new LOV("Likelihood", "Almost certain","5")
            };
        }

        public static List<LOV> ListImpacts()
        {
            return new List<LOV>()
            {
                new LOV("Impact", "Insignificant", "1"),
                new LOV("Impact", "Minor","2"),
                new LOV("Impact", "Moderate","3"),
                new LOV("Impact", "Major","4"),
                new LOV("Impact", "Catastrophic","5")
            };
        }

        public static List<LOV> ListRiskLevels()
        {
            return new List<LOV>()
            {
                new LOV("RiskLevel", "Very low", "1"),
                new LOV("RiskLevel", "Low", "2"),
                new LOV("RiskLevel", "Moderate", "3"),
                new LOV("RiskLevel", "High", "4"),
                new LOV("RiskLevel", "Critical", "5")
            };
        }

        public static string[,] RiskLevelMatrix = 
                new string[5,5] {{RISK_VERYLOW, RISK_VERYLOW, RISK_LOW, RISK_LOW, RISK_MODERATE,},
                                 {RISK_VERYLOW, RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_MODERATE,},                    
                                 {RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_MODERATE, RISK_HIGH,},
                                 {RISK_LOW, RISK_MODERATE, RISK_MODERATE, RISK_HIGH, RISK_CRITICAL,},
                                 {RISK_MODERATE, RISK_MODERATE, RISK_HIGH, RISK_CRITICAL, RISK_CRITICAL,}
                                 };

        public static List<LOV> ListEventStatus()
        {
            return new List<LOV>()
            {
                new LOV("EventStatus", "Cancelled", Constants.STATUS_CANCELLED),
                new LOV("EventStatus", "Completed", Constants.STATUS_COMPLETED),
                new LOV("EventStatus", "Incomplete", Constants.STATUS_INCOMPLETE),
                new LOV("EventStatus", "RFC in Progress", Constants.STATUS_RFC),
                new LOV("EventStatus", "Schedule Confirmed", Constants.STATUS_SCHDCONFIRMED),
                new LOV("EventStatus", "Tentative", Constants.STATUS_TENTATIVE)                
            };
        }
        public static List<LOV> ListSearchDate()
        {
            return new List<LOV>()
            {
                new LOV("SearchDate", "", "ND"),
                new LOV("SearchDate", "Creation Date", "CD"),
                new LOV("SearchDate", "Event Date", "SD"),
                new LOV("SearchDate", "Update Date", "UD")
            };
        }
        public static List<LOV> ListSearchRange()
        {
            return new List<LOV>()
            {
                new LOV("SearchRange", "", "C0", "CD"),
                new LOV("SearchRange", "Show Today", "C1", "CD"),
                new LOV("SearchRange", "Show last 7 days", "C2", "CD"),
                new LOV("SearchRange", "Show last 30 days", "C3", "CD"),
                new LOV("SearchRange", "Show last 90 days", "C4", "CD"),
                new LOV("SearchRange", "", "S0", "SD"),
                new LOV("SearchRange", "Show Today", "S1", "SD"),
                new LOV("SearchRange", "Show coming 7 days", "S2", "SD"),
                new LOV("SearchRange", "Show coming 30 days", "S3", "SD"),
                new LOV("SearchRange", "Show coming 90 days", "S4", "SD"),
                new LOV("SearchRange", "", "U0", "UD"),
                new LOV("SearchRange", "Show Today", "U1", "UD"),
                new LOV("SearchRange", "Show last 7 days", "U2", "UD"),
                new LOV("SearchRange", "Show last 30 days", "U3", "UD"),
                new LOV("SearchRange", "Show last 90 days", "U4", "UD")
            };
        }
    }


}
