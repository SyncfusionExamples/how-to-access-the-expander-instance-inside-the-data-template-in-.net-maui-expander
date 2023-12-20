# how-to-access-the-expander-instance-inside-the-data-template-in-.net-maui-expander
This example demonstrates about how to access the SfExpander control added inside the .NET MAUI SfPopupLayout.ContentTemplate(DataTemplate).

You can access a named SfExpander defined inside DataTemplate of SfPopup using Behavior.

XAML

In SfPopup, behavior added to parent of SfExpander.

<StackLayout>
    <StackLayout.Behaviors>
         <local:PopupBehavior/>
    </StackLayout.Behaviors>
     <Button Text="open popup" VerticalOptions="Start" HorizontalOptions="Center"/>
     <popup:SfPopup x:Name="popup" ShowFooter="False" ShowHeader="False" HeightRequest="400" WidthRequest="400">
         <popup:SfPopup.ContentTemplate>
             <DataTemplate>
                 <ScrollView>
                     <StackLayout>
                         <StackLayout.Behaviors>
                             <local:Behavior/>
                         </StackLayout.Behaviors>
                         <Button Text="Expand/collapse" x:Name="expanderButton"/>

                         <expander:SfExpander x:Name="firstExpander">
                             <expander:SfExpander.Header>
                                 <Grid>
                                     <Label VerticalTextAlignment="Center" HeightRequest="40" Text="Invoice Date"/>
                                 </Grid>
                             </expander:SfExpander.Header>
                             <expander:SfExpander.Content>
                                 <Grid>
                                     <Label Text="11.03 AM, 15 January 2019" VerticalTextAlignment="Center"/>
                                 </Grid>
                             </expander:SfExpander.Content>
                         </expander:SfExpander>
                         <expander:SfExpander x:Name="secondExpander">
                             <expander:SfExpander.Header>
                                 <Grid>
                                     <Label Text="Payment Details" VerticalTextAlignment="Center" HeightRequest="40"/>
                                 </Grid>
                             </expander:SfExpander.Header>
                             <expander:SfExpander.Content>
                                 <Grid Padding="10,10,10,10">
                                     <Grid.RowDefinitions>
                                         <RowDefinition Height="Auto" />
                                         <RowDefinition Height="Auto" />
                                         <RowDefinition Height="Auto" />
                                     </Grid.RowDefinitions>
                                     <Grid.ColumnDefinitions>
                                         <ColumnDefinition Width="*" />
                                         <ColumnDefinition Width="*" />
                                     </Grid.ColumnDefinitions>
                                     <Label Text="Card Payment" TextColor="#303030" HeightRequest="40"/>
                                     <Label Grid.Row="1" Text="Third-Party coupons"
                                            HeightRequest="40"/>
                                     <Label Grid.Row="2" FontAttributes="Bold" HeightRequest="40"
                                            Text="Total Amount Paid" />
                                     <Label Grid.Column="1" HorizontalTextAlignment="End"
                                            Text="$31,000.00"  HeightRequest="40"/>
                                     <Label Grid.Row="1" Grid.Column="1" Text="$5,000.00" HeightRequest="40"
                                            HorizontalTextAlignment="End" />
                                     <Label Grid.Row="2" Grid.Column="1" FontAttributes="Bold"
                                            HorizontalTextAlignment="End" Text="$36,000.00"
                                             HeightRequest="40"/>
                                 </Grid>
                             </expander:SfExpander.Content>
                         </expander:SfExpander>
                     </StackLayout>
                 </ScrollView>
             </DataTemplate>
         </popup:SfPopup.ContentTemplate>
     </popup:SfPopup>
 </StackLayout>
 
C#
In ChildAdded event you will get the instance of Expander.

 public class Behavior:Behavior<StackLayout>
 {
     StackLayout stack;   
     SfExpander expander;

     protected override void OnAttachedTo(StackLayout bindable)
     {
         stack = bindable;
         stack.ChildAdded += Stack_ChildAdded;
         base.OnAttachedTo(bindable);
     }

     private void Stack_ChildAdded(object? sender, ElementEventArgs e)
     {
        //Method 1 : Get SfExpander reference using StackLayout.ChildAdded Event
         if(e.Element is SfExpander)
         {
             expander = e.Element as SfExpander;
         }
     }
     
     protected override void OnDetachingFrom(StackLayout bindable)
     {
         stack.ChildAdded -= Stack_ChildAdded;
         stack = null;
         expander = null;
         base.OnDetachingFrom(bindable);
     }
 }
C#
You can also get the Expander using FindByName method from Parent element.

internal class Behavior:Behavior<StackLayout>
{
    StackLayout stack;
    Button button;
    SfExpander expander;

    protected override void OnAttachedTo(StackLayout bindable)
    {
        stack = bindable;
        stack.ChildAdded += Stack_ChildAdded;
        base.OnAttachedTo(bindable);
    }

    private void Stack_ChildAdded(object? sender, ElementEventArgs e)
    {
       if(e.Element is Button)
        {
            button = e.Element as Button;
            button.Clicked += Button_Clicked;
        }
    }

    private void Button_Clicked(object? sender, EventArgs e)
    {
        //Method 2 : Get SfExpander reference using FindByName
        expander = stack.FindByName<SfExpander>("firstExpander");
        App.Current!.MainPage!.DisplayAlert("Information", "first Expander instance is obtained and Expanded/Collapsed", "Ok");
         
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
View sample in Github

