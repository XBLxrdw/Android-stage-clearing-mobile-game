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
    }
    /// <summary>关卡</summary>
    public int Level=1;
    /// <summary>种子</summary>
    public int Seed;
    /// <summary>分数</summary>
    public int Score;
    /// <summary>玩家位置</summary>
    public Transform playertransform;
    /// <summary>玩家颜色</summary>
    public Color playerColor;
    /// <summary>玩家激励状态颜色</summary>
    public Color playerStateColor;
    /// <summary>史莱姆颜色</summary>
    public Color SlimeColor;
}
