using Recipes.Models;
using Recipes.Views;
using System;
using System.Collections.Generic;
using System.Linq;

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

            T currentCategory = tree[0];
            var neighbors = tree.Where(x => x.ParentId == currentCategory.Id).ToArray();

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

                    if (currentCategory.ParentId == 0) // if top category move down opens childs
                    {
                        foreach (var id in neighbors)
                        {
                            sortedTree[id.Id - 1].Visible = true; //all Id in sorted list == index + 1
                        }

                        parentId = currentCategory.ParentId;

                    }
                    else
                    {
                        SwitchCategory(false, currentCategory, sortedTree, autoExpandChildren);

                        if (parentId != 0 && neighbors.Length > horizontal + 1) //can we move down?
                        {
                            horizontal++;
                            currentCategory = neighbors[horizontal];
                        }
                    }

                }

                else if (destination == Destination.MoveUp)
                {
                    SwitchCategory(false, currentCategory, sortedTree, autoExpandChildren);

                    if (0 <= horizontal - 1)
                    {
                        horizontal--;

                        if (parentId != 0)
                            currentCategory = neighbors[horizontal];

                    }

                }

                else if (destination == Destination.MoveRight)
                {
                    SwitchCategory(false, currentCategory, sortedTree, autoExpandChildren);
                    var children = tree.Where(x => x.ParentId == currentCategory.Id).ToArray();

                    if (children.Length != 0)
                    {
                        horizontal      = 0;
                        parentId        = currentCategory.Id;
                        currentCategory = children[horizontal];
                        neighbors       = tree.Where(x => x.ParentId == parentId).ToArray();

                        foreach (var id in neighbors)
                        {
                            sortedTree[id.Id - 1].Visible = true;
                        }
                    }

                }

                else if (destination == Destination.MoveLeft)
                {

                    if (currentCategory.ParentId != 0)
                    {
                        SwitchCategory(false, currentCategory, sortedTree, autoExpandChildren);

                        if (parentId != 0)
                        {
                            foreach (var id in neighbors)
                            {
                                sortedTree[id.Id - 1].Visible = false;
                            }

                            currentCategory = sortedTree[currentCategory.ParentId - 1];
                        }

                        parentId  = currentCategory.ParentId;
                        neighbors = tree.Where(x => x.ParentId == parentId).ToArray();

                        if (parentId != 0) horizontal = Array.IndexOf(neighbors, currentCategory);

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

                SwitchCategory(true, currentCategory, sortedTree, autoExpandChildren);
                printer.ClearView(sortedTree);
                printer.PrintTree(sortedTree);

            }
        }

        void SwitchCategory(bool activate, ICategory selected, IList<T> tree,
            bool autoExpandChildren) //to implement auto expand
        {
            selected.Active = activate;

            var children = tree.Where(x => x.ParentId == selected.Id).ToArray();

            if (autoExpandChildren && children.Length != 0)
            {
                foreach (T child in children)
                {
                    child.Visible = activate;
                }
            }

        }

        void HideAllCategories(IList<T> tree)
        {

            foreach (T category in tree)
            {
                category.Visible = false;

            }

        }

        void ResetActive(IList<T> rootTree, ICategory currentCategory) // resets active category to root
        {
            currentCategory.Active = false;
            rootTree[0].Active     = true;
        }

    }

}