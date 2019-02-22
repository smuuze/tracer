using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Factory
{
    class FileFactory
    {

        /// <summary>
        /// The instance
        /// </summary>
        private static FileFactory instance = null;

        /// <summary>
        /// The _lock instance
        /// </summary>
        private static object _lockInstance = new object();

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceParser" /> class from being created.
        /// </summary>
        private FileFactory()
        {

        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static FileFactory getInstance()
        {
            if (instance == null)
            {
                lock (_lockInstance)
                {
                    if (instance == null)
                    {
                        instance = new FileFactory();
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public string[] getFileContentAsLineArray(string filePath)
        {
            System.IO.StreamReader contextReader = new System.IO.StreamReader(filePath);
            List<string> fileContent = new List<string>();

            while (!contextReader.EndOfStream)
            {
                fileContent.Add(contextReader.ReadLine());
            }

            contextReader.Close();
            return fileContent.ToArray();
        }
    }
}
