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
    private Map_Generation map_Generation;//地图生成层
    private Camera_Management_System camera_Management_System;//摄像头管理层
    private void Awake()
    {
        map_Generation = GameObject.FindGameObjectWithTag("Map").GetComponent<Map_Generation>();//获取地图生成层
        camera_Management_System = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Management_System>();//获取摄像头管理层
    }
    void Start()
    {
        //调用游戏开始逻辑
        //读取存档
        //根据存档的数据创建地图，如果数据是空的，就生成新的地图。
        if(SaveData.Instance.Seed == 0)
        {
            int seed = Time.time.GetHashCode();
            UnityEngine.Random.InitState(seed);
            SaveData.Instance.Seed = seed;
            map_Generation.GenerateMap(seed);
        }
        else
        {
            map_Generation.GenerateMap(SaveData.Instance.Seed);
        }
    }
    void Update()
    {
        //检测玩家死亡执行死亡逻辑
        if (Player_Data.Instance.isDead)
        {
            //存档
            //调用死亡动画
            //检测是否是一命
            if(Player_Data.Instance.isOneLife)
            {
                //调用复活逻辑
                //调出UI界面
                //看广告复活
            }
            else
            {
                //调用游戏结束逻辑
            }
        }
        //调用地图生成逻辑
        //调用滚石生成逻辑
        //调用存档逻辑
        //检测玩家胜利逻辑
        //检测玩家失败逻辑
        //调用广告
    }
    /// <summary>
    /// 重开游戏
    /// </summary>
    public void RestartGame()
    {
        Invoke(nameof(StartDeathCamera), 1f);
    }

    private void StartDeathCamera()
    {
        camera_Management_System.PlayerDieSetCamera(OnCameraFadeOutFinished);
    }
    private void OnCameraFadeOutFinished()
    {
        UIManager.Instance.ShowGameplayHint();
        Player_Data.Instance.transform.position = new Vector3(0,3,0);
        Player_Data.Instance.isOneLife = true;
        Player_Data.Instance.lastSco = Player_Data.Instance.currentSco;
        Player_Data.Instance.currentSco = 0;
        //玩家最远距离和路程。
        Player_Data.Instance.Reset();
        Player_Data.Instance.isDead = false;
    }
    /// <summary>
    /// 一命复活
    /// </summary>
    public void OneRevive()
    {
        UIManager.Instance.CloseReviveAdPanel();
        Player_Data.Instance.transform.position = new Vector3(0, Player_Data.Instance.transform.position.y, Player_Data.Instance.transform.position.z);
        Player_Data.Instance.lastSco = Player_Data.Instance.currentSco;
        Player_Data.Instance.currentSco = 0;
        //玩家最远距离和路程。
        Player_Data.Instance.Reset();
        Player_Data.Instance.isDead = false;
    }
}
