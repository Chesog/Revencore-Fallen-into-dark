using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private string PlayerIdleAnimationName = "Player_Idle";
    public PlayerIdleState(string name, State_Machine stateMachine, PlayerComponent player) : base(name, stateMachine,
        player)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        playIdleAnimation();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void playIdleAnimation()
    {
        _player.anim.Play(PlayerIdleAnimationName);
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        _player.anim.StopPlayback();
        base.OnExit();
    }
}