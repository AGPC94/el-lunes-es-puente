using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogueManager : MonoBehaviour
{
    //[SerializeField] Text txtName;
    [SerializeField] Text txtDialogue;
    [SerializeField] Animator animator;
    [SerializeField] float timeMinus = 1;

    [SerializeField] Dialogue dialogueStart;

    Queue<string> sentences;

    public bool isOpen;
    public static DialogueManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();

        StartDialogue(dialogueStart);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting dialogue with " + dialogue.name);

        if (isOpen)
            return;


        sentences.Clear();

        isOpen = true;

        GameManager.instance.player.canMove = false;

        animator.SetBool("isOpen", isOpen);

        //txtName.text = dialogue.name;

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentences(sentence));
    }

    IEnumerator TypeSentences(string sentence)
    {
        txtDialogue.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            txtDialogue.text += letter;
            AudioManager.instance.PlayOneShot("Text"); 
             yield return null;
        }
    }

    void EndDialogue()
    {
        isOpen = false;
        GameManager.instance.player.canMove = true;
        GameManager.instance.player.isSpraying = false;

        animator.SetBool("isOpen", isOpen);

        GameManager.instance.StartGame();

        Debug.Log("End conversation");

        if (GameManager.instance.isEnding)
        {
            SceneManager.LoadScene("Ending");
        }

        GameManager.instance.SusbtractTime(timeMinus);

    }

}
