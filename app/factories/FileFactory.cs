using System;
using System.Collections.Generic;
using System.IO;
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
        /// Files the exists.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public bool fileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Files the create.
        /// </summary>
        /// <param name="path">The path.</param>
        public void fileCreate(string path)
        {
            FileStream fileStream = File.Create(path);
            fileStream.Close();
        }

        /// <summary>
        /// Gets the content of the file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public string[] getFileContentAsLineArray(string filePath)
        {
            if (!fileExists(filePath))
            {
                return new string[0];
            }

            /*
            System.IO.StreamReader contextReader = new System.IO.StreamReader(filePath, System.Text.Encoding.ASCII);
            List<string> fileContent = new List<string>();

            while (!contextReader.EndOfStream)
            {
                fileContent.Add(contextReader.ReadLine());
            }

            contextReader.Close();
            return fileContent.ToArray();
            */

            return System.IO.File.ReadAllLines(@filePath);
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="newLine">The new line.</param>
        public void writeLine(string filePath, string newLine)
        {
            StreamWriter writer = System.IO.File.AppendText(filePath);
            writer.WriteLine("{0}", newLine);
            writer.Close();
        }
    }
}
