using project_bakery_app.Models;
namespace project_bakery_app;

public partial class OrderEntryPage : ContentPage
{
	public OrderEntryPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetOrderListsAsync();
    }
    async void OnOrderListAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OrderPage
        {
            BindingContext = new OrderList()
        });
    }
    async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new OrderPage
            {
                BindingContext = e.SelectedItem as OrderList
            });
        }
    }
}