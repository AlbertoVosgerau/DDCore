using System.Collections;
using System.Collections.Generic;
using DandiDino.Core;
using UnityEngine;

namespace DandyDino.Core
{
    public static class CorePaths
    {
        public static string ResourcesPath => AssetUtility.GetResourcesPath("_Game");
        
        public const string CORE_NAME = "Core";
        public const string MANAGER_CONTAINER_NAME = "MangerContainer";
        public const string SERVICES_COLLECTION_NAME = "ServicesCollection";
        
        public const string CORE_WINDOW = "Dandy Dino/Core/Open Core Window %&#c";
        
        public const string GO_CREATE_CORE_GAMEOBJECT = "GameObject/Dandy Dino/Create Core GameObject";
        public const string GO_CREATE_MANAGERS_GAMEOBJECT = "GameObject/Dandy Dino/Create Manager Container";
    }
}