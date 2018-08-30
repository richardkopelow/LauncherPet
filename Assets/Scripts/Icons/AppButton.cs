using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AndroidHelper PackageHelper;
    public App Package;
    public AppEvent Click;
    public AppEvent LongPress;
    public FloatValue LongPressThreshold;

    private bool pressed;
    private float pressStart;

    public void SetPackage(App package)
    {
        Package = package;
        GetComponent<Transform>().GetComponent<RawImage>().texture = package.Icon;
    }

    private void Update()
    {
        if (pressed)
        {
            if (Time.time - pressStart >= LongPressThreshold)
            {
                LongPress.Fire(Package);
                pressed = false;
            }
        }
    }

    public void OnClick()
    {
        if (Time.time-pressStart < LongPressThreshold)
        {
            Click.Fire(Package);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressStart = Time.time;
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
