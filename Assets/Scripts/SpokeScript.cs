using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpokeScript : MonoBehaviour
{
    [SerializeField] private Vector2 _throwForce;

    private bool _isActive = true;
    private Rigidbody2D _rb;
    private BoxCollider2D _spokeCollider;
    private GameUI _gameUI;
    private Button _pauseButton;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spokeCollider = GetComponent<BoxCollider2D>();
        _pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        _gameUI = GameController.Instance.GameUI;
    }

    private bool IsPointerOverUIObject()
    {
        return EventSystem.current.IsPointerOverGameObject() || (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Debug.Log(IsPointerOverUIObject());

        if (!_gameUI.IsPaused && Input.GetMouseButtonDown(0) && _isActive && !IsPointerOverUIObject())
        {
            _rb.AddForce(_throwForce, ForceMode2D.Impulse);
            _rb.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDisplayedSpokeCount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_gameUI.IsPaused)
            return;

        if (collision.tag == "Knob")
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            GameController.Instance.GameUI.UpdateScore();
            collision.gameObject.GetComponent<Animator>().SetTrigger("ScoreChange");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_isActive)
            return;

        _isActive = false;

        if (collision.collider.tag == "Ball")
        {
            GetComponent<ParticleSystem>().Play();

            _rb.velocity = new Vector2(0, 0);
            _rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);

            _spokeCollider.offset = new Vector2(_spokeCollider.offset.x, -0.4f);
            _spokeCollider.size = new Vector2(_spokeCollider.size.x, 1.2f);

            GameController.Instance.OnSuccessfulKnifeHit();
        }
        else if (collision.collider.tag == "Spoke")
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -2);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
