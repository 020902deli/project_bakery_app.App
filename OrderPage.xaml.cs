using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;
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
        var distance = myLocation.CalculateDistance(location,DistanceUnits.Kilometers);
        if (distance < 3)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de ridicat comanda din apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }
        await Map.OpenAsync(location, options);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var items = await App.Database.GetBakersAsync();
        Baker.ItemsSource = (System.Collections.IList)items;
        Baker.ItemDisplayBinding = new Binding("BakerDetails");

        var shopl = (OrderList)BindingContext;

        listView.ItemsSource = await App.Database.GetListDessertsAsync(shopl.ID);
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (OrderList)BindingContext;
        slist.Date = DateTime.UtcNow;
        Baker selectedShop = (Baker.SelectedItem as Baker );
        slist.BakerID = selectedShop.ID;
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
    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Dessert selectedDessert)
        {
            // Elimină desertul din lista afișată
            var orderList = (OrderList)BindingContext;
            var desserts = (List<Dessert>)listView.ItemsSource;
            desserts.Remove(selectedDessert);

            // Reîmprospătează listView
            listView.ItemsSource = null;
            listView.ItemsSource = desserts;
        }
    }

}