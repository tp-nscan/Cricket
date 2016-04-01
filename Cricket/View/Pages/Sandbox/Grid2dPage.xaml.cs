using Cricket.ViewModel.Pages.Sandbox;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace Cricket.View.Pages.Sandbox
{
    /// <summary>
    /// Interaction logic for Sandbox.xaml
    /// </summary>
    public partial class Grid2dPage : IContent
    {
        public Grid2dPage()
        {
            InitializeComponent();
            DataContext = new Grid2dPageVm();
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
