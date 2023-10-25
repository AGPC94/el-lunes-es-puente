using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] float timeSpawn;

    [SerializeField] float xPosSpawn = 8.5f;
    [SerializeField] float yPosSpawn = 4.5f;

    void OnEnable()
    {
        StartCoroutine(SpawnMeteors());
    }

    void OnDisable()
    {
        Meteor[] meteors = FindObjectsOfType<Meteor>();
        foreach (var item in meteors)
            item.gameObject.SetActive(false);
        StopCoroutine(SpawnMeteors());
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);
            ObjectPooler.instance.SpawnFromPool("Meteor", new Vector2(Random.Range(-xPosSpawn, xPosSpawn), yPosSpawn), Quaternion.identity, true);
        }
    }

    public void Restart()
    {
        StopCoroutine(SpawnMeteors());

        Meteor[] meteors = FindObjectsOfType<Meteor>();
        foreach (var item in meteors)
            item.gameObject.SetActive(false);

        StartCoroutine(SpawnMeteors());
    }
}
