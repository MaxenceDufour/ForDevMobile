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
    public sealed partial class MessagePage : Page
    {
        private Dictionary<string, ViewModelBase> parameters;
        private RubricViewModel rubricVm;
        public string TopicTitle;

        public MessagePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            parameters = (Dictionary<string, ViewModelBase>)e.Parameter;

            TopicViewModel topicVm = (TopicViewModel)parameters["topicVm"];

            TopicTitle = topicVm.TopicTitle;

            //TopicViewModel topicVm = (TopicViewModel) e.Parameter;

            MessageViewModel messageVM = new MessageViewModel();

            DataContext = messageVM;

            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            //ObservableCollection<ViewModelBase> ret = await ((MessageViewModel)DataContext).GetListMessage(topicVm.TopicId);

            messageVM.GetListMessage(topicVm.TopicId);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // On interdit la sortie de l'application par ce bouton
            e.Handled = true;
        }

        private void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            rubricVm = (RubricViewModel)parameters["rubricVm"];
            Frame.Navigate(typeof(TopicPage), rubricVm);
            //MessageViewModel messageVm = (MessageViewModel) ((Button) sender).DataContext;
            //Frame.Navigate(typeof(TopicPage), messageVm);
        }

        private void BtnRubricPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RubricPage));
        }
    }
}
