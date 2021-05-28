using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    private StringBuilder spellsList;

    public Transform FirePoint;
    public GameObject Fireball;
    public GameObject SnowBall;
    public GameObject Hero;
    private bool isSpeededUp = false;
    public Animator animator;
    public GameObject rock;
    public float pushForce;
    public GameObject Bomb;

    public GameObject Fire1;
    public GameObject Ice1;
    public GameObject Aura1;
    public GameObject Fire2;
    public GameObject Ice2;
    public GameObject Aura2;
    public GameObject Fire3;
    public GameObject Ice3;
    public GameObject Aura3;
    public GameObject Fire4;
    public GameObject Ice4;
    public GameObject Aura4;
    public GameObject Fire5;
    public GameObject Ice5;
    public GameObject Aura5;
    public GameObject Earth1;
    public GameObject Earth2;
    public GameObject Earth3;
    public GameObject Earth4;
    public GameObject Earth5;

    private List<GameObject> ElementsList;

    private void Awake()
    {
        spellsList = new StringBuilder();
        ElementsList = new List<GameObject> { Fire1, Fire2, Fire3, Fire4, Fire5, Ice1, Ice2, Ice3, Ice4, Ice5, Aura1, Aura2, Aura3, Aura4, Aura5, Earth1, Earth2, Earth3, Earth4, Earth5 };
    }

    void Update()
    {
        if (spellsList.Length < 5)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                spellsList.Append("F");
                ShowElement("Fire");
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                spellsList.Append("I");
                ShowElement("Ice");
            }

            else if (Input.GetKeyDown(KeyCode.V))
            {
                spellsList.Append("A");
                ShowElement("Aura");
            }

            else if (Input.GetKeyDown(KeyCode.R))
            {
                spellsList.Append("E");
                ShowElement("Earth");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var spell = spellsList.ToString();
            animator.SetTrigger("cast");
            if (spell == "F")
            {
                ShootFireBall();
            }

            if (spell == "EE")
            {
                ShootRock();
            }

            if (spell == "FAF")
            {
                Invoke("ShootFireBall", 0.15f);
                ShootFireBall();
            }

            if (spell == "FIAA")
            {
                if (!isSpeededUp)
                {
                    isSpeededUp = true;
                    SpeedPlayerUp();
                }
            }

            if (spell == "FAFAF")
            {
                Invoke("ShootFireBall", 0.15f);
                Invoke("ShootFireBall", 0.3f);
                ShootFireBall();
            }

            if (spell == "I")
            {
                ShootSnowBall();
            }

            if (spell == "AAAA")
            {
                HeroHealth health = Hero.GetComponent<HeroHealth>();
                health.AddHealth();
            }
            if (spell == "EA")
            {
                PushEnemiesAround();
            }
            if (spell == "EAI")
            {
                PushEnemiesAroundAndFreeze();
            }
            if (spell == "FE")
            {
                CreateBomb();
            }
            spellsList.Clear();
            foreach(var element in ElementsList)
                element.SetActive(false);
        }
        

    }

    private void ShootFireBall()
    {
        Instantiate(Fireball, FirePoint.position, FirePoint.rotation);
    }

    private void ShootRock()
    {
        Instantiate(rock, FirePoint.position, FirePoint.rotation);
    }

    private void ShootSnowBall()
    {
        Instantiate(SnowBall, FirePoint.position, FirePoint.rotation);
    }

    private void SpeedPlayerUp()
    {
        var playerController = Hero.GetComponent<PlayerController>();
        playerController.SpeedPlayerUp();
    }

    private void PushEnemiesAround()
    {
        var enemies = Physics2D.OverlapCircleAll(FirePoint.position, 3f).Where(x => x.tag == "eneny");
        foreach (var enemy in enemies)
        {
            Debug.Log(enemy.transform.name);
            var toEnemy = enemy.transform.position - Hero.transform.position;
            if (enemy.transform.name == "FlyingEnemy")
                enemy.attachedRigidbody.AddForce(toEnemy.normalized * pushForce*7);
            else
                enemy.attachedRigidbody.AddForce(toEnemy.normalized * pushForce);
        }
    }

    private void CreateBomb()
    {
        Instantiate(Bomb, FirePoint.position, FirePoint.rotation);
    }
    private void PushEnemiesAroundAndFreeze()
    {
        var enemies = Physics2D.OverlapCircleAll(FirePoint.position, 5f).Where(x => x.tag == "eneny");
        foreach (var enemy in enemies)
        {
            Debug.Log(enemy.transform.name);
            
            var toEnemy = enemy.transform.position - FirePoint.position;
            enemy.attachedRigidbody.AddForce(toEnemy.normalized * pushForce);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            var batController = enemy.GetComponent<EnemyAI>();

            if (batController != null)
            {
                batController.Freeze();
            }
            if (enemyController != null)
            {
                enemyController.Freeze();
            }
        }
    }


    private void ShowElement(string element)
    {
        if (spellsList.Length == 1)
        {
            if (element == "Fire")
                Fire1.SetActive(true);
            if (element == "Aura")
                Aura1.SetActive(true);
            if (element == "Ice")
                Ice1.SetActive(true);
            if (element == "Earth")
                Earth1.SetActive(true);
        }
        if (spellsList.Length == 2)
        {
            if (element == "Fire")
                Fire2.SetActive(true);
            if (element == "Aura")
                Aura2.SetActive(true);
            if (element == "Ice")
                Ice2.SetActive(true);
            if (element == "Earth")
                Earth2.SetActive(true);
        }
        if (spellsList.Length == 3)
        {
            if (element == "Fire")
                Fire3.SetActive(true);
            if (element == "Aura")
                Aura3.SetActive(true);
            if (element == "Ice")
                Ice3.SetActive(true);
            if (element == "Earth")
                Earth3.SetActive(true);

        }
        if (spellsList.Length == 4)
        {
            if (element == "Fire")
                Fire4.SetActive(true);
            if (element == "Aura")
                Aura4.SetActive(true);
            if (element == "Ice")
                Ice4.SetActive(true);
            if (element == "Earth")
                Earth4.SetActive(true);
        }
        if (spellsList.Length == 5)
        {
            if (element == "Fire")
                Fire5.SetActive(true);
            if (element == "Aura")
                Aura5.SetActive(true);
            if (element == "Ice")
                Ice5.SetActive(true);
            if (element == "Earth")
                Earth5.SetActive(true);
        }
    }
}
