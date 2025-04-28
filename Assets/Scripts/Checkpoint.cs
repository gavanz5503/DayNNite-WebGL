using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 pos = collision.transform.position;
            PlayerPrefs.SetFloat("CheckpointX", pos.x);
            PlayerPrefs.SetFloat("CheckpointY", pos.y);
            Debug.Log("Checkpoint Saved: " + pos);
        }
    }
}
