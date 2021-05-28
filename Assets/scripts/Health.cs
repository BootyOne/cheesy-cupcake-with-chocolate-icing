using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int FireCoef;
    public int IceCoef;
    public int EarthCoef;
    public float health = 100;

    
    public void TakeDamage(int fire, int ice, int earth)
    {
        health -= (float)0.01*(fire * FireCoef + ice * IceCoef + earth * EarthCoef);
        if (health <= 0)
        {
            Die();
        }
    }

    public void AddHealth()
    {
        health += 30;
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
