using SQLite;
using SQLiteNetExtensions;
using System;
using project_bakery_app.Data;
using System.IO;
namespace project_bakery_app;

public partial class App : Application
{
    static OrderListDatabase database;
    public static OrderListDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new
               OrderListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.
               LocalApplicationData), "OrderList.db3"));
            }
            return database;
        }
    }
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
