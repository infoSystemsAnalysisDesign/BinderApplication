using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace BinderApplication.Pages
{
    public partial class Match : ContentPage
    {
        public Match()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                API api = new API();
                string resultFromAPI = await api.GetResultFromAPI();

                responseLabel.Text = "TEST\n\n";
                responseLabel.Text += resultFromAPI; // Use += to append to existing text
            }
            catch (Exception ex)
            {
                responseLabel.Text = $"Exception: {ex.Message}";
            }
        }
    }
}