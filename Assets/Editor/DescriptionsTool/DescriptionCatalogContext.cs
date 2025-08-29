using Editor.DescriptionsTool.Folder;
using Editor.DescriptionsTool.Inspector;
using Editor.DescriptionsTool.ListTree;
using Editor.DescriptionsTool.SearchItems;

namespace Editor.DescriptionsTool
{
    public class DescriptionCatalogContext
    {
        public readonly ListTreeModel ListTreeModel = new ();
        public readonly InspectorModel InspectorModel = new ();
        public readonly SearchItemsModel SearchItemsModel = new();
    }
}