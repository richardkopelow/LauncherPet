using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PhysicalIconDataCollection
{
    public PhysicalIconData[] Datas;
}

[Serializable]
public class PhysicalIconData
{
    public string PackageName;
    public Vector3 Position;
    public Quaternion Rotation;

    public PhysicalIconData(string packageName, Vector3 position, Quaternion rotation)
    {
        PackageName = packageName;
        Position = position;
        Rotation = rotation;
    }
}
