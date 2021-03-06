﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class AndroidHelper : ScriptableObject
{
    private AndroidJavaClass plugin;
    private AndroidJavaObject currentActivity;
    private AndroidJavaObject packageManager;

    public List<App> Apps
    {
        get
        {
            List<App> packages = new List<App>();
            AndroidJavaObject packagesNative = plugin.CallStatic<AndroidJavaObject>("installedApps", packageManager);//packageManager.Call<AndroidJavaObject>("getInstalledApplications", 0);
            int count = packagesNative.Call<int>("size");

            for (int i = 0; i < count; i++)
            {
                AndroidJavaObject currentObject = packagesNative.Call<AndroidJavaObject>("get", i);
                AndroidJavaObject activityInfo = currentObject.Get<AndroidJavaObject>("activityInfo");
                AndroidJavaObject appInfo = activityInfo.Get<AndroidJavaObject>("applicationInfo");
                try
                {
                    byte[] decodedBytes = plugin.CallStatic<byte[]>("getIcon", packageManager, currentObject);
                    Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                    texture.LoadImage(decodedBytes);

                    packages.Add(new App(
                        currentObject,
                        activityInfo,
                        appInfo,
                        texture,
                        packageManager.Call<string>("getApplicationLabel", appInfo)
                        ));
                }
                catch (Exception e)
                {
                    Debug.LogError(e, this);
                }
            }

            return packages.OrderBy(app => app.Name).ToList();
        }
    }

    public Texture2D Wallpaper
    {
        get
        {
            byte[] wallpaperBytes = plugin.CallStatic<byte[]>("getWallpaper", currentActivity.Call<AndroidJavaObject>("getApplicationContext"));
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.LoadImage(wallpaperBytes);
            return texture;
        }
    }

    private void OnEnable()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        plugin = new AndroidJavaClass("androidhelper.brasshatstudios.androidhelper.PackageInfo");
#endif
    }

    public void StartApplication(App package)
    {
        AndroidJavaObject launchIntent = plugin.CallStatic<AndroidJavaObject>("getIntent", package.ActivityInfo);
        currentActivity.Call("startActivity", launchIntent);
    }
}
