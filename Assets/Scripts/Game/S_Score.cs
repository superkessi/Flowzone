using System;
using UnityEngine;
using UnityEngine.Serialization;

public class S_Score : MonoBehaviour
{
    //lizzy:
    public S_A_AudioManager audioManager;

    [SerializeField] private float growthRate = 100f;
    [SerializeField] private float offset = 20f;

  
    
    private int currentStage;
    private float scorePerSecond = 100f;
    private float scoreBuffer = 0f;
    
    private float currentScore = 0f;
    public float CurrentScore => currentScore;
    
    private float multiplier = 1f;
    public float Multiplier => multiplier;

    private float pointsNeededPerMultiplier = 7f;
    private int multiplierPoints = 0;
    public int MultiplierPoints => multiplierPoints;
    
    public static Action<int> ChangeStage;

    //Todo -> multiplier erhöhen on change
    //1 * (milestoneMulti * scoreRating^2 - milestoneMulti)
    public int scoreRating;
    
    //Todo get from Spawner
    private int maxStageCount = 15;
    [SerializeField] private int scoreRatingIntervals = 8;
    
    private bool gameOver = false;

    public void StopScore()
    {
        gameOver = true;
    }
    
    private void FixedUpdate()
    {
        CheckStage();
        IncreaseScoreOverTime();
    }

    private float CalculatePointsForNextStage() 
    {
        return growthRate * Mathf.Pow( currentStage +1f , 2f) + offset * currentStage;
    }

    void CheckStage()
    {
        if (currentScore > CalculatePointsForNextStage())
        {
            currentStage++;
            Debug.Log("Stage: " + currentStage);
            ChangeStage?.Invoke(currentStage);

        }

        if (currentStage >= 5)
        { //lizzy:
            audioManager.ChangeSoundtrackOne();
        }
        if (currentStage >= 11)
        {
            audioManager.ChangeSoundtrackTwo();
        }

    }
    
    private void IncreaseScoreOverTime()
    {
        if (!gameOver)
        {
            float scorePerFixedUpdate = scorePerSecond * multiplier * Time.fixedDeltaTime;
            
            scoreBuffer += scorePerFixedUpdate;
            
            if (scoreBuffer >= 1f)
            {
                int scoreToAdd = Mathf.FloorToInt(scoreBuffer);
                currentScore += scoreToAdd;
                scoreBuffer -= scoreToAdd;
            }
            CalculateScoreRating(currentStage);
        }
    }

    public void CalculateScoreRating(int stage)
    {
        float scoreRatingValue = (float)scoreRatingIntervals / (float)maxStageCount;
        scoreRating = Mathf.RoundToInt(scoreRatingValue * stage);
        if (scoreRating < 1)
            scoreRating = 1;
    }
    
    public void AddScore(float value)
    {
        currentScore += (value * multiplier);
    }

    public void AddMultiplier()
    {
        multiplier += 1;
    }

    public void AddMultiplierPoint()
    {
        Debug.Log("------Try Multiplier Point------\n" +
            $"Current Points : {multiplierPoints}\n" +
            $"Points needed : {pointsNeededPerMultiplier}");
        if (multiplierPoints < pointsNeededPerMultiplier - 1)
        {
            Debug.Log("Increase Multiplier Point");
            multiplierPoints += 1;
        }
        else
        {
            Debug.Log("Increase Multiplier");
            AddMultiplier();
            multiplierPoints = 0;
        }
    }

    public void ResetMultiplier()
    {
            multiplier = 1;
    }
}