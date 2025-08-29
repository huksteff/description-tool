using System;
using Editor.DescriptionsTool.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DescriptionsTool
{
    public class DescriptionCatalogToolWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
        private DescriptionCatalogContext _context;
        private DescriptionCatalogToolContainer _container;
        private DescriptionCatalogToolController _controller;

        [MenuItem("Tools/Description Catalog")]
        public static void ShowExample()
        {
            DescriptionCatalogToolWindow window = GetWindow<DescriptionCatalogToolWindow>();
            window.titleContent = new GUIContent("Description Catalog");
            window.minSize = new Vector2(650, 500);
        }

        public void CreateGUI()
        {
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/DescriptionsTool/Styles/GeneralWindowStyle.uss");
            rootVisualElement.styleSheets.Add(uss);

            _context = new DescriptionCatalogContext();
            _container = new DescriptionCatalogToolContainer();
            _controller = new DescriptionCatalogToolController(_context, _container);
            
            rootVisualElement.Add(_container.Root);
            _controller.Init();
        }

        public void OnDisable()
        {
            _controller?.Dispose();
            _controller = null;
            _container = null;
            _context = null;
        }
    }
}