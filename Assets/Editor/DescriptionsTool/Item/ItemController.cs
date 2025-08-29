using Editor.DescriptionsTool.Utilities.Extensions;
using Editor.Utilities;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Editor.DescriptionsTool.Item
{
    public class ItemController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly ItemModel _model;
        private readonly ItemContainer _container;
        private IValueAnimation _currentAnimation;

        public ItemController(DescriptionCatalogContext context, ItemModel model, ItemContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _container.DescriptionName.text = _model.Name;

            _model.OnSwitchVisibility += HandleHide;
            _container.DescriptionField.RegisterCallback<ClickEvent>(OnClick);

            _container.DescriptionField.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            _container.DescriptionField.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);

            _context.ListTreeModel.OnSelection += HandleSelectionChange;
        }

        public void Dispose()
        {
            _model.OnSwitchVisibility -= HandleHide;
            
            _container.DescriptionField.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
            _container.DescriptionField.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
            
            _context.ListTreeModel.OnSelection -= HandleSelectionChange;
        }

        private void HandleSelectionChange(ItemModel old, ItemModel model)
        {
            if (model == _model)
            {
                if (_model.Folder.IsHide)
                {
                    _model.Folder.IsHide = false;
                    _model.Folder.OnChangeSelection?.Invoke(true);
                }
                SelectItem();
            }
            else
            {
                DeselectItem();
            }
        }

        private void OnClick(ClickEvent evt)
        {
            _context.ListTreeModel.SelectItem(_model);
        }

        private void HandleHide(bool state)
        {
            if (state)
            {
                _container.Show();
            }
            else
            {
                _container.Hide();
            }
        }

        private void SelectItem()
        {
            _container.DescriptionField.AddStyle("file-on-selected");

            _model.IsSelect = true;
            ClickItemAnimation(30f, 8f);
        }

        private void DeselectItem()
        {
            _container.DescriptionField.RemoveStyle("file-on-selected");

            _model.IsSelect = false;
            ClickItemAnimation(20f, 0f);
        }

        private void OnMouseEnter(MouseEnterEvent evt)
        {
            if (_model.IsSelect) return;

            _model.IsHovered = true;
            
            HoverItemAnimation(30f);
        }

        private void OnMouseLeave(MouseLeaveEvent evt)
        {
            if (_model.IsSelect) return;

            _model.IsHovered = false;
            
            HoverItemAnimation(20f);
        }

        private void HoverItemAnimation(float marginHorizValue)
        {
            var currentMarginLeft = _container.DescriptionField.resolvedStyle.marginLeft;
            
            var animation = _container.DescriptionField.experimental.animation
                .Start(from: new StyleValues { marginLeft = currentMarginLeft}, 
                    to: new StyleValues { marginLeft = marginHorizValue},
                    durationMs: 300).Ease(Easing.OutSine);

            _currentAnimation = animation;
        }

        private void ClickItemAnimation(float marginHorizValue, float marginVerticalValue)
        {
            var currentMarginLeft = _container.DescriptionField.resolvedStyle.marginLeft;
            var currentMarginTop = _container.DescriptionField.resolvedStyle.marginTop;
            var currentMarginBottom = _container.DescriptionField.resolvedStyle.marginBottom;
            
            var animation = _container.DescriptionField.experimental.animation
                .Start(from: new StyleValues { marginLeft = currentMarginLeft, marginTop = currentMarginTop, marginBottom = currentMarginBottom}, 
                    to: new StyleValues { marginLeft = marginHorizValue, marginTop = marginVerticalValue, marginBottom = marginVerticalValue},
                    durationMs: 300).Ease(Easing.OutSine);

            _currentAnimation = animation;
        }
    }
}