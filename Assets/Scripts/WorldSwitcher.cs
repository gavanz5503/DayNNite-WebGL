using System.Collections;
using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public bool isPeeking = false;
    public ScreenFader screenFader;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(screenFader.FadeOutIn(ToggleWorld));
        }

        // Peek Logic
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPeeking = true;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            isPeeking = false;
        }

        UpdateEnemyVisibility();
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
            if (!obj.CompareTag("Enemy"))
            {
                obj.SetActive(isDay); // Day platforms active in Day
            }
        }

        foreach (GameObject obj in niteObjects)
        {
            if (!obj.CompareTag("Enemy"))
            {
                obj.SetActive(!isDay); // Nite platforms active in Nite
            }
        }

        backgroundDay.SetActive(isDay);
        backgroundNite.SetActive(!isDay);

        Camera.main.backgroundColor = isDay ? Color.cyan : Color.black;

        PushPlayerOutIfOverlapping();
        UpdateEnemyVisibility();

        FindObjectOfType<AudioManager>().PlaySwitch();
    }

    void SetInitialWorld()
    {
        isDay = true;

        foreach (GameObject obj in dayObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.SetActive(true); // Always keep enemies active
            }
            else
            {
                obj.SetActive(true); // Day platforms start active
            }
        }

        foreach (GameObject obj in niteObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                obj.SetActive(true); // Always keep enemies active
            }
            else
            {
                obj.SetActive(false); // Nite platforms hidden at start
            }
        }

        backgroundDay.SetActive(true);
        backgroundNite.SetActive(false);

        Camera.main.backgroundColor = Color.cyan;

        UpdateEnemyVisibility();
    }
    void UpdateEnemyVisibility()
    {
        foreach (GameObject obj in dayObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Collider2D col = obj.GetComponent<Collider2D>();
                SpriteRenderer outlineSR = null;

                Transform outline = obj.transform.Find("Outline");
                if (outline != null)
                    outlineSR = outline.GetComponent<SpriteRenderer>();

                bool shouldShow = (isDay || isPeeking);

                if (sr != null) sr.enabled = shouldShow;
                if (col != null) col.enabled = shouldShow;
                if (outlineSR != null) outlineSR.enabled = isPeeking;
            }
        }

        foreach (GameObject obj in niteObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                Collider2D col = obj.GetComponent<Collider2D>();
                SpriteRenderer outlineSR = null;

                Transform outline = obj.transform.Find("Outline");
                if (outline != null)
                    outlineSR = outline.GetComponent<SpriteRenderer>();

                bool shouldShow = (!isDay || isPeeking);

                if (sr != null) sr.enabled = shouldShow;
                if (col != null) col.enabled = shouldShow;
                if (outlineSR != null) outlineSR.enabled = isPeeking;
            }
        }
    }




    void PushPlayerOutIfOverlapping()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        Collider2D playerCol = player.GetComponent<Collider2D>();
        if (playerCol == null) return;

        int mask = LayerMask.GetMask("Platform", "Default");
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(playerCol.bounds.center, playerCol.bounds.size * 1.05f, 0f, mask);

        int platformCount = 0;
        foreach (Collider2D col in overlaps)
        {
            if (col != playerCol)
            {
                platformCount++;
            }
        }

        if (platformCount >= 2)
        {
            player.transform.position += new Vector3(0f, 0.75f, 0f);
        }
        else
        {
            foreach (Collider2D col in overlaps)
            {
                if (col != playerCol)
                {
                    Vector2 pushDir = (playerCol.bounds.center - col.bounds.center).normalized;
                    if (pushDir == Vector2.zero) pushDir = Vector2.up;
                    player.transform.position += (Vector3)(pushDir * 0.1f);
                }
            }
        }

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            StartCoroutine(DelayedGroundCheck(movement));
        }
    }

    IEnumerator DelayedGroundCheck(PlayerMovement movement)
    {
        yield return new WaitForSeconds(0.05f);
        movement.CheckGrounded();
    }
}
