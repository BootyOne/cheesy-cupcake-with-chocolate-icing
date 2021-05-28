using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public GameObject Hero;
    public float radius;
    public int damage;
    public bool canHit = true;
    public float hitTime;
    public LineRenderer lr;
    private void Awake()
    {
        Hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);
        foreach(var collider in colliders)
        {
            if (collider.name == "Hero" && canHit)
            {
                var collision = Physics2D.Raycast(gameObject.transform.position, (Hero.transform.position - gameObject.transform.position));
                if (collision.transform.name=="Hero")
                {
                    var heroHealth = collider.GetComponent<HeroHealth>();
                    heroHealth.TakeDamage(damage);
                    canHit = false;
                    Invoke("MakeAbleToHit", 3f);
                    Draw2DRay(gameObject.transform.position, Hero.transform.position);
                    Invoke("DisableLR", 0.1f);
                }
            }
        }
    }
    void MakeAbleToHit()
    {
        canHit = true;
    }
    void Draw2DRay(Vector2 start, Vector2 end)
    {
        lr.gameObject.SetActive(true);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

    void DisableLR()
    {
        lr.gameObject.SetActive(false);
    }
}
