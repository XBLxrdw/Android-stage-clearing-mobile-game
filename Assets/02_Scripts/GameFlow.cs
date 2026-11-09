using UnityEngine;
/// <summary>
/// 场景流程控制层，处理游戏流程逻辑。
/// </summary>
public class GameFlow : MonoBehaviour
{
    private static GameFlow gameFlow;
    public static GameFlow Instance
    {
        get
        {
            if (gameFlow == null)
            {
                gameFlow = FindFirstObjectByType<GameFlow>();
            }
            return gameFlow;
        }
        
    }
    private Save_Manager save;//存档管理器
    private Map_Generation map_Generation;//地图生成层
    private Camera_Management_System camera_Management_System;//摄像头管理层
    public AudioClip bgmClip;//背景音乐
    public PlayerController playerController;//玩家控制器
    private System.Collections.Generic.List<float> trapProgressPoints = new System.Collections.Generic.List<float>();
    private int nextTrapIndex = 0;
    private void Awake()
    {
        save = GetComponent<Save_Manager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        map_Generation = GameObject.FindGameObjectWithTag("Map").GetComponent<Map_Generation>();//获取地图生成层
        camera_Management_System = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Management_System>();//获取摄像头管理层
        ColorDrawer.SetRandomColor(ColorDrawer.fixedHues);//设置随机颜色
    }
    void Start()
    {
        AudioManager.instance.PlayBGM(bgmClip);//播放背景音乐
        //调用游戏开始逻辑
        Player_Data.Instance.Reset();//重置玩家数据
        Player_Data.Instance.ResetData();//重置玩家静态数据
        //读取存档
        SaveData.Instance = save.LoadGame();
        Player_Data.Instance.lastSco = SaveData.Instance.lastSco;
        Player_Data.Instance.maxDistance = SaveData.Instance.maxDistance;
        Player_Data.Instance.maxDistancePercent = SaveData.Instance.maxDistancePercent;
        UIManager.Instance.SetAllUIColor(Player_Data.Instance.color);
        //根据存档的数据创建地图，如果数据是空的，就生成新的地图。
        if (SaveData.Instance.Seed == 0)
        {
            int seed = System.Guid.NewGuid().GetHashCode();
            SaveData.Instance.Seed = seed;
            UnityEngine.Random.InitState(seed);
            map_Generation.GenerateMap(seed);
        }
        else
        {
            map_Generation.GenerateMap(SaveData.Instance.Seed);
        }
        InitTrapProgressPoints();//初始化关卡进度点
    }
    void Update()
    {
        CheckTrapProgress();
    }
    private void CheckTrapProgress()
    {
        if (trapProgressPoints.Count == 0) return;
        float progress = (float)Player_Data.Instance.totalDistance / Player_Data.Instance.total;

        while (nextTrapIndex < trapProgressPoints.Count &&
               progress >= trapProgressPoints[nextTrapIndex])
        {
            Debug.Log("生成滚石");
            map_Generation.TrapSpawn();
            nextTrapIndex++;
        }
    }
    /// <summary>
    /// 随机关卡进度点
    /// </summary>
    private void InitTrapProgressPoints()
    {
        trapProgressPoints.Clear();
        nextTrapIndex = 0;
        int count = UnityEngine.Random.Range(2, 7);
        float start = 0.20f, end = 1.0f;
        float seg = (end - start) / count;
        for (int i = 0; i < count; i++)
        {
            float lo = start + seg * i;
            float hi = lo + seg;
            trapProgressPoints.Add(UnityEngine.Random.Range(lo, hi));
        }
    }
    /// <summary>
    /// 重开游戏
    /// </summary>
    public void RestartGame()
    {
        Invoke(nameof(StartDeathCamera), 1f);
    }

    private void StartDeathCamera()//被延迟调用
    {
        camera_Management_System.PlayerDieSetCamera(OnCameraFadeOutFinished);
    }
    private void OnCameraFadeOutFinished()//摄像机结束后调用
    {
        float cur = Player_Data.Instance.totalDistance / Player_Data.Instance.total;
        if (cur > Player_Data.Instance.maxDistancePercent) Player_Data.Instance.maxDistancePercent = cur;
        if (Player_Data.Instance.totalDistance > Player_Data.Instance.maxDistance)
            Player_Data.Instance.maxDistance = Player_Data.Instance.totalDistance;
        if (Player_Data.Instance.lastSco < Player_Data.Instance.currentSco)
            Player_Data.Instance.lastSco = Player_Data.Instance.currentSco;
        //重新生成种子
        GenerateNewSeed();
        InitTrapProgressPoints();//初始化关卡进度点
        playerController.SetTrailStatus();//设置拖尾状态
        UIManager.Instance.ShowGameplayHint();//显示游戏提示面板
        Player_Data.Instance.currentSco = 0;//重置玩家分数
        Player_Data.Instance.transform.position = new Vector3(0,3,0);//玩家回到起点
        Player_Data.Instance.isOneLife = true;//玩家一命重置
        Player_Data.Instance.distance = 0;
        UIManager.Instance.progressBar.value = 0f;//当前路程进度条归零，避免残留上一局的值
        Player_Data.Instance.Reset();//重置玩家数据
        Player_Data.Instance.ResetData();
        Player_Data.Instance.isDead = false;
        //存档
        save.SaveGame(SaveData.Instance);
    }
    /// <summary>
    /// 一命复活
    /// </summary>
    public void OneRevive()
    {
        playerController.SetTrailStatus();//设置拖尾状态
        UIManager.Instance.CloseReviveAdPanel();//关闭复活面板
        Player_Data.Instance.transform.position = new Vector3(0, Player_Data.Instance.transform.position.y, Player_Data.Instance.transform.position.z);//玩家回到起点
        Player_Data.Instance.Reset();
        //玩家最远距离和路程。
        Player_Data.Instance.isDead = false;
        //存档
        save.SaveGame(SaveData.Instance);
    }
    /// <summary>
    /// 下一关
    /// </summary>
    public void NextLevel()
    {
        //调用下一关逻辑
        camera_Management_System.PlayerDieSetCamera(OnCameraFadeOutFinished2);
    }
    private void OnCameraFadeOutFinished2()
    {
        GenerateNewSeed();
        InitTrapProgressPoints();//初始化关卡进度点
        playerController.SetTrailStatus();//设置拖尾状态
        ColorDrawer.SetRandomColor(ColorDrawer.fixedHues);//设置随机颜色
        UIManager.Instance.SetAllUIColor(Player_Data.Instance.color);
        UIManager.Instance.ShowGameplayHint();//显示游戏提示面板
        Player_Data.Instance.transform.position = new Vector3(0, 3, 0);//玩家回到起点
        Player_Data.Instance.isDead = false;
        Player_Data.Instance.ResetData();
        Player_Data.Instance.Reset();
        Player_Data.Instance.lastSco = 0;
        Player_Data.Instance.maxDistance = 0;
        Player_Data.Instance.maxDistancePercent = 0;
        SaveData.Instance.Level++;
        //存档
        save.SaveGame(SaveData.Instance);
    }
    /// <summary>
    /// 生成新的种子以及地图
    /// <summary>
    public void GenerateNewSeed()
    {
        int seed = System.Guid.NewGuid().GetHashCode();
        SaveData.Instance.Seed = seed;
        UnityEngine.Random.InitState(seed);
        map_Generation.GenerateMap(seed);
    }
}
