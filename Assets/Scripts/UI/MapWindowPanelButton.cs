using UnityEngine;


public class MapWindowPanelButton : MyButtonBase<string>
{
    protected override void OnEventCall()
    {
        ButtonClicked.Raise(transform.parent.name);
    }
}
