using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Task
{
    public class E_RoomType
    {
        Int32 _RoomTypeID;
        String _RoomType;
        String _Action;

        public Int32 RoomTypeID
        {
            set { _RoomTypeID = value; }
            get { return _RoomTypeID; }
        }
        public String RoomType
        {
            set { _RoomType = value; }
            get { return _RoomType; }
        }

        public String Action
        {
            set { _Action = value; }
            get { return _Action; }
        }


    }
}
