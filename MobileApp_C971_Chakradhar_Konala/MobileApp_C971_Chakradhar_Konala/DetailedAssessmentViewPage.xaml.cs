using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using MobileApp_C971_Chakradhar_Konala.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedAssessmentViewPage : ContentPage
    {
        private Assessments assessments { get; set; }

        public DetailedAssessmentViewPage(Assessments assessments)
        {
            InitializeComponent();
            this.assessments = assessments;
            BindingContext = new CoursePageViewModel();
        }

        private void EditDeleteAssessment_OnClicked(object sender, EventArgs e)
        {
           // throw new NotImplementedException();
        }

        protected override async void OnAppearing()
        {
            AssessmentInfo.Text = assessments.assessmentTitle;
            AssessmentCode.Text = assessments.assessmentCode;
            assessmentDueDatePicker.Text = assessments.anticipatedDueDate.Date.ToString("MMMM dd, yyyy");
            AssessmentNotification.Text = assessments.assessmentNotifications ? "Yes" : "No";
            base.OnAppearing();
        }
    }
}