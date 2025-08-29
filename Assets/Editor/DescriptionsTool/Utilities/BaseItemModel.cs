namespace Editor.DescriptionsTool.Utilities
{
    public abstract class BaseItemModel : IHierarchyItemModel
    {
        public string Name { get; }
        public string Path { get; }
        public abstract HierarchyItemType Type { get; }
        
        protected BaseItemModel(string name = null, string path = null)
        {
            Name = name;
            Path = path;
        }
    }
}