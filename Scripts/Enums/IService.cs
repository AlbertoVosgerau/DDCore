using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DandyDino.Core
{
    public interface IService : ITogglable
    {
        public Action<IService> onInitialize { get; }
        public Action<IService> onEnable { get; }
        public Action<IService> onDisable { get; }
        public Action<IService> onDestroy { get; }
        public void Init();
        public void OnEnable();
        public void OnDisable();
        public void OnDestroy();
        public void Update();
        public void Destroy();
    }
}