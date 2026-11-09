using UnityEngine;
/// <summary>
/// 玩家激励状态
/// </summary>
[CreateAssetMenu(menuName = "Data/Player/State/Boot", fileName = "PlayerState_Boot")]
public class PlayerState_Boot : PlayerState
{
    private float tick;
    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(AudioManager.instance.perfect);
        Player_Data.Instance.isBoost = true;
        //变颜色
        SpriteRenderer sr = Player_Data.Instance.transform.Find("Player/Head").GetComponent<SpriteRenderer>();
        sr.color = Player_Data.Instance.color3;
        TrailRenderer trail = Player_Data.Instance.GetComponent<TrailRenderer>();
        Color playerColor = Player_Data.Instance.color3;
        trail.startColor = new Color(playerColor.r, playerColor.g, playerColor.b, 1f);
        trail.endColor = new Color(playerColor.r, playerColor.g, playerColor.b, 0f);
    }

    public override void Exit()
    {
        base.Exit();
        Player_Data.Instance.isBoost = false;
        Player_Data.Instance.rigidbody.linearVelocity = Vector2.zero;
    }

    public override void LogicUpdate()
    {
        //生成拖尾粒子
        //检测有没有5秒没额外激励加分
        if (Player_Data.Instance.boost5Sco == 0)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Idle));//切换状态
        }
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
            if (Player_Data.Instance.isStartInput)//如果开始输入
            {
                AudioManager.instance.PlayLoopSFX2(AudioManager.instance.ski);
                Player_Data.Instance.dir40 *= new Vector2(-1, 1);
                Player_Data.Instance.dir50 *= new Vector2(-1, 1);
                Player_Data.Instance.speed -= 0.5f;
                Player_Data.Instance.transform.localScale = new Vector3(-Player_Data.Instance.transform.localScale.x, Player_Data.Instance.transform.localScale.y, Player_Data.Instance.transform.localScale.z);//翻转玩家模型
                Player_Data.Instance.isTurn = true;
            }
            if (Player_Data.Instance.isStopInput)//如果停止输入
            {
                AudioManager.instance.StopLoopSFX2();
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
                AudioManager.instance.PlayLoopSFX(AudioManager.instance.ski);
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
    }

    public override void PhysicsUpdate()
    {
        if (Player_Data.Instance.isMove)
        {
            player_Control_System.SetMove(Player_Data.Instance.speed);//设置玩家移动
        }
    }
}
