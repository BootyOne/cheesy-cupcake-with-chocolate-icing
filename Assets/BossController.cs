using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject Hero;

    public float speed;
    public Rigidbody2D rb;

    public float coefficientFire;
    public float coefficientIce;
    public float coefficientEarth;

    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float spawnY;

    public Vector2 startvect; 

    public float accelerationTime = 2f;
    public float maxSpeed = 5f;
    private Vector2 movement;
    private float timeLeft;

    public List<Vector3> spawnList;

    private bool canhit = true;

    public GameObject Golem;
    public GameObject Bat;
    public GameObject Slime;
    public GameObject Mushroom;
    public GameObject GolemShot;
    public GameObject Shot;


    private void Awake()
    {
        spawnList.Add(new Vector3(GameObject.Find("pos1").transform.position.x, GameObject.Find("pos1").transform.position.y, GameObject.Find("pos1").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos2").transform.position.x, GameObject.Find("pos2").transform.position.y, GameObject.Find("pos2").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos3").transform.position.x, GameObject.Find("pos3").transform.position.y, GameObject.Find("pos3").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos4").transform.position.x, GameObject.Find("pos4").transform.position.y, GameObject.Find("pos4").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos5").transform.position.x, GameObject.Find("pos5").transform.position.y, GameObject.Find("pos5").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos6").transform.position.x, GameObject.Find("pos6").transform.position.y, GameObject.Find("pos6").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos7").transform.position.x, GameObject.Find("pos7").transform.position.y, GameObject.Find("pos7").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos8").transform.position.x, GameObject.Find("pos8").transform.position.y, GameObject.Find("pos8").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos9").transform.position.x, GameObject.Find("pos9").transform.position.y, GameObject.Find("pos9").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos10").transform.position.x, GameObject.Find("pos10").transform.position.y, GameObject.Find("pos10").transform.position.z));
        spawnList.Add(new Vector3(GameObject.Find("pos11").transform.position.x, GameObject.Find("pos11").transform.position.y, GameObject.Find("pos11").transform.position.z));

        startvect = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)).normalized;
    }


    private void Update()
    {
        var attackNum = Random.Range(0, 6);

        if (canhit)
        {
            if (attackNum == 0)
            {
                SpawnSlimes();
            }
            if (attackNum == 1)
            {
                SpawnBats();
            }
            if (attackNum == 2)
            {
                SpawnGolems();
            }
            if (attackNum == 3)
            {
                SpawnMushrooms();
            }
            if (attackNum == 4)
            {
                ShootGolemShots();
            }
            if (attackNum == 5)
            {
                ShootStandard();
            }
            canhit = false;
            Invoke("MakeAbleToHit", 10f);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }
    private void MakeAbleToHit()
    {
        canhit = true;
    }

    private void Move()
    {

    }

    private void CountCoefs()
    {

    }
    
    private void SpawnSlimes()
    {
        var rnd = new System.Random();
        for (var i = 0; i < 5; i++)
        {
            var trans = spawnList[rnd.Next(0, spawnList.Count)];
            Instantiate(Slime, trans,transform.rotation);
        }
    }

    private void SpawnGolems()
    {
        var rnd = new System.Random();
        for (var i = 0; i < 2; i++)
        {
            var trans = spawnList[rnd.Next(0, spawnList.Count)];
            Instantiate(Golem, trans, transform.rotation);
        }
    }

    private void SpawnBats()
    {
        var rnd = new System.Random();
        for (var i = 0; i < 3; i++)
        {
            var trans = spawnList[rnd.Next(0, spawnList.Count)];
            Instantiate(Bat, trans, transform.rotation);
        }
    }

    private void SpawnMushrooms()
    {
        var rnd = new System.Random();
        for (var i = 0; i < 2; i++)
        {
            var trans = spawnList[rnd.Next(0, spawnList.Count)];
            Instantiate(Mushroom, trans, transform.rotation);
        }
    }

    private void ShootGolemShots()
    {
        var rnd = new System.Random();
        for (var i = 0; i < 4; i++)
        {
            var trans = spawnList[rnd.Next(0, spawnList.Count)];
            Instantiate(GolemShot, trans, transform.rotation);
        }
    }

    private void ShootStandard()
    {
        for (var i = 0; i < 5; i++)
        {
            Invoke("CreateShot", i * 0.01f);
        }
    }
    private void CreateShot()
    {
        Instantiate(Shot, transform.position, transform.rotation);

    }
    private void Laser()
    {

    }
}
