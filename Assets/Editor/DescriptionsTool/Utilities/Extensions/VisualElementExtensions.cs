using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Utilities.Extensions
{
    public static class VisualElementExtensions
    {
        public static Label CreateLabel(this VisualElement root, string contentText = null, string styles = null)
        {
            var label = new Label(contentText);
            label.AddStyle(styles);
            root.Add(label);

            return label;
        }

        public static VisualElement CreateIcon(this VisualElement root, string styles = null)
        {
            var icon = new VisualElement();
            icon.AddStyle(styles);
            root.Add(icon);
            return icon;
        }
    }
}