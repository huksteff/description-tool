using System.IO;
using System.Linq;
using Editor.DescriptionsTool.Folder;
using Editor.Utilities;

namespace Editor.DescriptionsTool.ListTree
{
    public class ListTreeController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly ListTreeModel _model;
        private readonly ListTreeContainer _container;
        private readonly ControllersList _controllers = new();
        private string _rootFolderPath = "Assets/Content/ScriptableObjects/";

        public ListTreeController(DescriptionCatalogContext context, ListTreeModel model, ListTreeContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _container.HideElement(_container.PopupField);

            _model.OnAdd += HandleAdd;
            GetSoData();
        }


        public void Dispose()
        {
            _model.OnAdd -= HandleAdd;
        }

        private void HandleAdd(FolderModel newModel)
        {
            var folderContainer = new FolderContainer(_container.ScrollView);
            var newController = new FolderController(_context, newModel, folderContainer);

            newController?.Init();
            _controllers.Add(newController);
        }

        private void GetSoData()
        {
            var folderPaths = Directory.GetDirectories(_rootFolderPath);
            
            foreach (var folderPath in folderPaths)
            {
                var folderName = folderPath.Replace(_rootFolderPath, "");
                var folderModel = _model.AddFolder(folderName, folderPath);
                
                var filePaths = Directory.GetFiles(folderPath).Where(path => !path.Contains(".meta"));
                
                foreach (var filePath in filePaths)
                {
                    var fileName = filePath.Split("\\").Last().Replace(".asset", "");
                    
                    folderModel.AddItem(fileName, filePath);
                }
            }
        }


    }
}