using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    class TraceRawElementTable
    {

        /// <summary>
        /// The operation qeue
        /// </summary>
        protected Queue<TraceElement> rawElementQeue;

        /// <summary>
        /// The instance
        /// </summary>
        private static TraceRawElementTable instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// The enqeue operation mutex
        /// </summary>
        private object _qeueLock;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static TraceRawElementTable getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TraceRawElementTable();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceRawElementTable"/> class from being created.
        /// </summary>
        private TraceRawElementTable()
        {
            _qeueLock = new object();
            rawElementQeue = new Queue<TraceElement>();
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void clearAll()
        {
            rawElementQeue.Clear();
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        public void addRawElement(TraceElement newElement)
        {
            if (newElement == null)
            {
                return;
            }

            if (newElement.Type == TraceType.UNKNOWN)
            {
                return;
            }

            if (newElement.FileName == null)
            {
                return;
            }

            if (newElement.FileName.Length == 0)
            {
                return;
            }

            lock (_qeueLock)
            {
                rawElementQeue.Enqueue(newElement);
            }
        }

        /// <summary>
        /// Gets the next.
        /// </summary>
        /// <returns></returns>
        public TraceElement getNext()
        {
            TraceElement element = new TraceElement();

            lock (_qeueLock)
            {
                if (rawElementQeue.Count > 0)
                {
                    element = rawElementQeue.Dequeue();
                }
            }

            return element;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <returns></returns>
        public int getSize()
        {
            return rawElementQeue.Count;
        }
    }
}
