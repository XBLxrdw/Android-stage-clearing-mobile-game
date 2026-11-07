using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// 玩家控制系统
/// </summary>
public class Player_Control_System : MonoBehaviour
{
    private Player_Data data;//玩家数据层
    private Player_cont inputSystem;//玩家输入系统
    private float duration;//临时变量持续时间
    void Awake()
    {
        inputSystem = new Player_cont();//实例化玩家输入系统
        data = GetComponent<Player_Data>();//获取玩家数据层

    }
    void Start()
    {
        inputSystem.Move.Enable();//启用玩家输入系统
    }
    void Update()
    {
        data.isStartInput = inputSystem.Move.input.WasPressedThisFrame(); //获取玩家是否有输入
        data.isStopInput = inputSystem.Move.input.WasReleasedThisFrame();//获取玩家是否停止输入
        SetMoveDir();//设置玩家移动方向

    }
    void FixedUpdate()
    {

    }
    public void SetMove(float speed)//设置玩家移动和路程和分数（暂时这么合起来）
    {
        data.rigidbody.linearVelocity = data.dir * speed;//设置玩家移动速度
        float distance = data.rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime;
        data.distance += distance;
        data.currentSco = Mathf.RoundToInt(data.distance * 2.6f) * 30;
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
            data.dir = Vector3.RotateTowards(data.dir, data.dir50, data.playerStaticData.turnAngle * 8f * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            if (Vector2.Dot(data.dir, data.dir50) > 0.95f && !data.isCorrectDirection)
            {
                data.moveDir = data.dir50;
                data.dir = data.moveDir;
                data.isCorrectDirection = true;
            }
        }
    }
}
