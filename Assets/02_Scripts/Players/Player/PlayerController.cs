using UnityEngine;
/// <summary>
/// 玩家数据处理层，处理碰撞等死亡等检测。
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Player_Data data;//玩家数据层
    private float duration;//临时变量持续时间
    private void Awake()
    {
        data = GetComponent<Player_Data>();//获取玩家数据层
    }
    void Start()
    {
        
    }
    void Update()
    {
        SetMoveDir();//设置玩家移动方向
    }
    void SetMoveDir()//设置玩家转弯时移动方向
    {
        if (data.isTurn)
        {
            data.dir = Vector3.RotateTowards(data.dir, data.dir40, data.playerStaticData.turnAngle * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            duration += Time.deltaTime;
            if (duration >= 1f)
            {
                duration = 0;
                data.isTurn = false;
                data.isCorrectDirection = false;
            }
        }
        else
        {
            data.dir = Vector3.RotateTowards(data.dir, data.dir50, data.playerStaticData.turnAngle * 4f * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            if (Vector2.Dot(data.dir, data.dir50) > 0.95f && !data.isCorrectDirection)
            {
                data.moveDir = data.dir50;
                data.dir = data.moveDir;
                data.isCorrectDirection = true;
            }
        }
    }
    public void SetSco()
    {
        data.currentSco += 30;
    }
}
