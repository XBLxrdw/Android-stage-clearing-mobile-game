using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// 玩家待机状态
/// </summary>

[CreateAssetMenu(menuName = "Data/Player/State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()//进入
    {
        base.Enter();
        moveSpeed = 0;
    }
    public override void Exit()//退出
    {
        base.Exit();
    }
    public override void LogicUpdate()//逻辑更新
    {
        //如果移动
        if (data.isMove)
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, data.speed, data.playerStaticData.moveAcceleration * Time.deltaTime);//移动速度逐渐接近玩家静态数据的速度
            if (data.isStartInput)//如果开始输入
            {
                data.dir40 *= new Vector2(-1, 1);
                data.dir50 *= new Vector2(-1, 1);
                data.speed -= 0.5f;
                data.transform.localScale = new Vector3(-data.transform.localScale.x, data.transform.localScale.y, data.transform.localScale.z);//翻转玩家模型
                data.isTurn = true;
            }
            if (data.isStopInput)//如果停止输入
            {
                data.speed += 0.5f;
                data.isTurn = false;
            }
            if (data.audioSource.isPlaying)
            {
                data.audioSource.PlayOneShot(data.hitSound);//播放音效
            }
        }
        else
        {
            if (data.isStartInput)//如果开始输入
            {

            }
            if (data.isStopInput)//如果停止输入
            {
                data.isMove = true;
            }
        }
        //如果死亡
        if (data.isDead)
        {
            data.lastSco = data.currentSco;
            data.currentSco = 0;
            data.distance = 0;
            if (data.isOneLife)//是否是一命
            {

            }
            else
            {

            }
        }
    }
    public override void PhysicsUpdate()//物理更新
    {
        if (data.isMove)
        {
            player_Control_System.SetMove(moveSpeed);//设置玩家移动
        }
    }
}
