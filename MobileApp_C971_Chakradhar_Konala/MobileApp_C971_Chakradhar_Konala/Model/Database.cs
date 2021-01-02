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

    public class Assessments
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string assessmentTitle { get; set; }

        public string assessmentCode { get; set; }

        public DateTime anticipatedDueDate { get; set; }

        public bool assessmentNotifications { get; set; }

        public int courseID { get; set; }
    }

    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        #region Term Queries
        public void CreateTermTable()
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

        public Task<List<Term>> RetreiveTermLastRow()
        {
            return _database.QueryAsync<Term>("SELECT * FROM Term ORDER BY ID DESC LIMIT 1");
        }

        public Task<int> UpdateTermAsync(Term term)
        {
            return _database.UpdateAsync(term);
        }

        public Task<int> DeleteTermAsync(Term term)
        {
            return _database.DeleteAsync(term);
        }
        #endregion

        #region Course Queries
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

        public Task<List<Course>> RetreiveCourseLastRow()
        {
            return _database.QueryAsync<Course>("SELECT * FROM Course ORDER BY ID DESC LIMIT 1");
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

        #endregion

        #region Assessment Queries

        public void CreateAssessmentsTable()
        {
            _database.CreateTableAsync<Assessments>().Wait();
        }

        public Task<List<Assessments>> GetAssessmentsAsync()
        {
            return _database.Table<Assessments>().ToListAsync();
        }

        public Task<int> InsertAssessmentsAsync(Assessments assessment)
        {
            return _database.InsertAsync(assessment);
        }

        public Task<List<Assessments>> RetreiveAssessmentsLastRow()
        {
            return _database.QueryAsync<Assessments>("SELECT * FROM Assessments ORDER BY ID DESC LIMIT 1");
        }

        public Task<int> UpdateAssessmentsAsync(Assessments assessment)
        {
            return _database.UpdateAsync(assessment);
        }

        public Task<int> DeleteAssessmentsAsync(Assessments assessment)
        {
            return _database.DeleteAsync(assessment);
        }

        public Task<List<Assessments>> GetAssessmentsAsync(int courseID)
        {
            return _database.QueryAsync<Assessments>("SELECT * From Assessments where courseID=?", courseID);
        }

        #endregion
    }
}
