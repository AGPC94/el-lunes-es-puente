using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter()");
        if (anim != null)
            anim.SetBool("isOn", true);
    }

    void OnMouseExit()
    {
        Debug.Log("OnMouseExit()");
        if (anim != null)
            anim.SetBool("isOn", false);
    }

    public virtual void Interact()
    {
        Debug.Log("Interact()");
    }
}
