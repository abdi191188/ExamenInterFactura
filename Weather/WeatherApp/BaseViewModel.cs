using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WeatherApp
{
    public class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        public void RaisePropertyChanged(string propertyName) // agregado domingo 7 de julio del 2019
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
            }
            OnPropertyChanged(propertyName);
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get => _IsBusy;
            set => Set(ref _IsBusy, value);
        }





    }

    #region ObservableObject 
    public class ObservableObject : INotifyPropertyChanged
    {
        #region [ PropertyChanged ]
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion [ PropertyChanged ]
    }
    #endregion
}
