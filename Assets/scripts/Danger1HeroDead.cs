using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger1HeroDead : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hero" || other.gameObject.name == "legs")
        {
            BaseStatic.DeadHero = true;
        }
    }
}
