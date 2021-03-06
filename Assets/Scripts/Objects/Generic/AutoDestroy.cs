﻿using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float timeToDestroy;

    protected virtual void Start()
    {
        if (timeToDestroy > 0)
            Invoke("Destroy", timeToDestroy);
    }

    public virtual void Destroy() { Destroy(gameObject); }
}
