using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 0.25f;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * speed) * height; 
    }
}
