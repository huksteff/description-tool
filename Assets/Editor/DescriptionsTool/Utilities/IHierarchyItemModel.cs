namespace Editor.DescriptionsTool.Utilities
{
    public interface IHierarchyItemModel
    {
        public string Name { get; }
        public string Path { get; }
        
        HierarchyItemType Type { get; }
    }
}