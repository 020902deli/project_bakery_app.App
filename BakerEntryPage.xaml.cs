using project_bakery_app.Models;
namespace project_bakery_app;

public partial class BakerEntryPage : ContentPage
{
	public BakerEntryPage()
	{
        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listView.ItemsSource = await App.Database.GetBakersAsync();
    }
    async void OnBakerAddedClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BakerPage
        {
            BindingContext = new Baker()
        });
    }
    async void OnListViewItemSelected(object sender,
   SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new BakerPage
            {
                BindingContext = e.SelectedItem as Baker
            });
        }
    }

}