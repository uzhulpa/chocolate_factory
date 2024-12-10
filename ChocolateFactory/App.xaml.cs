using ChocolateFactory.Data;

namespace ChocolateFactory
{
    public partial class App : Application
    {
        private readonly XmlDatabaseManager _xmlDatabaseManager;

        public App(XmlDatabaseManager xmlDatabaseManager)
        {
            InitializeComponent();

            _xmlDatabaseManager = xmlDatabaseManager;

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            base.OnStart();
            _xmlDatabaseManager.FillWithTestData();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.MinimumHeight = 800;
            window.MinimumWidth = 1280;
            return window;
        }
    }
}
