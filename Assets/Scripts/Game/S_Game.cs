using System;
using UnityEngine;
using UnityEngine.Rendering;

public class S_Game : MonoBehaviour
{
    #region References
    private S_Score score;
    private S_Player player;
    private S_ModuleSpawner spawner;
    private S_GameUI gameUI;
    private S_IngamePlayerUI ingameUI;
    #endregion
    
    public static Action<float> PassScore;

    public float maxPlayTime = 60f;
    private float playTime = 0f;
    private bool playTimeUp = false;
    
    private void Awake()
    {
        score = FindFirstObjectByType<S_Score>();
        player = FindFirstObjectByType<S_Player>();
        spawner = FindFirstObjectByType<S_ModuleSpawner>();
        gameUI = FindFirstObjectByType<S_GameUI>();
        ingameUI = FindFirstObjectByType<S_IngamePlayerUI>();
        
        Cursor.visible = false;

        AwakeDebug();
        
        S_Interactable.AddMultiplier += score.AddMultiplier;
        S_Interactable.AddMultiplierPoint += score.AddMultiplierPoint;
        
        S_Score.ChangeStage += ChangeState;
        
        S_Player.OnPlayerDeath += OnPlayerDeath;
        S_Player.AddScore += score.AddScore;
        S_Player.JumpPickupCollected += ingameUI.ShowJumpPickup;
        S_Player.SlowPickupCollected += ingameUI.ShowSlowPickup;
        S_Player.ResetMultiplier += score.ResetMultiplier;
        S_Player.AddMultiplier += score.AddMultiplier;
        
        PassScore += gameUI.getEndScore;
    }

    private void Update()
    {
        if (playTimeUp)
            return;

        playTime += Time.deltaTime;
        if (playTime >= maxPlayTime)
        {
            playTimeUp = true;
            player.SetForcedState(S_MovementState.StateType.DEAD);
            Debug.Log("TIME IS UP, INITIATING PLAYERKILLING DEVICE AAAAA");
        }
    }


    private void OnDisable()
    {
        S_Interactable.AddMultiplier -= score.AddMultiplier;
        S_Interactable.AddMultiplierPoint -= score.AddMultiplierPoint;
            
        S_Score.ChangeStage -= ChangeState;
        
        S_Player.OnPlayerDeath -= OnPlayerDeath;
        S_Player.AddScore -= score.AddScore;
        S_Player.JumpPickupCollected -= ingameUI.ShowJumpPickup;
        S_Player.SlowPickupCollected -= ingameUI.ShowSlowPickup;
        S_Player.ResetMultiplier -= score.ResetMultiplier;
        S_Player.AddMultiplier -= score.AddMultiplier;
        
        PassScore -= gameUI.getEndScore;
    }

    void OnPlayerDeath()
    {
        gameUI.showGameOverWindow();
        SetScore();
        score.StopScore();
        
        Cursor.visible = true;

        playTimeUp = true;
        playTime = 0f;
    }

    private void SetScore()
    {
        PassScore?.Invoke(score.CurrentScore);
    }
    
    private void ChangeState(int stage)
    {
        spawner.LoadStage(stage);
    }

    void AwakeDebug()
    {
        Debug.Assert(score, "SCORE NOT ASSIGNED TO GAME");
        Debug.Assert(player, "PLAYER NOT ASSIGNED TO GAME");
        Debug.Assert(spawner, "SPAWNER NOT ASSIGNED TO GAME");
        Debug.Assert(gameUI, "UIMANAGER NOT ASSIGNED TO GAME");
        Debug.Assert(ingameUI, "INGAMEUI NOT ASSIGNED TO GAME");
    }
}