using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_A_GameOverWindow : S_A_BaseWindow
{
    [SerializeField]
    private S_A_PauseUIManager gameUiManager;

    [SerializeField]
    private S_A_SceneLoader gameSceneLoader;

    [SerializeField]
    private TextMeshProUGUI inputScore;

    [SerializeField]
    private GameObject rankImage;

    [SerializeField]
    private S_A_ScoreManager scoreManager;

    private int yourScore;

    Sprite rankA, rankB, rankC, rankS;


    private void Awake()
    {
        //yourScore = int.Parse(inputScore.text);

        rankA = Resources.Load<Sprite>("Rank_A");
        rankB = Resources.Load<Sprite>("Rank_B");
        rankC = Resources.Load<Sprite>("Rank_C");
        rankS = Resources.Load<Sprite>("Rank_S");
    }

    private void Update()
    {
        yourScore = int.Parse(inputScore.text);
        checkRank();
    }

    private void checkRank()
    {
        if(yourScore < 5000)
        {
            rankImage.GetComponent<Image>().sprite = rankC;
        }else if(yourScore < 6500)
        {
            rankImage.GetComponent<Image>().sprite = rankB;
        }else if(yourScore < 8000)
        {
            rankImage.GetComponent<Image>().sprite = rankA;
        }
        else
        {
            rankImage.GetComponent<Image>().sprite = rankS;
        }
    } 
    public void OnRestartButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
        gameUiManager.gameOver = false;
        scoreManager.submitted = false;
    }

    public void OnMainMenuButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
        scoreManager.submitted = false;
        gameUiManager.gameOver = false;
        
    }

    public void OnLeaderBoardButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUiManager.WindowDictionary["gameOverWindow"].Hide();
        gameUiManager.WindowDictionary["leaderBoardWindow"].Show();

    }

    public void endScore(float endscore)
    {
        inputScore.text = endscore.ToString();
    }
}
