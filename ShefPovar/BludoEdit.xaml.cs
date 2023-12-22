using System;
using Microsoft.Maui.Controls;

namespace ShefPovar
{
    public partial class BludoEdit : ContentPage
    {
        private readonly BludoDatabaseService bludoDatabaseService;
        public string SelectedCategory { get; set; }


        public BludoEdit(BludoDatabaseService bludoDatabaseService)
        {
            InitializeComponent();
            this.bludoDatabaseService = bludoDatabaseService;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            Bludo newBludo = new Bludo
            {
                Name = EntryName.Text,
                Description = EntryDescription.Text,
                Price = EntryPrice.Text,
                Category = Picker.AnchorXProperty.ToString()
            };
            await bludoDatabaseService.SaveBludoAsync(newBludo);
            await Navigation.PopAsync();
        }

    }
}

