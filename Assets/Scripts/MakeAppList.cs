using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAppList : MonoBehaviour
{
    public PackageHelper packageHelper;
    public RectTransform ScrollContent;
    public RectTransform AppDisplayButton;

    private void Start()
    {
        List<App> packages = packageHelper.Packages;
        Vector3 sizeDelta = ScrollContent.sizeDelta;
        sizeDelta.y = packages.Count * AppDisplayButton.sizeDelta.y;
        ScrollContent.sizeDelta = sizeDelta;
        
        for (int i = 0; i < packages.Count; i++)
        {
            var button = Instantiate(AppDisplayButton, ScrollContent);
            button.localPosition = new Vector3(0, -i * button.sizeDelta.y);
            button.GetComponent<AppButton>().SetPackage(packages[i]);
        }
    }
}
