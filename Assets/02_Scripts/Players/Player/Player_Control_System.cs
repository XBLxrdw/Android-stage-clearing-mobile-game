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
    }
    void FixedUpdate()
    {

    }
    public void SetMove(float speed)//设置玩家移动和路程和分数
    {
        data.rigidbody.linearVelocity = data.dir * speed;//设置玩家移动速度
        float distance = data.rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime;
        data.distance += distance;
        if(data.distance > 0.5f)
        {
            data.distance = 0;
            data.currentSco += 30;
        }
    }
}
