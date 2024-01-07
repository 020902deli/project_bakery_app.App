using Microsoft.Maui.Devices.Sensors;
using project_bakery_app.Models;
namespace project_bakery_app;

public partial class BakerPage : ContentPage
{
	public BakerPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Baker)BindingContext;
        await App.Database.SaveBakerAsync(shop);
        await Navigation.PopAsync();
    }
     async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var b = (Baker)BindingContext;
        await App.Database.DeleteBakerAsync(b);
        await Navigation.PopAsync();
        
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Baker)BindingContext;
        var address = shop.Adress;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meupreferat" };
        var location = locations?.FirstOrDefault();
        // var myLocation = await Geolocation.GetLocationAsync();
        var myLocation = new Location(46.7731796289, 23.6213886738);
        await Map.OpenAsync(location, options);
    }
}