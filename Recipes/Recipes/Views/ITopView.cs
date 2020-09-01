namespace Recipes.Views
{

    internal interface ITopView
    {

        public void ShowMenu();
        void ShowMenu(string menuHead);
        void ShowMenu(string rememberMenuHead, bool remember);

    }

}