using System;
using Editor.DescriptionsTool.Inspector;
using Editor.DescriptionsTool.ListTree;
using Editor.DescriptionsTool.SearchItems;
using Editor.Utilities;

namespace Editor.DescriptionsTool
{
    public class DescriptionCatalogToolController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly DescriptionCatalogToolContainer _container;
        private readonly ControllersList _controllersList = new ();
        
        public DescriptionCatalogToolController(DescriptionCatalogContext context, DescriptionCatalogToolContainer container)
        {
            _context = context;
            _container = container;
        }
        
        public void Init()
        {
            _controllersList.Add(new ListTreeController(_context, _context.ListTreeModel, _container.ListTreeContainer));
            _controllersList.Add(new InspectorController(_context, _context.InspectorModel, _container.InspectorContainer));
            _controllersList.Add(new SearchItemsController(_context, _context.SearchItemsModel, _container.ListTreeContainer));
            _controllersList.Init();
        }

        public void Dispose()
        {
            _controllersList.Dispose();
            _controllersList.Clear();
        }
    }
}