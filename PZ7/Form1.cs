namespace PZ7;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private async void addRandom_Click(object sender, EventArgs e) {
        HttpClient client = new();
        var result = await client.GetAsync("https://fakerapi.it/api/v1/custom?_quantity=4&city=city&_locale=ru_RU");
        var content = await result.Content.ReadAsStringAsync();
        System.Text.Json.

    }
}
