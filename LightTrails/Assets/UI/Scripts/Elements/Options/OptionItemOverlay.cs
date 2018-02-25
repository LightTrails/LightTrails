using System;

public class OptionItemOverlay : AnimatedObject
{
    public void FadeOut(Action callBack)
    {
        AddAnimation(new AnimatedImageTransparency(gameObject)
        {
            NewTarget = 0.0f,
            OldTarget = 1.0f,
            Callback = callBack
        });
    }
}
