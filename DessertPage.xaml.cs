using project_bakery_app.Models;
namespace project_bakery_app;

public partial class DessertPage : ContentPage
{
    OrderList ol;
	public DessertPage(OrderList olist)
	{
		InitializeComponent();
        ol = olist;
	}
    
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Dessert d;
        if (listView.SelectedItem != null)
        {
            d = listView.SelectedItem as Dessert;
            var lp = new ListDessert()
            {
                OrderListID = ol.ID,
                DessertID = d.ID
            };
            await App.Database.SaveListDessertAsync(lp);
            d.ListDesserts = new List<ListDessert> { lp };
            await Navigation.PopAsync();
        }
    }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Dessert)BindingContext;
            await App.Database.SaveDessertAsync(product);
            listView.ItemsSource = await App.Database.GetDessertsAsync();
        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = (Dessert)BindingContext;
            await App.Database.DeleteDessertAsync(product);
            listView.ItemsSource = await App.Database.GetDessertsAsync();
        }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetDessertsAsync();
    }
}