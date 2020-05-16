﻿using System;
using UnityEngine;

public static class Level
{
    private static int _checkpoint;

    #region Properties
    public static Marjory marjory { get; set; }
    public static GameObject activeCamera { get; set; }
    public static int checkpoint
    { 
        get { return _checkpoint; }
        set
        {
            if (value > 0)
                onCheckpointEvent?.Invoke();

            _checkpoint = value;
        }
    }
    #endregion

    #region Events
    private static Action onCheckpointEvent;

    public static event Action OnCheckpointEvent
    {
        add { onCheckpointEvent += value; }
        remove { onCheckpointEvent -= value; }
    }
    #endregion
}
