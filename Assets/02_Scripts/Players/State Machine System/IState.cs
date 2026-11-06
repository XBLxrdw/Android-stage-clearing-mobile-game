/// <summary>
/// 状态机接口
/// </summary>
public interface IState
{
    void Enter();//进入
    void Exit();//退出
    void LogicUpdate();//逻辑更新
    void PhysicsUpdate();//物理更新
}
