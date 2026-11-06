using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 状态机系统类
/// 持有所有状态类，并进行管理和控制状态之间的转换
/// 对当前状态进行更新
/// </summary>
public class StateMachine : MonoBehaviour
{
    IState state;
    protected Dictionary<System.Type, IState> stateTable;//状态表
    void Update()
    {
        state.LogicUpdate();//逻辑帧
    }
    void FixedUpdate()
    {
        state.PhysicsUpdate();//物理帧
    }

    protected void SwitchOn(IState newState)
    {
        state = newState;
        state.Enter();
    }
    public void SwitchState(IState newState)//切换状态
    {
        state?.Exit();
        state = newState;
        SwitchOn(state);
    }
    public void SwitchState(System.Type newState)//切换状态重载
    {
        SwitchState(stateTable[newState]);
    }
}
