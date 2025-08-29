using System;
using System.Collections.Generic;
using Editor.DescriptionsTool.Folder;
using Editor.DescriptionsTool.Item;
using Editor.DescriptionsTool.Utilities;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.ListTree
{
    public class ListTreeModel
    {
        public Action<FolderModel> OnAdd;
        public Action<ItemModel, ItemModel> OnSelection;

        private readonly List<FolderModel> Categories = new();
        public ItemModel SelectedItem;

        public FolderModel AddFolder(string name, string path)
        {
            var newModel = new FolderModel(name, path);

            Categories.Add(newModel);

            OnAdd?.Invoke(newModel);
            
            return newModel;
        }
        
        public void SelectItem(ItemModel itemModel)
        {
            var previousItem = SelectedItem;
            
            SelectedItem = itemModel;
            
            OnSelection?.Invoke(previousItem, SelectedItem);
        }


        
    }
}