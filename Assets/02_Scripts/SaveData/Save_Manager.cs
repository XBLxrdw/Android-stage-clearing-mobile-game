using System.IO;
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
    public void SaveGame(SaveData saveData)
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
        Debug.Log("存档成功，路径：" + savePath);
    }
    public void LoadGame(out SaveData saveData)
    {
        saveData = new SaveData();
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("读档成功");
        }
        else
        {
            Debug.Log("没存档文件");
        }
    }
}
