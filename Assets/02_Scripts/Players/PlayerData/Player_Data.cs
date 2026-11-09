using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 玩家数据层，存储玩家的所有数据
/// </summary>
public class Player_Data : MonoBehaviour
{
    private static Player_Data player;
    public static Player_Data Instance
    {
        get
        {
            if (player == null)
            {
                player = FindFirstObjectByType<Player_Data>();
            }
            return player;
        }
    }
    [Tooltip("玩家位置")] public Transform playerTransform;
    [Tooltip("玩家当前分数")] public int currentSco;
    [Tooltip("玩家上一次的分数")] public int lastSco;
    [Tooltip("玩家激励分数")] public int boostSco;
    [Tooltip("玩家5s连续激励分数")] public int boost5Sco;
    [Tooltip("玩家起点")] public Vector3 startPoint;
    [Tooltip("玩家移动路程(曲线)")] public float distance;
    [Tooltip("玩家总路程（直线）")] public float totalDistance;
    [Tooltip("总路程")] public float total;
    [Tooltip("玩家最远抵达路程")] public float maxDistance;
    [Tooltip("玩家最远抵达百分比")]public float maxDistancePercent;
    [Tooltip("玩家移动速度")] public float speed;
    [Tooltip("玩家动画状态")] public Animator animator;
    [Tooltip("玩家静态数据")] public Player_Static_Data playerStaticData;
    [Tooltip("玩家刚体组件")] public Rigidbody2D rigidbody;
    [Tooltip("玩家固定移动方向")] public Vector2 moveDir;
    [Tooltip("玩家当前方向")] public Vector2 dir;
    [Tooltip("玩家目标方向")] public Vector2 dir40;
    [Tooltip("玩家目标方向")] public Vector2 dir50;
    [Tooltip("玩家颜色")] public Color color;
    [Tooltip("史莱姆颜色")] public Color color2;
    [Tooltip("玩家激励颜色")] public Color color3;
    [Tooltip("玩家是否是一命")] public bool isOneLife;
    [Tooltip("玩家是否开始输入")] public bool isStartInput;
    [Tooltip("玩家是否停止输入")] public bool isStopInput;
    [Tooltip("玩家是否移动")] public bool isMove;
    [Tooltip("玩家是否在转弯")] public bool isTurn;
    [Tooltip("玩家是否正确朝向")] public bool isCorrectDirection;
    [Tooltip("玩家是否死亡")] public bool isDead;
    [Tooltip("玩家是否处于无敌状态")] public bool isInvincible;
    [Tooltip("玩家是否是激励状态")] public bool isBoost;
    [Tooltip("玩家是否胜利")] public bool isWin;
    [Tooltip("玩家死亡特效")] public Object die;
    private void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        Player_Data.Instance.transform.localScale = new Vector3(Mathf.Abs(Player_Data.Instance.transform.localScale.x), Player_Data.Instance.transform.localScale.y, Player_Data.Instance.transform.localScale.z);
        moveDir = Quaternion.Euler(0, 0, -50) * Vector2.right;
        dir40 = Quaternion.Euler(0, 0, -40) * Vector2.right;
        dir = dir50 = moveDir;
        isCorrectDirection = true;
    }
    public void ResetData()
    {
        startPoint = playerTransform.position;
        currentSco = 0;
        boostSco = 0;
        boost5Sco = 0;
        distance = 0;
        totalDistance = 0;
        isOneLife = true;
        isMove = false;
    }
}
