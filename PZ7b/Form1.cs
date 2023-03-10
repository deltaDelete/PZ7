using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace PZ7b;

public partial class Form1 : Form {

    private ObservableCollection<ErrorInfo> errorInfos = new ObservableCollection<ErrorInfo>();

    private ErrorHandler errorHandler = new ErrorHandler();
    private ErrorMonitor monitor = new ErrorMonitor();
    private Random rnd = Random.Shared;

    private List<string> randomBullshit = new List<string>() {
        "�����", "������", "������", "������", "�����������", "�������������", "���������������", "����������", "�����", "������",
        "���������", "���������", "�����������", "��������", "���������", "���������", "���������", "�������", "�������", "��������",
        "Baryga", "Decoration", "Floss", "Getting", "it", "Low-grade", "Naimit", "Sakharovar", "To", "be", "besieged", "Vershok", "Volins-nolens"

    };

    public Form1() {
        InitializeComponent();

        errorInfos.CollectionChanged += (sender, e) => {
            dataGridView1.DataSource = errorInfos.ToList();
        };

        dataGridView1.DataSource = errorInfos;
        dataGridView1.Columns[0].HeaderText = "��� ������";
        dataGridView1.Columns[1].HeaderText = "���������";
        dataGridView1.Columns[2].HeaderText = "����� ������";

        monitor.Subscribe(errorHandler);
        monitor.OnUpdateStatus += (sender, e) => {
            errorInfos.Add(e);
        };
    }

    private void button1_Click(object sender, EventArgs e) {
        var code = rnd.Next(0, 700);
        var rndRangeStart = () => rnd.Next(0, randomBullshit.Count());
        var rndRangeEnd = () => rnd.Next(rndRangeStart(), randomBullshit.Count());
        var rndMessage = string.Join(' ', randomBullshit.Where(i => {
            var index = randomBullshit.IndexOf(i);
            return index >= rndRangeStart() && index < rndRangeEnd();
        }
    ));
        var rndLocation = string.Join(' ', randomBullshit.Where(i => {
            var index = randomBullshit.IndexOf(i);
            return index >= rndRangeStart() && index < rndRangeEnd();
        }
    ));
        errorHandler.ErrorStatus(code, rndMessage, rndLocation);
    }
}
