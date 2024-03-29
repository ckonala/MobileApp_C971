﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp_C971_Chakradhar_Konala.ViewModel
{
    public class CoursePageViewModel : ViewModelBase
    {
        public List<string> CourseStatusSource
        {
            get
            {
                return new List<string> { "Not Started", "In Progress", "Completed", "Failed" };
            }
        }

        public List<string> AssessmentTypeSource
        {
            get
            {
                return new List<string> { "Objective Assessment", "Performance Assessment"};
            }
        }

        string selectedCourseStatus;
        public string SelectedMonkey
        {
            get { return selectedCourseStatus; }
            set
            {
                if (selectedCourseStatus != value)
                {
                    selectedCourseStatus = value;
                    OnPropertyChanged();
                }
            }
        }
	}
}
