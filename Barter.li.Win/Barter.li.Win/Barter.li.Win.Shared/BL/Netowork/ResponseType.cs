using System;
using System.Collections.Generic;
using System.Text;

namespace Barter.li.Win.BL.Netowork
{
    public class ApiResponse<T>
    {
        public int Id
        {
            get;
            set;
        }

        public T value
        {
            get;
            set;
        }
    }   
}
