using System;
using System.IO;
using System.Linq;
using Editor.DescriptionsTool.Item;
using Editor.DescriptionsTool.ListTree;
using Editor.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool.SearchItems
{
    public class SearchItemsController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly SearchItemsModel _model;
        private readonly ListTreeContainer _container;
        private readonly ControllersList _controllers = new();
        private string _rootFolderPath = "Assets/Content/ScriptableObjects/";

        public SearchItemsController(DescriptionCatalogContext context, SearchItemsModel model,
            ListTreeContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _container.SearchFilter.value = _model.SearchField;

            _container.SearchFilter.RegisterValueChangedCallback(UpdateSearchValue);

            _model.OnSearch += Searching;
            _model.OnAdd += HandleAdd;
        }

        public void Dispose()
        {
            _container.SearchFilter.UnregisterValueChangedCallback(UpdateSearchValue);
            _model.OnSearch -= Searching;

            _model.OnAdd -= HandleAdd;
        }

        private void UpdateSearchValue(ChangeEvent<string> evt)
        {
            var newText = evt.newValue;
            _model.Search(newText);
            _container.SearchFilter.value = newText;
        }

        private void Searching(string request)
        {
            if (string.IsNullOrEmpty(_model.SearchField))
            {
                _container.HideElement(_container.PopupField);
            }
            else
            {
                _container.ShowElement(_container.PopupField);
            }

            FindSOByRequest(request);
        }

        private void FindSOByRequest(string request)
        {
            _container.PopupScrollView.Clear();

            if (request.Contains("t:SO"))
            {
                var req = request.Split("\\").Last().Replace("t:SO", "");
                req = req.Replace(" ", "");
                
                var folderPaths = Directory.GetDirectories(_rootFolderPath);

                foreach (var folderPath in folderPaths)
                {
                    if (string.IsNullOrEmpty(request)) continue;
                    
                    var filePaths = Directory.GetFiles(folderPath).Where(path => !path.Contains(".meta"));
                    
                    foreach (var filePath in filePaths)
                    {
                        var fileName = filePath.Split("\\").Last().Replace(".asset", "");
                        var folderName = filePath.Replace("\\", "/").Replace(".asset", "");

                        if (fileName.Contains(req, StringComparison.OrdinalIgnoreCase))
                        {
                            _model.AddItem($"{folderName} \u27f6 [ By request: {req} ]", filePath);
                        }
                    }
                }
            }

            if (request.Contains("t:Field"))
            {
                var req = request.Split("\\").Last().Replace("t:Field", "");
                req = req.Replace(" ", "");
                
                if (!String.IsNullOrEmpty(req))
                {
                    var folderPaths = Directory.GetDirectories(_rootFolderPath);

                    foreach (var folderPath in folderPaths)
                    {
                        if (string.IsNullOrEmpty(request)) continue;

                        var filePaths = Directory.GetFiles(folderPath).Where(path => !path.Contains(".meta"));
                        
                        foreach (var filePath in filePaths)
                        {
                            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(filePath);
                            SerializedObject serializedSo = new SerializedObject(so);
            
                            SerializedProperty descriptionField = serializedSo.FindProperty("Description");
            
                            SerializedProperty property = descriptionField.Copy();
                            SerializedProperty endProperty = descriptionField.GetEndProperty();
                            
                            var fileName = filePath.Split("\\").Last().Replace(".asset", "");
                            var folderName = filePath.Replace("\\", "/").Replace(".asset", "");
                            var  enterChildren = true;
                            
                            for (var nextVisible = property.NextVisible(enterChildren);
                                 nextVisible && !SerializedProperty.EqualContents(property, descriptionField);
                                 nextVisible = property.NextVisible(enterChildren))
                            {
                                if (SerializedProperty.EqualContents(property, descriptionField))
                                    continue;
                                
                                if (property.depth > 1)
                                    continue;

                                if (property.name.Equals(req))
                                {
                                    _model.AddItem($"{folderName} \u27f6 [ By request: {req} ]", filePath);
                                }
                                
                                enterChildren = false;
                            }
                        }
                    }
                }
            }
        }

        private void HandleAdd(ItemModel model)
        {
            var newContainer = new ItemContainer(_container.PopupScrollView);
            var searchController = new ItemController(_context, model, newContainer);

            searchController?.Init();
            _controllers.Add(searchController);
        }
    }
}