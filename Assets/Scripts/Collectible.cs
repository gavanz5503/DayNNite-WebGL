using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GemCounterUI>().AddGem();
            FindObjectOfType<AudioManager>().PlayGem();
            Destroy(gameObject); // LAST thing you call
        }
    }
}

