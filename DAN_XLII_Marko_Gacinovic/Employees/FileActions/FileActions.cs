using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class FileActions
    {
        /// <summary>
        /// Singleton design pattern
        /// </summary>
        private static FileActions instance;

        public static FileActions Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileActions();
                }
                return instance;
            }
        }

        // static fields for logging actions to the file
        public static string path = @"../../FileActions/Log.txt";
        public static List<string> actions = new List<string>();


        private FileActions()
        {

        }

        /// <summary>
        /// method for writing action of adding user to the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <param name="datetime"></param>
        /// <param name="employee"></param>
        public void AddingEmployee(string path, List<string> list, DateTime datetime, string employee)
        {
            list.Add(datetime + " - added employee " + employee);
            File.AppendAllLines(path, list);

            list.Clear();
        }

        /// <summary>
        /// method for writing action of deleting user to the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <param name="datetime"></param>
        /// <param name="employee"></param>
        public void DeletingEmployee(string path, List<string> list, DateTime datetime, string employee)
        {
            list.Add(datetime + " - deleted employee " + employee);
            File.AppendAllLines(path, list);

            list.Clear();
        }

        /// <summary>
        /// method for writing action of editing user to the file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="list"></param>
        /// <param name="datetime"></param>
        /// <param name="employee"></param>
        public void EditingEmployee(string path, List<string> list, DateTime datetime, string employee)
        {
            list.Add(datetime + " - edited employee " + employee);
            File.AppendAllLines(path, list);

            list.Clear();
        }
    }
}
