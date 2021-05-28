using System.Linq;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float speed = 2000f;
    public float timeToLive = 3f;
    public Rigidbody2D BombRB;
    public GameObject Bomb;
    private Transform FirePoint;
    public Animator anim;
    public float checkRadius = 0.1f;
    public LayerMask colliders;
    private bool canHit = true;
    public int damage = 10;
    void Awake()
    {
        FirePoint = GameObject.Find("FirePoint").transform;
    }

    private void Start()
    {
        Vector2 firePointPos = new Vector2(FirePoint.transform.position.x, FirePoint.transform.position.y);
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        BombRB.velocity = (mousePos - firePointPos).normalized * speed;
        Invoke("Explode", timeToLive);
    }

    private void Explode()
    {
        var enemies = Physics2D.OverlapCircleAll(Bomb.transform.position, 0.6f).Where(x => x.tag == "eneny" || x.tag == "Player");
        foreach (var target in enemies)
        {
            var health = target.GetComponent<Health>();
            var healthOfHero = target.GetComponent<HeroHealth>();
            if (health != null)
            {
                health.TakeDamage(damage,damage,damage);
            }
            if (healthOfHero != null)
            {
                healthOfHero.TakeDamage(10);
            }
        }
        anim.SetTrigger("explode");
        Invoke("Destroy", 0.5f);
    }
    private void  Destroy()
    {
        Destroy(Bomb);
    }
}
