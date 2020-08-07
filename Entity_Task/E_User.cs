using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Task
{
    public class E_User
    {
        Int32 _UserId;
        String _UserName;
        String _Surname;
        String _MobileNo;
        String _EmailID;
        String _Action;


        public Int32 UserId
        {
            set { _UserId = value; }
            get { return _UserId; }
        }
        public String UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }
        public String Surname
        {
            set { _Surname = value; }
            get { return _Surname; }
        }
        public String EmailID
        {
            set { _EmailID = value; }
            get { return _EmailID; }
        }
        public String MobileNo
        {
            set { _MobileNo = value; }
            get { return _MobileNo; }
        }
        public String Action
        {
            set { _Action = value; }
            get { return _Action; }
        }
    }
}
