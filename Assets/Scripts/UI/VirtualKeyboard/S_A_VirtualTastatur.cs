using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_A_VirtualTastatur : S_A_BaseWindow
{
    [SerializeField]
    private TextMeshProUGUI nameView;

    [SerializeField]
    private TMP_InputField inputName;

    [SerializeField]
    private TMP_InputField leaderBoardInputField;

    [SerializeField]
    private S_A_LeaderBoardWindow lbw;

    private string _name;
    private string letter;

    EventSystem eventSystem;

    [SerializeField]
    private GameObject firstSelectedButton;

    private void Awake()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(firstSelectedButton);
    }

    public void OnEnterButtonClicked()
    {
        leaderBoardInputField.text = _name;
        this.Hide();
        lbw.Show();
    }

    public void OnBackButtonClicked()
    {
       
        this.Hide();
        lbw.Show();
    }


    public void OnQButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "Q";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnDeleteButtonClicked()
    {
        _name = _name.Remove(_name.Length-1);
        nameView.text = _name;
    }

    public void OnWButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "W";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnEButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "E";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnRButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "R";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnTButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "T";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnZButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "Z";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnUButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "U";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnIButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "I";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnOButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "O";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnPButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "P";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnAButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "A";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnSButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "S";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnDButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "D";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnFButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "F";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnGButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "G";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnHButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "H";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnJButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "J";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnKButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "K";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnLButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "L";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnYButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "Y";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnXButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "X";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnCButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "C";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnVButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "V";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnBButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "B";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnNButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "N";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnMButtonClicked()
    {

        if (nameView.text.Length <= 6)
        {
            letter = "M";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }
}
