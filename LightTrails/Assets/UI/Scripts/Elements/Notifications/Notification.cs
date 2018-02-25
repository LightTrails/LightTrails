using System;
using UnityEngine;

public class Notification : AnimatedObject
{
    private Action _callBack;

    public void PlaySaveNotification(Action callBack)
    {
        EndAllAnimations();
        PlayAnimationsInSequence(new AnimatedMovement(gameObject)
        {
            NewTarget = new Vector2(-50, 50),
            OldTarget = new Vector2(-50, -135),
        },
        new AnimationWait(2000),
        new AnimatedMovement(gameObject)
        {
            NewTarget = new Vector2(400, 50),
            OldTarget = new Vector2(-50, 50),
            Callback = callBack
        });
    }

    public void NotificationDone()
    {
        if (_callBack != null)
        {
            _callBack();
            _callBack = null;
        }
    }

    private void Update()
    {
        base.Update();
    }
}
