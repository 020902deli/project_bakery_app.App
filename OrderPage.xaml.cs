using project_bakery_app.Models;
namespace project_bakery_app;

public partial class OrderPage : ContentPage
{
	public OrderPage()
	{
		InitializeComponent();
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