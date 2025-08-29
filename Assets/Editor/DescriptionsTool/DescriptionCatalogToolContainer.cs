using Editor.DescriptionsTool.Inspector;
using Editor.DescriptionsTool.ListTree;
using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool
{
    public class DescriptionCatalogToolContainer : BaseEditorContainer
    {
        public VisualElement HorizontalGroup;
        public ListTreeContainer ListTreeContainer;
        public InspectorContainer InspectorContainer;
        
        public DescriptionCatalogToolContainer()
        {
            Root.AddToClassList("window");

            ListTreeContainer = new ListTreeContainer(Root);
            InspectorContainer = new InspectorContainer(Root);
            
            HorizontalGroup = new VisualElement();
            HorizontalGroup.AddStyle("horizontal-container");
            Root.Add(HorizontalGroup);
            ListTreeContainer.Root.AddStyle("container-element");
            InspectorContainer.Root.AddStyle("container-element");
            HorizontalGroup.Add(ListTreeContainer.Root);
            HorizontalGroup.Add(InspectorContainer.Root);
        }
    }
}