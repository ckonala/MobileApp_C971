using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp_C971_Chakradhar_Konala.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp_C971_Chakradhar_Konala
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTermPage : ContentPage
    {
        public AddTermPage()
        {
            InitializeComponent();
        }

        private async void SaveTerm_OnClicked(object sender, EventArgs e)
        {
            Term term = new Term();

            if (!(TermInfo.Text is null))
            {
                if (TermInfo.Text.Length > 1)
                {
                    term.termTitle = TermInfo.Text;

                    if (termStartDatePicker.Date <= termendDatePicker.Date)
                    {
                        term.startDate = termStartDatePicker.Date;

                        if (termendDatePicker.Date >= termStartDatePicker.Date)
                        {
                            term.endDate = termendDatePicker.Date;
                            await App.Database.InsertTermAsync(term);
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error.", "End Date should be after the start date", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error.", "Start Date should be before the end date", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Error.", "Please ensure Term field is not empty", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Error.", "Please ensure Term field is not empty", "Ok");
            }
        }
    }
}