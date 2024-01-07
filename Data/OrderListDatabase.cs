using SQLite;
using SQLiteNetExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_bakery_app;
using project_bakery_app.Models;
using project_bakery_app.Data;

namespace project_bakery_app.Data
{

    public class OrderListDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public OrderListDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<OrderList>().Wait();
            _database.CreateTableAsync<Dessert>().Wait();
            _database.CreateTableAsync<ListDessert>().Wait();
            _database.CreateTableAsync<Baker>().Wait();
        }
        //pentru bakers
        public Task<List<Baker>> GetBakersAsync()
        {
            return _database.Table<Baker>().ToListAsync();
        }
        public Task<int> SaveBakerAsync(Baker baker)
        {
            if (baker.ID != 0)
            {
                return _database.UpdateAsync(baker);
            }
            else
            {
                return _database.InsertAsync(baker);
            }
        }
        public Task<int> DeleteBakerAsync(Baker baker)
        {
            return _database.DeleteAsync(baker);
        }


        //pentru dessert
        public Task<int> SaveDessertAsync(Dessert dessert)
        {
            if (dessert.ID != 0)
            {
                return _database.UpdateAsync(dessert);
            }
            else
            {
                return _database.InsertAsync(dessert);
            }
        }
        public Task<int> DeleteDessertAsync(Dessert dessert)
        {
            return _database.DeleteAsync(dessert);
        }
        public Task<List<Dessert>> GetDessertsAsync()
        {
            return _database.Table<Dessert>().ToListAsync();
        }
        public Task<int> SaveListDessertAsync(ListDessert listd)
        {
            if (listd.ID != 0)
            {
                return _database.UpdateAsync(listd);
            }
            else
            {
                return _database.InsertAsync(listd);
            }
        }
        public Task<List<Dessert>> GetListDessertsAsync(int orderlistid)
        {
            return _database.QueryAsync<Dessert>(
            "select D.ID, D.Description from Dessert D"
            + " inner join ListDessert LD"
            + " on D.ID = LD.DessertID where LD.OrderListID = ?",
            orderlistid);
        }


        //pentru orders
        public Task<List<OrderList>> GetOrderListsAsync()
        {
            return _database.Table<OrderList>().ToListAsync();
        }
        public Task<OrderList> GetOrderListAsync(int id)
        {
            return _database.Table<OrderList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveOrderListAsync(OrderList slist)
        {
            if (slist.ID != 0)
            {
                return _database.UpdateAsync(slist);
            }
            else
            {
                return _database.InsertAsync(slist);
            }
        }
        public Task<int> DeleteOrderListAsync(OrderList slist)
        {
            return _database.DeleteAsync(slist);
        }
    }
}
