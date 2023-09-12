using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    public EnemyComponent enemy;

    public EnemyBaseState(string name, State_Machine stateMachine , EnemyComponent enemy) : base(name, stateMachine)
    {
        this.enemy = enemy;
    }
}
