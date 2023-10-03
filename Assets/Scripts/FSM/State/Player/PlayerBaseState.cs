using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    protected PlayerComponent _player;
    public PlayerBaseState(string name, State_Machine stateMachine, PlayerComponent player) : base(name, stateMachine) { this._player = player; }
}
