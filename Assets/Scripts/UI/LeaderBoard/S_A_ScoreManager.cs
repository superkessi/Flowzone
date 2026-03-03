using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Linq;
public class S_A_ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI inputScore;

    [SerializeField]
    private TMP_InputField inputName;

    [SerializeField]
    private TMP_InputField debugScore;

    public UnityEvent<string, int> submitScoreEvent;


    private string[] badWords = { "ARSCH", "PENIS", "MUSCHI", "WIXER", "WIXXER",  };

    public bool submitted = false;

    public void SubmitScore()
    {
        if (!submitted)
        {

            //if (System.Array.IndexOf(badWords, inputName.text) != -1) return;

            if (badWords.Any(inputName.text.ToUpper().Contains)) { return; }


            if (inputScore != null && debugScore == null)
            {
                submitScoreEvent.Invoke(inputName.text.ToUpper(), int.Parse(inputScore.text));

            }
            else if (inputScore != null && debugScore != null)
            {
                submitScoreEvent.Invoke(inputName.text.ToUpper(), int.Parse(debugScore.text));
            }

            submitted = true;

        }
        
    }
}
