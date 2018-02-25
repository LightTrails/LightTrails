using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AnimationWait : Animation
{
    public AnimationWait(float waitTime)
    {
        Duration = waitTime;
    }

    public override void ProgressUpdated(float newProgress)
    {

    }
}

