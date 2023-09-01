using System;
using System.Collections.Generic;
using System.Linq;
using DandiDino.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DandyDino.Core
{
    public class Core : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod] 
        public static void Init()
        { 
            if (_main == null)
            {
                List<Core> cores = FindObjectsOfType<Core>().ToList();
                if (cores.Count > 0)
                {
                    _main = cores[0];
                    for (int i = 1; i < cores.Count; i++)
                    {
                        if (Application.isPlaying)
                        {
                            Destroy(cores[i].gameObject);
                        }
                        else
                        {
                            DestroyImmediate(cores[i].gameObject);
                        }
                    }
                }
                
                if (_main == null)
                {
                    GameObject newObject = new GameObject(CorePaths.CORE_NAME);
                    _main = newObject.AddComponent<Core>();
                    if (Application.isPlaying)
                    {
                        DontDestroyOnLoad(newObject);
                    }
                }
            }
            
            _main.InitializeServices();
        }

        public static Core Main => _main;
        private static Core _main;

        public ServicesCollection Services => _services;
        [SerializeReference] private ServicesCollection _services;

        public List<ManagerContainer> Managers => _managers;
        [SerializeReference] private List<ManagerContainer> _managers = new List<ManagerContainer>();

        private void InitializeServices()
        {
            _services = AssetUtility.GetOrCreateScriptableObject<ServicesCollection>(CorePaths.ResourcesPath, CorePaths.SERVICES_COLLECTION_NAME);
        }

        public List<CoreService> GetAllServices()
        {
            return Services.CoreServices;
        }

        public List<CoreService> GetAllActiveServices()
        {
            return Services.CoreServices.Where(x => x.IsEnabled).ToList();
        }

        public T GetService <T>(bool forceEnable = false) where T : CoreService
        {
            T service = Services.CoreServices.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
            if (service == null)
            {
                return null;
            }
            
            if (!service.IsEnabled)
            {
                if (forceEnable)
                {
                    service.SetEnabled(true);
                    return service;
                }
                Debug.LogWarning($"Service of type {typeof(T).Name} exists but it is not enabled");
                return null;
            }

            return service;
        }
        
        public T GetManager<T>(bool forceEnabled = false) where T : Manager<CoreService>
        {
            for (int i = 0; i < Managers.Count; i++)
            {
                ManagerContainer container = Managers[i];
                T manager = container.GetManager<T>(forceEnabled);
                if (manager != null)
                {
                    return manager;
                }
            }

            return null;
        }
        
        public T GetManagerOnScene<T>(Scene scene,bool forceEnabled = false) where T : Manager<CoreService>
        {
            for (int i = 0; i < Managers.Count; i++)
            {
                ManagerContainer container = Managers[i];
                if (container.gameObject.scene != scene)
                {
                    continue;
                }
                
                T manager = container.GetManager<T>(forceEnabled);
                if (manager != null)
                {
                    return manager;
                }
            }

            return null;
        }

        public List<T> GetManagers<T>(bool forceEnabled = false) where T : Manager<CoreService>
        {
            List<T> managers = new List<T>();
            for (int i = 0; i < Managers.Count; i++)
            {
                ManagerContainer container = Managers[i];
                T manager = container.GetManager<T>(forceEnabled);
                if (manager != null)
                {
                    managers.Add(manager);
                }
            }

            return managers;
        }

        public void RegisterManagerContainer(ManagerContainer managerContainer)
        {
            if (_managers.Contains(managerContainer))
            {
                return;
            }
            _managers.Add(managerContainer);
        }

        public void UnRegisterManagerContainer(ManagerContainer managerContainer)
        {
            if (!_managers.Contains(managerContainer))
            {
                return;
            }
            _managers.Remove(managerContainer);
        }

        private void OnEnable()
        {
            Init();
            if (Main != this)
            {
                Destroy(gameObject);
                return;
            }
            InitializeServices();
        }

        private void OnDisable()
        {
            foreach (CoreService service in Services.CoreServices)
            {
                service.OnDisable();
            }
        }

        private void OnDestroy()
        {
            foreach (CoreService service in Services.CoreServices)
            {
                service.OnDestroy();
            }
        }

        private void Update()
        {
            foreach (CoreService service in Services.CoreServices)
            {
                service.Update();
            }
        }
    }
}