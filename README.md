# Plugin.Xamarin.Controls
Controls
## Download NuGet
https://www.nuget.org/packages/Plugin.Xamarin.Controls.
### Dependendies

SkiaSharp;

SkiaSharp.Views.Forms;

```xml
xmlns:controls="clr-namespace:Plugin.Xamarin.Controls;assembly=Plugin.Xamarin.Controls"
```


### USAGE:

**BUTTON:**
```xml
<controls:PXC_Button BorderColor="#1CA5B8" 
                    BorderWidth="4" Margin="5" 
                    CornerRadius="24" 
                    HeightRequest="20" 
                    IsSelected="{Binding IsSelected}"
                    FontSize="14"
                    StartColor="#1CA5B8" 
                    EndColor="#1CA5B8" 
                    HorizontalOptions="Center"
                    Command="{Binding Source={x:Reference listParti},Path=BindingContext.ShareCommand}"
                    CommandParameter="{Binding .}" 
                    SelectedIconSource="md-done" 
                    UseEnabled="True"
                    IconTypeFace="Material" 
                    Text="Enviar" 
                    SelectedTextColor="#777" 
                    HasSelected="True" 
                    SelectedBackgroundColor="#ccc"
                    SelectedText="Enviado.." 
                    TextColor="White" 
                    IconSource="md-send"/>
```           
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200425-215756.jpg)
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200428-225423.jpg)


if you will your button change color and text when is clicked

you can set (HasSelected to True) and enabled your button set (UseEnable to true)

also can set showpopup to true

**CHECKBOX:**
```xml
<controls:PXC_Checkbox BorderWidth="5" 
                       Margin="5" 
                       CheckedColor="Red" 
                       IconTypeFace="Material"
                       UnCheckedColor="Gray" 
                       IconSource="md-check-box" 
                       IsChecked="{Binding IsSelected}"/>
```    
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200425-215824.jpg)

**SWITCH:**
```xml
<controls:PXC_Switch IsToggled="{Binding IsSelected}" 
                     VerticalOptions="Center"
                     ToggledCommand="{Binding Source={x:Reference listParti},Path=BindingContext.ShareCommand}"
                     ToggledCommandParameter="{Binding .}"
                     ToggledColor="LightSkyBlue"
                     ThumbColor="White" 
                     UnToggledColor="LightGray"/>
```                    
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200425-215923.jpg)

**For Multi Select you can use:**

Plugin.Xamarin.Controls.MultySelectable;
```csharp
ObservableCollection<SelectableData<Your Model>>
```
**LISTVIEW COLLECTIONVIEW:**

LoadMoreUpCommand

LoadMoreDownCommand

Set IsHorizontal to true if you using collectioview on Horizontal

**SEGMENTEDTAB:**
```xml
<StackLayout HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand">
            <control:PXC_SegmentedTab x:Name="segment"
                                      UnSelectedColor="#ccc"
                                      FontIconName="Material"
                                      SelectedColor="Red"
                                      SelectedItemChanged="segment_SelectedItemChanged">

            </control:PXC_SegmentedTab>
            <Grid x:Name="gridcontrol"/>
</StackLayout>
```
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200427-005936.jpg)

```csharp
using Plugin.Xamarin.Controls.Helpers;

List<BarIconAndTitle> barTexts = new List<BarIconAndTitle>();
barTexts.Add(new BarIconAndTitle { IconText = "md-person", Title = "Users" });
barTexts.Add(new BarIconAndTitle { IconText = "md-group", Title = "Group" });
segment.Children = barTexts;

private void segment_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
{
   if(e.SelectedItem== "Users")
   {
     Add your ContentView here
   }
}
```
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200425-220033.jpg)
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200427-004149.jpg)
![](https://github.com/dodesilva/Plugin.Xamarin.Controls/blob/master/Screenshot_20200427-004156.jpg)

### Inconvenient
This plugin is not tested on ios;

#### License
Licensed under MIT, see license file
