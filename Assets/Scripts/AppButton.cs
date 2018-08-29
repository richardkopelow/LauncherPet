using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppButton : MonoBehaviour
{
    public AndroidHelper PackageHelper;
    public App Package;
    public AppEvent Event;

    public void SetPackage(App package)
    {
        Package = package;
        GetComponent<Transform>().GetComponent<RawImage>().texture = package.Icon;
    }

    public void OnClick()
    {
        Event.Fire(Package);
    }
}
