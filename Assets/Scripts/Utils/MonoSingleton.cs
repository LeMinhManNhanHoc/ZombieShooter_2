using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static bool IsInitialized { get; private set; }

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }

                IsInitialized = true;
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;

            // Initialize existing instance
            InitializeSingleton();
        }
        else
        {

            // Destory duplicates
            if (Application.isPlaying)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }
    }

    protected virtual void InitializeSingleton() { }

    public virtual void ClearSingleton() { }

    public static void DestroyInstance()
    {
        if (instance == null)
        {
            return;
        }

        instance.ClearSingleton();
        instance = default(T);
    }
}
