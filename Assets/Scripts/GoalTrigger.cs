using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    private bool levelComplete = false;

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (levelComplete) return;

        if (other.CompareTag("Player"))
        {
            if (!AllGemsCollected()) return;

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null && rb.linearVelocity.y <= 0f) // Player is falling
            {
                float playerBottom = other.bounds.min.y;
                float platformTop = GetComponent<Collider2D>().bounds.max.y;

                if (playerBottom > platformTop - 0.1f) // Approaching from above 
                {
                    levelComplete = true;
                    FindObjectOfType<LevelManager>().LevelComplete();
                }
            }
        }
    }

    private bool AllGemsCollected()
    {
        return GameObject.FindGameObjectsWithTag("Gem").Length == 0;
    }
}
