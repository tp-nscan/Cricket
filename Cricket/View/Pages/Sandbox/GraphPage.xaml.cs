using System.Windows;
using Cricket.ViewModel.Pages.Sandbox;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace Cricket.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for GraphPage.xaml
    /// </summary>
    public partial class GraphPage : IContent
    {
        public GraphPage()
        {
            InitializeComponent();
            DataContext = new GraphPageVm();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {

        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //if (MessageBoxResult.No == ModernDialog.ShowMessage("Navigate away?", "navigate", System.Windows.MessageBoxButton.YesNo))
            //{
            //    e.Cancel = true;
            //}
        }
    }
}
