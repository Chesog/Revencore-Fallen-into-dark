using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(string name, State_Machine state_Machine, EnemyComponent enemy) : base(name, state_Machine,enemy)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);
        if (distance <= enemy.lookRad || distance <= enemy.stopDistance)
            FaceTarget();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    private void FaceTarget()
    {
        enemy.transform.LookAt(enemy.target.position);
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
