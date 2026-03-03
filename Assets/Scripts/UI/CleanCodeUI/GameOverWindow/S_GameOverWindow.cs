using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class S_GameOverWindow : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField] private S_A_SceneLoader gameSceneLoader;
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private GameObject rankImage;
    [SerializeField] private S_A_ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI rankingName;

    private int yourScore;

    Sprite rankA, rankB, rankC, rankD, rankE, rankF, rankS, rankGod;
   


    private void Awake()
    {
        rankA = Resources.Load<Sprite>("Icon_rankA");
        rankB = Resources.Load<Sprite>("Icon_rankB");
        rankC = Resources.Load<Sprite>("Icon_rankC");
        rankD = Resources.Load<Sprite>("Icon_rankD");
        rankE = Resources.Load<Sprite>("Icon_rankE");
        rankF = Resources.Load<Sprite>("Icon_rankF");
        rankS = Resources.Load<Sprite>("Icon_rankS");
        rankGod = Resources.Load<Sprite>("Icon_rankGod");
    }

    private void Update()
    {
        yourScore = int.Parse(inputScore.text);
        checkRank();
        LeaderBoardButtonGamepad();
    }

    private void checkRank()
    {
        if (yourScore < 31800)
        {
            rankImage.GetComponent<Image>().sprite = rankF;
            rankingName.text = "Shattered Halo";

        }
        else if (yourScore < 88000)
        {
            rankImage.GetComponent<Image>().sprite = rankE;
            rankingName.text = "Wingborne";
        }
        else if (yourScore < 172200)
        {
            rankImage.GetComponent<Image>().sprite = rankD;
            rankingName.text = "Skywarden";
        }
        else if (yourScore < 284400)
        {
            rankImage.GetComponent<Image>().sprite = rankC;
            rankingName.text = "Archangel's Veil";
        }
        else if (yourScore < 424600)
        {
            rankImage.GetComponent<Image>().sprite = rankB;
            rankingName.text = "Virtue's Flame";
        }
        else if (yourScore < 592800)
        {
            rankImage.GetComponent<Image>().sprite = rankA;
            rankingName.text = "Celestial Beacon";
        }
        else if (yourScore < 789000)
        {
            rankImage.GetComponent<Image>().sprite = rankS;
            rankingName.text = "Seraphic Ember";
        }
        else
        {
            rankImage.GetComponent<Image>().sprite = rankGod;
            rankingName.text = "Sovereign of Empyrean";
        }

       
    }
    public void OnRestartButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
        gameUI.gameOver = false;
        scoreManager.submitted = false;
    }

    public void OnMainMenuButtonClicked(int index)
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameSceneLoader.LoadSceneByIndex(index);
        Time.timeScale = 1.0f;
        scoreManager.submitted = false;
        gameUI.gameOver = false;

    }

    public void OnLeaderBoardButtonClicked()
    {
        S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
        gameUI.showLeaderboardWindow();
    }

    public void LeaderBoardButtonGamepad()
    {
        if (S_UIInput.instance.LeaderBoardController)
        {
            S_A_AudioManager.Instance.PlaySFXOneShoot(S_A_AudioManager.Instance.UIButtonClicked);
            gameUI.showLeaderboardWindow();
        }
           
    }

    public void endScore(float endscore)
    {
        inputScore.text = endscore.ToString();
    }
}
