using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBussinessApplications.Models
{
    class LineUp
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        private string _from;

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }
        private string _until;

        public string Until
        {
            get { return _until; }
            set { _until = value; }
        }
        private Stage _stage;

        public Stage Stage
        {
            get { return _stage; }
            set { _stage = value; }
        }
        private Band _band;

        public Band Band
        {
            get { return _band; }
            set { _band = value; }
        }


    }
}
