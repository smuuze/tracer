using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.app.types
{
    class TraceElementTable
    {

        /// <summary>
        /// The operation qeue
        /// </summary>
        protected Queue<TraceElement> elementQeue;

        /// <summary>
        /// The instance
        /// </summary>
        private static TraceElementTable instance = null;

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
        public static TraceElementTable getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new TraceElementTable();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceElementTable"/> class from being created.
        /// </summary>
        private TraceElementTable()
        {
            _qeueLock = new object();
            elementQeue = new Queue<TraceElement>();
        }

        /// <summary>
        /// Clears all.
        /// </summary>
        public void clearAll()
        {
            elementQeue.Clear();
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        public void addElement(TraceElement newElement)
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
                elementQeue.Enqueue(newElement);
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
                if (elementQeue.Count > 0)
                {
                    element = elementQeue.Dequeue();
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
            return elementQeue.Count;
        }
    }
}
