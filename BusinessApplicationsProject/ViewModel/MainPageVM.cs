﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplicationsProject.ViewModel
{
    class MainPageVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "MainPage"; }
        }
    }
}
