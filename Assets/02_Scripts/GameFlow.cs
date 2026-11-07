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
                gameFlow = new GameFlow();
            }
            return gameFlow;
        }
        
    }
    private Player_Data data;//玩家数据层
    private Map_Generation map_Generation;//地图生成层
    private void Awake()
    {
        data = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Data>();//获取玩家数据层
        map_Generation = GameObject.FindGameObjectWithTag("Map").GetComponent<Map_Generation>();//获取地图生成层
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
        if (data.isDead)
        {
            //存档
            //调用死亡动画
            //检测是否是一命
            if(data.isOneLife)
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
        //调用摄像头遮罩
        //摄像头向上移动
        //如果遮罩拉满
        //重置玩家位置到起点
        //重置一命
        //重置分数
    }
}
