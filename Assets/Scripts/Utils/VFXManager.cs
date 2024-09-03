using ProjectTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoSingleton<VFXManager>
{ 
    [SerializeField] SerializableDictionary<string, ObjectPool> vfxDictionary;

    private void Start()
    {
        foreach (var item in vfxDictionary)
        {
            item.Value.InitPool();
        }
    }

    public ParticleSystem GetVFXPool(string id)
    {
        ObjectPool objPool = vfxDictionary[id];
        return objPool.GetPooledObject().GetComponent<ParticleSystem>();
    }
}
