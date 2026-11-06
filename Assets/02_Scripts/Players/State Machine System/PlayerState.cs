using UnityEngine;
/// <summary>
/// 玩家状态机系统，所有玩家状态的基类
/// </summary>
public class PlayerState : ScriptableObject, IState
{
    protected float moveSpeed;
    [SerializeField] string stateName;//状态名称
    [SerializeField,Range(0f,1f)]float transitionDuration = 0.1f;//状态时间 
    int stateHash;//状态hash
    float stateStartTime;//状态开始时间
    protected PlayerStateMachine playerStateMachine;
    protected Animator animator;
    protected Player_Data data;//玩家数据
    protected Player_Control_System player_Control_System;//玩家控制系统
    protected bool IsAnimationFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;//判断动画是否播放完毕
    protected float stateDuration => Time.time - stateStartTime;//状态持续时间
    void OnEnable()
    {
        stateHash = Animator.StringToHash(stateName);
    }
    public void Initialize(PlayerStateMachine playerStateMachine, Animator animator, Player_Data data, Player_Control_System player_Control_System)
    {
        this.playerStateMachine = playerStateMachine;
        this.animator = animator;
        this.data = data;
        this.player_Control_System = player_Control_System;
    }
    public virtual void Enter()
    {
        animator.CrossFade(stateHash,transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
