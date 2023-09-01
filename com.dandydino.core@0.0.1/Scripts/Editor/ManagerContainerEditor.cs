using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DandyDino.Elements;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

namespace DandyDino.Core
{
    [CustomEditor(typeof(ManagerContainer))]
    public class ManagerContainerEditor : Editor
    {
        private ManagerContainer _target;
        private List<Type> _classes;
        private SerializedProperty _managers;
        private string _scene;

        private void OnEnable()
        {
            _target = (ManagerContainer)target;
            _scene = _target.gameObject.scene.name;
            _target.gameObject.name = $"{_scene} - Managers";
            _classes = ReflectionUtility.GetAllClassesOfType<Manager<CoreService>>();
        }

        public override void OnInspectorGUI()
        {
            _managers = serializedObject.FindProperty(nameof(_target.Managers));
            
            GUIStyle editorStyle = new GUIStyle();
            editorStyle.normal.background = DDElements.Helpers.GenerateColorTexture(DDElements.Colors.MidLightGray);
            
            DDElements.Layout.Column(() =>
            {
                DrawEditor();
            }, style: editorStyle);
        }

        private void DrawEditor()
        {
            serializedObject.Update();
            DrawItemHeader();
            DrawServices(serializedObject, _managers, _target.Managers);
        }

        private void DrawItemHeader()
        {
            DDElements.ReflectionUtilities.AddClassInstanceBar<IManager>(serializedObject, $"{_scene} - Managers", "Manager", _classes, _target.Managers);
        }

        private void DrawToggle(IManager manager)
        {
            DDElements.Essentials.Switch(manager.IsEnabled, value =>
            {
                manager.SetEnabled(value);
            });
        }

        private void DrawServices(SerializedObject so, SerializedProperty property, List<IManager> itemsList)
        {
            DDElements.Lists.DrawSerializedObjectsItemsList<IManager>(so, property, itemsList, DrawToggle);
        }
    }
}