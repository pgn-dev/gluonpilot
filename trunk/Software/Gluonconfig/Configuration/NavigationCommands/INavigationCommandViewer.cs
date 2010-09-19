using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Communication.Frames.Incoming;

namespace Configuration.NavigationCommands
{
    interface INavigationCommandViewer
    {
        NavigationInstruction GetNavigationInstruction();
        void SetNavigationInstruction(NavigationInstruction ni);
    }
}
