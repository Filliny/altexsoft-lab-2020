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

        private readonly IFileManager _fileManager;

        public UnitOfWork(IFileManager fileManager)
        {
            _fileManager = fileManager;

        }

        public RecipeRepository Recipes
        {
            get
            {
                if (_recipeRepository == null)
                    _recipeRepository = new RecipeRepository(_storage.RecipesFile.ItemsList);

                return _recipeRepository;

            }
        }

        public IngredientsRepository Ingredients
        {
            get
            {
                if (_ingredientsRepository == null)
                    _ingredientsRepository = new IngredientsRepository(_storage.IngredientsFile.ItemsList);

                return _ingredientsRepository;

            }
        }

        public CategoriesRepository Categories
        {
            get
            {
                if (_categoriesRepository == null)
                    _categoriesRepository = new CategoriesRepository(_storage.RecipesTree.ItemsList);

                return _categoriesRepository;

            }
        }

        public TopMenuRepository TopMenu
        {
            get
            {
                if (_topMenuRepository == null)
                    _topMenuRepository = new TopMenuRepository(_storage.TopCategories.ItemsList);

                return _topMenuRepository;

            }
        }

        public void ReadFiles()
        {
            _storage.RecipesTree.ItemsList     = _fileManager.ReadFile<Category>();
            _storage.IngredientsFile.ItemsList = _fileManager.ReadFile<Ingredient>();
            _storage.RecipesFile.ItemsList     = _fileManager.ReadFile<Recipe>();
            _storage.TopCategories.ItemsList   = _fileManager.ReadFile<TopCategory>();
        }

        public void SaveFiles()
        {
            _fileManager.WriteFile(_storage.IngredientsFile.ItemsList);
            _fileManager.WriteFile(_storage.RecipesFile.ItemsList);
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