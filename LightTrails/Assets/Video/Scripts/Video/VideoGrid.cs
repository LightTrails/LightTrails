﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class VideoGrid : MonoBehaviour
{
    public void Start()
    {
        GetComponentInChildren<VideoContainer>().Initialize();
    }
}
