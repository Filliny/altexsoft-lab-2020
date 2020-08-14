using Recipes.Models;
using Recipes.Views;

namespace Recipes.Navigation
{

    class TreeNavigator
    {

        // Cos we have no parent categories pointer in children categories - all tree parens fill during navigation
        public ICategory Navigate(ICategory tree, IKeyReader reader, ITreePrinter printer,
            bool autoExpandChildren, int posX, int fieldWidth)
        {
            ICategory currentCategory = tree;
            ICategory parent = null;
            int horizontal = 0;

            currentCategory.Visible = true;
            printer.PrintTree(tree, posX, fieldWidth);

            while (true)
            {
                Destination destination = reader.GetDestination();

                if (destination == Destination.MoveDown)
                {
                    if (currentCategory.GetParent() == null) // if top category
                    {
                        foreach (ICategory childCat in currentCategory.GetChildren())
                        {
                            childCat.Visible = true;
                        }

                        parent         = currentCategory;
                        currentCategory = currentCategory.GetChildren()[horizontal];
                        // currentCategory.SetParent(parent);
                        // ActivateCategory(currentCategory, autoExpandChildren);

                    }
                    else
                    {
                        DeactivateCategory(currentCategory, autoExpandChildren);

                        if (parent != null && parent.GetChildren().Count > horizontal + 1)
                        {
                            horizontal++;
                            currentCategory = parent.GetChildren()[horizontal];
                            // ActivateCategory(currentCategory, autoExpandChildren);//
                            // currentCategory.SetParent(parent);//

                        }
                    }

                }
                else if (destination == Destination.MoveUp)
                {
                    DeactivateCategory(currentCategory, autoExpandChildren);

                    if (0 <= horizontal - 1)
                    {
                        horizontal--;

                        if (parent != null) currentCategory = parent.GetChildren()[horizontal];
                        // ActivateCategory(currentCategory, autoExpandChildren);
                        // currentCategory.SetParent(parent);
                    }

                }
                else if (destination == Destination.MoveRight)
                {
                    DeactivateCategory(currentCategory, autoExpandChildren);

                    if (currentCategory.GetChildren().Count != 0)
                    {

                        foreach (ICategory childCat in currentCategory.GetChildren())
                        {
                            childCat.Visible = true;
                        }

                        horizontal     = 0;
                        parent         = currentCategory;
                        currentCategory = currentCategory.GetChildren()[horizontal];
                        // currentCategory.SetParent(parent);
                        // ActivateCategory(currentCategory, autoExpandChildren);

                    }

                }
                else if (destination == Destination.MoveLeft)
                {

                    if (currentCategory.GetParent() != null)
                    {
                        DeactivateCategory(currentCategory, autoExpandChildren);

                        if (parent != null)
                        {
                            foreach (ICategory childCat in parent.GetChildren())
                            {
                                childCat.Visible = false;
                            }

                            currentCategory = currentCategory.GetParent();
                        }

                        parent         = currentCategory.GetParent();
                        //ActivateCategory(currentCategory, autoExpandChildren);

                        if (parent != null) horizontal = parent.GetChildren().IndexOf(currentCategory);

                    }
                }
                else if (destination == Destination.Select)
                {
                    currentCategory.Active = false;
                    tree.Active = true;
                    return currentCategory;
                }
                else if (destination == Destination.Esc)
                {
                    HideAllCategories(tree);
                    printer.ClearView(tree, posX, fieldWidth);

                    return null;

                }

                currentCategory.SetParent(parent);
                ActivateCategory(currentCategory, autoExpandChildren);

                printer.ClearView(tree, posX, fieldWidth);
                printer.PrintTree(tree, posX, fieldWidth);

            }
        }

        void ActivateCategory(ICategory selected, bool autoExpandChildren) //to implement auto expand
        {
            selected.Active = true;

            if (autoExpandChildren && selected.GetChildren().Count != 0)
            {
                foreach (ICategory children in selected.GetChildren())
                {
                    children.Visible = true;
                }
            }

        }

        void DeactivateCategory(ICategory selected, bool autoExpandChildren) //to implement auto expand
        {
            selected.Active = false;

            if (autoExpandChildren && selected.GetChildren().Count != 0)
            {
                foreach (ICategory children in selected.GetChildren())
                {
                    children.Visible = false;
                }
            }
        }

        public void HideAllCategories(ICategory treeRoot) 
        {
            treeRoot.Visible = false;

            foreach (ICategory category in treeRoot.GetChildren())
            {
                category.Visible = false;
                HideAllCategories(category);
            }

        }

    }

}