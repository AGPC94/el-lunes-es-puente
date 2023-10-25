using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogue : Interactable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] bool isSpraying;

    public override void Interact()
    {
        GameManager.instance.player.isSpraying = isSpraying;
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
