using ScriptableObjectFramework.Sets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class PhysicalIconManager : MonoBehaviour
{
    public AndroidHelper Interop;
    public GameObjectRuntimeSet IconSet;
    public PhysicalAppIcon IconPrefab;
    
    private string jsonPath;

    private void OnEnable()
    {
        jsonPath = Application.persistentDataPath + "/RoomSetup.json";
        Deserialize();
    }

    public void Serialize()
    {
        int i = 0;
        PhysicalIconDataCollection iconData = new PhysicalIconDataCollection();
        iconData.Datas = new PhysicalIconData[IconSet.Count];
        foreach (GameObject iconGO in IconSet)
        {
            var iconTrans = iconGO.GetComponent<Transform>();
            var iconAppIcon = iconGO.GetComponent<PhysicalAppIcon>();

            iconData.Datas[i] = new PhysicalIconData(
                iconAppIcon.App.PackageName,
                iconTrans.position,
                iconTrans.rotation
                );
            i++;
        }
        string json = JsonUtility.ToJson(iconData);
        File.WriteAllText(jsonPath, json);
    }

    public void Deserialize()
    {
        if (!File.Exists(jsonPath))
        {
            return;
        }

        string json = File.ReadAllText(jsonPath);
        PhysicalIconDataCollection iconData = JsonUtility.FromJson<PhysicalIconDataCollection>(json);
        List<App> apps = Interop.Apps;

        foreach (var data in iconData.Datas)
        {
            App app = apps.First(a => a.PackageName == data.PackageName);
            if (app != null)
            {
                PhysicalAppIcon icon = Instantiate(IconPrefab);
                icon.App = app;
                Transform iconTrans = icon.GetComponent<Transform>();
                iconTrans.position = data.Position;
                iconTrans.rotation = data.Rotation;
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Serialize();
        }
    }
}
