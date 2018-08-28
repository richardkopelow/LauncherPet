using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAppIcon : MonoBehaviour
{
    public AppEvent StartApp;
    public App App;

    private void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material iconMat = new Material(renderer.material);
        iconMat.SetTexture("_MainTex", App.Icon);
        renderer.material = iconMat;
    }

    private void OnMouseUpAsButton()
    {
        StartApp.Fire(App);
    }
}
