using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatemachine : State_Machine
{
    [SerializeField] private Player_Input_Manager _inputManager;
    [SerializeField] private PlayerComponent _playerComponent;
    private PlayerIdleState _idleState;

    private void OnEnable()
    {
        _idleState = new PlayerIdleState(nameof(_idleState),this,_playerComponent);
    }

    protected override State GetInitialState()
    {
        return null;

    }
}
