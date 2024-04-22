using UnityEngine;

public class UserInputButton : MyButtonBase<string>
{
    [SerializeField]
    private TMPro.TMP_InputField inputField;
    protected override void OnEventCall()
    {
        ButtonClicked.Raise(inputField.text);
    }
}

public enum USER_KEY_CODE
{
    PLAYER1_UP,
    PLAYER1_LEFT,
    PLAYER1_RIGHT,
    PLAYER1_DOWN,
    PLAYER1_BOMB,
    PLAYER2_UP      
}