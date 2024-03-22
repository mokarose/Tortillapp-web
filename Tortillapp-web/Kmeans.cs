using System.Data.Entity;
using Tortillapp_web.Data;
using System.Linq;
using Tortillapp_web.Models;
using System.Dynamic;
using Azure.Storage.Blobs.Models;

namespace Tortillapp_web.Kmeans
{
    public class Kmeans
    {
        public List<string> _ingredients = new List<string>();
        /*public Kmeans(List<string> ingredients)
        {
            _ingredients = ingredients;
        }*/

        public List<string> Process(List<string> ingredients)
        {
            //List<string> ingredients = GetIngredients();
            List<string> preprocessedIngredients = PreprocessIngredients(ingredients);

            // Print the cleaned ingredients
            Console.WriteLine("Cleaned Ingredients:");
            foreach (var ingredient in preprocessedIngredients)
            {
                Console.WriteLine(ingredient);
            }

            return ingredients;

        }

        private static List<string> PreprocessIngredients(List<string> ingredients)
        {
            /*var cleanedIngredients = ingredients
                .Select(ingredient => ingredient.Trim())
                .Where(ingredient => !string.IsNullOrWhiteSpace(ingredient))
                .ToList();*/

            List<string> cleanedIngredients = new List<string>();
            char[] delimiter = {',','(',')',' '};
            string[] part;

            foreach(string i in ingredients)
            {
                part = i.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                if (part.Length > 1)
                {
                    foreach (string s in part)
                    {
                        cleanedIngredients.Add(s);
                    }
                }
                else
                {
                    cleanedIngredients.Add(i);
                }
            }

            return cleanedIngredients;
        }
    }
}