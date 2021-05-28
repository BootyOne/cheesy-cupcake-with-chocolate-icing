using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogEnemy : MonoBehaviour
{
    public GameObject Hero;
    public Transform FirePoint;
    Transform m_transform;
    public LineRenderer m_lineRenderer;
    [SerializeField] private float defDistaceRay = 100f;
    RaycastHit hit;

    void Awake()
    {
        m_transform = GetComponent<Transform>();
        Hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        var distance = FindDistToHero();
        if (distance < 500)
            FireTongue();
    }
    float FindDistToHero()
    {
        var heroPos = Hero.transform.position;
        var FrogPos = FirePoint.position;
        var distance = (heroPos - FrogPos).magnitude;
        return distance;
    }
    void FireTongue()
    {
        if (Physics.Linecast(FirePoint.transform.position, Hero.transform.position, out hit))
        {
            if ((hit.transform.position - Hero.transform.position).magnitude<100)
            {
                var heroHealth = hit.transform.GetComponent<HeroHealth>();
                if (heroHealth == null)
                    Debug.Log("@@");
                heroHealth.TakeDamage(5);
            }
        } 
    }
    }
