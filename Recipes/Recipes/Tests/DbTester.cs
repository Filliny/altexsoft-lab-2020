using Recipes.DbHandler;
using Recipes.Models;
using Recipes.Views;

namespace Recipes
{
    class DbTester
    {
        public void SetDb()
        {

            //create ingredient
            Ingredient ingredient1 = new Ingredient();
            ingredient1.Name         = "Carrot";
            ingredient1.Id = 1;
            ingredient1.Measure      = Measurements.Grams;
            
            Ingredient ingredient2 = new Ingredient();
            ingredient2.Name         = "Potato";
            ingredient2.Id = 2;
            ingredient2.Measure      = Measurements.Grams;

            //add to ingred db
            Ingredients ingredients = new Ingredients();
            ingredients.IngredientsList.Add(ingredient1);
            ingredients.IngredientsList.Add(ingredient2);

            //Create recipes
            Recipe recipe1 = new Recipe();
            recipe1.CategoryId = 1;
            recipe1.Name        = "Sate";
            recipe1.Explanation = "dsflhdsklgjhsadkjghkdfashg";
            recipe1.IngredientsId.Add(1);
            recipe1.IngredientsId.Add(2);
            recipe1.Steps = new string[] {"boil potato", "boil carrot"};

            Recipe recipe2 = new Recipe();
            recipe2.CategoryId = 2;
            recipe2.Name        = "Free";
            recipe2.Explanation = "dsflhdsklgjhsfdg wfewg adkjghkdfashg";
            recipe2.IngredientsId.Add(1);
            recipe2.IngredientsId.Add(2);
            recipe2.Steps = new string[] { "fry potato", "fry carrot" };

            RecipesList recipesList = new RecipesList();
            recipesList.Storage.Add(recipe1);
            recipesList.Storage.Add(recipe2);


            Categories categories = new Categories();
            
            Category Main = new Category();
            Main.Name = "Main";

            Category Child1 = new Category();
            Category Child2 = new Category();
            Category Child3 = new Category();
            Category Child4 = new Category();


            Child1.Name = "Child1";
            Child2.Name = "Child2";
            Child3.Name = "Child3";
            Child4.Name = "Child4";

            Child3.SetParent(Child2) ;
            Child4.SetParent(Child2); 

            Child2.SetParent(Main);
            Child1.SetParent(Main);

            Main.ChildrenCategories.Add(Child1);
            Main.ChildrenCategories.Add(Child2);

            Child2.ChildrenCategories.Add(Child3);
            Child2.ChildrenCategories.Add(Child4);

            Main.Id = 0;
            Child1.Id = 1;
            Child2.Id = 2;
            Child3.Id = 3;
            Child4.Id = 4;

            categories.RootCategory = Main;
            DbWriter DbWriter = new DbWriter();
            //DbWriter.WriteDbFile(categories);
            TopMenu top = new TopMenu();
            top.Name = "Рецепты";
            TopMenu chl = new TopMenu();
            chl.Name = "Ингридиенты";
            top.ChildrenCategories.Add(chl);
            DbWriter.WriteDbFile(top);


            // string recipesdDbText = JsonConvert.SerializeObject(recipesList, Formatting.Indented);
            // string ingredsdDbText = JsonConvert.SerializeObject(ingredients, Formatting.Indented);
            // string caregdDbText = JsonConvert.SerializeObject(Main, Formatting.Indented,
            //     new JsonSerializerSettings
            //     {
            //         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //     });
            //
            // var categoriess = JsonConvert.DeserializeObject<Category>(caregdDbText);
            //
            // File.WriteAllText("recipes.json",recipesdDbText);
            // File.WriteAllText("ingridients.json", ingredsdDbText );
            // File.WriteAllText("categ.json", caregdDbText);





        }
    }
}
