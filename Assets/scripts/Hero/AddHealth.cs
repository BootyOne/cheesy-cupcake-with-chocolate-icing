using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public GameObject hero;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var health = hero.GetComponent<HeroHealth>();
            health.AddHealth();
        }
    }
}
