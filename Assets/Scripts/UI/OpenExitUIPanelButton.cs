using UnityEngine;

public class OpenExitUIPanelButton : MyButtonBase<GameObject>
{
    [SerializeField]
    private GameObject _myUIPanel;

    protected override void OnEventCall()
    {
        ButtonClicked.Raise(_myUIPanel);
    }
}
