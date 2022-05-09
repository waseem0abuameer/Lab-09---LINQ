using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LINQ
{
    public class Lab09
    {
        public class RootObject
        {
            public string type { get; set; }
            public Feature[] features { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public Geometry geometry { get; set; }
            public Properties properties { get; set; }
        }

        public class Geometry
        {
            public string type { get; set; }
            public float[] coordinates { get; set; }
        }

        public class Properties
        {
            public string zip { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string address { get; set; }
            public string borough { get; set; }
            public string neighborhood { get; set; }
            public string county { get; set; }
        }
      
        static void Main(string[] args)
        {
            ShowAllData();
        }
        public static void ShowAllData()
        {

            using (StreamReader reader = File.OpenText(@"../../../waseem.json"))

            {
                JObject dataFile = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
                var result = JsonConvert.DeserializeObject<RootObject>(dataFile.ToString());

                // Q1: Output all of the neighborhoods in this data list (Final Total: 147 neighborhoods).
                var neighborhoods = from n in result.features
                                    select n.properties.neighborhood;
                Console.WriteLine($"All neighborhoods by using LINQ query statements: { neighborhoods.Count()}.");


                // Q2: Filter out all the neighborhoods that do not have any names (Final Total: 143).
                var filteredNeighborhoods = from n in neighborhoods
                                            where (n != "")
                                            select n;
                Console.WriteLine($"Filtered neighborhoods by using LINQ query statements: { filteredNeighborhoods.Count()}.");


                // Q3: Remove the duplicates (Final Total: 39 neighborhoods).
                var uniqueNeighborhoods = filteredNeighborhoods.Distinct().ToList();
                Console.WriteLine($"Distinct neighborhoods after filtering: { uniqueNeighborhoods.Count()}.");


                // Q4: Rewrite the queries from above and consolidate all into one single query.
                var finalNeighborhoods = result.features.Where(s => s.properties.neighborhood != "").Select(s => s.properties.neighborhood).Distinct().ToList();
                Console.WriteLine($"Distinct neighborhoods by using LINQ method calls: { finalNeighborhoods.Count()}.");


                // Q5: Rewrite at least one of these questions only using the opposing method   
                var NeighborhoodsUsingMethod = result.features.Select(s => s.properties.neighborhood);
                Console.WriteLine($"All neighborhoods by using LINQ method calls: { NeighborhoodsUsingMethod.Count()}.");

            }
        }

    }
}