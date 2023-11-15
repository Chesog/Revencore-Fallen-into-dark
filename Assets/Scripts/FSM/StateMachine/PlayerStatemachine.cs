using System;
using System.Collections;
using UnityEngine;

public class PlayerStatemachine : State_Machine
{
    [SerializeField] private float attack1Duration;
    [SerializeField] private float attack2Duration;
    [SerializeField] private float attack3Duration;
    [SerializeField] private PlayerInputManager _inputManager;
    [SerializeField] private PlayerComponent _playerComponent;
    private PlayerIdleState _idleState;
    private PlayerMeleAttackState _attackState;
    private PlayerMeleAttackState _attackState1;
    private PlayerMeleAttackState _attackState2;
    private PlayerMoveState _moveState;
    private float invulneravilityTime = 1.5f;
    private float rangedMaxTime = 10.0f;
    private bool attackCalled;
    private IEnumerator resetRangedAttack;
    

    private void OnAttackEnd()
    {
        SetState(_idleState);
        _playerComponent.anim.SetInteger("CurrentAttack", 0);
    }

    private void OnPlayerInteract()
    {
        Vector3 sphereCenter = _playerComponent.characterSprite.transform.position +
                               _playerComponent.characterSprite.transform.right * _playerComponent._attackRange;
        Collider[] hitEntities = Physics.OverlapSphere(sphereCenter, _playerComponent._attackRange);
        if (hitEntities != null)
        {
            foreach (Collider potion in hitEntities)
            {
                HealthPotion healthPotion = potion?.GetComponent<HealthPotion>();
                ShotPotion shotPotion = potion?.GetComponent<ShotPotion>();
                if (healthPotion != null)
                    healthPotion.Interact(_playerComponent);
                if (shotPotion != null)
                    shotPotion.Interact(_playerComponent);
            }
        }
    }

    private void OnplayerDecreaseHeath()
    {
        if (!_playerComponent.isPlayer_Damaged)
        {
            _playerComponent.isPlayer_Damaged = true;
            StartCoroutine(InvulerabilityFrame());
        }
    }

    private IEnumerator InvulerabilityFrame()
    {
        yield return new WaitForSeconds(invulneravilityTime);
        _playerComponent.isPlayer_Damaged = false;
    }

    private void OnplayerInsufficientHeath()
    {
        _playerComponent.anim.Play("Player_Muerte");
        _playerComponent.isDead = true;
    }

    private void OnEnable()
    {
        _playerComponent.isDead = false;

        if (_idleState == null)
            _idleState = new PlayerIdleState(nameof(_idleState), this, _playerComponent);

        if (_attackState == null)
            _attackState = new PlayerMeleAttackState("attackState", this, _playerComponent, attack1Duration);

        if (_attackState1 == null)
            _attackState1 = new PlayerMeleAttackState("attackState1", this, _playerComponent, attack2Duration);

        if (_attackState2 == null)
            _attackState2 = new PlayerMeleAttackState("attackState2", this, _playerComponent, attack3Duration);

        if (_moveState == null)
            _moveState = new PlayerMoveState(nameof(_moveState), this, _playerComponent);

        _inputManager.OnPlayerMove += OnPlayerMove;
        _inputManager.OnPlayerAttack += OnPlayerAttack;
        _inputManager.OnPlayerPause += OnPlayerPause;
        _inputManager.OnPlayerPickUp += OnPlayerInteract;
        
        _attackState.OnPlayerShoot += OnplayerShoot;
        _attackState1.OnPlayerShoot += OnplayerShoot;
        _attackState2.OnPlayerShoot += OnplayerShoot;
        
        _attackState.OnAttackEnd += OnAttackEnd;
        _attackState1.OnAttackEnd += OnAttackEnd;
        _attackState2.OnAttackEnd += OnAttackEnd;

        _playerComponent.character_Health_Component.OnInsufficient_Health += OnplayerInsufficientHeath;
        _playerComponent.character_Health_Component.OnDecrease_Health += OnplayerDecreaseHeath;

        resetRangedAttack = ResetAttack();
        attackCalled = false;

        base.OnEnable();
    }

    private void OnplayerShoot()
    {
        if (_playerComponent.characterSprite.transform.rotation.y == 0f)
            _playerComponent.rot = Quaternion.EulerRotation(_playerComponent.characterSprite.transform.rotation.x,
                _playerComponent.characterSprite.transform.rotation.y + 89.5f,
                _playerComponent.characterSprite.transform.rotation.z + 89.5f);
        else
            _playerComponent.rot = Quaternion.EulerRotation(_playerComponent.characterSprite.transform.rotation.x,
                _playerComponent.characterSprite.transform.rotation.y - 90.5f,
                _playerComponent.characterSprite.transform.rotation.z - 90.5f);
        
        
        GameObject projectile = Instantiate(_playerComponent.bulletPrefab, _playerComponent.shootingPoint.position,
            _playerComponent.rot);

        if (!attackCalled)
            StartCoroutine(resetRangedAttack);
    }

    private IEnumerator ResetAttack()
    {
        attackCalled = true;
        yield return new WaitForSeconds(rangedMaxTime);
        _playerComponent.isRanged_Attacking = false;
        attackCalled = false;
        yield return null;
    }

    private void OnPlayerPause()
    {
        if (!_playerComponent.isDead)
            SetState(_idleState);
    }

    private void OnPlayerAttack(bool obj)
    {
        if (!_playerComponent.isDead)
        {
            if (currentState == _attackState)
            {
                SetState(_attackState1);
                _playerComponent.anim.SetInteger("CurrentAttack", 2);
            }
            else if (currentState == _attackState1)
            {
                SetState(_attackState2);
                _playerComponent.anim.SetInteger("CurrentAttack", 3);
            }
            else
            {
                SetState(_attackState);
                _playerComponent.anim.SetInteger("CurrentAttack", 1);
            }
        }
    }

    private void OnPlayerMove(Vector2 obj)
    {
        if (!_playerComponent.isDead)
            SetState(_moveState);
    }

    protected override State GetInitialState()
    {
        return _idleState;
    }

    private void OnDisable()
    {
        _inputManager.OnPlayerMove -= OnPlayerMove;
        _inputManager.OnPlayerAttack -= OnPlayerAttack;
        _inputManager.OnPlayerPause -= OnPlayerPause;
        _inputManager.OnPlayerPickUp -= OnPlayerInteract;
        
        _attackState.OnPlayerShoot -= OnplayerShoot;
        _attackState1.OnPlayerShoot -= OnplayerShoot;
        _attackState2.OnPlayerShoot -= OnplayerShoot;
        
        _attackState.OnAttackEnd -= OnAttackEnd;
        _attackState1.OnAttackEnd -= OnAttackEnd;
        _attackState2.OnAttackEnd -= OnAttackEnd;

        _playerComponent.character_Health_Component.OnInsufficient_Health -= OnplayerInsufficientHeath;
        _playerComponent.character_Health_Component.OnDecrease_Health -= OnplayerDecreaseHeath;
    }
}