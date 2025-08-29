using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Inspector
{
    public class InspectorContainer : BaseEditorUnitContainer
    {
        public readonly Label InspectorContainerLabel;
        public readonly Label FileTitle;
        public readonly ScrollView InspectorField;
        public readonly VisualElement PropertyBlock;
        
        public InspectorContainer(VisualElement root) : base(root)
        {
            InspectorContainerLabel = Root.CreateLabel("Inspector", "title");
            InspectorField = new ScrollView();
            InspectorField.AddStyle("inspector-element");
            
            FileTitle = Root.CreateLabel("Title",styles:"file-title");
            InspectorField.Add(FileTitle);

            PropertyBlock = new VisualElement();
            InspectorField.Add(PropertyBlock);

            
            Root.Add(InspectorField);
        }
    }
}