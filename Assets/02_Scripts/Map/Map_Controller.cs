using UnityEditor;
using UnityEngine;
/// <summary>
/// 地图控制器
/// </summary>
[CustomEditor(typeof(Map_Generation))]
public class Map_Controller : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("生成地图"))
        {
            ((Map_Generation)target).GenerateMap();
        }
        if (GUILayout.Button("删除地图"))
        {
            ((Map_Generation)target).DeleteMap();
        }
    }
}
