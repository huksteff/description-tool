using Content.Scripts;
using Editor.DescriptionsTool.Item;
using Editor.Utilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System;
using Editor.DescriptionsTool.Utilities.Extensions;
using UnityEditor.UIElements;

namespace Editor.DescriptionsTool.Inspector
{
    public class InspectorController : IController
    {
        private readonly DescriptionCatalogContext _context;
        private readonly InspectorModel _model;
        private readonly InspectorContainer _container;

        public InspectorController(DescriptionCatalogContext context, InspectorModel model,
            InspectorContainer container)
        {
            _context = context;
            _model = model;
            _container = container;
        }

        public void Init()
        {
            _container.HideElement(_container.InspectorField.contentContainer);
            _context.ListTreeModel.OnSelection += OnSelectionChange;
        }

        public void Dispose()
        {
            _context.ListTreeModel.OnSelection -= OnSelectionChange;
        }

        private void OnSelectionChange(ItemModel old, ItemModel model)
        {
            _container.PropertyBlock.Clear();

            ShowItem(model);
        }

        public void ShowItem(ItemModel itemModel)
        {
            _container.ShowElement(_container.InspectorField.contentContainer);

            _container.FileTitle.text = itemModel.Name;
            
            SetupPropertiesOfDescription(itemModel.Path);
        }

        private void SetupPropertiesOfDescription(string path)
        {
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            SerializedObject serializedSo = new SerializedObject(so);
            
            SerializedProperty descriptionField = serializedSo.FindProperty("Description");
            
            SerializedProperty property = descriptionField.Copy();
            SerializedProperty endProperty = descriptionField.GetEndProperty();

            while (property.NextVisible(true) && !SerializedProperty.EqualContents(property, endProperty))
            {
                if (SerializedProperty.EqualContents(property, descriptionField))
                    continue;

                if (property.depth > 1)
                    continue;
                
                var propertyField = new PropertyField(property);
                propertyField.AddStyle("propertyField");
                _container.PropertyBlock.Add(propertyField);
                propertyField.Bind(serializedSo);
            }
            
            serializedSo.ApplyModifiedProperties();
        }
    }
}