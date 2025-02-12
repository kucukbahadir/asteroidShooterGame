using System;
using UnityEngine;

[DisallowMultipleComponent]
public class MonoBehaviourSingleton<T> : MonoBehaviour
{
    private static T Instance;
    
    public virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("The PlayerInputHandler already exist in the scene " + gameObject.name);
            Destroy(gameObject);
        }
        
        Instance = gameObject.GetComponent<T>();
    }
}
