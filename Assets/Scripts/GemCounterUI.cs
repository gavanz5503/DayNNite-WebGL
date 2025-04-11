using UnityEngine;
using TMPro;

public class GemCounterUI : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    private int totalGems;
    private int collectedGems;

    void Start()
    {
        collectedGems = 0;
        totalGems = GameObject.FindGameObjectsWithTag("Gem").Length;
        UpdateUI();
    }

    public void AddGem()
    {
        collectedGems++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        counterText.text = collectedGems + " / " + totalGems + " Gems Collected";
    }
}

