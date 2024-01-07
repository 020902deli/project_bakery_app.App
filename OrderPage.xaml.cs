using project_bakery_app.Models;
namespace project_bakery_app;

public partial class OrderPage : ContentPage
{
	public OrderPage()
	{
		InitializeComponent();
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

}