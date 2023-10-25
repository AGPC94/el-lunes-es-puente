using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] Dialogue dialogueStart;
    [SerializeField] Dialogue dialogueEnd;
    [HideInInspector] public Player player;
    [HideInInspector] public Interactable interactableCurrent;
    public bool isStart;
    public bool isEnding;

    public float time;


    public static GameManager instance;

    [SerializeField] GameObject minigameWindow;
    [SerializeField] GameObject cameraGo;
    GameObject minigameCurrent;

    [SerializeField] Text txtTime;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        CloseMinigame();

        AudioManager.instance.PlayMusic("Ambient");
    }

    // Update is called once per frame
    void Update()
    {
        DisplayTime(time);
    }

    public void ActivateText()
    {
        txtTime.enabled = true;
    }

    public void StartGame()
    {
        if (!isStart)
        {
            player.AnimStart();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (txtTime.IsActive())
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);
            txtTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void OpenMinigame(GameObject minigame)
    {
        player.canMove = false;
        minigameWindow.SetActive(true);
        cameraGo.SetActive(true);

        minigameCurrent = minigame;
        minigameCurrent.SetActive(true);
    }

    public void CloseMinigame()
    {
        player.canMove = true;
        minigameWindow.SetActive(false);
        cameraGo.SetActive(false);

        if (minigameCurrent != null)
        {
            minigameCurrent.SetActive(false);
            minigameCurrent = null;
        }
    }

    public void SusbtractTime(float amount)
    {
        time -= amount;

        if (time <= 0 )
        {
            Final();
        }
    }

    void Final()
    {
        CloseMinigame();
        AudioManager.instance.Play("Earthquake");
        isEnding = true;
        time = 0;
        player.AnimFinal();
        FindObjectOfType<ScreenShake>().Shake();
        DialogueManager.instance.StartDialogue(dialogueEnd);
    }
}
