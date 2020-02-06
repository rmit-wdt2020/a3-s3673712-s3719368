using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3673712_s3719368.Exceptions
{
    public class ItemNotFoundExcpetion : Exception
    {
        public ItemNotFoundExcpetion()
        {
        }

        public ItemNotFoundExcpetion(string message)
            : base(message)
        {
        }

        public ItemNotFoundExcpetion(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
