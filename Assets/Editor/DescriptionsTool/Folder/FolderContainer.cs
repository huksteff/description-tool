using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Folder
{
    public class FolderContainer : BaseEditorUnitContainer
    {
        public readonly Label FolderName;
        public readonly ScrollView ScrollView;
        public readonly VisualElement Icon;
        
        public FolderContainer(VisualElement root) : base(root)
        {
            FolderName = Root.CreateLabel(styles:"label-folder");
            ScrollView = new ScrollView();
            ScrollView.AddStyle("scroll-view");
            
            Icon = Root.CreateIcon("folder-icon");
            FolderName.Add(Icon);
            
            Root.Add(ScrollView);
        }
    }
}