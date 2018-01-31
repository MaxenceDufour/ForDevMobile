using BusinessClassPortable;
using ForDevUniversel.MVVM.ViewsModels;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;


namespace ForDevUniversel.MVVM.Models
{
    public sealed class ForDevModel
    {
        private ObservableCollection<TopicViewModel> _colTopicViewModels = new ObservableCollection<TopicViewModel>();
        private ObservableCollection<RubricViewModel> _colRubricViewModels = new ObservableCollection<RubricViewModel>();
        private ObservableCollection<MessageViewModel> _colMessageViewModels = new ObservableCollection<MessageViewModel>();

        //private RestClient _client = new RestClient(new Uri("http://localhost:6000/Service.svc"));
        private RestClient _client = new RestClient(new Uri("http://user19.2isa.org/Service.svc"));

        #region Singleton

        private static ForDevModel _getInstance;
        private ForDevModel() { }
        public static ForDevModel GetInstance
        {
            get
            {
                if (_getInstance == null)
                {
                    _getInstance = new ForDevModel();
                }

                return _getInstance;
            }
        }

        #endregion

        private BitmapImage Logo(int id)
        {
            BitmapImage bi = new BitmapImage();
            switch (id)
            {
                case 1:
                    bi.UriSource = new Uri("ms-appx:///../../Assets/computer.png", UriKind.Absolute);
                    break;
                case 2:
                    bi.UriSource = new Uri("ms-appx:///../../Assets/multimedia.png", UriKind.Absolute);
                    break;
                case 3:
                    bi.UriSource = new Uri("ms-appx:///../../Assets/round.png", UriKind.Absolute);
                    break;
                case 4:
                    bi.UriSource = new Uri("ms-appx:///../../Assets/computer-1.png", UriKind.Absolute);
                    break;
            }

            return bi;
        }
        public async Task<ObservableCollection<RubricViewModel>> GetListRubric()
        {
            _colRubricViewModels.Clear();
            var request = new RestRequest("Rubrics", Method.GET);
            var response = await _client.Execute<List<Rubric>>(request);
            foreach (Rubric rubric in response.Data)
            {
                RubricViewModel myRubric = new RubricViewModel(rubric);
                BitmapImage bi = Logo(myRubric.RubricId);
                myRubric.RubricImageSource = bi;
                _colRubricViewModels.Add(myRubric);
            }

            return _colRubricViewModels;
        }
        public async Task<ObservableCollection<TopicViewModel>> GetListTopic(int rubricId)
        {
            _colTopicViewModels.Clear();
            var request = new RestRequest("Topics/{rubricId}", Method.GET);
            request.AddParameter("rubricId", rubricId, ParameterType.UrlSegment);
            var response = await _client.Execute<List<Topic>>(request); //TODO Erreur non bloquante Voir IRestRespons<T>
            foreach (var topic in response.Data)
            {
                TopicViewModel myTopic = new TopicViewModel(topic);
                BitmapImage bi = Logo(topic.Rubric.RubricId);
                myTopic.TopicImageSource = bi;
                _colTopicViewModels.Add(myTopic);
            }

            return _colTopicViewModels;
        }
        public async Task<ObservableCollection<MessageViewModel>> GetListMessage(int topicId)
        {
            _colMessageViewModels.Clear();
            var request = new RestRequest("Messages/{topicId}", Method.GET);
            request.AddParameter("topicId", topicId, ParameterType.UrlSegment);
            var response = await _client.Execute<List<Message>>(request);
            foreach (Message message in response.Data)
            {
                MessageViewModel myMessage = new MessageViewModel(message);
                _colMessageViewModels.Add(myMessage);
            }

            return _colMessageViewModels;
        }
    }
}
