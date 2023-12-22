using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace ShefPovar
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService databaseService;
        private readonly BludoDatabaseService bludoDatabaseService;
        public ObservableCollection<Product> Ingredients { get; set; }
        public ObservableCollection<Bludo> Bluda { get; set; }
        private int currentIndex = 0;
        private int itemsPerPage = 5;
        public MainPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydatabase.db");
            databaseService = new DatabaseService(dbPath);

            string dbPath2 = Path.Combine(FileSystem.AppDataDirectory, "mydatabase2.db");
            bludoDatabaseService = new BludoDatabaseService(dbPath2);

            Bluda = new ObservableCollection<Bludo>();
            Ingredients = new ObservableCollection<Product>();
            LoadIngredientsAsync();

            BindingContext = this;
            LoadBludoAsync();
            BindingContext = this;
        }


        private void Button_Back_Clicked(object sender, EventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                LoadIngredientsAsync();
            }
        }

        private void Button_Forward_Clicked(object sender, EventArgs e)
        {
            currentIndex++;
            LoadIngredientsAsync();
        }
        private double _totalPrice;

        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private async Task LoadIngredientsAsync()
        {
            var ingredients = await databaseService.GetIngredientsAsync();
            Ingredients.Clear();

            // Вычислите начальный и конечный индексы для текущей страницы
            int startIndex = currentIndex * itemsPerPage;
            int endIndex = startIndex + itemsPerPage;

            double total = 0;

            for (int i = startIndex; i < endIndex && i < ingredients.Count; i++)
            {
                Ingredients.Add(ingredients[i]);
                total += ingredients[i].Price; // предположим, что цена находится в поле Price
            }

            TotalPrice = total;
        }
        private async Task LoadBludoAsync()
        {
            var bludo = await bludoDatabaseService.GetBludosAsync();
            Bluda.Clear();

            foreach (var bludos in bludo)
            {
                Bluda.Add(bludos);
            }

            Colll.ItemsSource = Bluda;
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedCategory = picker.SelectedItem?.ToString();

            if (selectedCategory == "Все")
            {
                LoadBludoAsync(); 
            }
            else
            {
                var filteredBluda = Bluda.Where(b => b.Category == selectedCategory).ToList();
                Colll.ItemsSource = filteredBluda;
            }
        }

        private void Exit_Button(object sender, EventArgs e)
        {
            App.Current.Quit();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (true)
            {
                stack1.IsVisible = true;
                stack2.IsVisible = false;
                stack3.IsVisible = false;
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (true)
            {
                stack1.IsVisible = false;
                stack2.IsVisible = true;
                stack3.IsVisible =  false;
            }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new NewPage1(databaseService));
        }

        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.BindingContext;

            await Navigation.PushAsync(new EditPage(databaseService, product));
            await LoadIngredientsAsync();
        }

        private async void Button_Clicked_4(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var product = (Product)button.BindingContext;

            bool result = await DisplayAlert("Подтверждение", $"Вы уверены, что хотите удалить {product.Name}?", "Да", "Отмена");

            if (result)
            {
                await databaseService.DeleteIngredientAsync(product);
                await LoadIngredientsAsync();
            }
        }

        public async void Button_Clicked_5(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BludoEdit(bludoDatabaseService));
        }

        private async void Button_Clicked_6(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var bludo = (Bludo)button.BindingContext;

            bool result = await DisplayAlert("Подтверждение", $"Вы уверены, что хотите удалить {bludo.Name}?", "Да", "Отмена");

            if (result)
            {
                await bludoDatabaseService.DeleteBludoAsync(bludo);
                await LoadBludoAsync();
            }
        }

        private async void Button_Clicked_7(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var bludo = (Bludo)button.BindingContext;

            bool result = await DisplayAlert("Подтверждение", $"Вы уверены, что хотите удалить {bludo.Name}?", "Да", "Отмена");

            if (result)
            {
                await bludoDatabaseService.DeleteBludoAsync(bludo);
                await LoadBludoAsync();
            }
        }
        private void SearchButton(object sender, EventArgs e)
        {
            var result = Ingredients.Where(c => c.Name.ToLower().Contains(Search1.Text.ToLower()));
            Coll.ItemsSource = result;
        }
        private void SearchButton2(object sender, EventArgs e)
        {
            var result = Bluda.Where(c => c.Name.ToLower().Contains(Search2.Text.ToLower()));
            Colll.ItemsSource = result;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}