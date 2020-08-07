using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Task
{
    public class E_Room
    {
        Int32 _RoomNo;
        Int32 _RoomType_ID;
        Int32 _Floor;
        Decimal _Area;
        Int32 _NoofPlaces;
        Decimal _PriceForNight;
        String _Action;

        public Int32 RoomNo
        {
            set { _RoomNo = value; }
            get { return _RoomNo; }
        }
        public Int32 RoomType_ID
        {
            set { _RoomType_ID = value; }
            get { return _RoomType_ID; }
        }

        public Int32 Floor
        {
            set { _Floor = value; }
            get { return _Floor; }
        }

        public Decimal Area
        {
            set { _Area = value; }
            get { return _Area; }
        }

        public Int32 NoofPlaces
        {
            set { _NoofPlaces = value; }
            get { return _NoofPlaces; }
        }

        public Decimal  PriceForNight
        {
            set { _PriceForNight = value; }
            get { return _PriceForNight; }
        }

        public String Action
        {
            set { _Action = value; }
            get { return _Action; }
        }
    }
}
