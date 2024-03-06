using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    public Action OnEnemyShoot;
    public Action OnEnemyMeleeHit;
    
    public EnemyAttackState(string name, State_Machine stateMachine, EnemyComponent enemy) : base(name, stateMachine,enemy)
    {
        
    }
    public override void OnEnter()
    {
        enemy.sphereCenter = enemy.transform.position + enemy.transform.forward * enemy.stopDistance;
        base.OnEnter();
    }

    public override void UpdateLogic()
    {
        enemy.sphereCenter = enemy.transform.position + enemy.transform.forward * enemy.stopDistance;
        if (enemy.IsRangedEnemy)
        {
            DistanceAttack();
            
        }
        else
        {
            MeleeAttack();
           
        }
        base.UpdateLogic();
    }

    private void MeleeAttack()
    {

        //Collider[] hitEnemies = Physics.OverlapSphere(enemy.sphereCenter,enemy.stopDistance + 0.5f);
        //
        //foreach (Collider obj in hitEnemies)
        //{
        //    if (obj != null && obj.tag == "Player" && !enemy.target.GetComponentInParent<PlayerComponent>().isPlayer_Damaged)
        //    {
        //        if (!enemy.IsAttacking)
        //        {
        //            obj.GetComponentInParent<HealthComponent>().DecreaseHealth(enemy.damage);
        //            OnEnemyMeleeHit?.Invoke();
        //            playAttackAnimation();
        //            MeleeAttackSound();
        //
        //            Knockback knockback = obj.GetComponentInParent<Knockback>();
        //            if (knockback != null)
        //                knockback.PlayKnockback(enemy.transform);
        //        }
        //    }
        //}
        
        Collider[] hitEnemies = Physics.OverlapSphere(enemy.sphereCenter,enemy.stopDistance + 0.5f);
        float distance = 1.0f;
        float minDistace = enemy.stopDistance + 0.5f;
        Transform temp = null;

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i] != null && hitEnemies[i].tag == "Player" && !enemy.target.GetComponentInParent<PlayerComponent>().isPlayer_Damaged) 
            {
                distance = Vector3.Distance(enemy.transform.position, hitEnemies[i].transform.position);
                if (distance < minDistace)
                    temp = hitEnemies[i].transform;
            }
        }

        stateMachine.StartCoroutine(LerpToEnemy(temp));
    }
    
    private IEnumerator LerpToEnemy(Transform target)
    {
        Vector3 startPosition = enemy.transform.position;
        float journeyLength = Vector3.Distance(startPosition, target.position);
        float startTime = Time.time;
        float fractionOfJourney = 1.0f;
        Vector3 newpos = target.position;

        if (target.position.x > enemy.transform.position.x)
        {
            newpos.x -= fractionOfJourney * 1.5f;
        }
        else if (target.position.x > enemy.transform.position.x)
        {
            newpos.x += fractionOfJourney * 1.5f;
        }

        if (target.position.y > enemy.transform.position.y)
            newpos.y -= (target.position.y - enemy.transform.position.y);
        
        while (fractionOfJourney <= 1.0f)
        {
            // Calcula la distancia recorrida.
            float distCovered = (Time.time - startTime) * enemy.speed;

            // Calcula la fracción del recorrido completado.
            fractionOfJourney = distCovered / journeyLength;

            // Interpola suavemente entre la posición inicial y final.
            enemy.transform.position = Vector3.Lerp(startPosition, newpos, fractionOfJourney);

            // Espera hasta el siguiente frame.
            yield return null;
        }

        // El movimiento ha terminado.
        Debug.Log("Lerp Completed");
        if (!enemy.IsAttacking)
        {
            target.GetComponentInParent<HealthComponent>().DecreaseHealth(enemy.damage);
            Debug.Log("enemy.damage" + enemy.damage);
            OnEnemyMeleeHit?.Invoke();
            playAttackAnimation();
            MeleeAttackSound();

            Knockback knockback = target.GetComponentInParent<Knockback>();
            if (knockback != null)
                knockback.PlayKnockback(enemy.transform);
        }
    }

    private void DistanceAttackSound()
    {
        AkSoundEngine.PostEvent("EnemySpitterAttack", enemy.gameObject); // sfx
    }
    private void MeleeAttackSound()
    {
        AkSoundEngine.PostEvent("EnemyMinionAttack", enemy.gameObject); // sfx
    }

    private void DistanceAttack()
    {
        float _distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.target.transform.position);

        if (_distanceToPlayer > enemy.stopDistance)
        {
            Vector3 targetPosition = new Vector3(enemy.target.transform.position.x, enemy.transform.position.y, enemy.target.transform.position.z);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.speed * Time.deltaTime);
        }
        else if (_distanceToPlayer <= enemy.stopDistance && Mathf.Approximately(enemy.transform.position.z, enemy.target.transform.position.z))
        {
            Vector3 direction = enemy.target.transform.position - enemy.transform.position;
            direction.y = 0f;

            if (direction.x > 0)
                enemy.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            else
                enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
             
            Quaternion rot = Quaternion.LookRotation(-enemy.transform.right);

            if (!enemy.IsAttacking)
            {
                OnEnemyShoot?.Invoke();
                playAttackAnimation();
                DistanceAttackSound();
            }

        }
        else if(!Mathf.Approximately(enemy.transform.position.x, enemy.target.transform.position.x))
        {
            Vector3 targetPosition = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.target.transform.position.z);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPosition, enemy.speed * Time.deltaTime);
        }

    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
    
    private void playAttackAnimation()
    {
        enemy.anim.Play(enemy.attackAnimationName);
    }

    public override void AddStateTransitions(string transitionName, State transitionState)
    {
        base.AddStateTransitions(transitionName, transitionState);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
