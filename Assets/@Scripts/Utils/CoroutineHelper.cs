// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static MonoBehaviour _instance;
    private static Dictionary<float, WaitForSeconds> _waitForSecondsCache = new();
    public static WaitForEndOfFrame WaitForEndOfFrame { get; } = new();
    public static WaitForFixedUpdate WaitForFixedUpdate { get; } = new();
    
    public static void Init(Action callback = null)
    {
        if (_instance != null)
        {
            callback?.Invoke();
            return;
        }
            
        _instance = new GameObject($"[{nameof(CoroutineHelper)}]")
            .AddComponent<CoroutineHelper>();
        DontDestroyOnLoad(_instance.gameObject);
        callback?.Invoke();
    }

    public new static Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return _instance.StartCoroutine(coroutine);
    }

    public new static void StopCoroutine(Coroutine coroutine)
    {
        _instance.StopCoroutine(coroutine);
    }
    
    public static WaitForSeconds GetWaitForSeconds(float seconds)
    {
        if (!_waitForSecondsCache.TryGetValue(seconds, out var wait))
        {
            wait = new WaitForSeconds(seconds);
            _waitForSecondsCache[seconds] = wait;
        }
        return wait;
    }
}