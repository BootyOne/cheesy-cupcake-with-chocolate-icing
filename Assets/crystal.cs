using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystal : MonoBehaviour
{
    public GameObject Hero;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Hero")
        {
            var death = Hero.GetComponent<HeroHealth>();
            death.Die();
        }
    }
}
