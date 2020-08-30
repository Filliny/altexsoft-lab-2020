using System.Collections.Generic;
using Recipes.Models;
using Recipes.Views;

namespace Recipes.Navigation
{

    class TreeNavigator<T> where T : class, ICategory
    {

        //For Category tree and ICategory related types navigate and display
        public ICategory Navigate(IList<T> tree, IKeyReader reader, ITreePrinter<T> printer,
            bool autoExpandChildren)
        {
            List<T> sortedTree = (List<T>) tree;
            sortedTree.Sort((x, y) => x.Id - y.Id);

            ICategory currentCategory = tree[0];
            int parentId = 0;
            int horizontal = 0;
            sortedTree[0].Active = true; //Highlight root category

            currentCategory.Visible = true;
            printer.PrintTree(sortedTree);

            while (true)
            {
                Destination destination = reader.GetDestination();

                if (destination == Destination.MoveDown)
                {
                    if (currentCategory.ParentCategoryId == 0) // if top category move down opens childs
                    {

                        foreach (int childId in currentCategory.ChildIds)
                        {
                            sortedTree[childId - 1].Visible = true; //all Id in sorted list == index + 1
                        }

                        parentId = currentCategory.ParentCategoryId;
                        //currentCategory = currentCategory.GetChildren()[horizontal];

                    }
                    else
                    {
                        DeactivateCategory(currentCategory, sortedTree, autoExpandChildren);

                        if (parentId != 0 && sortedTree[parentId - 1].ChildIds.Count > horizontal + 1
                        ) //can we move down?
                        {
                            horizontal++;
                            currentCategory = sortedTree[sortedTree[parentId - 1].ChildIds[horizontal] - 1];
                        }
                    }

                }

                else if (destination == Destination.MoveUp)
                {
                    DeactivateCategory(currentCategory, sortedTree, autoExpandChildren);

                    if (0 <= horizontal - 1)
                    {
                        horizontal--;

                        if (parentId != 0)
                            currentCategory = sortedTree[sortedTree[parentId - 1].ChildIds[horizontal] - 1];

                    }

                }

                else if (destination == Destination.MoveRight)
                {
                    DeactivateCategory(currentCategory, sortedTree, autoExpandChildren);

                    if (currentCategory.ChildIds.Count != 0)
                    {
                        foreach (int childId in currentCategory.ChildIds)
                        {
                            sortedTree[childId - 1].Visible = true;
                        }

                        horizontal      = 0;
                        parentId        = currentCategory.Id;
                        currentCategory = sortedTree[currentCategory.ChildIds[horizontal] - 1];

                    }

                }

                else if (destination == Destination.MoveLeft)
                {

                    if (currentCategory.ParentCategoryId != 0)
                    {
                        DeactivateCategory(currentCategory, sortedTree, autoExpandChildren);

                        if (parentId != 0)
                        {
                            foreach (int childId in sortedTree[parentId - 1].ChildIds)
                            {
                                sortedTree[childId - 1].Visible = false;
                            }

                            currentCategory = sortedTree[currentCategory.ParentCategoryId - 1];
                        }

                        parentId = currentCategory.ParentCategoryId;
                        if (parentId != 0) horizontal = sortedTree[parentId - 1].ChildIds.IndexOf(currentCategory.Id);

                    }
                }
                else if (destination == Destination.Select)
                {
                    ResetActive(tree, currentCategory);

                    return currentCategory;
                }
                else if (destination == Destination.Esc)
                {
                    ResetActive(sortedTree, currentCategory);
                    HideAllCategories(sortedTree);
                    printer.ClearView(sortedTree);

                    return null;

                }

                //currentCategory.SetParent(parent);
                ActivateCategory(currentCategory, sortedTree, autoExpandChildren);

                printer.ClearView(sortedTree);
                printer.PrintTree(sortedTree);

            }
        }

        void ActivateCategory(ICategory selected, IList<T> tree, bool autoExpandChildren) //to implement auto expand
        {
            selected.Active = true;

            if (autoExpandChildren && selected.ChildIds.Count != 0)
            {
                foreach (int childrenId in selected.ChildIds)
                {
                    tree[childrenId - 1].Visible = true;
                }
            }

        }

        void DeactivateCategory(ICategory selected, IList<T> tree, bool autoExpandChildren) //to implement auto expand
        {
            selected.Active = false;

            if (autoExpandChildren && selected.ChildIds.Count != 0)
            {
                foreach (int childrenId in selected.ChildIds)
                {
                    tree[childrenId - 1].Visible = false;
                }
            }
        }

        void HideAllCategories(IList<T> tree)
        {

            foreach (T category in tree)
            {
                category.Visible = false;
                //HideAllCategories(category);
            }

        }

        void ResetActive(IList<T> rootTree, ICategory currentCategory) // resets active category to root
        {
            currentCategory.Active = false;
            rootTree[0].Active     = true;
        }

    }

}