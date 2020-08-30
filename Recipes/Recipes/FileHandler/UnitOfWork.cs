using Recipes.Controllers;
using Recipes.Models;

using System;

namespace Recipes.FileHandler
{

    class UnitOfWork : IDisposable, IUnitOfWork
    {

        private bool _disposed;

        private readonly IStorageContext _storage = new StorageContext();
        private RecipeRepository _recipeRepository;
        private IngredientsRepository _ingredientsRepository;
        private CategoriesRepository _categoriesRepository;
        private TopMenuRepository _topMenuRepository;

        private readonly IFileReader _reader;
        private readonly IFileWriter _writer;

        public UnitOfWork(IFileReader reader, IFileWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }

        //public IStorageContext Context => storage;

        public RecipeRepository Recipes
        {
            get
            {
                if (_recipeRepository == null)
                    _recipeRepository = new RecipeRepository(_storage.RecipesFile.Storage);

                return _recipeRepository;

            }
        }

        public IngredientsRepository Ingredients
        {
            get
            {
                if (_ingredientsRepository == null)
                    _ingredientsRepository = new IngredientsRepository(_storage.IngredientsFile.IngredientsList);

                return _ingredientsRepository;

            }
        }

        public CategoriesRepository Categories
        {
            get
            {
                if (_categoriesRepository == null)
                    _categoriesRepository = new CategoriesRepository(_storage.RecipesTree.CategoryList);

                return _categoriesRepository;

            }
        }

        public TopMenuRepository TopMenu
        {
            get
            {
                if (_topMenuRepository == null)
                    _topMenuRepository = new TopMenuRepository(_storage.TopCategories.TopArticles);

                return _topMenuRepository;

            }
        }

        public void ReadFiles()
        {
            _storage.RecipesTree     = (Categories) _reader.ReadFile<Categories>();
            _storage.IngredientsFile = (Ingredients) _reader.ReadFile<Ingredients>();
            _storage.RecipesFile     = (RecipesList) _reader.ReadFile<RecipesList>();
            _storage.TopCategories   = (TopMenu) _reader.ReadFile<TopMenu>();
        }

        public void SaveFiles()
        {
            _writer.WriteFile(_storage.IngredientsFile);
            _writer.WriteFile(_storage.RecipesFile);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _storage.Dispose();
                }
        
                this._disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

}