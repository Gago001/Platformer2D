using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float totalhealth = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator _animator;
    private float _health;

    private void Start()
    {
        _health = totalhealth;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        _health -= damage;
        InitHealth();
        _animator.SetTrigger("takeDamage");
        if(_health <= 0f)
        {
            Die();
        }
    }
    private void InitHealth()
    {
        healthSlider.value = _health / totalhealth;
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
