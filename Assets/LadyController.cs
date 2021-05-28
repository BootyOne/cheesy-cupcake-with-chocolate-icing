using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyController : MonoBehaviour
{
    public GameObject Hero;
    public GameObject Dialogue;
    public float dist =1f;

    void Update()
    {
        if ((Hero.transform.position - transform.position).magnitude < dist)
            Dialogue.SetActive(true);
        else
            Dialogue.SetActive(false);

        if (transform.position.x> Hero.transform.position.x)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else 
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
