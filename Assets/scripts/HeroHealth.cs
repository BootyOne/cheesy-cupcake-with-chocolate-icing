using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void AddHealth()
    {
        if (health + 30 > 100)
            health = 100;
        else
            health += 30;
    }

    // Update is called once per frame
    public void Die()
    {
        BaseStatic.DeadHero=true;
    }

}
