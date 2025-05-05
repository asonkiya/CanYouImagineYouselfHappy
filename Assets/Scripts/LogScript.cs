using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogScript : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition; // freeze movement only
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.None; // unfreeze everything
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Builder"))
        {
            Debug.Log("Entered Nun trigger — freezing position");
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        QuestManager.GetInstance().EndQuest("builder");
        Debug.Log(QuestManager.GetInstance().NunIteration);
    }
}
