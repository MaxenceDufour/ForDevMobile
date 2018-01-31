using BusinessClassPortable;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace ForDevUniversel.MVVM.ViewsModels
{
    public class TopicViewModel : ViewModelBase
    {
        private ObservableCollection<TopicViewModel> _colTopicVM;

        private int _topicId;
        private string _topicTitle;
        private string _topicDescription;
        private string _topicAuthor;
        private DateTime _topicDateCreate;
        private ImageSource _topicImageSource;

        #region Builder
        public TopicViewModel()
        {
            _colTopicVM = new ObservableCollection<TopicViewModel>();
        }

        public TopicViewModel(Topic topic)
        {
            _topicId = topic.TopicId;
            _topicTitle = topic.Title;
            _topicDescription = topic.Description;
            _topicAuthor = $"{topic.Person.PersonFirstName} {topic.Person.PersonLastName}";
            _topicDateCreate = topic.DateAdd;
        }

        #endregion

        #region Properties Bind

        public int TopicId
        {
            get { return _topicId; }
            set
            {
                if (value != _topicId)
                {
                    _topicId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TopicTitle
        {
            get { return _topicTitle; }
            set
            {
                if (value != _topicTitle)
                {
                    _topicTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TopicDescription
        {
            get { return _topicDescription; }
            set
            {
                if (value != _topicDescription)
                {
                    _topicDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource TopicImageSource
        {
            get { return _topicImageSource; }
            set
            {
                if (value != _topicImageSource)
                {
                    _topicImageSource = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TopicAuthor
        {
            get { return _topicAuthor; }
            set
            {
                if (value != _topicAuthor)
                {
                    _topicAuthor = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DateCreate
        {
            get { return $"Le {_topicDateCreate}"; }
        }

        public DateTime TopicDateCreate
        {
            get { return _topicDateCreate; }
        }

        public ObservableCollection<TopicViewModel> ColTopicVM
        {
            get { return _colTopicVM; }
        }

        #endregion

        public async void GetListTopic(int rubricId)
        {
            _colTopicVM.Clear();

            ObservableCollection<TopicViewModel> list = await ObjForDevModel.GetListTopic(rubricId);
            foreach (TopicViewModel topicViewModel in list)
            {
                _colTopicVM.Add(topicViewModel);
            }
        }
    }
}
