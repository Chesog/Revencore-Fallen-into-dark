using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(string name, State_Machine stateMachine, EnemyComponent enemy) : base(name, stateMachine,enemy)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (enemy != null)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
            if (distance <= enemy.lookRad || distance <= enemy.stopDistance)
                FaceTarget();
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void FaceTarget()
    {
        //enemy.transform.LookAt(enemy.target.position);
        
        if (enemy.target.position.x < enemy.transform.position.x)
            enemy.transform.forward = Vector3.forward;
        else if (enemy.target.position.x > enemy.transform.position.x)
            enemy.transform.forward = Vector3.back;
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