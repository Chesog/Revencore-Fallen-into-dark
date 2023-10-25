using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class That Contains The Player Variables
/// </summary>
public class PlayerComponent : CharacterComponent
{
   public PlayerInputManager input;
   public Player_Data_Source player_Source;
   public PlayerMovementController _movementController;

    public Transform feet_Pivot;
    public Transform camera;
    public float _attackRange = 2f;
    public Rigidbody rigidbody;

    public float turn_Smooth_Velocity;
    public float turnSmoothTime;
    public float lastAngle;
    public float maxDistance;
    public float minJumpDistance;
    [Header("Character Coyote Time Setup")]
    public float coyoteTime;
    public float coyoteTimerCounter;

    public bool isPlayer_Attacking;
    public bool isPlayer_Damaged;

    private void Awake()
    {
        player_Source._player = this;
        character_Health_Component._health = character_Health_Component._maxHealth;

        feet_Pivot ??= GetComponent<Transform>();
        player_Source._player.damage = 20.0f;
        isPlayer_Damaged = false;
    }

    private void OnEnable()
    {
        character_Health_Component._health = character_Health_Component._maxHealth;
        initialSpeed = speed;
    }

    private void OnDrawGizmos()
    {
        if (isPlayer_Attacking)
        {
            Gizmos.color = Color.yellow;
        }
    }
}
