using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Task
{
    public class E_Reservation
    {
        Int32 _Reservation_ID;
        Int32 _Room_ID;
        Int32 _UserID;
        Int32 _No_of_Guest;
        DateTime _From;
        DateTime _To;
        String _Action;

        public Int32 Reservation_ID
        {
            set { _Reservation_ID = value; }
            get { return _Reservation_ID; }
        }

        public Int32 Room_ID
        {
            set { _Room_ID = value; }
            get { return _Room_ID; }
        }

        public Int32 UserID
        {
            set { _UserID = value; }
            get { return _UserID; }
        }

        public Int32 No_of_Guest
        {
            set { _No_of_Guest = value; }
            get { return _No_of_Guest; }
        }

        public DateTime From
        {
            set { _From = value; }
            get { return _From; }
        }
        public DateTime To
        {
            set { _To = value; }
            get { return _To; }
        }

        public String Action
        {
            set { _Action = value; }
            get { return _Action; }
        }
    }
}
