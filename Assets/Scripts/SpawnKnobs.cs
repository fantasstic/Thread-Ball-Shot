using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKnobs : MonoBehaviour
{
    [SerializeField] private GameObject _knobPrefab;
    [SerializeField] private Transform _ball;
    [SerializeField] private int _minKnobs = 2;
    [SerializeField] private int _maxKnobs = 4;  
    
    private List<Vector3> _spawnPositions = new List<Vector3>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            _spawnPositions.Add(child.position); 
        }

        SpawnTargets();
    }

    public void SpawnTargets()
    {
        int numTargets = Random.Range(_minKnobs, _maxKnobs + 1);

        for (int i = 0; i < numTargets; i++)
        {
            Vector3 spawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)];
            GameObject spawnedObject = Instantiate(_knobPrefab, _ball);
            spawnedObject.transform.position = spawnPosition;
        }
    }

    public void DeleteKnobs(bool delete)
    {
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("Knob");

        if (delete)
        {
            foreach (var obj in spawnedObjects)
            {
                Destroy(obj);
            }
        }
    }
}
