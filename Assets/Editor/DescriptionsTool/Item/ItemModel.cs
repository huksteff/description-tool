using System;
using Editor.DescriptionsTool.Folder;
using Editor.DescriptionsTool.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Item
{
    public class ItemModel : BaseItemModel
    {
        public Action<bool> OnSwitchVisibility;
        
        public override HierarchyItemType Type => HierarchyItemType.Item;
        public bool IsHovered;
        public bool IsSelect;
        public FolderModel Folder;
        public bool IsHide;
        
        public ItemModel(string name, string path) : base(name, path)
        {
            
        }
        
        public void SwitchVisibility(bool state)
        {
            IsHide = state;
            OnSwitchVisibility?.Invoke(state);
        }
    }
}