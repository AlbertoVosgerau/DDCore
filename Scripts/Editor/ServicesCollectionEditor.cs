using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DandyDino.Elements;
using UnityEditor;
using UnityEngine;

namespace DandyDino.Core
{
    [CustomEditor(typeof(ServicesCollection))]
    public class ServicesCollectionEditor : Editor
    {
        private ServicesCollection _target;
        private List<Type> _classes;
        private SerializedProperty _coreServices;

        private int _selectedIndex = 0;

        private void OnEnable()
        {
            _target = (ServicesCollection)target;
            _classes = ReflectionUtility.GetAllClassesOfType<CoreService>();
        }

        public override void OnInspectorGUI()
        {
            _coreServices = serializedObject.FindProperty(nameof(_target.CoreServices));
            
            GUIStyle editorStyle = new GUIStyle();
            editorStyle.normal.background = DDElements.Helpers.GenerateColorTexture(DDElements.Colors.MidDarkGray);
            
            DDElements.Layout.Column(() =>
            {
                DrawEditor();
            }, style: editorStyle);
        }

        private void DrawEditor()
        {
            DrawItemHeader();
            DrawServices(serializedObject, _coreServices, _target.CoreServices);
        }

        private void DrawItemHeader()
        {
            DDElements.ReflectionUtilities.AddClassInstanceBar<CoreService>(serializedObject, "Services", "Service", _classes, _target.CoreServices);
        }
        
        private void DrawToggle(CoreService service)
        {
            DDElements.Essentials.Switch(service.IsEnabled, value =>
            {
                service.SetEnabled(value);
            });
        }

        private void DrawServices(SerializedObject so, SerializedProperty property, List<CoreService> itemsList)
        {
            DDElements.Lists.DrawSerializedObjectsItemsList<CoreService>(so, property, itemsList, DrawToggle);
        }
    }
}