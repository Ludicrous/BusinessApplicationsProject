﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplicationsProject.ViewModel
{
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Line Up"; }
        }
    }
}
