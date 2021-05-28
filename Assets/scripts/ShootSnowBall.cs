using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSnowBall : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject SnowBall;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        Instantiate(SnowBall, FirePoint.position, FirePoint.rotation);
    }
}
