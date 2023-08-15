using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    [SerializeField] private int _spokeCount;
    [SerializeField] private SpawnKnobs _knobsSpawner;

    [Header("Knife Spawning")]
    [SerializeField] private Vector2 _spokeSpawnPosition;
    [SerializeField] private GameObject _spokeObject;

    private bool _shouldDeleteObjects = false;
    private GameObject _lastSpawnedSpoke;

    public GameUI GameUI { get; private set; }
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedSpokeCount(_spokeCount);
        GameUI.UpdateScore(0);
        SpawnKnife();
    }

    public void OnSuccessfulKnifeHit()
    {
        if (_spokeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }

    public void ToggleLastSpawnedSpoke(bool isActive)
    {
        if (_lastSpawnedSpoke != null)
        {
            _lastSpawnedSpoke.SetActive(isActive);
        }
    }

    private void SpawnKnife()
    {
        if (_shouldDeleteObjects)
        {
            GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("Spoke");
            foreach (var obj in spawnedObjects)
            {
                Destroy(obj);
            }
            _shouldDeleteObjects = false;
        }

        _spokeCount--;
        _lastSpawnedSpoke = Instantiate(_spokeObject, _spokeSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            
            yield return new WaitForSecondsRealtime(0.3f);
            _shouldDeleteObjects = true;
            _spokeCount = 8;
            GameUI.SetSpokeIconIndex(7);
            GameUI.ClearBar();
            GameUI.SetInitialDisplayedSpokeCount(_spokeCount);
            OnSuccessfulKnifeHit();
            _knobsSpawner.DeleteKnobs(true);
            _knobsSpawner.SpawnTargets();

        }
        else
        {
            GameUI.ShoGameOverScreen();
        }
    }

  
}
