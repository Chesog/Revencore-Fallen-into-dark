using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(string name, State_Machine stateMachine, EnemyComponent enemy) : base(name, stateMachine,enemy)
    {
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (enemy != null && enemy.target != null)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
            if (distance <= enemy.lookRad && distance > enemy.stopDistance)
                MoveTowardsTarget();
            if (distance <= enemy.lookRad || distance <= enemy.stopDistance)
                FaceTarget();

            playMoveAnimation();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void MoveTowardsTarget()
    {
        float distance = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);
        Vector3 direction = enemy.target.transform.position - enemy.transform.position;

        if (distance > enemy.stopDistance)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.target.transform.position, enemy.speed * Time.deltaTime);
        }
    }

    private void FaceTarget()
    {
        if (enemy.target.position.x < enemy.transform.position.x)
            enemy.transform.forward = Vector3.forward;
        else if (enemy.target.position.x > enemy.transform.position.x)
            enemy.transform.forward = Vector3.back;
    }

    private void playMoveAnimation()
    {
        enemy.anim.Play(enemy.moveAnimationName);
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
