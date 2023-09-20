using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyHitState : EnemyBaseState
{
    private const string moveAnimation = "MinionHit";
    
    public EnemyHitState(string name, State_Machine stateMachine, EnemyComponent enemy) : base(name, stateMachine,enemy)
    {
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        enemy.isHit = true;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        enemy.character_Health_Component.DecreaseHealth(enemy.target.GetComponent<PlayerComponent>().damage);
        enemy.floatingTextHandleer.ShowFloatingText(enemy.floatingTextPrefab, enemy.floatingTextSpawn, enemy.character_Health_Component._health);
        enemy.isHit = false;
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    private void playMoveAnimation()
    {
       // enemy.anim.Play(moveAnimation);
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
