using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderDead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Hero") BaseStatic.DeadHero = true;
    }
}
