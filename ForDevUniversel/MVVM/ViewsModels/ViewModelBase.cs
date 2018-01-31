using ForDevUniversel.Annotations;
using ForDevUniversel.MVVM.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ForDevUniversel.MVVM.ViewsModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ForDevModel ObjForDevModel = ForDevModel.GetInstance;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
