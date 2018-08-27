using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppButton : MonoBehaviour
{
    public PackageHelper PackageHelper;
    public Package Package;


    public void StartApp()
    {
        PackageHelper.StartApplication(Package);
    }

    public void SetPackage(Package package)
    {
        Package = package;
        GetComponent<Transform>().GetComponent<RawImage>().texture = package.Icon;
    }
}
