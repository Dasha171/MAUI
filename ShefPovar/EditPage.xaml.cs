namespace ShefPovar;
public partial class EditPage : ContentPage
{
    private readonly DatabaseService databaseService;
    private readonly Product product;

    public EditPage(DatabaseService databaseService, Product product)
    {
        InitializeComponent();
        this.databaseService = databaseService;
        this.product = product;

        // Заполните элементы управления данными из выбранного продукта
        // Например:
        IngredientNameEntry.Text = product.Name;
        IngredientPriceEntry.Text = product.Price.ToString();
        IngredientKolvEntry.Text = product.Kolv;
    }

    private async void UpdateIngredient_Clicked(object sender, EventArgs e)
    {
        // Получите обновленные данные из элементов управления
        // Например:
        product.Name = IngredientNameEntry.Text;
        if (int.TryParse(IngredientPriceEntry.Text, out int price))
        {
            product.Price = price;
        }
        product.Kolv = IngredientKolvEntry.Text;

        await databaseService.UpdateIngredientAsync(product);

        await DisplayAlert("Успех", "Ингредиент обновлен", "OK");
        // Вернуться на предыдущую страницу
        await Navigation.PopAsync();
    }
}