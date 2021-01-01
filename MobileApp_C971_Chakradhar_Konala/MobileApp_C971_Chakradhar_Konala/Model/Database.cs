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

    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string courseTitle { get; set; }

        public DateTime courseStartDate { get; set; }

        public DateTime courseEndDate { get; set; }

        public string courseStatus { get; set; }

        public string courseInstructor { get; set; }

        public string courseInstructorPhone { get; set; }

        public string courseInstructorEmail { get; set; }

        public string courseNotes { get; set; }

        public bool courseNotifications { get; set; }

        public int termID { get; set; }
    }

    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public  void CreateTermTable()
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

        public void CreateCourseTable()
        {
            _database.CreateTableAsync<Course>().Wait();
        }

        public Task<List<Course>> GetCourseAsync()
        {
            return _database.Table<Course>().ToListAsync();
        }

        public Task<int> InsertCourseAsync(Course course)
        {
            return _database.InsertAsync(course);
        }

        public Task<int> UpdateCourseAsync(Course course)
        {
            return _database.UpdateAsync(course);
        }

        public Task<int> DeleteCourseAsync(Course course)
        {
            return _database.DeleteAsync(course);
        }

        public Task<List<Course>> GetCourseAsync(int termID)
        {
            return _database.QueryAsync<Course>("SELECT * From Course where termID=?",termID);
        }
    }
    }
