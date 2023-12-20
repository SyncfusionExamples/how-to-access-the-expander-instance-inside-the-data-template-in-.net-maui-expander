using Syncfusion.Maui.Popup;

namespace ExpanderDemo
{
    internal class PopupBehavior : Behavior<StackLayout>
    {
        SfPopup popup1;
        Button button;
        private StackLayout stack;

        protected override void OnAttachedTo(StackLayout bindable)
        {
            stack = bindable;
            stack.ChildAdded += Popup1_ChildAdded;
            base.OnAttachedTo(bindable);
        }

        private void Popup1_ChildAdded(object? sender, ElementEventArgs e)
        {
            if (e.Element is Button)
            {
                button = e.Element as Button;
                button.Clicked += Button_Clicked1;
            }
            else if (e.Element is SfPopup)
            {
                popup1 = e.Element as SfPopup;
            }
        }

        private void Button_Clicked1(object? sender, EventArgs e)
        {
            popup1.Show();
        }

        protected override void OnDetachingFrom(StackLayout bindable)
        {
            button.Clicked -= Button_Clicked1;
            stack.ChildAdded -= Popup1_ChildAdded;
            stack = null;
            button = null;
            popup1 = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
