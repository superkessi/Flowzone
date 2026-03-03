using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_VirtualKeyboard : S_BaseWindow
{
    [SerializeField] private S_GameUI gameUI;
    [SerializeField]
    private TextMeshProUGUI nameView;

    [SerializeField]
    private TMP_InputField inputName;

    [SerializeField]
    private TMP_InputField leaderBoardInputField;


    private string _name;
    private string letter;


    private void Update()
    {
        OnBackButtonClicked();
    }

    public void OnEnterButtonClicked()
    {
        leaderBoardInputField.text = _name;
        //this.Hide();
        //lbw.Show();
        gameUI.closeCurrentWindow();

    }

    public void OnBackButtonClicked()
    {

        //this.Hide();
        //lbw.Show();
        if (S_UIInput.instance.BackButtonController)
        {
            gameUI.closeCurrentWindow();
        }
       
    }


    public void OnQButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "Q";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnDeleteButtonClicked()
    {
        _name = _name.Remove(_name.Length - 1);
        nameView.text = _name;
    }

    public void OnWButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "W";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnEButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "E";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnRButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "R";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnTButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "T";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnZButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "Z";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnUButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "U";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnIButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "I";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnOButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "O";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnPButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "P";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnAButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "A";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnSButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "S";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnDButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "D";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnFButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "F";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnGButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "G";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnHButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "H";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnJButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "J";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnKButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "K";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnLButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "L";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnYButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "Y";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnXButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "X";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnCButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "C";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnVButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "V";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnBButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "B";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnNButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "N";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }

    public void OnMButtonClicked()
    {

        if (nameView.text.Length <= 9)
        {
            letter = "M";
            _name = _name + letter;
            nameView.text = _name;
        }
        //return name;
    }
}
