public class MyVoidEventButton : MyButtonBase<Void>
{
    protected override void OnEventCall()
    {
        ButtonClicked.Raise(new Void());
    }
}
