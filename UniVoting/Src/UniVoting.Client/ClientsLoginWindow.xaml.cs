using System.Collections.Generic;
using System.Reactive.Linq;
using Akavache;
using MahApps.Metro.Controls;
using UniVoting.Model;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientsLoginWindow.xaml
    /// </summary>
    public partial class ClientsLoginWindow : MetroWindow
    {
        public static Voter Voter { get; set; }
        private IEnumerable<Model.Position> _positions;
         private Stack<Model.Position> _positionsStack;
        public ClientsLoginWindow()
        {
            InitializeComponent();
            _positionsStack=new Stack<Model.Position>();
            Loaded += ClientsLoginWindow_Loaded;
            //IgnoreTaskbarOnMaximize = true;
            BtnGo.Click += BtnGo_Click;
        }

        private async void ClientsLoginWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _positions = new List<Model.Position>();
            _positions = await BlobCache.LocalMachine.GetObject<IEnumerable<Model.Position>>("ElectionPositions");
            foreach (var position in _positions)
            {
                _positionsStack.Push(position);
            }
        }

        private void BtnGo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new MainWindow(_positionsStack).Show();
            Hide();
        }
    }
}
