using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMelee_T : MonoBehaviour
{
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackDamage = 10f;
    [SerializeField] private float _attackXPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            MeleeAttack();
    }

    private void MeleeAttack()
    {
        Vector3 sphereCenter = transform.position + transform.forward * _attackRange;
        Collider[] hitEnemies = Physics.OverlapSphere(sphereCenter, _attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            Enemy_T meleeEnemy = enemy.GetComponent<Enemy_T>();
            DistanceEnemy distanceEnemy = enemy.GetComponent<DistanceEnemy>();
            if (meleeEnemy != null)
                meleeEnemy.TakeDamage(_attackDamage);
            else if (distanceEnemy != null)
                distanceEnemy.TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
