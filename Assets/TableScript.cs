using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    public GameObject Hero;
    public GameObject Dialogue;
    public float dist = 1f;

    void Update()
    {
        if ((Hero.transform.position - transform.position).magnitude < dist)
            Dialogue.SetActive(true);
        else
            Dialogue.SetActive(false);

    }
}
