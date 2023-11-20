using UnityEngine;


public class ShadyDeadState : EnemyState
{

    private Enemy_Shady enemy;
    public ShadyDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Shady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

        // Tự clear khi phát nổ gây sát thương cho người chơi
        if (triggerCalled)
            enemy.SelfDestroy();
    }
}
