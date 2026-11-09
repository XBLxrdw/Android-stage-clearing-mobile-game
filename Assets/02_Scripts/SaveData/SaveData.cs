using System;
using UnityEngine;
/// <summary>
/// 存档类
/// </summary>
[Serializable]
public class SaveData
{
    private static SaveData saveData;
    public static SaveData Instance
    {
        get
        {
            if (saveData == null)
            {
                saveData = new SaveData();
            }
            return saveData;
        }
        set
        {
            saveData = value;
        }
    }
    /// <summary>关卡</summary>
    public int Level=1;
    /// <summary>种子</summary>
    public int Seed;
    /// <summary>玩家位置</summary>
    public Transform playertransform;
    /// <summary>玩家颜色</summary>
    public Color playerColor;
    /// <summary>玩家激励状态颜色</summary>
    public Color playerStateColor;
    /// <summary>史莱姆颜色</summary>
    public Color SlimeColor;
    /// <summary>历史最高分数</summary>
    public int lastSco;

    /// <summary>最远抵达距离</summary>
    public float maxDistance;
    /// <summary>最远抵达百分比</summary>
    public float maxDistancePercent;
}
