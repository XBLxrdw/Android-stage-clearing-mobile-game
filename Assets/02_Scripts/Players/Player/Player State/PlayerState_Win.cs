using UnityEngine;
/// <summary>
/// 玩家胜利状态
/// </summary>
[CreateAssetMenu(menuName = "Data/Player/State/Win", fileName = "PlayerState_Wie")]
public class PlayerState_Win : PlayerState
{
    private float tick;
    private float winDuration;//胜利临时变量持续时间
    private Vector2 playerEndTransform;

    public override void Enter()
    {
        base.Enter();
        winDuration = 0;
        playerEndTransform = Player_Data.Instance.playerTransform.position;
        Player_Data.Instance.isMove = false;
        tick = 0;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void PhysicsUpdate()
    {
        SineWaveMove(playerEndTransform);
    }
    /// <summary>
    /// 正弦波向下移动
    /// </summary>
    public void SineWaveMove(Vector2 originPos)
    {
        winDuration += Time.fixedDeltaTime;
        float y = originPos.y - (4f * winDuration);
        float x = originPos.x + Mathf.Sin(winDuration * 5f) * 1f;
        Transform playerTF = Player_Data.Instance.playerTransform;
        playerTF.position = new Vector2(x, y);
        float currentAbsScaleX = Mathf.Abs(playerTF.localScale.x);
        if (Mathf.Cos(winDuration * 5f) > 0)
            playerTF.localScale = new Vector3(currentAbsScaleX, playerTF.localScale.y, 1);
        else
            playerTF.localScale = new Vector3(-currentAbsScaleX, playerTF.localScale.y, 1);
    }
}
