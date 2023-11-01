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
        if (!_playerComponent.isDead)
        {
            _playerComponent.movement.x = newMov.x * _playerComponent.speed;
            _playerComponent.movement.z = newMov.y * _playerComponent.zspeed;
            //_playerComponent.movement.y = _playerComponent.rigidbody.velocity.y;
            _playerComponent.movement.y = 0.0f;
        }
    }

    public void UpdateMovement()
    {
        _playerComponent.rigidbody.velocity = _playerComponent.movement;

        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;

        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, 5.0f))
        {
            _playerComponent.transform.position = hit.point + Vector3.up * 1.5f;
        }
    }
}