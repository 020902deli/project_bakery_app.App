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
}