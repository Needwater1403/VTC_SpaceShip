using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AsteroidGenerator : MonoBehaviour
{
    public float spawnRange;
    public int asteroidAmountToSpawn;
    public int totalAmountToSpawn;
    private Vector3 spawnPoint;
    public GameObject asteroid;
    public GameObject bomb;
    public float startSafeRange;
    private List<GameObject> objectsToPlace = new List<GameObject>();
    private int[] numArr;
    void Start()
    {
        numArr = new int[asteroidAmountToSpawn];
        for (int i = 0; i < asteroidAmountToSpawn; i++)
        {
            numArr[i] = Random.Range(1, totalAmountToSpawn);
        }
        for (int i = 0; i < totalAmountToSpawn; i++)
        {
            var s = Array.Find(numArr, sound => sound == i);
            PickSpawnPoint();
            while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
            {
                PickSpawnPoint();
            }
            if(s!=0) objectsToPlace.Add(Instantiate(asteroid, spawnPoint, Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
            else objectsToPlace.Add(Instantiate(bomb, spawnPoint, Quaternion.Euler(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
            objectsToPlace[i].transform.parent = this.transform;
        }
    }

    private void PickSpawnPoint()
    {
        spawnPoint = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));
        if(spawnPoint.magnitude > 1)
        {
            spawnPoint.Normalize();
        }
        spawnPoint *= spawnRange;
    }
}

