using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;
    private int playerTouchCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Locked by default
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerTouchCount++;
            rb.bodyType = RigidbodyType2D.Dynamic; // Enable movement
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerTouchCount--;
            if (playerTouchCount <= 0)
            {
                rb.bodyType = RigidbodyType2D.Static; // Lock back
            }
        }
    }
}