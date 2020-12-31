using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_Chakradhar_Konala.Model
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string termTitle { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }
    }

    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public  void CreateTable()
        {
             _database.CreateTableAsync<Term>().Wait();
        }

        public Task<List<Term>> GetTermAsync()
        {
            return _database.Table<Term>().ToListAsync();
        }

        public Task<int> InsertTermAsync(Term term)
        {
            return _database.InsertAsync(term);
        }

        public Task<int> UpdateTermAsync(Term term)
        {
            return _database.UpdateAsync(term);
        }

        public Task<int> DeleteTermAsync(Term term)
        {
            return _database.DeleteAsync(term);
        }
    }
}
