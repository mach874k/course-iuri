﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePooler : MonoBehaviour
{
    public string key = string.Empty;
    public GameObject prefab = null;
    public int prepopulate = 0;
    public int maxCount = int.MaxValue;
    public bool autoRegister = true;
    public bool autoClear = true;
    public bool isRegistered { get; private set; }

    protected virtual void Awake ()
    {
        if (autoRegister)
            Register();
    }
    protected virtual void OnDestroy ()
    {
        EnqueueAll();
        if (autoClear)
            UnRegister();
    }
    protected virtual void OnApplicationQuit()
    {
        EnqueueAll();
    }

    public void Register ()
    {
        if (string.IsNullOrEmpty(key))
            key = prefab.name;
        GameObjectPoolController.AddEntry(key, prefab, prepopulate, maxCount);
        isRegistered = true;
    }

    public void UnRegister ()
    {
        GameObjectPoolController.ClearEntry(key);
        isRegistered = false;
    }

    public Action<Poolable> willEnqueue;
    public Action<Poolable> didDequeue;

    public virtual void Enqueue(Poolable item)
    {
        if(willEnqueue != null)
            willEnqueue(item);
        GameObjectPoolController.Enqueue(item);
    }

    public virtual void EnqueueObject (GameObject obj)
    {
        Poolable item = obj.GetComponent<Poolable>();
        if (item != null)
            Enqueue (item);
    }
    public virtual void EnqueueScript (MonoBehaviour script)
    {
        Poolable item = script.GetComponent<Poolable>();
        if (item != null)
            Enqueue (item);
    }

    public virtual Poolable Dequeue ()
    {
        Poolable item = GameObjectPoolController.Dequeue(key);
        if (didDequeue != null)
            didDequeue(item);
        return item;
    }

    public virtual U DequeueScript<U> () where U : MonoBehaviour
    {
        Poolable item = Dequeue();
        return item.GetComponent<U>();
    }

    public abstract void EnqueueAll ();
}
