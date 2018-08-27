using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadIcons : MonoBehaviour
{
    public SpriteRenderer sr;

    void Start()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject packages = pm.Call<AndroidJavaObject>("getInstalledApplications", 0);
        int count = packages.Call<int>("size");
        string[] names = new string[count];
        int ii = 0;
        for (int i = 0; ii < count;)
        {
            AndroidJavaObject currentObject = packages.Call<AndroidJavaObject>("get", ii);
            try
            {
                names[i] = pm.Call<string>("getApplicationLabel", currentObject);
                var plugin = new AndroidJavaClass("androidhelper.brasshatstudios.androidhelper.PackageInfo");
                if (plugin.CallStatic<bool>("isSystem", currentObject))
                {
                    ii++;
                    continue;
                }
                byte[] decodedBytes = plugin.CallStatic<byte[]>("getIcon", pm, currentObject);
                Texture2D text = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                text.LoadImage(decodedBytes);
                Sprite sprite = Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(.5f, .5f));
                i++;
                ii++;
                sr.sprite = sprite;
            }
            catch (Exception e)
            {
                Debug.LogError(e, this);
                ii++;
            }

        }
    }
}
