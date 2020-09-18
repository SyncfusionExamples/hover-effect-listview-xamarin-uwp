# How to apply hover effect for ListView Item in Xamairn.Forms UWP (SfListView)

You can apply mouse hover effect for [ListViewItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.ListViewItem.html) by loading custom control in Xamarin.Forms. To enable the hovering effect, implement a [custom renderer](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/custom-renderer/) for the UWP platform.

You can also refer the following article.

https://www.syncfusion.com/kb/11936/how-to-apply-hover-effect-for-listview-item-in-xamairn-forms-uwp-sflistview

**C#**

Define the custom control derived from the [Grid](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.grid?view=xamarin-forms) in the Xamarin.Forms PCL project.

``` c#
namespace ListViewXamarin
{
    public class CustomGrid : Grid
    {
        
    }
}
```
**XAML**

Load the **CustomGrid** in the [SfListView.ItemTemplate](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.SfListView.html#Syncfusion_ListView_XForms_SfListView_ItemTemplate).

``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
    <ContentPage.Content>
        <StackLayout>
            <syncfusion:SfListView x:Name="listView" ItemSize="70" SelectionBackgroundColor="#a6dcef" ItemsSource="{Binding ContactsInfo}" SelectionChangedCommand="{Binding ListViewSelection}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <local:CustomGrid x:Name="grid">
                            <local:CustomGrid.ColumnDefinitions>
                                <ColumnDefinition Width="70" />
                                <ColumnDefinition Width="*" />
                            </local:CustomGrid.ColumnDefinitions>
                            <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                            <Grid Grid.Column="1" RowSpacing="1" Padding="10,0,0,0" VerticalOptions="Center">
                                <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}"/>
                                <Label Grid.Row="1" Grid.Column="0" TextColor="#474747" LineBreakMode="NoWrap" Text="{Binding ContactNumber}"/>
                            </Grid>
                        </local:CustomGrid>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Create custom platform renderer for UWP and hook [PointerEntered](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.uielement.pointerentered?view=winrt-19041) and [PointerExited](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.uielement.pointerexited?view=winrt-19041) events. In the events, you can get the **CustomGrid** from **Element** property and change the [BackgroundColor](https://docs.microsoft.com/en-us/dotnet/api/xamarin.forms.visualelement.backgroundcolor?view=xamarin-forms) of the item. Also, you can change the selected item color in the [PointerPressed](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.uielement.pointerpressed?view=winrt-19041) event and skip the hovering effect for the selected item based on model class property.

``` c#
[assembly: ExportRenderer(typeof(CustomGrid), typeof(CustomGridRenderer))]
namespace ListViewXamarin.UWP
{
    public class CustomGridRenderer : VisualElementRenderer<CustomGrid, FrameworkElement>
    {
        public CustomGridRenderer()
        {
            this.PointerEntered += CustomGridRenderer_PointerEntered;
            this.PointerExited += CustomGridRenderer_PointerExited;
            this.PointerPressed += CustomGridRenderer_PointerPressed;
        }

        private void CustomGridRenderer_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            item.BackgroundColor = Xamarin.Forms.Color.FromHex("#a6dcef");
        }

        private void CustomGridRenderer_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            item.BackgroundColor = Xamarin.Forms.Color.Transparent;
        }

        private void CustomGridRenderer_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = this.Element;
            var itemData = item.BindingContext as Contacts;
            if (itemData.IsSelected)
                item.BackgroundColor = Xamarin.Forms.Color.Transparent;
            else
                item.BackgroundColor = Xamarin.Forms.Color.WhiteSmoke;
        }
    }
}
```
**C#**

In the [SelectionChangedCommand](https://help.syncfusion.com/cr/xamarin/Syncfusion.ListView.XForms.SfListView.html#Syncfusion_ListView_XForms_SfListView_SelectionChangedCommand), update the **IsSelected** model property to update the selection color in platform renderer.

``` c#
namespace ListViewXamarin
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public Command<object> ListViewSelection { get; set; }

        public ContactsViewModel()
        {
            ListViewSelection = new Command<object>(OnItemSelected);
        }

        private void OnItemSelected(object obj)
        {
            var args = obj as Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs;
            if (args.AddedItems.Count > 0)
            {
                var item = args.AddedItems[0] as Contacts;
                item.IsSelected = true;
            }
            else
            {
                var item = args.RemovedItems[0] as Contacts;
                item.IsSelected = false;
            }
        }
    }
}
```
**Output**

![HoverEffectListView](https://github.com/SyncfusionExamples/hover-effect-listview-xamarin-uwp/blob/master/ScreenShot/HoverEffectListView.gif)
