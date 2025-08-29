using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEditor.Search;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.ListTree
{
    public class ListTreeContainer : BaseEditorUnitContainer
    {
        public readonly Label ListTreeLabel;
        public readonly VisualElement Tree;
        public readonly ScrollView ScrollView;

        public readonly VisualElement SearchBlock;
        public readonly TextField SearchFilter;
        public readonly VisualElement SearchIcon;
        public readonly VisualElement PopupField;
        public readonly ScrollView PopupScrollView;
        
        public ListTreeContainer(VisualElement root) : base(root)
        {
            ListTreeLabel = Root.CreateLabel("Tree", "label-category title");

            SearchBlock = new VisualElement();
            SearchBlock.AddStyle("search-block");
            SearchBlock.tooltip = "\u25cf t:SO [assetName] - Finds ScriptableObjects by name. \n" +
                                  "\u25cf t:Field [fieldName] - Finds assets by field name.";
            
            SearchFilter = new TextField();
            SearchFilter.AddStyle("search-field");

            SearchIcon = new VisualElement();
            SearchIcon.AddStyle("search-icon");

            SearchBlock.Add(SearchIcon);
            SearchBlock.Add(SearchFilter);

            Tree = new VisualElement();
            Tree.Add(SearchBlock);

            ScrollView = new ScrollView();
            ScrollView.AddStyle("scroll-view");
            
            PopupField = new VisualElement();
            PopupField.AddStyle("popup-field");
            
            PopupScrollView = new ScrollView();
            PopupScrollView.AddStyle("scroll-view");
            PopupField.Add(PopupScrollView);
            
            Tree.AddStyle("list-tree");
            Tree.Add(ScrollView);

            Root.Add(Tree);
            Root.Add(PopupField);
        }
    }
}