using Recipes.Models;
using Recipes.Views;

namespace Recipes.Navigation
{

    class TreeNavigator
    {

        //For Category tree and ICategory related types navigate and display
        // Cos we have no parent categories pointer in children categories - all tree parents fill during navigation
        public ICategory Navigate(ICategory tree, IKeyReader reader, ITreePrinter printer,
            bool autoExpandChildren)
        {
            ICategory currentCategory = tree;
            ICategory parent = null;
            int horizontal = 0;
            tree.Active = true; //Highlight root category

            currentCategory.Visible = true;
            printer.PrintTree(tree);

            while (true)
            {
                Destination destination = reader.GetDestination();

                if (destination == Destination.MoveDown)
                {
                    if (currentCategory.GetParent() == null) // if top category move down opens childs
                    {
                        foreach (ICategory childCat in currentCategory.GetChildren())
                        {
                            childCat.Visible = true;
                        }

                        parent          = currentCategory;
                        currentCategory = currentCategory.GetChildren()[horizontal];

                    }
                    else
                    {
                        DeactivateCategory(currentCategory, autoExpandChildren);

                        if (parent != null && parent.GetChildren().Count > horizontal + 1) //can we move down?
                        {
                            horizontal++;
                            currentCategory = parent.GetChildren()[horizontal];
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

                        horizontal      = 0;
                        parent          = currentCategory;
                        currentCategory = currentCategory.GetChildren()[horizontal];

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

                        parent = currentCategory.GetParent();
                        if (parent != null) horizontal = parent.GetChildren().IndexOf(currentCategory);

                    }
                }
                else if (destination == Destination.Select)
                {
                    ResetActive(tree, currentCategory);

                    return currentCategory;
                }
                else if (destination == Destination.Esc)
                {
                    ResetActive(tree, currentCategory);
                    HideAllCategories(tree);
                    printer.ClearView(tree);

                    return null;

                }

                currentCategory.SetParent(parent);
                ActivateCategory(currentCategory, autoExpandChildren);

                printer.ClearView(tree);
                printer.PrintTree(tree);

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

        void HideAllCategories(ICategory treeRoot)
        {
            treeRoot.Visible = false;

            foreach (ICategory category in treeRoot.GetChildren())
            {
                category.Visible = false;
                HideAllCategories(category);
            }

        }

        void ResetActive(ICategory rootTree, ICategory currentCategory)
        {
            currentCategory.Active = false;
            rootTree.Active        = true;
        }

    }

}