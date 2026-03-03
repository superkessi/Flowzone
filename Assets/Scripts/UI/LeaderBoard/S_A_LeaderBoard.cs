using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Dan.Main;
using System;

using NUnit.Framework;
using UnityEngine.ProBuilder.MeshOperations;
using System.Linq;

public class S_A_LeaderBoard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;

    [SerializeField]
    private List<TextMeshProUGUI> scores;



    private string[] badWords = { "TEST" };


    private string publicLeaderboardKey =
        "9c7a8a9d7dba0ba0b30bfdf392c2d9d57f29f8cf65423105ff1b32aa4fefe7d2";

    private void Start()
    {
        GetLeaderboard();

        
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; ++i)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();

            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username.ToUpper(), score,
            ((msg) =>
            {

                //if (System.Array.IndexOf(badWords, username) != -1) return;
                if (badWords.Any(username.ToUpper().Contains)) { return; }
                else
                {
                    GetLeaderboard();
                }
                
            }));

        LeaderboardCreator.ResetPlayer();
    }
}
