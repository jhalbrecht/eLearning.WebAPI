﻿using System;
using System.Threading.Tasks;
using AppDevPro.Utility;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Learning.Win8.Common;
using Learning.Win8.Model;
using Learning.Win8.Service;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;

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

        public string name { get; set; }
        public double duration { get; set; }
        public string description { get; set; }

        private RelayCommand _RefreshButtonCommand;

        public IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IDialogService>();
            }
        }

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

        private RelayCommand _PostButtonCommand;

        /// <summary>
        /// Gets the PostButtonCommand.
        /// </summary>
        public RelayCommand PostButtonCommand
        {
            get
            {
                return _PostButtonCommand
                    ?? (_PostButtonCommand = new RelayCommand(
                                         async () =>
                                         {
                                             _logger.Log(this, "RelayCommand PostButtonCommand ");
                                             // it's a proof of concept not a fully functional app! :-)
                                             // using existing know values for subject and tutor rather than creating UI.
                                             var poster = new ApiResult
                                             {
                                                 name = name,
                                                 // duration = Convert.ToDouble(duration),
                                                 duration = duration,
                                                 description = description,
                                                 subject = new Subject
                                                {
                                                    id = 6
                                                },
                                                tutor = new Tutor
                                                {
                                                    id = 2
                                                }
                                             };
                                             var answer = await _eLearningDataService.PostApiResultAsync(poster);
                                             await DialogService.ShowMessage(answer.ToString(), "Success result from post is");
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
                name = "jLearning of eLearning";
                duration = 7;
                description = "jeffa is on a quest to learn web api. Thanks for eLearning";
            }
            catch (Exception ex)
            {
                // Report error here
            }
        }
    }
}