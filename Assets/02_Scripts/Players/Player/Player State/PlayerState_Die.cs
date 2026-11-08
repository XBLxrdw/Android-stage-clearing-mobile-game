using System.Collections;
using UnityEngine;
/// <summary>
/// 玩家死亡状态
/// </summary>
[CreateAssetMenu(menuName = "Data/Player/State/Die", fileName = "PlayerState_Die")]
public class PlayerState_Die : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        if(Player_Data.Instance.totalDistance> Player_Data.Instance.maxDistance)
        {
            Player_Data.Instance.maxDistance = Player_Data.Instance.totalDistance;
        }
        Player_Data.Instance.distance = 0;
        SetModelActive(false);
        Player_Data.Instance.isMove = false;
        Object.Instantiate(Player_Data.Instance.die, Player_Data.Instance.transform.position, Quaternion.identity);
        if (Player_Data.Instance.isOneLife)//是否是一命
        {

            Player_Data.Instance.isOneLife = false;
            //弹出是否复活广告
            UIManager.Instance.ShowReviveAdPanel();
            player_Control_System.StartCoroutine(Countdown());
        }
        else
        {
            GameFlow.Instance.RestartGame();
            //重开游戏逻辑
        }
    }

    public override void Exit()
    {
        base.Exit();
        SetModelActive(true);
    }

    public override void LogicUpdate()
    {
        if (!Player_Data.Instance.isDead)
        {
            playerStateMachine.SwitchState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicsUpdate()
    {

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.Countdown(3);
        yield return new WaitForSeconds(1f);
        if (!Player_Data.Instance.isDead) yield break; // 如果中途复活了，终止倒计时

        UIManager.Instance.Countdown(2);
        yield return new WaitForSeconds(1f);
        if (!Player_Data.Instance.isDead) yield break;

        UIManager.Instance.Countdown(1);
        yield return new WaitForSeconds(1f);

        if (Player_Data.Instance.isDead)
        {
            UIManager.Instance.CloseReviveAdPanel();
            GameFlow.Instance.RestartGame();
            // 这里调用重开逻辑
        }
    }
    private void SetModelActive(bool active)
    {
        Transform model = Player_Data.Instance.transform.Find("Player");
        if (model != null)
        {
            model.gameObject.SetActive(active);
        }
    }
}
