using System;
using System.Collections;
using System.Collections.Generic;
using DandiDino.Core;
using DandyDino.Elements;
using UnityEditor;
using UnityEngine;

namespace DandyDino.Core
{
    public class CoreEditorWindow : EditorWindow
    {
        private static Editor _servicesEditor;
        [MenuItem(CorePaths.CORE_WINDOW)]
        public static void Init()
        {
            CoreEditorWindow window = GetWindow<CoreEditorWindow>();
            window.titleContent = new GUIContent("Core Services");
            
            RefreshAsset();
        }

        private void OnFocus()
        {
            RefreshAsset();
        }

        private static void RefreshAsset()
        {
            ServicesCollection services = AssetUtility.GetOrCreateScriptableObject<ServicesCollection>(CorePaths.ResourcesPath, CorePaths.SERVICES_COLLECTION_NAME);
            _servicesEditor = Editor.CreateEditor(services);
            _servicesEditor.CreateInspectorGUI(); 
        }

        private void OnGUI()
        {
            DDElements.Layout.Column(() =>
            {
                _servicesEditor.OnInspectorGUI();
            });
        }
    }
}