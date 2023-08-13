using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    [SerializeField] private int _spokeCount;

    [Header("Knife Spawning")]
    [SerializeField] private Vector2 _spokeSpawnPosition;
    [SerializeField] private GameObject _spokeObject;

    //reference to the GameUI on GameController's game object
    public GameUI GameUI { get; private set; }
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        //simple kind of a singleton instance (we're only in 1 scene)
        Instance = this;

        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        //update the UI as soon as the game starts
        GameUI.SetInitialDisplayedSpokeCount(_spokeCount);
        GameUI.UpdateScore(0);
        //also spawn the first knife
        SpawnKnife();
    }

    //this will be called from KnifeScript
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

    //a pretty self-explanatory method
    private void SpawnKnife()
    {
        _spokeCount--;
        Instantiate(_spokeObject, _spokeSpawnPosition, Quaternion.identity);
    }

    //the public method for starting game over
    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    //this is a coroutine because we want to wait for a while when the player wins
    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            
            yield return new WaitForSecondsRealtime(0.3f);
            _spokeCount = 8;
            GameUI.ClearBar();
            GameUI.SetInitialDisplayedSpokeCount(_spokeCount);
            OnSuccessfulKnifeHit();

        }
        else
        {
            GameUI.ShoGameOverScreen();
        }
    }

  
}
