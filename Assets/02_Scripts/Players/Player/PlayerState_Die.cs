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
        data.distance = 0;
        GameObject.Find("Player").SetActive(false);
        Object.Instantiate(data.die, data.transform.position, Quaternion.identity);
        if (data.isOneLife)//是否是一命
        {

            data.isOneLife = false;
            //弹出是否复活广告
            player_Control_System.StartCoroutine(Countdown());
        }
        else
        {
            data.lastSco = data.currentSco;
            data.currentSco = 0;
            //重开游戏逻辑
        }
    }

    public override void Exit()
    {
        base.Exit();
        GameObject.Find("Player").SetActive(true);
    }

    public override void LogicUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("UI: 3");
        yield return new WaitForSeconds(1f);

        if (!data.isDead) yield break; // 如果中途复活了，直接终止倒计时

        Debug.Log("UI: 2");
        yield return new WaitForSeconds(1f);

        if (!data.isDead) yield break;

        Debug.Log("UI: 1");
        yield return new WaitForSeconds(1f);

        if (data.isDead)
        {
            // 这里调用重开逻辑
        }
    }
}
