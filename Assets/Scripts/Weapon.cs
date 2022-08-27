using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private AudioSource enemyHitSoound;
    private AttackController _attackController;

    private void Start()
    {
        _attackController = transform.root.GetComponent<AttackController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if(enemyHealth != null && _attackController.IsAttack)
        {
            enemyHealth.ReduceHealth(damage);
            enemyHitSoound.Play();
        }
    }
}