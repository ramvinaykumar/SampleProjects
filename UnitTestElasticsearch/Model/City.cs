using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestElasticsearch.Model
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Population { get; set; }

        public City(int id, string name, string state, string country, string population)
        {
            ID = id;
            Name = name;
            State = state;
            Country = country;
            Population = Population;
        }
    }
}
