﻿using MobileApp_C971_Chakradhar_Konala.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp_C971_Chakradhar_Konala
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PerformDummyInsert = true;
        }

        private bool PerformDummyInsert { get; set; }

        private async void addTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTermPage());
        }

        protected override async void OnAppearing()
        {
            if (PerformDummyInsert)
            {
                var term = new Term()
                {
                    termTitle = "Term 1",
                    startDate = DateTime.Now.Date,
                    endDate = DateTime.Now.Date.AddDays(30)
                };

                App.Database.CreateTermTable();

                if (!(await App.Database.GetTermAsync()).Any())
                {
                   await App.Database.InsertTermAsync(term);
                   var temptermPrimaryKey = await App.Database.RetreiveTermLastRow();
                   var termPrimaryKey = temptermPrimaryKey[0].ID;

                   var course = new Course()
                       {
                           courseTitle = "Course 1",
                           courseStartDate = DateTime.Now.Date,
                           courseEndDate = DateTime.Now.Date.AddDays(30),
                           courseStatus = "Not Started",
                           courseInstructor = "Chuck Konala",
                           courseInstructorPhone = "8177275432",
                           courseInstructorEmail = "ckonala@wgu.edu",
                           courseNotes = "",
                           courseNotifications = true,
                           termID = termPrimaryKey
                       };

                       App.Database.CreateCourseTable();
                       await App.Database.InsertCourseAsync(course);

                       var tempcoursePrimaryKey = await App.Database.RetreiveCourseLastRow();
                       var coursePrimaryKey = tempcoursePrimaryKey[0].ID;

                       var objAssessment = new Assessments()
                       {
                           assessmentTitle = "Objective Assessment",
                           assessmentCode = "KVO1",
                           anticipatedDueDate = DateTime.Now.Date.AddDays(15),
                           assessmentNotifications = true,
                           courseID = coursePrimaryKey
                       };

                       var perfAssessment = new Assessments()
                       {
                           assessmentTitle = "Performance Assessment",
                           assessmentCode = "PRT1",
                           anticipatedDueDate = DateTime.Now.Date.AddDays(30),
                           assessmentNotifications = true,
                           courseID = coursePrimaryKey
                       };

                       App.Database.CreateAssessmentsTable();
                       await App.Database.InsertAssessmentsAsync(objAssessment);
                       await App.Database.InsertAssessmentsAsync(perfAssessment);
                }

                PerformDummyInsert = false;
            }

            var _termList = await App.Database.GetTermAsync();
            TermInformation.ItemsSource = _termList;
            base.OnAppearing();
        }

        private async void TermInformation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new CourseViewPage(e.CurrentSelection.FirstOrDefault() as Term));

        }
    }
}
