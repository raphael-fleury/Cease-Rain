﻿using System;
using UnityEngine;

[RequireComponent(typeof(Steam))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] GameObject colliders;

    private Action onCheckpointEvent;

    public event Action OnCheckpointEvent
    {
        add { onCheckpointEvent += value; }
        remove { onCheckpointEvent -= value; }
    }

    private void Start()
    {
        GetComponent<Steam>().OnEnableEvent += UpdateCheckpoint;

        if (Level.checkpoint == index)
        {           
            Level.marjory.transform.position = transform.position;

            if (colliders)
                colliders.SetActive(true);

            Level.activeCamera.GetComponent<PlayerCamera>().limits.lower = transform.position.x;
        }
    }

    private void UpdateCheckpoint()
    {
        if (index <= Level.checkpoint)
            return;

        Level.checkpoint = index;
        onCheckpointEvent?.Invoke();
    }
}
