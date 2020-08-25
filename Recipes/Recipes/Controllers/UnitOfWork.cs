using Recipes.Controllers;
using Recipes.Models;

using System;

namespace Recipes.FileHandler
{

    internal interface IUnitOfWork
    {

        void ReadFiles();
        void SaveFiles();

        IStorageContext Context { get; }

        RecipeRepository Recipes { get; }
        IngredientsRepository Ingredients { get; }
        CategoriesRepository Categories { get; }
        TopMenuRepository TopMenu { get; }
    }

    class UnitOfWork : IDisposable, IUnitOfWork
    {

        private bool disposed = false;

        private IStorageContext storage = new StorageContext();
        private RecipeRepository recipeRepository;
        private IngredientsRepository ingredientsRepository;
        private CategoriesRepository categoriesRepository;
        private TopMenuRepository topMenuRepository;

        private IFileReader _reader;
        private IFileWriter _writer;

        public UnitOfWork(IFileReader reader, IFileWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        public IStorageContext Context => storage;

        public RecipeRepository Recipes
        {
            get
            {
                if (recipeRepository == null)
                    recipeRepository = new RecipeRepository(storage);

                return recipeRepository;

            }
        }

        public IngredientsRepository Ingredients
        {
            get
            {
                if (ingredientsRepository == null)
                    ingredientsRepository = new IngredientsRepository(storage);

                return ingredientsRepository;

            }
        }

        public CategoriesRepository Categories
        {
            get
            {
                if (categoriesRepository == null)
                    categoriesRepository = new CategoriesRepository(storage);

                return categoriesRepository;

            }
        }

        public TopMenuRepository TopMenu
        {
            get
            {
                if (topMenuRepository == null)
                    topMenuRepository = new TopMenuRepository(storage);

                return topMenuRepository;

            }
        }

        public void ReadFiles()
        {
            storage.RecipesTree     = (Categories) _reader.ReadFile<Categories>();
            storage.IngredientsFile = (Ingredients) _reader.ReadFile<Ingredients>();
            storage.RecipesFile     = (RecipesList) _reader.ReadFile<RecipesList>();
            storage.TopMenu         = (TopMenu) _reader.ReadFile<TopMenu>();
        }

        public void SaveFiles()
        {
            _writer.WriteFile(storage.IngredientsFile);
            _writer.WriteFile(storage.RecipesFile);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    storage.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}