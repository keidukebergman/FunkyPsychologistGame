using System;

namespace DH.DrawingModule.Exceptions
{
    public class SystemCouldNotBeActivated : Exception
    {
        public SystemCouldNotBeActivated() : base("Drawing system could not be activated")
        {}
    }
}