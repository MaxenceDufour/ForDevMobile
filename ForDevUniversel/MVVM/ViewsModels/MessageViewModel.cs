using BusinessClassPortable;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace ForDevUniversel.MVVM.ViewsModels
{
    public class MessageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageViewModel> _colMessageViewModels;

        private string _topicName;
        private string _authorFirstName;
        private string _authorLastName;
        private ImageSource _voteImageSource;
        //private int _nbVote;
        private string _messageContent;
        private DateTime _messageDate;

        #region Builder
        
        public MessageViewModel()
        {
            _colMessageViewModels = new ObservableCollection<MessageViewModel>();
        }

        public MessageViewModel(Message message)
        {
            _topicName = message.Topic.Title;
            _authorFirstName = message.AuthorFirstName;
            _authorLastName = message.AuthorLastName;
            //_nbVote = message.Vote;
            _messageDate = message.DateUp;
            //if (message.ReplyContent.Substring(0, 1) == "{")
            //    newUcMessage.richTextBoxMessage.Rtf = topicReply.ReplyContent;
            //else
            //    newUcMessage.richTextBoxMessage.Text = topicReply.ReplyContent;
            _messageContent = message.ReplyContent;
        }

        #endregion

        #region Properties Bind

        public string TopicName
        {
            get { return _topicName; }
            set
            {
                if (value != _topicName)
                {
                    _topicName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MessageAuthor
        {
            get { return $"Par {AuthorFirstName} {AuthorLastName}"; }
        }

        public string AuthorFirstName
        {
            get { return _authorFirstName; }
            set
            {
                if (value != _authorFirstName)
                {
                    _authorFirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string AuthorLastName
        {
            get { return _authorLastName; }
            set
            {
                if (value != _authorLastName)
                {
                    _authorLastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public ImageSource VoteImageSource
        {
            get { return _voteImageSource; }
            set
            {
                if (value != _voteImageSource)
                {
                    _voteImageSource = value;
                    OnPropertyChanged();
                }
            }
        }
        //public int NbVote
        //{
        //    get { return _nbVote; }
        //    set
        //    {
        //        if (value != _nbVote)
        //        {
        //            _nbVote = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public string MessageContent
        {
            get { return _messageContent; }
            set
            {
                if (value != _messageContent)
                {
                    _messageContent = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DateMess
        {
            get
            {
                return
                    $"Le {MessageDate.Day}/{MessageDate.Month}/{MessageDate.Year} à {MessageDate.Hour}:{MessageDate.Minute}";
            }
        }

        public DateTime MessageDate
        {
            get { return _messageDate; }
            set
            {
                if (value != _messageDate)
                {
                    _messageDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<MessageViewModel> MessageList
        {
            get { return _colMessageViewModels; }
        }

        #endregion

        public async void GetListMessage(int topicId)
        {
            _colMessageViewModels.Clear();
            ObservableCollection<MessageViewModel> list = await ObjForDevModel.GetListMessage(topicId);
            foreach (MessageViewModel message in list)
            {
                _colMessageViewModels.Add(message);
            }
        }
    }
}
