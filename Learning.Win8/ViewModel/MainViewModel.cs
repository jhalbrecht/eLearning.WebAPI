using System;
using System.Threading.Tasks;
using AppDevPro.Utility;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Learning.Win8.Common;
using Learning.Win8.Model;
using Learning.Win8.Service;
using System.Collections.ObjectModel;

namespace Learning.Win8.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Logger _logger;

        /// <summary>
        /// The <see cref="WelcomeTitle" /> property's name.
        /// </summary>
        public const string WelcomeTitlePropertyName = "WelcomeTitle";

        private readonly IDataService _dataService;
        private readonly INavigationService _navigationService;
        private readonly IELearningDataService _eLearningDataService;

        private RelayCommand _navigateCommand;
        private string _originalTitle;
        private string _welcomeTitle = string.Empty;

        /// <summary>
        /// Gets the NavigateCommand.
        /// </summary>
        public RelayCommand NavigateCommand
        {
            get
            {
                return _navigateCommand
                       ?? (_navigateCommand = new RelayCommand(
                           () => _navigationService.Navigate(typeof(SecondPage))));
            }
        }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string WelcomeTitle
        {
            get
            {
                return _welcomeTitle;
            }

            set
            {
                Set(ref _welcomeTitle, value);
            }
        }

        // jha
        public ObservableCollection<ApiResult> Courses { get; set; }

        private RelayCommand _RefreshButtonCommand;

        /// <summary>
        /// Gets the RefreshButtonCommand.
        /// </summary>
        public RelayCommand RefreshButtonCommand
        {
            get
            {
                return _RefreshButtonCommand
                    ?? (_RefreshButtonCommand = new RelayCommand(
                                          async () =>
                                          {
                                              _logger.Log(this, "RelayCommand RefreshButtonCommand");
                                              await Initialize();
                                          }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService, INavigationService navigationService, IELearningDataService eLearningDataService)
        {
            _logger = new Logger();

            _dataService = dataService;
            _navigationService = navigationService;
            _eLearningDataService = eLearningDataService;
            Initialize();
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
        public void Load(DateTime lastVisit)
        {
            if (lastVisit > DateTime.MinValue)
            {
                WelcomeTitle = string.Format(
                    "{0} (last visit on the {1})",
                    _originalTitle,
                    lastVisit);
            }
        }

        private async Task Initialize()
        {
            try
            {
                var item = await _dataService.GetData();
                _originalTitle = item.Title;
                WelcomeTitle = item.Title;
                Courses = await _eLearningDataService.GetCoursesAsync();
            }
            catch (Exception ex)
            {
                // Report error here
            }
        }
    }
}