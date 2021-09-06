using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomQuotes.Models
{
    public class QuotesList
    {
        private static List<QuotesViewModel> QuotesDb = new List<QuotesViewModel>() {
            new QuotesViewModel()
            {
                Quote = "Do not Despise the days of your little beginning",
                Author = "Annonynous"
            },

            new QuotesViewModel()
            {
                Quote = "He who fights and run away, must come back another day",
                Author = "Annonynous"
            },

            new QuotesViewModel()
            {
                Quote = "There is only one thing to do now",
                Author = "Annonynous"
            },

            new QuotesViewModel()
            {
                Quote = "However it goes, we will see",
                Author = "Annonynous"
            },
        };

        public static IEnumerable<QuotesViewModel> Inputs => QuotesDb;

        public static void AddQuotes(QuotesViewModel input)
        {
            //Inputs.Add(input);
        }
    }
}
