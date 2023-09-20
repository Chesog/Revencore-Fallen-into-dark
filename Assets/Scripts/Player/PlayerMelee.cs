using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _attackDamage = 10f;
    #endregion

    #region PRIVATE_FIELDS
    private Vector3 sphereCenter;
    #endregion

    #region UNITY_CALLS
    private void Update()
    {
        sphereCenter = transform.position + transform.right * _attackRange;

        if (Input.GetKeyDown(KeyCode.F))
            MeleeAttack();
    }
    #endregion

    #region PRIVATE_METHODS
    private void MeleeAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(sphereCenter, _attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyInputManager meleeEnemy = enemy.GetComponent<EnemyInputManager>();
            DistanceEnemy distanceEnemy = enemy.GetComponent<DistanceEnemy>();

            if (meleeEnemy != null)
                meleeEnemy.OnTakeDamage();
            else if (distanceEnemy != null)
                distanceEnemy.TakeDamage(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(sphereCenter, _attackRange);
    }
    #endregion
}
