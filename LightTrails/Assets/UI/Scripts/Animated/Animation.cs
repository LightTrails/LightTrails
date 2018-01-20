using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Animation
{
    public float Duration = 260.0f;
    public float Progress = 0;

    public bool IsDone;

    public abstract void RunProgress(float deltaTime, GameObject gameObject);

    public Animation SetStartState(GameObject gameObject)
    {
        RunProgress(0, gameObject);
        return this;
    }

    public Animation SetEndState(GameObject gameObject)
    {
        Progress = 1;
        RunProgress(0, gameObject);
        return this;
    }
}
