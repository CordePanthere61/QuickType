using System;
using System.Collections.Generic;
using System.Windows.Input;
using SimpleMvvmToolkit.Express;
using TP2.Library.Models;

namespace TP2.Library
{
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        private const string JSON_TEXTBOX_DEFAULT_VALUE = "Entrez du JSON ici...";

        private Dictionary<int, IClassTemplate> AvaiableLanguages;

        private string _jsonText;
        private string _convertedText;
        private int _selectedLanguage;
        private string _className;

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

        public int SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                NotifyPropertyChanged(vm => vm.SelectedLanguage);
                ConvertJson();
            }
        }

        public string ClassName
        {
            get => _className;
            set
            {
                _className = value;
                NotifyPropertyChanged(vm => vm.ClassName);
            }
        }

        public MainViewModel()
        {
            JsonText = JSON_TEXTBOX_DEFAULT_VALUE;
            AvaiableLanguages = new Dictionary<int, IClassTemplate>();
            AvaiableLanguages.Add(0, new CsharpClass());
            AvaiableLanguages.Add(1, new JavaClass());
        }

        private void ConvertJson()
        {
            if (Validator.IsJsonValid(JsonText))
            {
                AvaiableLanguages[SelectedLanguage].Generate(ClassName, JsonText);
                ConvertedText = AvaiableLanguages[SelectedLanguage].Format();
            }
            
        }

        private bool CanConvertJson()
        {
            return true;
        }
    }
}
