using System;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitDeadlock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly TimeSpan LongRunningOperationTime = TimeSpan.FromSeconds(3);
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task LongRunningOperationDefaultAwait()
        {
            await Task.Delay(LongRunningOperationTime);
        }

        private async Task LongRunningOperationConfigureAwait()
        {
            await Task.Delay(LongRunningOperationTime).ConfigureAwait(false);
        }

        private void DeadlockButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This operation will cause deadlock.You will have to restart app manually");
            LongRunningOperationDefaultAwait().Wait();
            MessageBox.Show("Finished processing"); // this will never be hit
        }

        private void NoDeadlockButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This operation will NOT cause deadlock, however this is a blocking UI call.");
            LongRunningOperationConfigureAwait().Wait();
            MessageBox.Show("Finished processing");
        }

        private async void ProperWayButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is a proper non-blocking, async/await all the way down implementation.");
            await LongRunningOperationDefaultAwait();
            MessageBox.Show("Finished processing");
        }
    }
}
