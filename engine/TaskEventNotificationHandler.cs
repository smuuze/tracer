using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEngineModule
{
    class ChipdesignEventNotificationHandler
    {
        public delegate void DisplayNotification();
        public DisplayNotification Event_UpdateDisplay;

        public delegate void StatusNotification();
        public StatusNotification Event_UpdateStatus;
    }
}
