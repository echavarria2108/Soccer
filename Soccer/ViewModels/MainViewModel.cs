﻿using GalaSoft.MvvmLight.Command;
using Soccer.Models;
using Soccer.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System;

namespace Soccer.ViewModels
{
    public class MainViewModel
    {

        #region Properties

        public LoginViewModel Login { get; set; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public User CurrentUser { get; set; }


        #endregion



        #region Constructor

        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            LoadMenu();
        }



		#region Singleton
		private static MainViewModel instance;

		public static MainViewModel GetInstance()
		{
			if (instance == null)
			{
				instance = new MainViewModel();
			}

			return instance;
		}
		#endregion



		#endregion

		#region Methods

		private void LoadMenu()
		{
            Menu = new ObservableCollection<MenuItemViewModel>();

			Menu.Add(new MenuItemViewModel
			{
				Icon = "predictions.png",
				PageName = "SelectTournamentPage",
				Title = "Predictions",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "groups.png",
				PageName = "GroupsPage",
				Title = "Groups",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "tournaments.png",
				PageName = "TournamentsPage",
				Title = "Tournaments",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "myresults.png",
				PageName = "ResultsPage",
				Title = "My Results",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "config.png",
				PageName = "ConfigPage",
				Title = "Config",
			});

			Menu.Add(new MenuItemViewModel
			{
				Icon = "logut.png",
				PageName = "LoginPage",
				Title = "Logut",
			});

		}

        #endregion

    }
}
