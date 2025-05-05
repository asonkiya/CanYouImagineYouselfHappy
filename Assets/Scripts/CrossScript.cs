using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CrossScript : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionX; // freeze X only
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            rb.constraints = RigidbodyConstraints2D.None; // unfreeze all
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nun"))
        {
            Debug.Log("Entered Nun trigger — freezing X");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }

        QuestManager.GetInstance().EndQuest("nun");
        Debug.Log(QuestManager.GetInstance().NunIteration);
    }
}