using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatemachine : State_Machine
{
    [SerializeField] private PlayerInputManager _inputManager;
    [SerializeField] private PlayerComponent _playerComponent;
    private PlayerIdleState _idleState;
    private PlayerMeleAttackState _attackState;
    private PlayerMoveState _moveState;
    private float invulneravilityTime = 1.5f;

    private void Start()
    {
        _idleState = new PlayerIdleState(nameof(_idleState), this, _playerComponent);
        _attackState = new PlayerMeleAttackState(nameof(_attackState), this, _playerComponent);
        _moveState = new PlayerMoveState(nameof(_moveState), this, _playerComponent);
        _inputManager.OnPlayerMove += OnPlayerMove;
        _inputManager.OnPlayerAttack += OnPlayerAttack;
        _inputManager.OnPlayerPause += OnPlayerPause;
        _playerComponent.character_Health_Component.OnInsufficient_Health += OnplayerInsufficientHeath;
        _playerComponent.character_Health_Component.OnDecrease_Health += OnplayerDecreaseHeath;

        base.OnEnable();
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
        if (_playerComponent.character_Health_Component._health <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Gameplay");
        }
    }

    private void OnEnable()
    {
        if (_idleState == null)
            _idleState = new PlayerIdleState(nameof(_idleState), this, _playerComponent);

        if (_attackState == null)
            _attackState = new PlayerMeleAttackState(nameof(_attackState), this, _playerComponent);

        if (_moveState == null)
            _moveState = new PlayerMoveState(nameof(_moveState), this, _playerComponent);

        _inputManager.OnPlayerMove += OnPlayerMove;
        _inputManager.OnPlayerAttack += OnPlayerAttack;
        _inputManager.OnPlayerPause += OnPlayerPause;

        base.OnEnable();
    }

    private void OnPlayerPause()
    {
        if (!_playerComponent.isDead)
            SetState(_idleState);
    }

    private void OnPlayerAttack(bool obj)
    {
        if (!_playerComponent.isDead)
            SetState(_attackState);
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
    }
}