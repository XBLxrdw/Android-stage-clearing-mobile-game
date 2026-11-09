using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class Save_Manager : MonoBehaviour
{
    string savePath
    {
        get
        {
            return Application.persistentDataPath + "/save.json";
        }
    }
    /// <summary>
    /// 存档
    /// </summary>
    /// <param 存档类="saveData"></param>
    public void SaveGame(SaveData saveData)
    {
        saveData.playerColor = Player_Data.Instance.color;
        saveData.playerStateColor = Player_Data.Instance.color3;
        saveData.SlimeColor = Player_Data.Instance.color2;
        if (Player_Data.Instance.lastSco < Player_Data.Instance.currentSco)
        {
            Player_Data.Instance.lastSco = Player_Data.Instance.currentSco;
        }
        saveData.lastSco = Player_Data.Instance.lastSco;
        if(Player_Data.Instance.totalDistance > Player_Data.Instance.maxDistance)
        {
            Player_Data.Instance.maxDistance = Player_Data.Instance.totalDistance;
        }
        saveData.maxDistance = Player_Data.Instance.maxDistance;
        saveData.maxDistancePercent = Player_Data.Instance.maxDistancePercent;
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        Debug.Log("存档成功，路径：" + savePath);
    }
    /// <summary>
    /// 读档
    /// </summary>
    /// <param 存档类="saveData"></param>
    public SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Debug.Log("读档成功");
            // 将 JSON 转成 SaveData 对象并返回
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.Log("没存档文件，返回全新默认值");
            return new SaveData();
        }
    }
}
