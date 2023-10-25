using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;
    Animator anim;

    [SerializeField] float xSpeed;
    [SerializeField] float ySpeedMin;
    [SerializeField] float ySpeedMax;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void OnObjectSpawn()
    {
        anim.SetBool("isHit", false);
        rb.velocity = new Vector2(Random.Range(-xSpeed, xSpeed), Random.Range(ySpeedMax, ySpeedMin));
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitZone"))
            gameObject.SetActive(false);
    }
    public void Hit()
    {
        anim.SetBool("isHit", true);
        Invoke("Desactivate", .5f);
    }


    void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
