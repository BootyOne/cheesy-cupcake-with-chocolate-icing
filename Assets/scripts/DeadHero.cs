using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadHero : MonoBehaviour
{
    private void Update()
    {
        if (BaseStatic.DeadHero)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            BaseStatic.DeadHero = false;
        }
    }
}
