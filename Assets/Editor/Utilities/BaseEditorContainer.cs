using UnityEngine.UIElements;

namespace Editor.Utilities
{
    public abstract class BaseEditorContainer
    {
        public readonly VisualElement Root = new();

        protected BaseEditorContainer()
        {
            Root.style.flexGrow = 1;
        }
    }
}
