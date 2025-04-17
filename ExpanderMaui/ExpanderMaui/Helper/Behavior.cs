using Syncfusion.Maui.Expander;

namespace ExpanderMaui
{
    public class Behavior:Behavior<StackLayout>
    {
        StackLayout stack;
        Button button;
        SfExpander expander;
        bool flag;

        protected override void OnAttachedTo(StackLayout bindable)
        {
            stack = bindable;
            stack.ChildAdded += Stack_ChildAdded;
            base.OnAttachedTo(bindable);
        }

        private void Stack_ChildAdded(object? sender, ElementEventArgs e)
        {
            if(e.Element is SfExpander)
            {
                //Mehtod 1: Get SfExpander reference using StackLayout.ChildAdded Event
                expander = e.Element as SfExpander;
            }
            else if(e.Element is Button)
            {
                button = e.Element as Button;
                button.Clicked += Button_Clicked;
            }
        }

        private void Button_Clicked(object? sender, EventArgs e)
        {
            //Method 2 : Get SfExpander reference using FindByName
            expander = stack.FindByName<SfExpander>("firstExpander");
            if (!flag)
            {
                App.Current!.MainPage!.DisplayAlert("Information", "first Expander instance is obtained and Expanded/Collapsed", "Ok");
                flag = true;
            }

            if (expander.IsExpanded)
            {
                expander.IsExpanded = false;
            }
            else
            {
                expander.IsExpanded = true;
            }
        }

        protected override void OnDetachingFrom(StackLayout bindable)
        {
            stack.ChildAdded -= Stack_ChildAdded;
            button.Clicked -= Button_Clicked;
            button = null;
            expander = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
