using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image bar;
    public GameObject obj;
    private float health;
    private float fullHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = obj.GetComponent<Health>().health;
        fullHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        health = obj.GetComponent<Health>().health;

        bar.fillAmount = health/fullHealth;
    }
}
