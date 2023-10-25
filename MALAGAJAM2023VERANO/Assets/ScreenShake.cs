using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float amount;

    public void Shake()
    {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + amount, transform.position.z);
            yield return null;
            transform.position = new Vector3(transform.position.x, transform.position.y - amount, transform.position.z);
            yield return null;
        }
    }
}
