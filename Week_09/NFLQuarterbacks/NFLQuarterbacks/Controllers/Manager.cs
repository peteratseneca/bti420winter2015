using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using NFLQuarterbacks.Models;
using CsvHelper;
using AutoMapper;
using System.IO;
using Excel;

namespace NFLQuarterbacks.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        public Manager()
        {
            // Initialize AutoMapper mappings

            Mapper.CreateMap<Models.Quarterback, Controllers.QuarterbackBase>();
            Mapper.CreateMap<Controllers.QuarterbackAdd, Models.Quarterback>();
            Mapper.CreateMap<Controllers.QuarterbackAdd, Controllers.QuarterbackBase>();
        }

        public IEnumerable<QuarterbackBase> ReadFromCSV()
        {
            // File system path to the data file (in this project's App_Data folder)
            string path = HttpContext.Current.Server.MapPath("~/App_Data/NFLStats2014.csv");

            // Create a stream reader object, to read from the file system
            StreamReader sr = File.OpenText(path);

            // Create the CsvHelper object
            var csv = new CsvReader(sr);

            // Configure the mapping classes (if it exists)
            csv.Configuration.RegisterClassMap<QuarterbackMap>();

            // You can read the contents of the source file in one statement
            // Or, you can read each line of the source file in a loop

            // Choose one of the options below, and comment out the other

            /*
            // ########################################
            // "One statement" option

            // Configure a collection to hold the results
            List<QuarterbackAdd> results = null;

            // Read the data file in one operation
            results = csv.GetRecords<QuarterbackAdd>().ToList();

            // Clean up
            sr.Close();
            sr = null;

            // Return the results
            return Mapper.Map<IEnumerable<QuarterbackBase>>(results);

            // ########################################
            */

            // ########################################
            // "In a loop" option

            // Configure a collection to hold the results
            List<QuarterbackAdd> results = new List<QuarterbackAdd>();

            // Go through the data file
            while (csv.Read())
            {
                // Read one line in the source file
                QuarterbackAdd qb = csv.GetRecord<QuarterbackAdd>();

                results.Add(qb);
            }

            // Clean up
            sr.Close();
            sr = null;

            // Return the results
            return Mapper.Map<IEnumerable<QuarterbackBase>>(results);

            // ########################################
        }

        public IEnumerable<QuarterbackBase> LoadFromCSV()
        {
            // Test if there's data first, and if yes, return the existing data
            // In other words, do NOT load the data again
            if (ds.Quarterbacks.Count() > 0)
            {
                var fetchedObjects = ds.Quarterbacks.OrderBy(q => q.Rank);
                return Mapper.Map<IEnumerable<QuarterbackBase>>(fetchedObjects);
            }

            // File system path to the data file (in this project's App_Data folder)
            string path = HttpContext.Current.Server.MapPath("~/App_Data/NFLStats2014.csv");

            // Create a stream reader object, to read from the file system
            StreamReader sr = File.OpenText(path);

            // Create the CsvHelper object
            var csv = new CsvReader(sr);

            // Configure the mapping classes (if it exists)
            csv.Configuration.RegisterClassMap<QuarterbackMap>();

            // Go through the data file
            while (csv.Read())
            {
                // Read one line in the source file into a new object
                QuarterbackAdd qb = csv.GetRecord<QuarterbackAdd>();

                // Add the new object to the data store
                ds.Quarterbacks.Add(Mapper.Map<Quarterback>(qb));
            }

            ds.SaveChanges();

            // Clean up
            sr.Close();
            sr = null;

            // Return the results
            var results = ds.Quarterbacks.OrderBy(q => q.Rank);
            return Mapper.Map<IEnumerable<QuarterbackBase>>(results);
        }

        public IEnumerable<QuarterbackBase> ReadFromXLSX()
        {
            // This uses a nice little library from Dietmar Schoder
            // http://www.codeproject.com/Tips/801032/Csharp-How-To-Read-xlsx-Excel-File-With-Lines-of 

            // File system path to the data file (in this project's App_Data folder)
            string path = HttpContext.Current.Server.MapPath("~/App_Data/NFLStats2014.xlsx");

            // Create a collection to hold the results
            var results = new List<QuarterbackBase>();

            var ws = Excel.Workbook.Worksheets(path).First();
            for (int i = 1; i < ws.Rows.Count(); i++)
            {
                // Create a new object
                var qb = new QuarterbackBase();

                // Manually set the unique identifier
                qb.Id = i;

                // Create a column variable, which simplifies the coding syntax
                var c = ws.Rows[i].Cells;

                // Configure the new object
                qb.Rank = (int)c[0].Amount;
                qb.Player = c[1].Text;
                qb.Team = c[2].Text;
                qb.Completions = (int)c[3].Amount;
                qb.Attempts = (int)c[4].Amount;
                qb.Percentage = c[5].Amount;
                qb.AttemptsPerGame = c[6].Amount;
                qb.Yards = (int)c[7].Amount;
                qb.YardsPerCompletion = c[8].Amount;
                qb.YardsPerGame = c[9].Amount;
                qb.Touchdowns = (int)c[10].Amount;
                qb.Interceptions = (int)c[11].Amount;
                qb.Longest = c[12].Text;
                qb.Sacks = (int)c[13].Amount;
                qb.Rating = c[14].Amount;

                results.Add(qb);
            }

            return results;
        }

        public void RemoveStoredData()
        {
            while (ds.Quarterbacks.Count() > 0)
            {
                // Get an object
                var qb = ds.Quarterbacks.First();
                // Remove it, and save changes
                ds.Quarterbacks.Remove(qb);
                ds.SaveChanges();
            }
        }
    }

}
