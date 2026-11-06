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
    public void SetMove(float speed)
    {
        data.rigidbody.linearVelocity = data.dir * speed;//设置玩家移动速度
    }
    void SetMoveDir()
    {
        if (data.isTurn)
        {
            data.dir = Vector3.RotateTowards(data.dir, data.dir40, 45f * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            if (Vector2.Dot(data.dir, data.dir40) > 0.95f)
            {
                data.isTurn = false;
            }
            data.isCorrectDirection = false;
        }
        else
        {
            if (data.isCorrectDirection)
            {
                data.dir = Vector3.RotateTowards(data.dir, data.dir50, 360f * Mathf.Deg2Rad * Time.deltaTime, 0f).normalized;
            }
            if (Vector2.Dot(data.dir, data.dir50) > 0.9f && !data.isCorrectDirection)
            {
                data.moveDir = data.dir = data.dir50;
                data.dir40 *= new Vector2(-1, 1);
                data.dir50 *= new Vector2(-1, 1);
                data.isCorrectDirection = true;
            }
        }
    }
}
