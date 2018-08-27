﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Package
{
    public AndroidJavaObject ResolveInfo;
    public AndroidJavaObject ActivityInfo;
    public AndroidJavaObject ApplicationInfo;
    public Texture2D Icon;
    public string Name;

    public Package(AndroidJavaObject resolveInfo,
        AndroidJavaObject activityInfo,
        AndroidJavaObject applicationInfo,
        Texture2D icon,
        string name)
    {
        ResolveInfo = resolveInfo;
        ActivityInfo = activityInfo;
        ApplicationInfo = applicationInfo;
        Icon = icon;
        Name = name;
    }
}
