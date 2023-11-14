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
        Vector3 force = Vector3.zero;

        // Attraction force towards the target
        float distanceToTarget = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);
        if (distanceToTarget > enemy.stopDistance)
        {
            Vector3 directionToTarget = (enemy.target.transform.position - enemy.transform.position).normalized;
            force += directionToTarget * enemy.speed;
        }

        // Repulsion force from other enemies
        Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, enemy.avoidanceRadius);
        foreach (var hitCollider in hitColliders)
        {
            EnemyComponent otherEnemy = hitCollider.GetComponent<EnemyComponent>();
            if (otherEnemy != null && otherEnemy != enemy)
            {
                Vector3 directionFromOther = (enemy.transform.position - otherEnemy.transform.position).normalized;
                force += directionFromOther * enemy.avoidanceForce;
            }
        }

        // Apply the force
        enemy.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime, ForceMode.VelocityChange);
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
        //enemy.anim.Play(enemy.moveAnimationName);
        enemy.anim.SetFloat("Movement",enemy.movement.magnitude - enemy.movement.y);
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
