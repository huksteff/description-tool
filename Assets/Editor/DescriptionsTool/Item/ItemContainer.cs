using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Item
{
    public class ItemContainer : BaseEditorUnitContainer
    {
        public readonly VisualElement DescriptionField;
        public readonly Label DescriptionName;
        public readonly VisualElement Icon;
        
        public ItemContainer(VisualElement root) : base(root)
        {
            DescriptionField = new VisualElement();
            DescriptionField.AddStyle("description-field");
            Root.Add(DescriptionField);
            
            DescriptionName = Root.CreateLabel(styles:"label-description");
            DescriptionField.Add(DescriptionName);

            Icon = new VisualElement();
            Icon.AddStyle("description-icon");
            Root.Add(Icon);
            DescriptionName.Add(Icon);
        }
    }
}