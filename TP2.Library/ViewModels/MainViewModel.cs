using System;
using System.Collections.Generic;
using System.Windows.Input;
using SimpleMvvmToolkit.Express;

namespace TP2.Library
{
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        private const string JSON_TEXTBOX_DEFAULT_VALUE = "Entrez du JSON ici...";

        private string _jsonText;
        private string _convertedText;
        private string _selectedLanguage;

        public ICommand JsonConvertCommand => new DelegateCommand(ConvertJson, CanConvertJson);

        public string JsonText
        {
            get => _jsonText;
            set
            {
                _jsonText = value;
                NotifyPropertyChanged(vm => vm.JsonText);
            }
        }

        public string ConvertedText
        {
            get => _convertedText;
            set
            {
                _convertedText = value;
                NotifyPropertyChanged(vm => vm.ConvertedText);
            }
        }

        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                NotifyPropertyChanged(vm => vm.SelectedLanguage);
            }
        }

        public MainViewModel()
        {
            JsonText = JSON_TEXTBOX_DEFAULT_VALUE;
        }

        private void ConvertJson()
        {
            ConvertedText = SelectedLanguage;
        }

        private bool CanConvertJson()
        {
            return true;
        }
    }
}
