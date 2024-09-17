using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPatient : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject[] postionObject;

    public int[] onActive;

    public GameObject patient;

    public int countBed = 0;

    public void SpawnPrefabs() {

        System.Random random = new System.Random();

        int idPrefabs = random.Next(0, prefabs.Length - 1);
        int idPosition = random.Next(0, postionObject.Length - 1);

        while (onActive[idPosition] != 0)
        {
            idPosition = random.Next(0, postionObject.Length - 1);
        }

        GameObject randomPrefabs = prefabs[idPrefabs];
        Vector3 randomPosition = postionObject[idPosition].transform.position;
        onActive[idPosition] = 1;
        postionObject[idPosition].gameObject.SetActive(true);
        countBed++;

        patient = Instantiate(randomPrefabs, randomPosition, Quaternion.identity);

    }
}
