using Cricket.ViewModel.Pages.Sandbox;

namespace Cricket.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for LegendPage.xaml
    /// </summary>
    public partial class LegendPage
    {
        public LegendPage()
        {
            InitializeComponent();
            DataContext = new LegendPageVm();
        }
    }
}
