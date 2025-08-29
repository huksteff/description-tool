using System.Linq;
using Editor.DescriptionsTool.Item;
using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.Folder
{
    public class FolderController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly FolderModel _model;
        private readonly FolderContainer _container;
        private readonly ControllersList _controllers = new();

        public FolderController(DescriptionCatalogContext context, FolderModel model, FolderContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _container.FolderName.text = _model.Name;

            _container.FolderName.RegisterCallback<ClickEvent>(ChangeVisible);
            _model.OnAdd += HandleAdd;

            _context.ListTreeModel.OnSelection += OnSelectedItem;
        }

        public void Dispose()
        {
            _model.OnAdd -= HandleAdd;
            _context.ListTreeModel.OnSelection -= OnSelectedItem;
        }

        private void ChangeVisible(ClickEvent evt)
        {
            _model.IsHide = !_model.IsHide;

            foreach (var item in _model.Items.Where(element => !element.IsSelect))
            {
                item.SwitchVisibility(!_model.IsHide);
            }

            if (_model.IsHide)
            {
                _container.FolderName.AddStyle("folder-is-close");
                _container.Icon.AddStyle("folder-icon-hidden");
            }
            else
            {
                _container.FolderName.RemoveStyle("folder-is-close");
                _container.Icon.RemoveStyle("folder-icon-hidden");
            }
        }

        private void OnSelectedItem(ItemModel old, ItemModel itemModel)
        {
            bool itemIsSameFolder = itemModel?.Folder == _model;

            _model.ChangeSelection(itemIsSameFolder);

            if (_model.IsHide && !itemIsSameFolder)
            {
                foreach (var item in _model.Items)
                {
                    item.SwitchVisibility(false);
                }
            }

            if (old?.Folder == _model && itemModel?.Folder != _model && _model.IsHide)
            {
                foreach (var item in _model.Items)
                {
                    if (item != old)
                    {
                        item.SwitchVisibility(false);
                    }
                }
            }
        }

        private void HandleAdd(ItemModel newModel)
        {
            var itemContainer = new ItemContainer(_container.ScrollView);
            var newController = new ItemController(_context, newModel, itemContainer);

            newController?.Init();
            _controllers.Add(newController);
        }
    }
}