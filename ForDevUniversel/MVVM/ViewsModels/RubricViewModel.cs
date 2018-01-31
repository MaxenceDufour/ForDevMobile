using BusinessClassPortable;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;

namespace ForDevUniversel.MVVM.ViewsModels
{
    public class RubricViewModel : ViewModelBase
    {
        private ObservableCollection<RubricViewModel> _colRubricVM;

        private int _rubricId;
        private string _rubricTitle;
        private ImageSource _rubricImageSource;

        #region Builder

        public RubricViewModel()
        {
            _colRubricVM = new ObservableCollection<RubricViewModel>();
        }

        public RubricViewModel(Rubric rubric)
        {
            _rubricId = rubric.RubricId;
            _rubricTitle = rubric.Title;
            _rubricImageSource = null;
        }

        #endregion

        #region Properties Bind

        public int RubricId
        {
            get { return _rubricId; }
            set
            {
                if (value != _rubricId)
                {
                    _rubricId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RubricTitle
        {
            get { return _rubricTitle; }
            set
            {
                if (value != _rubricTitle)
                {
                    _rubricTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource RubricImageSource
        {
            get { return _rubricImageSource; }
            set
            {
                if (value != _rubricImageSource)
                {
                    _rubricImageSource = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public ObservableCollection<RubricViewModel> ColRubricVm
        {
            get { return _colRubricVM; }
            set
            {
                if (value != _colRubricVM)
                {
                    _colRubricVM = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        public async void GetListRubric()
        {
            ObservableCollection<RubricViewModel> list = await ObjForDevModel.GetListRubric();
            foreach (RubricViewModel rubricViewModel in list)
            {
                _colRubricVM.Add(rubricViewModel);
            }
        }
    }
}
