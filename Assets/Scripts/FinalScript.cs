using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
    [Header("Reference to Boulder Rigidbody2D")]
    [SerializeField] private Rigidbody2D boulderRb;

    [Header("Force Settings")]
    [SerializeField] private Vector2 pushDirection = new Vector2(-1f, -0.5f); // down-left by default
    [SerializeField] private float pushForce = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        QuestManager.GetInstance().ResetAllQuests();

        if (boulderRb != null)
        {
            boulderRb.AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
        }
    }
}