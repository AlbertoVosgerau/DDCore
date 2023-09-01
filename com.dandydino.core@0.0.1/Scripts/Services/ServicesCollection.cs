using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DandyDino.Core
{
    [CreateAssetMenu(order = 0, fileName = CorePaths.SERVICES_COLLECTION_NAME, menuName = CorePaths.SERVICES_COLLECTION_NAME)]
    public class ServicesCollection : ScriptableObject
    {
        [SerializeReference] public List<CoreService> CoreServices = new List<CoreService>();
        
        public void AddService(CoreService service)
        {
            if (CoreServices.Contains(service))
            {
                return;
            }
            CoreServices.Add(service);
        }

        public void RemoveService(CoreService service)
        {
            if (!CoreServices.Contains(service))
            {
                return;
            }
            CoreServices.Remove(service);
        }
    }
}