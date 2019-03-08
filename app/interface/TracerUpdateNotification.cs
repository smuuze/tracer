using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.src.app
{
    public class TracerUpdateNotification
    {
        /// <summary>
        /// 
        /// </summary>
        public delegate void UpdateNotificationHandler();

        /// <summary>
        /// The event_ update notification
        /// </summary>
        public UpdateNotificationHandler Event_UpdateNotification;    
    }
}
