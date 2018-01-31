using ForDevUniversel.MVVM.ViewsModels;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForDevUniversel.MVVM.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class TopicPage : Page
    {

        private RubricViewModel rubricVm;

        public TopicPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rubricVm = (RubricViewModel)e.Parameter;

            TopicViewModel topicVM = new TopicViewModel();

            DataContext = topicVM;

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            topicVM.GetListTopic(rubricVm.RubricId);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //On interdit la sortie de l'application par ce bouton
            e.Handled = true;
        }

        private void BtnMainPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RubricPage));
        }

        private void BtnTopic_Click(object sender, RoutedEventArgs e)
        {
            TopicViewModel topicVM = (TopicViewModel)((Button)sender).DataContext;
            //Frame.Navigate(typeof(MessagePage), topicVM);

            Dictionary<string, ViewModelBase> parameters = new Dictionary<string, ViewModelBase>();
            parameters.Add("topicVm", topicVM);
            parameters.Add("rubricVm", rubricVm);
            Frame.Navigate(typeof(MessagePage), parameters);
        }
    }
}
