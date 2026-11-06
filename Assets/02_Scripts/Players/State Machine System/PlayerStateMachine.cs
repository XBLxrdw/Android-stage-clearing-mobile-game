using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家状态机系统，管理玩家的所有状态
/// </summary>
public class PlayerStateMachine : StateMachine
{
    private Player_Data data;//玩家数据
    [SerializeField] PlayerState[] states;//状态数组
    private Player_Control_System player_Control_System;//玩家控制系统

    void Awake()
    {
        data = GetComponent<Player_Data>();//获取玩家数据
        player_Control_System = GetComponent<Player_Control_System>();//获取玩家控制系统
        stateTable = new Dictionary<System.Type,IState>(states.Length);//创建状态表
        foreach (PlayerState state in states)//遍历状态数组
        {
            state.Initialize(this, data.animator, data, player_Control_System);//初始化状态
            stateTable.Add(state.GetType(), state);
        }
    }
    void Start()
    {
        SwitchOn(stateTable[typeof(PlayerState_Idle)]);//切换到Idle状态
    }
}
