using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Animator _animator;
    [SerializeField] private float totalhealth = 200f;
    [SerializeField] private GameObject gamOverCanvas;
    [SerializeField] private AudioSource enemyHitSound;

    private float _health;
    private void Start()
    {
        _health = totalhealth;
        InitHealth();
    }

    public void ReduceHealth(float damage)
    {
        InitHealth();
        _health -= damage;
        enemyHitSound.Play();
        _animator.SetTrigger("takeDamage");
        if (_health <= 0f)
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
        gamOverCanvas.SetActive(true);

    }
}
