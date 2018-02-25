using UnityEngine;

public class CameraScaling : AnimatedObject
{
    public bool DebugMode = false;

    public Zoom Zoom;

    private float LastUsedZoomFactor;

    public void Start()
    {
        Zoom = FindObjectOfType<Zoom>();
    }

    // Update is called once per frame
    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.D))
        {
            DebugMode = !DebugMode;
        }
#endif
        var camera = GetComponent<Camera>();

        if (DebugMode)
        {
            //FindObjectOfType<FlexableFrame>().GetComponentInChildren<Image>
            var imagePicker = FindObjectOfType<RecorderAreaPicker>();

            int width = imagePicker.Width;
            int height = imagePicker.Height;

            float widthRatio = camera.pixelWidth / (float)width;
            float heightRatio = camera.pixelHeight / (float)height;

            if (widthRatio > heightRatio)
            {
                camera.orthographicSize = camera.pixelHeight / 200.0f / heightRatio;
            }
            else
            {
                camera.orthographicSize = camera.pixelHeight / 200.0f / widthRatio;
            }

            camera.transform.position = new Vector3((imagePicker.X / 100.0f), (imagePicker.Y / 100.0f), 0);
        }
        else
        {
            if (LastUsedZoomFactor != Zoom.ZoomFactor)
            {
                var newOrthographicSize = (camera.pixelHeight / 200.0f) / Zoom.ZoomFactor;
                LastUsedZoomFactor = Zoom.ZoomFactor;
                SetOrthographicSize(newOrthographicSize);
            }

            camera.transform.position = new Vector3(0, 0, 0);
        }

        base.Update();
    }

    public void SetOrthographicSize(float newValue)
    {
        var camera = GetComponent<Camera>();

        EndAllAnimations();
        AddAnimation(new AnimatedFloat()
        {
            NewTarget = newValue,
            OldTarget = camera.orthographicSize,
            TargetUpdated = newFloatValue =>
            {
                camera.orthographicSize = newFloatValue;
            },
            UsedFunction = Animation.EaseFunction.Linear,
            Duration = 100
        });
    }
}
