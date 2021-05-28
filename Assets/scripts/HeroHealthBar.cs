using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealthBar : MonoBehaviour
{
    public Image bar;
    public GameObject Hero;
    private void Start()
    {
        bar.fillAmount = 1f;
    }
    void Update()
    {
        HeroHealth heroHealth = Hero.GetComponent<HeroHealth>();
        bar.fillAmount = heroHealth.health * 0.01f;
    }
}
