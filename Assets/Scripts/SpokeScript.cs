using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpokeScript : MonoBehaviour
{
    [SerializeField] private Vector2 _throwForce;

    private bool _isActive = true;
    private Rigidbody2D _rb;
    private BoxCollider2D _spokeCollider;
    private int _score;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spokeCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isActive)
        {
            _rb.AddForce(_throwForce, ForceMode2D.Impulse);
            _rb.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDisplayedSpokeCount();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Knob")
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            _score++;
            GameController.Instance.GameUI.UpdateScore(_score);
            Debug.Log(_score);
            collision.gameObject.GetComponent<Animator>().SetTrigger("ScoreChange");
            /*collision.gameObject.SetActive(false);*/
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
