using System;
using System.Collections.Generic;
using Editor.DescriptionsTool.Item;
using Editor.DescriptionsTool.Utilities;

namespace Editor.DescriptionsTool.Folder
{
    public class FolderModel : BaseItemModel
    {
        public Action<ItemModel> OnAdd;
        public Action<bool> OnChangeSelection;
        
        public override HierarchyItemType Type => HierarchyItemType.Folder;
        public readonly List<ItemModel> Items = new();
        public bool IsHide = false;
        public bool IsCurrentSelected;

        public FolderModel(string name, string path) : base(name, path)
        {
            
        }

        public ItemModel AddItem(string name, string path)
        {
            var newModel = new ItemModel(name, path)
            {
                Folder = this
            };

            Items.Add(newModel);

            OnAdd?.Invoke(newModel);
            
            return newModel;
        }

        public void ChangeSelection(bool state)
        {
            if (IsCurrentSelected == state)
                return;

            IsCurrentSelected = state;
            
            OnChangeSelection?.Invoke(state);
        }
    }
}