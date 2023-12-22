namespace ShefPovar;

public partial class NewPage1 : ContentPage
{
    private readonly DatabaseService databaseService;
    public NewPage1(DatabaseService databaseService)
	{
		InitializeComponent();
        this.databaseService = databaseService;
    }
    private async void AddIngredient_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(IngredientNameEntry.Text))
        {
            await DisplayAlert("Ошибка", "Введите название ингредиента", "OK");
            return;
        }

        if (!int.TryParse(IngredientPriceEntry.Text, out int price))
        {
            await DisplayAlert("Ошибка", "Введите корректную цену", "OK");
            return;
        }

        string kolv = IngredientKolvEntry.Text;

        var newIngredient = new Product
        {
            Name = IngredientNameEntry.Text,
            Price = price,
            Kolv = kolv
        };

        await databaseService.SaveIngredientAsync(newIngredient);

        await DisplayAlert("Успех", "Ингредиент добавлен", "OK");
        // Вернуться на предыдущую страницу
        await Navigation.PopAsync();

    }

}