using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DandyDino.Core;
using UnityEngine;

public class ManagerContainer : MonoBehaviour
{
    [SerializeReference] public List<IManager> Managers = new List<IManager>();

    public void Init()
    {
        for (int i = 0; i < Managers.Count; i++)
        {
            IManager manager = Managers[i];
            manager.Init();
        }
#if UNITY_EDITOR
        if (Application.isPlaying && Core.Main == null)
        {
            Core.Init();
        }
#endif
    }
    
    public T GetManager <T>(bool forceEnabled = false) where T : Manager<CoreService>
    {
        T manager = Managers.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        
        if (manager != null)
        {
            if (!manager.IsEnabled)
            {
                if (forceEnabled)
                {
                    manager.SetEnabled(true);
                    return manager;
                }
                Debug.LogWarning($"Manager of type {typeof(T).Name} exists but it is not enabled");
                return null;
            }
            return manager;
        }

        return null;
    }

    private void RegisterSelf()
    {
        Core.Main.RegisterManagerContainer(this);
    }

    private void UnRegisterSelf()
    {
        Core.Main.UnRegisterManagerContainer(this);
    }

    private void OnEnable()
    {
        Init();
        RegisterSelf();
    }

    private void OnDisable()
    {
        UnRegisterSelf();
    }
}
