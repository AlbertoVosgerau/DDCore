using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DandyDino.Core
{
    public class CreateGameObjectsMenus
    {
        // Should we allow the creation of a GameObjet? So far we are keeping only the Editor Window
        //[MenuItem(CorePaths.GO_CREATE_CORE_GAMEOBJECT)]
        public static void CreateCore()
        {
            Core.Init();
            Core.Main.transform.SetSiblingIndex(0);
            Selection.activeObject = Core.Main;
        }

        [MenuItem(CorePaths.GO_CREATE_MANAGERS_GAMEOBJECT)]
        public static void CreateManagersContainer()
        {
            ManagerContainer existingContainer = Object.FindObjectOfType<ManagerContainer>();
            if (existingContainer != null)
            {
                Selection.activeObject = existingContainer;
                return;
            }

            GameObject newObject = new GameObject(CorePaths.MANAGER_CONTAINER_NAME);
            newObject.name = $"{newObject.scene.name} - Managers";
            newObject.transform.SetSiblingIndex(0);
            if (Core.Main != null)
            {
                Core.Main.transform.SetSiblingIndex(0);
            }
            
            Selection.activeObject = newObject;

            newObject.AddComponent<ManagerContainer>();
        }
    }
}