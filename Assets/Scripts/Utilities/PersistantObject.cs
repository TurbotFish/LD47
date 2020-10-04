﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
