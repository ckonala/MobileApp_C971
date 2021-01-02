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

        private Course course { get; set; }

        public DetailedAssessmentViewPage(Assessments assessments,Course course)
        {
            InitializeComponent();
            this.assessments = assessments;
            this.course = course;
            BindingContext = new CoursePageViewModel();
        }

        private async void EditDeleteAssessment_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssessmentPage(assessments,course));
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