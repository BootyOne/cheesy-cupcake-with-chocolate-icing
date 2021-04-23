using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject FireBallPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cast();
        }
    }

    void Cast()
    {
        Instantiate(FireBallPrefab, FirePoint.position, FirePoint.rotation);
    }
}
