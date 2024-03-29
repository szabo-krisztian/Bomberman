using UnityEngine;

public class NewMapCreatorButton : MyButtonBase<string>
{
    [SerializeField]
    private TMPro.TMP_InputField inputField;
    protected override void OnEventCall()
    {
        ButtonClicked.Raise(inputField.text);
    }
}
