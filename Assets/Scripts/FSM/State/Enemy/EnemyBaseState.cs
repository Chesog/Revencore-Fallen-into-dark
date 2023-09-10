using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : State
{
    public EnemyComponent enemy;

    public EnemyBaseState(string name, State_Machine state_Machine , EnemyComponent enemy) : base(name, state_Machine)
    {
        this.enemy = enemy;
    }
}
