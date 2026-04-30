using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model.Statistics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FrontEnd;

public class StatisticsViewModel : INotifyPropertyChanged
{
    private readonly IStatisticsLogicLayer _statisticsLogicLayer;

    public ObservableCollection<string> Modes { get; } = new()
    {
        "Period",
        "Dashboard"
    };

    private string _selectedMode;
    public string SelectedMode
    {
        get => _selectedMode;
        set
        {
            _selectedMode = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsPeriodMode));
            _ = LoadStatistics();
        }
    }

    public bool IsPeriodMode => SelectedMode == "Period";

    public StatisticsViewModel(IStatisticsLogicLayer statisticsLogicLayer)
    {
        _statisticsLogicLayer = statisticsLogicLayer;

        StartDate = DateTime.Today.AddDays(-7);
        EndDate = DateTime.Today;

        LoadPeriodStatisticsCommand = new Command(async () => await LoadStatistics());

        SelectedMode = "Period"; // 🔥 trigger load korrekt
    }

    // DATE
    private DateTime _startDate;
    public DateTime StartDate
    {
        get => _startDate;
        set { _startDate = value; OnPropertyChanged(); }
    }

    private DateTime _endDate;
    public DateTime EndDate
    {
        get => _endDate;
        set { _endDate = value; OnPropertyChanged(); }
    }

    // STATS
    private int _totalSales;
    public int TotalSales
    {
        get => _totalSales;
        set { _totalSales = value; OnPropertyChanged(); }
    }

    private string _mostSoldName;
    public string MostSoldName
    {
        get => _mostSoldName;
        set { _mostSoldName = value; OnPropertyChanged(); }
    }

    private int _mostSoldCount;
    public int MostSoldCount
    {
        get => _mostSoldCount;
        set { _mostSoldCount = value; OnPropertyChanged(); }
    }

    public ObservableCollection<ItemStatsDataTransferObject> Items { get; set; }
        = new();

    public ICommand LoadPeriodStatisticsCommand { get; }

    private async Task LoadStatistics()
    {
        if (SelectedMode == "Dashboard")
            await LoadDashboardStats();
        else
            await LoadPeriodStatistics();
    }

    private async Task LoadDashboardStats()
    {
        var stats = await _statisticsLogicLayer.DashBoardStastisticsAsync();

        TotalSales = stats.TotalSalesThisFriday;

        if (stats.MostSoldItem != null)
        {
            MostSoldName = stats.MostSoldItem.Name;
            MostSoldCount = stats.MostSoldItem.Count;
        }
        else
        {
            MostSoldName = "None";
            MostSoldCount = 0;
        }

        Items.Clear();
    }

    private async Task LoadPeriodStatistics()
    {
        var startUtc = DateTime.SpecifyKind(StartDate.Date, DateTimeKind.Utc);
        var endUtc = DateTime.SpecifyKind(EndDate.Date.AddDays(1), DateTimeKind.Utc);

        var simpleStats = await _statisticsLogicLayer
            .SimplePeriodStatisticsAsync(startUtc, endUtc);

        TotalSales = simpleStats.TotalSales;

        if (simpleStats.MostSoldItem != null)
        {
            MostSoldName = simpleStats.MostSoldItem.Name;
            MostSoldCount = simpleStats.MostSoldItem.Count;
        }
        else
        {
            MostSoldName = "None";
            MostSoldCount = 0;
        }

        var allStats = await _statisticsLogicLayer
            .AllSalesStatisticsInPeriodAsync(startUtc, endUtc);

        Items.Clear();
        foreach (var item in allStats.Items)
            Items.Add(item);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}