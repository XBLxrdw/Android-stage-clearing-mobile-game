using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// 玩家控制系统
/// </summary>
public class Player_Control_System : MonoBehaviour
{
    private Player_cont inputSystem;//玩家输入系统
    void Awake()
    {
        inputSystem = new Player_cont();//实例化玩家输入系统

    }
    void Start()
    {
        inputSystem.Move.Enable();//启用玩家输入系统
    }
    void Update()
    {
        Player_Data.Instance.isStartInput = inputSystem.Move.input.WasPressedThisFrame(); //获取玩家是否有输入
        Player_Data.Instance.isStopInput = inputSystem.Move.input.WasReleasedThisFrame();//获取玩家是否停止输入
    }
    void FixedUpdate()
    {

    }
    public void SetMove(float speed)//设置玩家移动和路程和分数
    {
        Player_Data.Instance.rigidbody.linearVelocity = Player_Data.Instance.dir * speed;//设置玩家移动速度
        float distance = Player_Data.Instance.rigidbody.linearVelocity.magnitude * Time.fixedDeltaTime;
        Player_Data.Instance.distance += distance;
        if(Player_Data.Instance.distance > 0.5f)
        {
            Player_Data.Instance.distance = 0;
            Player_Data.Instance.currentSco += 30;
        }
    }
}
