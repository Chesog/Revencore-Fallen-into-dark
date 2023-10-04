using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    private string PlayerMoveAnimationName = "Player_Run";
    public PlayerMoveState(string name, State_Machine stateMachine, PlayerComponent player) : base(name, stateMachine, player)
    {
    }
    
    public override void OnEnter()
    {
        _player.input.OnPlayerMove += OnPlayerMove;
        base.OnEnter();
    }

    private void OnPlayerMove(Vector2 obj)
    {
        _player.movement.x = obj.x * _player.speed;
        _player.movement.z = obj.y * _player.zspeed;
        _player.movement.y = _player.rigidbody.velocity.y;
    }

    public override void UpdateLogic()
    {
        if (_player.movement.x > 0)
            _player.characterSprite.transform.forward = Vector3.forward;
        if (_player.movement.x < 0)
            _player.characterSprite.transform.forward = Vector3.back;

        _player.rigidbody.velocity = _player.movement;
        playMoveAnimation();
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void playMoveAnimation()
    {
        _player.anim.Play(PlayerMoveAnimationName);
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        _player.input.OnPlayerMove -= OnPlayerMove;
        _player.anim.StopPlayback();
        base.OnExit();
    }
}
