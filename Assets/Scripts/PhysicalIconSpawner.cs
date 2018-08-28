using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalIconSpawner : MonoBehaviour
{
    public PhysicalAppIcon IconPrefab;

    public void SpawnIcon(App app)
    {
        PhysicalAppIcon icon = Instantiate(IconPrefab);
        icon.App = app;
        icon.GetComponent<Transform>().position = GetComponent<Transform>().position;
    }
}
