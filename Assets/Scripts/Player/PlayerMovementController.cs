using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public PlayerComponent _playerComponent;
    private void OnEnable()
    {
        _playerComponent.input.OnPlayerMove += SetMovement;
    }

    public void SetMovement(Vector2 newMov)
    {
        _playerComponent.movement.x = newMov.x * _playerComponent.speed;
        _playerComponent.movement.z = newMov.y * _playerComponent.zspeed;
        _playerComponent.movement.y = _playerComponent.rigidbody.velocity.y;
    }

    public void UpdateMovement()
    {
        _playerComponent.rigidbody.velocity = _playerComponent.movement;
    }
}
