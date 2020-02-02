﻿using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class restartwhenfail : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private new Collider2D collider;
    [SerializeField]
    private freezeoncollide collide;
    [SerializeField]
    private Transform arrows;
    [SerializeField]
    private GameObject victoryscreen;
    [SerializeField]
    private GameObject seed;
    [SerializeField]
    private bool hasSeed = true;
    [SerializeField]
    private AudioSource audio;
    [SerializeField]
    private AudioClip[] walking;
    [SerializeField]
    private AudioClip restart;
    [SerializeField]
    private AudioClip win;
    [SerializeField]
    private ParticleSystem walkParticles;
    [SerializeField]
    private float dragThreshold = 30;
    private bool canMove = true;

    private Vector2 downPosition = Vector2.zero;

    private void OnValidate()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        arrows.position = transform.position + Vector3.up;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            downPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float delta = Input.mousePosition.x - downPosition.x;
            if (delta > 0 && delta >= dragThreshold)
            {
                TryMoveRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryMoveRight();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canMove == false)
        {
            canMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("You won!");
            Time.timeScale = 0;
            victoryscreen.SetActive(true);
            audio.PlayOneShot(win);
            return;
        }
        if (collision.gameObject.tag == "Respawn")
        {
            if (hasSeed)
            {
                hasSeed = false;
                seed.SetActive(false);
                var item = collision.gameObject.GetComponentInParent<freezeoncollide>();
                item.growZone.SetActive(false);
                item.child.SetActive(true);
            }
            return;
        }
        body.velocity = Vector2.zero;
        transform.position = start.position;
        collide?.child.SetActive(true);
        arrows.position = transform.position + Vector3.up;
        hasSeed = true;
        seed.SetActive(true);
        audio.PlayOneShot(restart);
    }

    private void ResetArrowPosition()
    {
        arrows.position = transform.position + Vector3.up;
    }

    private void TryMoveRight()
    {
        if (canMove == false)
        {
            Debug.Log("can't move yet");
            return;
        }
        canMove = false;
        walkParticles.Play();
        audio.PlayOneShot(walking[Random.Range(0, 3)]);
        LeanTween.moveX(gameObject, transform.position.x + 1, 0.25f).setOnComplete(ResetArrowPosition);
    }
}
