using Unity.Android.Gradle.Manifest;
using UnityEngine;
/// <summary>
/// 玩家数据处理层，处理碰撞等死亡等检测。
/// </summary>
public class PlayerController : MonoBehaviour
{
    private float duration;//临时变量持续时间
    private float timesco;//临时变量时间分数
    private void Awake()
    {
    }
    void Start()
    {
        
    }
    void Update()
    {
        SetMoveDir();//设置玩家移动方向
        if (timesco < 5)
        {
            timesco += Time.deltaTime;
        }
        else
        {
            Player_Data.Instance.boost5Sco = 0;
        }
        if (Player_Data.Instance.isMove)
        {
            Player_Data.Instance.totalDistance = Player_Data.Instance.playerTransform.position.y-Player_Data.Instance.startPoint.position.y;
        }
    }
    public void SetMoveDir()//设置玩家转弯时移动方向
    {
        if (Player_Data.Instance.isTurn)
        {
            Player_Data.Instance.dir = Vector3.RotateTowards(Player_Data.Instance.dir, Player_Data.Instance.dir40, Player_Data.Instance.playerStaticData.turnAngle * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            duration += Time.deltaTime;
            if (duration >= 1f)
            {
                duration = 0;
                Player_Data.Instance.isTurn = false;
                Player_Data.Instance.isCorrectDirection = false;
            }
        }
        else
        {
            Player_Data.Instance.dir = Vector3.RotateTowards(Player_Data.Instance.dir, Player_Data.Instance.dir50, Player_Data.Instance.playerStaticData.turnAngle * 4f * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            if (Vector2.Dot(Player_Data.Instance.dir, Player_Data.Instance.dir50) > 0.95f && !Player_Data.Instance.isCorrectDirection)
            {
                Player_Data.Instance.moveDir = Player_Data.Instance.dir50;
                Player_Data.Instance.dir = Player_Data.Instance.moveDir;
                Player_Data.Instance.isCorrectDirection = true;
            }
        }
    }

    public void SetSco()
    {
        timesco = 0;
        Player_Data.Instance.boost5Sco += 30;
        Player_Data.Instance.currentSco += 30;
        Player_Data.Instance.boostSco += 30;
    }
}
