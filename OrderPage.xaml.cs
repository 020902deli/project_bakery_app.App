using Microsoft.Maui.Devices.Sensors;
using project_bakery_app.Models;
namespace project_bakery_app;

public partial class OrderPage : ContentPage
{
	public OrderPage()
	{
		InitializeComponent();
	}
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (OrderList)BindingContext;
        var address = shop.Adress;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat" };
        var location = locations?.FirstOrDefault();
         var myLocation = await Geolocation.GetLocationAsync();
        //var myLocation = new Location(46.7731796289, 23.6213886738);
        await Map.OpenAsync(location, options);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (OrderList)BindingContext;

        listView.ItemsSource = await App.Database.GetListDessertsAsync(shopl.ID);
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (OrderList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveOrderListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (OrderList)BindingContext;
        await App.Database.DeleteOrderListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DessertPage((OrderList)
       this.BindingContext)
        {
            BindingContext = new Dessert()
        });

    }
    
}