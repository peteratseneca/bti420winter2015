using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//added...
using System.ComponentModel.DataAnnotations;
using CsvHelper;
using CsvHelper.Configuration;

namespace NFLQuarterbacks.Controllers
{
    public class QuarterbackAdd
    {
        public int Rank { get; set; }
        public string Player { get; set; }
        public string Team { get; set; }
        public int Completions { get; set; }
        public int Attempts { get; set; }

        // Standard numeric format strings, reference...
        // https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.110).aspx

        [DisplayFormat(DataFormatString = "{0:N1}")]
        public double Percentage { get; set; }

        [Display(Name = "Attempts Per Game")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public double AttemptsPerGame { get; set; }

        public int Yards { get; set; }

        [Display(Name = "Yards Per Completion")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public double YardsPerCompletion { get; set; }

        [Display(Name = "Yards Per Game")]
        [DisplayFormat(DataFormatString = "{0:N1}")]
        public double YardsPerGame { get; set; }

        public int Touchdowns { get; set; }
        public int Interceptions { get; set; }
        public string Longest { get; set; }
        public int Sacks { get; set; }

        [DisplayFormat(DataFormatString = "{0:N1}")]
        public double Rating { get; set; }
    }

    public class QuarterbackMap : CsvClassMap<QuarterbackAdd>
    {
        // This class customizes the property-to-column mappings

        // It identifies the properties that have different
        // column (field) names in the source CSV file

        public QuarterbackMap()
        {
            // First, map all the properties that have matching names
            AutoMap();

            // Then, map the differences

            // When mapping AttemptsPerGame, look for the APG column
            Map(m => m.AttemptsPerGame).Name("APG");

            // When mapping YardsPerCompletion, look for the YPC column
            Map(m => m.YardsPerCompletion).Name("YPC");

            // When mapping YardsPerGame, look for the YPG column
            Map(m => m.YardsPerGame).Name("YPG");
        }
    }

    public class QuarterbackBase : QuarterbackAdd
    {
        public int Id { get; set; }
    }
}
