using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleAttackState : PlayerBaseState
{
    private readonly float _duration;

    #region PRIVATE_FIELDS

    private string PlayerMeleAnimationName = "Player_Mele_HIt";
    private Vector3 sphereCenter;

    #endregion

    public Action OnPlayerShoot;
    public Action OnAttackEnd;
    private Coroutine endCoroutine;

    public PlayerMeleAttackState(string name, State_Machine stateMachine, PlayerComponent player,float duration) : base(name,
        stateMachine, player)
    {
        _duration = duration;
    }

    public override void OnEnter()
    {
        if (_player.isRanged_Attacking)
            OnPlayerShoot?.Invoke();
        sphereCenter = _player.transform.position + _player.transform.right * _player._attackRange;
        _player.input.OnPlayerAttack += OnMeleeAttack;
        _player.input.OnPlayerMove += OnPlayerMove;
        MeleeAttack();
        endCoroutine = stateMachine.StartCoroutine(EndAttack());
        base.OnEnter();
    }

    private void OnPlayerMove(Vector2 obj)
    {
        _player._movementController.SetMovement(obj);
    }

    private void OnMeleeAttack(bool obj)
    {
        if (_player.isRanged_Attacking)
            OnPlayerShoot?.Invoke();
        else
            MeleeAttack();
    }

    public override void UpdateLogic()
    {
        sphereCenter = _player.characterSprite.transform.position +
                       _player.characterSprite.transform.right * _player._attackRange;
        if (_player.movement != Vector3.zero)
            _player._movementController.UpdateMovement();
        
        base.UpdateLogic();
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(_duration);
        Debug.Log("EndAttack");
        OnAttackEnd?.Invoke();
    }

    private void MeleeAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(sphereCenter, _player._attackRange);
        float distance = 0.0f;
        float minDistace = _player._attackRange;
        Transform temp = null;
        
        for (int i = 0; i < hitEnemies.Length; i++)
        {
            distance = Vector3.Distance(_player.transform.position, hitEnemies[i].transform.position);
            if (distance < minDistace)
                temp = hitEnemies[i].transform;
        }

        stateMachine.StartCoroutine(LerpToEnemy(temp));
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy != null && enemy.tag == "Enemy")
            {
                enemy.GetComponent<HealthComponent>().DecreaseHealth(_player.damage);

                Knockback knockback = enemy.GetComponent<Knockback>();
                if (knockback != null)
                    knockback.PlayKnockback(_player.transform);
            }
        }
    }

    private IEnumerator LerpToEnemy(Transform target)
    {
        yield return null;
        Vector3 originalPos = _player.transform.position;
        float distance = Vector3.Distance(_player.transform.position, target.position);
        while (distance > 0.5f)
        {
            distance = Vector3.Distance(_player.transform.position, target.position);
            _player.transform.position = Vector3.Lerp(originalPos,target.position,_player.speed * Time.deltaTime);
            Debug.Log("Lerp");
            yield return null;
        }
        yield return null;
    }

    public override void OnExit()
    {
        _player.input.OnPlayerAttack -= OnMeleeAttack;
        _player.anim.StopPlayback();
        
        if (endCoroutine != null)
            stateMachine.StopCoroutine(endCoroutine);
        
        base.OnExit();
    }
}