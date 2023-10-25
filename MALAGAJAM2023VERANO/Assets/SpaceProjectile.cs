using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceProjectile : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnObjectSpawn()
    {
        rb.velocity = Vector2.up * speed;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitZone"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            Meteor m = collision.GetComponent<Meteor>();

            m.Hit();

            gameObject.SetActive(false);
        }
    }
}
