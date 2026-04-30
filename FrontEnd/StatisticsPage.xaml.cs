using BusinessLogic.InterfaceBusiness;

namespace FrontEnd;

public partial class StatisticsPage : ContentPage
{
    public StatisticsPage(IStatisticsLogicLayer statisticsLogicLayer)
    {
        InitializeComponent();
        BindingContext = new StatisticsViewModel(statisticsLogicLayer);
    }
}