using Employees.Commands;
using Employees.Models;
using Employees.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Employees.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        private List<tblEmployee> employeeList;
        public List<tblEmployee> EmployeeList
        {
            get
            {
                return employeeList;
            }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private tblEmployee viewEmployee;

        public tblEmployee ViewEmployee
        {
            get { return viewEmployee; }
            set { viewEmployee = value;
                OnPropertyChanged("ViewEmployee");
            }
        }

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            EmployeeList = GetAllEmployees().ToList();
        }

        private ICommand addNewEmployee;
        public ICommand AddNewEmployee
        {
            get
            {
                if (addNewEmployee == null)
                {
                    addNewEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());
                }
                return addNewEmployee;
            }
        }

        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }

        /// <summary>
        /// method for button for opening the view for adding the new user
        /// </summary>
        private void AddNewEmployeeExecute()
        {
            try
            {
                AddEmployee user = new AddEmployee();
                user.ShowDialog();
                if ((user.DataContext as AddEmployeeViewModel).IsUpdateEmployee == true)
                {
                    EmployeeList = GetAllEmployees().ToList();

                    // sorting the view in the MainWindow
                    main.DataGridEmployees.Items.SortDescriptions.Add(new SortDescription("LocationOfEmployee", ListSortDirection.Ascending));
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }

        private bool CanDeleteEmployeeExecute()
        {
            if (ViewEmployee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteEmployeeExecute()
        {
            try
            {
                using (EmployeesEntities context = new EmployeesEntities())
                {
                    // geting the registration number of the user
                    string jmbg = ViewEmployee.JMBG;

                    // confirmation for the action
                    MessageBoxResult messageBoxResult = MessageBox.Show("Deleting the employee will delete all his records. \nAre you sure you want to delete?", "Delete Confirmation", MessageBoxButton.YesNo);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        // getting the user from the database
                        tblEmployee employeeToDelete = (from x in context.tblEmployees where x.JMBG == jmbg select x).First();
                        
                        // removin the user from the database
                        context.tblEmployees.Remove(employeeToDelete);
                        
                        context.SaveChanges();

                        // logging the action
                        FileActions.Instance.DeletingEmployee(FileActions.path, FileActions.actions, DateTime.Now, employeeToDelete.FullName);

                        EmployeeList = GetAllEmployees().ToList();
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("The employee can not be deleted, please try again.");
            }
        }

        private ICommand editEmployee;
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        private bool CanEditEmployeeExecute()
        {
            if (ViewEmployee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// method for opening the view for the editing user
        /// </summary>
        private void EditEmployeeExecute()
        {
            //try
            //{
            //    EditEmployee editEmployee = new EditEmployee(ViewEmployee);
            //    editEmployee.ShowDialog();
            //    if ((editEmployee.DataContext as EditEmployeeViewModel).IsUpdateUser == true)
            //    {
            //        EmployeeList = GetAllEmployees().ToList();
            //        main.DataGridEmployees.Items.SortDescriptions.Add(new SortDescription("Location", ListSortDirection.Ascending));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            //}
        }

        public List<tblEmployee> GetAllEmployees()
        {
            try
            {
                using (EmployeesEntities context = new EmployeesEntities())
                {
                    List<tblEmployee> list = new List<tblEmployee>();
                    list = (from x in context.tblEmployees select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
