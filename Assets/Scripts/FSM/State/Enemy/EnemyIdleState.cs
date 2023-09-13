using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private const string idleAnimation = "MinionIdle";

    public EnemyIdleState(string name, State_Machine stateMachine, EnemyComponent enemy) : base(name, stateMachine,
        enemy)
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
            if (distance <= enemy.lookRad || distance <= enemy.stopDistance)
                FaceTarget();
            playIdleAnimation();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void FaceTarget()
    {
        if (enemy.target.position.x < enemy.transform.position.x)
            enemy.transform.forward = Vector3.forward;
        else if (enemy.target.position.x > enemy.transform.position.x)
            enemy.transform.forward = Vector3.back;
    }

    private void playIdleAnimation()
    {
        enemy.anim.Play(idleAnimation);
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        enemy.anim.StopPlayback();
        base.OnExit();
    }
}