using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketHand : MonoBehaviour
{
    [SerializeField] float timeMinus;

    [Header("Aim")]
    [SerializeField] float speed;
    [SerializeField] float angleMax;
    [SerializeField] float angleMin;

    [SerializeField] BasketBall ball;

    [Header("Hold")]
    [SerializeField] float forceCurrent;
    [SerializeField] float forceIncrease;
    [SerializeField] float forceMax;

    enum HandStates { AIM, HOLD, THROW};

    HandStates state;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = HandStates.AIM;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case HandStates.AIM:
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, Mathf.PingPong(Time.time * speed, angleMax) - angleMin);
                if (Input.GetMouseButtonDown(0))
                {
                    state = HandStates.HOLD;
                    anim.SetTrigger("hold");
                }

                break;

            case HandStates.HOLD:
                if (Input.GetMouseButton(0))
                {
                    forceCurrent += Time.deltaTime * forceIncrease;
                }

                //Throw ball at up hand direction on forceCurrent
                if (Input.GetMouseButtonUp(0) || forceCurrent >= forceMax)
                {
                    state = HandStates.THROW;

                    anim.SetTrigger("throw");

                    if (forceCurrent >= forceMax)
                        forceCurrent = forceMax;

                    ball.Throw(transform.up, forceCurrent);

                    ball.transform.SetParent(null);

                    forceCurrent = 0;

                    GameManager.instance.SusbtractTime(timeMinus);
                    AudioManager.instance.Play("ThrowBall");
                }

                break;

            case HandStates.THROW:

                break;

        }

    }

    public void Restart()
    {
        state = HandStates.AIM;
        anim.SetTrigger("aim");
    }
}
