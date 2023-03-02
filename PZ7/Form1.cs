using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PZ7;

public partial class Form1 : Form
{
    private ObservableCollection<LuggageInfo> luggageInfos = new ObservableCollection<LuggageInfo>();

    private LuggageHandler luggageHandler = new LuggageHandler();
    private Monitor monitor = new Monitor("Главный");
    private Random rnd = Random.Shared;

    public Form1()
    {
        InitializeComponent();

        luggageInfos.CollectionChanged += (sender, e) => {
            dataGridView1.DataSource = luggageInfos.ToList();
        };

        dataGridView1.DataSource = luggageInfos.ToList();
        dataGridView1.Columns[0].HeaderText = "Рейс";
        dataGridView1.Columns[1].HeaderText = "Откуда";
        dataGridView1.Columns[2].HeaderText = "Номер карусели";

        monitor.Subscribe(luggageHandler);
        monitor.OnUpdateStatus += (sender, e) => {
            textBox1.Text += $"Информация о прибытии из {(sender as Monitor)?.Name}\r\n";
            foreach (var flightInfo in e)
                textBox1.Text += $"{flightInfo}\r\n";
        };
    }

    public List<string> Cities = new List<string>() { "Абакан", "Черногорск", "Красноярск", "Москва"};

    private void addRandom_Click(object sender, EventArgs e) {

        var cityId = rnd.Next(Cities.Count());
        var cityName = Cities[cityId];

        var carousel = rnd.Next(10);
        var flight = rnd.Next(10);
        var luggage = new LuggageInfo(flight, cityName, carousel);
        luggageInfos.Add(luggage);
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
        var dgv = sender as DataGridView;
        if (dgv is null || !dgv.CurrentRow.Selected) return;

        var index = dgv.CurrentRow.Index;
        var status = rnd.Next(1000);
        var luggage = luggageInfos[index];
        luggageHandler.LuggageStatus(1000,luggage.From, luggage.Carousel);
    }
}
