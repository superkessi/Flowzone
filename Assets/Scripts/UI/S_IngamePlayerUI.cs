using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_IngamePlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] private S_Score score;
    [SerializeField] private List<Image> multiplierPoints;
    [SerializeField] private Image jumpPickupIcon;
    [SerializeField] private Image slowPickupIcon;


    private void Awake()
    {
        score = FindFirstObjectByType<S_Score>();
    }

    private void Update()
    {
        var scoreTextString = new string($"{score.CurrentScore}");
        scoreText.text = scoreTextString;
        
        var multiTextString = new string($"x{score.Multiplier}");
        multiplierText.text = multiTextString;
        
        UpdateMultiplierPoint(score.MultiplierPoints);
    }

    void UpdateMultiplierPoint(int currentPoints)
    {
        for (int i = 0; i < currentPoints; i++)
        {
            multiplierPoints[i].gameObject.SetActive(true);
        }
        for (int i = currentPoints; i < multiplierPoints.Count; i++)
        {
            multiplierPoints[i].gameObject.SetActive(false);
        }
    }

    public void ShowJumpPickup(bool value)
    {
        jumpPickupIcon.enabled = value;
    }
    
    public void ShowSlowPickup(bool value)
    {
        slowPickupIcon.enabled = value;
    }
    
}
