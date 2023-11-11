using System.Collections;
using UnityEngine;

public class DeathBringerSpellCastState : EnemyState
{
    private Enemy_DeathBringer enemy;

    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }


}