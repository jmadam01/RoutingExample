using System.Text;

namespace RoutingAssignment.Helpers
{
    public class CountriesHelper
    {
        public Dictionary<int, string> countries { get; set; }

        public CountriesHelper() {

            countries = new Dictionary<int, string>();
            countries.Add(1, "United States");
            countries.Add(2, "Canada");
            countries.Add(3, "United Kingdom");
            countries.Add(4, "India");
            countries.Add(5, "Japan");

        }

        public string WriteCountries() { 

            StringBuilder sb = new StringBuilder();

            foreach (var country in this.countries)
            {
                sb.AppendLine(country.Key + " " + country.Value);

            }

            return sb.ToString();
        
        }

        public string GetCountryById(int id)
        {

            if (id == 0)
            {
                return "Bad Request, Please Try Again!";
            }

            else if (id> countries.Count && id <= 100)
            {
                return "Not Found, Please Try Again!";
            }
            else if(id > 100)
            {

                return "The CountryID should be between 1 and 100!";
            }


            return id + " "+ this.countries[id];

        }

        public int GetTotalCountries()
        {

            return this.countries.Count;

        }

    }
}
