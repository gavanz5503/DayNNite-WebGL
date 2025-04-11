using UnityEngine;
using System.Collections;

public class WorldSwitcher : MonoBehaviour
{
    public ScreenFader screenFader; // Drag this in the inspector
    public GameObject[] dayObjects;
    public GameObject[] niteObjects;
    private bool isDay = true;
    private bool isSwitching = false;
    public GameObject backgroundDay;
    public GameObject backgroundNite;

    void Start()
    {
        SetInitialWorld();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(screenFader.FadeOutIn(ToggleWorld));
        }
    }

    IEnumerator SwitchWorld()
    {
        isSwitching = true;
        yield return StartCoroutine(screenFader.FadeOutIn(ToggleWorld));
        isSwitching = false;
    }

    void ToggleWorld()
    {
        isDay = !isDay;

        foreach (GameObject obj in dayObjects)
        {
            bool isEnemy = obj.CompareTag("Enemy");
            obj.SetActive(isDay || isEnemy);

            if (isEnemy)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Collider2D col = obj.GetComponent<Collider2D>();

                if (sr) sr.enabled = isDay;
                if (col) col.enabled = isDay;
            }
        }

        foreach (GameObject obj in niteObjects)
        {
            bool isEnemy = obj.CompareTag("Enemy");
            obj.SetActive(!isDay || isEnemy);

            if (isEnemy)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Collider2D col = obj.GetComponent<Collider2D>();

                if (sr) sr.enabled = !isDay;
                if (col) col.enabled = !isDay;
            }
        }

        backgroundDay.SetActive(isDay);
        backgroundNite.SetActive(!isDay);

        Camera.main.backgroundColor = isDay ? Color.cyan : Color.black;

        FindObjectOfType<AudioManager>().PlaySwitch();
    }


    void SetInitialWorld()
    {
        isDay = true; // or false if I want to start in Nite mode

        foreach(GameObject obj in dayObjects)
            obj.SetActive(true);

        foreach(GameObject obj in niteObjects)
            obj.SetActive(false);

        backgroundDay.SetActive(true);
        backgroundNite.SetActive(false);

        // Set camera background color to match Day mode
        Camera.main.backgroundColor = Color.cyan;
    }
}
