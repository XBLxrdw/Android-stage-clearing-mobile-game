using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.RuleTile.TilingRuleOutput;
/// <summary>
/// 玩家待机状态
/// </summary>

[CreateAssetMenu(menuName = "Data/Player/State/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    private float tick;
    public override void Enter()//进入
    {
        base.Enter();
        AudioManager.instance.PlayLoopSFX(AudioManager.instance.ski);
        moveSpeed = 4f;
        GameObject.Find("Player").SetActive(true);
        SpriteRenderer sr = Player_Data.Instance.transform.Find("Player/Head").GetComponent<SpriteRenderer>();
        sr.color = Player_Data.Instance.color;
        TrailRenderer trail = Player_Data.Instance.GetComponent<TrailRenderer>();
        Color playerColor = Player_Data.Instance.color;
        trail.startColor = new Color(playerColor.r, playerColor.g, playerColor.b, 1f);
        trail.endColor = new Color(playerColor.r, playerColor.g, playerColor.b, 0f);
    }
    public override void Exit()//退出
    {
        base.Exit();
        Player_Data.Instance.rigidbody.linearVelocity = Vector2.zero;
    }
    public override void LogicUpdate()//逻辑更新
    {
        //如果移动
        if (Player_Data.Instance.isMove)
        {
            if (Player_Data.Instance.isInvincible == true)
            {
                tick += Time.deltaTime;
                if (tick > 1)
                {
                    tick = 0;
                    Player_Data.Instance.isInvincible = false;
                }
            }
            UIManager.Instance.CloseGameplayHint();
            moveSpeed = Mathf.MoveTowards(moveSpeed, Player_Data.Instance.speed, Player_Data.Instance.playerStaticData.moveAcceleration * Time.deltaTime);//移动速度逐渐接近玩家静态数据的速度
            if (Player_Data.Instance.isStartInput)//如果开始输入
            {
                AudioManager.instance.PlayLoopSFX(AudioManager.instance.ski);
                Player_Data.Instance.dir40 *= new Vector2(-1, 1);
                Player_Data.Instance.dir50 *= new Vector2(-1, 1);
                Player_Data.Instance.speed -= 0.5f;
                Player_Data.Instance.transform.localScale = new Vector3(-Player_Data.Instance.transform.localScale.x, Player_Data.Instance.transform.localScale.y, Player_Data.Instance.transform.localScale.z);//翻转玩家模型
                Player_Data.Instance.isTurn = true;
            }
            if (Player_Data.Instance.isStopInput)//如果停止输入
            {
                AudioManager.instance.StopLoopSFX();
                Player_Data.Instance.speed += 0.5f;
                Player_Data.Instance.isTurn = false;
            }
        }
        else
        {
            if (Player_Data.Instance.isStartInput)//如果开始输入
            {

            }
            if (Player_Data.Instance.isStopInput)//如果停止输入
            {
                Player_Data.Instance.isInvincible = true;
                Player_Data.Instance.isMove = true;
            }
        }
        //判断死亡
        if (Mathf.Abs(Player_Data.Instance.transform.position.x) >= 4.5f || Player_Data.Instance.isDead)
        {
            Player_Data.Instance.isDead = true;
            playerStateMachine.SwitchState(typeof(PlayerState_Die));//切换状态
        }
        //判断胜利
        if (Player_Data.Instance.isWin)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Win));//切换状态
        }
        //判断是否进入激励状态
        if (Player_Data.Instance.boost5Sco >= 120)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Boot));//切换状态
        }
    }
    public override void PhysicsUpdate()//物理更新
    {
        if (Player_Data.Instance.isMove)
        {
            player_Control_System.SetMove(moveSpeed);//设置玩家移动
        }
    }
}
