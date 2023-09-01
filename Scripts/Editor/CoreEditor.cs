using System;
using System.Collections.Generic;
using System.Reflection;
using DandyDino.Elements;
using UnityEditor;
using UnityEngine;

namespace DandyDino.Core
{
    [CustomEditor(typeof(Core))]
    public class CoreEditor : Editor
    {
        private Core _target;
        private Editor _servicesEditor;

        private void OnEnable()
        {
            Core.Init();
            _target = (Core)target;
            if (Core.Main != _target)
            {
                Selection.activeObject = Core.Main.gameObject;
                if (_target != null)
                {
                    DestroyImmediate(_target.gameObject);
                }
                return;
            }
            _servicesEditor = CreateEditor(_target.Services);
            _servicesEditor.CreateInspectorGUI();
        }
        
        public override void OnInspectorGUI()
        {
            _servicesEditor.OnInspectorGUI();
        }
    }
}