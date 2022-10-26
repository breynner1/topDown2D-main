using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    Vector3 enemigo;
    public Bullet bulletPrefab;
    public float bulletForce = 20f;
    public float shootingInterval ;
    private float period = 0.0f;
    public int daño;
    private bool startShotting = false,detection = false;
    private Rigidbody2D rb;
    Player jugador;
    GameObject enemi;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador= GetComponent<Player>();
    }

    //https://www.youtube.com/watch?v=LNLVOjbrQj4




    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "PowerSource" || collision.gameObject.tag == "Tower")&& gameObject.tag=="Player"&& collision.isTrigger==false &&(detection == false|| enemi==collision.gameObject) )
        {
            jugador.startMoving = false;
            enemi = collision.gameObject;
            detection = true;
            startShotting = true;
            lookAtTarget(collision.transform);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Tower" && collision.isTrigger == false && (detection == false || enemi == collision.gameObject))
        {
            jugador.startMoving = false;
            enemi = collision.gameObject;
            detection = true;
            startShotting = true;
            lookAtTarget(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "PowerSource" || collision.gameObject.tag == "Tower") && gameObject.tag == "Player" && collision.isTrigger == false && enemi == collision.gameObject)
        {
            jugador.startMoving = true;
            startShotting = false;
            enemi = null;
            detection = false;
            lookAtTarget(collision.transform);
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Tower" && collision.isTrigger == false && enemi == collision.gameObject)
        {
            jugador.startMoving = true;
            startShotting = false;
            enemi = null;
            detection = false;
            lookAtTarget(collision.transform);
        }
    }



    public void lookAtTarget(Transform W)
    {
        Vector2 lookDir = new Vector2(W.position.x, W.position.y) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    void Update()
    {
        if (!startShotting)
            return;

        if (period > shootingInterval)
        {
            shoot();
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    private void shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.name = ""+ daño;
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet.GetComponent<GameObject>(), 3);
    }
}
