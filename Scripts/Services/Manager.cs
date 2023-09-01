using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DandyDino.Core
{
    [Serializable]
    public abstract class Manager<T> : IManager where T : CoreService
    {
        public T Service => _service ??= Core.Main.GetService<T>();
        private T _service;
        public Action<IService> onInitialize { get; }
        public Action<IService> onEnable { get; }
        public Action<IService> onDisable { get; }
        public Action<IService> onDestroy { get; }
        public bool IsEnabled => _isEnabled;
        [HideInInspector] [SerializeField] protected bool _isEnabled = true;
        public virtual void Init()
        {
            onInitialize?.Invoke(this);
        }
        

        public virtual void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            if (!Application.isPlaying)
            {
                return;
            }
            if (isEnabled)
            {
                OnEnable();
                return;
            }
            OnDisable();
        }

        public virtual void OnEnable()
        {
            onEnable?.Invoke(this);
        }

        public virtual void OnDisable()
        {
            onDisable?.Invoke(this);
        }

        public virtual void OnDestroy()
        {
            onDestroy?.Invoke(this);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Destroy()
        {
            
        }
    }
}