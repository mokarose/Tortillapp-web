using System.Data.Entity;
using Tortillapp_web.Data;
using System.Linq;
using Tortillapp_web.Models;
using System.Dynamic;

namespace Tortillapp_web.Kmeans
{
    public class Kmeans
    {
        public List<string> KmeansMain(List<string> ingredients)
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
            var cleanedIngredients = ingredients
                .Select(ingredient => ingredient.Trim())
                .Where(ingredient => !string.IsNullOrWhiteSpace(ingredient))
                .ToList();

            return ingredients;
        }
    }
}