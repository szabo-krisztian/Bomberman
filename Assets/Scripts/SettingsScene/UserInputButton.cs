using UnityEngine;

public class UserInputButton : MyButtonBase<USER_KEY_CODE>
{
    [SerializeField]
    private USER_KEY_CODE playerKeyPair;
    protected override void OnEventCall()
    {
        ButtonClicked.Raise(playerKeyPair);
    }
}