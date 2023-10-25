using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvents : MonoBehaviour
{


    public void StartToPlay(int i)
    {
        if (i == 0)
            GetComponentInParent<Player>().canMove = false;
        else
        {
            GetComponentInParent<Player>().canMove = true;
            GameManager.instance.ActivateText();
        }

    }

    public void PlaySound(Sound s)
    {
        AudioManager.instance.PlayOneShot(s.name);
    }
}
