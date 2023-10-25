using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Transform originalPosition;

    [SerializeField] BasketHand hand;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //originalPosition.position = transform.localPosition;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Throw(Vector2 direction, float force)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = direction * force;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LimitZone"))
        {
            Restart();
            hand.Restart();
        }
    }

    public void Restart()
    {
        transform.position = originalPosition.position;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.SetParent(hand.transform);
    }
}
