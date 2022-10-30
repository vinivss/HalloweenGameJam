using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int health;
    int maxHealth;

    public int SceneNumber;

    public HealthBar healthBar;
    void Start()
    {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
    }
    void Update()
    {
        if (health <= 0)
        {
            GameOver();
        }
        healthBar.SetHealth(health);
    }

    void GameOver()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
    }
}
