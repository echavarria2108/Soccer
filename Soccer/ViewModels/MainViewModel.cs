﻿using System;
namespace Soccer.ViewModels
{
    public class MainViewModel
    {

        #region Properties

        public LoginViewModel Login { get; set; }


		#endregion



		#region Constructor

		public MainViewModel()
		{
            Login = new LoginViewModel();
		}

        #endregion

    }
}
