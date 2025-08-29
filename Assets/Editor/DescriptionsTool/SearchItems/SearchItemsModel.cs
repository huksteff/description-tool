using System;
using System.Collections.Generic;
using Editor.DescriptionsTool.Item;

namespace Editor.DescriptionsTool.SearchItems
{
    public class SearchItemsModel
    {
        public Action<ItemModel> OnAdd;
        public Action<string> OnSearch;
        
        public string SearchField = "";
        public readonly List<ItemModel> Items = new();

        public SearchItemsModel()
        {
            
        }
        
        public void Search(string request)
        {
            SearchField = request;
            OnSearch?.Invoke(SearchField);
        }
        
        public ItemModel AddItem(string name, string path)
        {
            var newModel = new ItemModel(name, path);
            
            Items.Add(newModel);

            OnAdd?.Invoke(newModel);
            
            return newModel;
        }
    }
}