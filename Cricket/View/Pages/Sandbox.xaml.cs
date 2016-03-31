using Cricket.ViewModel.Pages;

namespace Cricket.View.Pages
{
    /// <summary>
    /// Interaction logic for Sandbox.xaml
    /// </summary>
    public partial class Sandbox
    {
        public Sandbox()
        {
            InitializeComponent();
            DataContext = new SandboxPageVm();
        }
    }
}
