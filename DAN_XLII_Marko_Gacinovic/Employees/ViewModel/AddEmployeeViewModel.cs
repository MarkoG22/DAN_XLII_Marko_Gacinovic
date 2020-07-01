using Employees.Commands;
using Employees.Models;
using Employees.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Employees.ViewModel
{
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployee AddEmployee;

        static BackgroundWorker bw = new BackgroundWorker();

        private tblEmployee employee;
        public tblEmployee Employee
        {
            get { return employee; }
            set { employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<tblEmployee> employeeList;

        public List<tblEmployee> EmployeeList
        {
            get { return employeeList; }
            set { employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private bool isUpdateEmployee;
        public bool IsUpdateEmployee
        {
            get
            {
                return isUpdateEmployee;
            }
            set
            {
                isUpdateEmployee = value;
            }
        }

        public AddEmployeeViewModel(AddEmployee addEmployeeOpen)
        {
            employee = new tblEmployee();
            AddEmployee = addEmployeeOpen;
            bw.DoWork += bw_DoWork;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);

            try
            {
                using (EmployeesEntities context = new EmployeesEntities())
                {
                    tblEmployee newEmployee = new tblEmployee();


                    // name validations
                    if (employee.FullName.All(Char.IsLetter))
                    {
                        newEmployee.FullName = employee.FullName;
                    }
                    else
                    {
                        MessageBox.Show("Wrong Full Name input, please try again.");
                    }


                    newEmployee.JMBG = employee.JMBG;
                    newEmployee.GenderID = employee.GenderID;

                    DateTime dateOfBirth = new DateTime();

                    // JMBG validations and getting the date of birth from the JMBG
                    if (JmbgInputValidation(newEmployee.JMBG) == false)
                    {
                        MessageBox.Show("Wrong input, please check your JMBG (13 characters).");
                    }

                    dateOfBirth = DateTime.Parse("1/1/1");
                    if (newEmployee.JMBG[4] == '0')
                    {
                        dateOfBirth = DateTime.ParseExact(newEmployee.JMBG[0].ToString() + newEmployee.JMBG[1].ToString() + "/" + newEmployee.JMBG[2] + newEmployee.JMBG[3]
                           + "/" + "2" + newEmployee.JMBG[4] + newEmployee.JMBG[5] + newEmployee.JMBG[6], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (newEmployee.JMBG[4] == '9')
                    {
                        dateOfBirth = DateTime.ParseExact(newEmployee.JMBG[0].ToString() + newEmployee.JMBG[1].ToString() + "/" + newEmployee.JMBG[2] + newEmployee.JMBG[3]
                            + "/" + "1" + newEmployee.JMBG[4] + newEmployee.JMBG[5] + newEmployee.JMBG[6], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    newEmployee.DateOfBirth = employee.DateOfBirth;
                    employee.EmployeeID = newEmployee.EmployeeID;

                    newEmployee.IdCardNumber = employee.IdCardNumber;
                    newEmployee.LocationOfEmployee = employee.LocationOfEmployee;
                    newEmployee.PhoneNumber = employee.PhoneNumber;
                    newEmployee.Manager = employee.Manager;
                    newEmployee.GenderID = employee.GenderID;
                    newEmployee.SectorID = employee.SectorID;

                    // saving data to the database
                    context.tblEmployees.Add(newEmployee);

                    context.SaveChanges();

                    // logging the action
                    FileActions.Instance.AddingEmployee(FileActions.path, FileActions.actions, DateTime.Now, newEmployee.FullName);

                    IsUpdateEmployee = true;
                }
                AddEmployee.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong input, please check your JMBG.");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                bw.RunWorkerAsync();
            }
            catch (Exception)
            {
                MessageBox.Show("Already saving...");
            }
        }

        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(employee.FullName) || String.IsNullOrEmpty(employee.JMBG) || String.IsNullOrEmpty(employee.IdCardNumber) || 
                 String.IsNullOrEmpty(employee.LocationOfEmployee) || JmbgInputValidation(employee.JMBG) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // command for closing the window
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// method for closing the window
        /// </summary>
        private void CloseExecute()
        {
            try
            {
                AddEmployee.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        private bool JmbgInputValidation(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Length == 13 && input[i] >= '0' && input[i] <= '9' && input.All(Char.IsDigit))
                {
                    if (input[0] <= '3' && input[2] < '2')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


    }
}
