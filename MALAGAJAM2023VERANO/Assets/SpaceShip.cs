using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;
    Animator anim;

    public bool canMove;

    [SerializeField] float maxMoveX = 8.5f;
    [SerializeField] float maxMoveY = 4.5f;
    [SerializeField] float timeMinus;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isHit", !canMove);

        if (!canMove)
            return;

        Movement();
        Shot();
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.SusbtractTime(timeMinus);
            ObjectPooler.instance.SpawnFromPool("Projectile", transform.position, Quaternion.identity);
        }
    }

    void Movement()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localPosition = mouseWorldPosition;

        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x, -maxMoveX, maxMoveX), Mathf.Clamp(transform.localPosition.y, -maxMoveY, maxMoveY));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meteor"))
        {
            canMove = false;
            collision.gameObject.SetActive(false);
            Invoke("Restart", 3);
        }
    }


    void Restart()
    {
        canMove = true;
        if (FindObjectOfType<MeteorSpawner>() != null)
            FindObjectOfType<MeteorSpawner>().Restart();
    }
}
