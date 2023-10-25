using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMinigame : Interactable
{
    [SerializeField] GameObject minigame;
    public override void Interact()
    {
        GameManager.instance.OpenMinigame(minigame);
    }
}
