using System;

namespace DH.DrawingModule.Exceptions
{
    public class SystemIsNotActive : Exception
    {
        public SystemIsNotActive() : base("Drawing module is not active")
        {
        }
    }
}