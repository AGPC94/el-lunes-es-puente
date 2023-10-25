using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    bool isInteracting;
    public bool canMove;
    public bool isSpraying;

    NavMeshAgent agent;
    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        Anim();

        if (!canMove)
            return;
        PointAndClick();
        Interact();

    }

    public void AnimStart()
    {
        anim.SetTrigger("Start");
    }
    public void AnimFinal()
    {
        canMove = false;
        anim.SetTrigger("Final");
    }


    void PointAndClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            agent.SetDestination(mouseWorldPosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("PLAYER click the object: " + hit.collider.gameObject.name);

                if (hit.collider.CompareTag("Interactable"))
                {
                    isInteracting = true;
                    GameManager.instance.interactableCurrent = hit.collider.GetComponent<Interactable>();
                }
            }
            else
            {
                isInteracting = false;
                GameManager.instance.interactableCurrent = null;
            }
        }
    }

    void Interact()
    {
        //Check if player is stopped in front of interactable and then interact
        if (isInteracting)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        canMove = false;
                        isInteracting = false;
                        GameManager.instance.interactableCurrent.Interact();
                    }
                }
            }
        }
    }

    void Anim()
    {
        anim.SetFloat("speedX", agent.velocity.x);
        anim.SetBool("isWalking", agent.velocity.sqrMagnitude != 0f);
        anim.SetBool("isSpraying", isSpraying);
    }

    

}
