using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class DeptViewModel : BindableBase
    {
        public DeptViewModel(Dept dept)
        {
            CurrentDept = dept;
        }

        private Dept currentDept;

        public Dept CurrentDept
        {
            get { return currentDept; }
            set { SetProperty(ref currentDept, value); }
        }
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDept.Name))
                {
                    isValid = false;
                }
                return isValid;
            }
        }

        ICommand _saveBtnCommand;
        public ICommand SaveBtnCommand
        {
            get
            {
                return _saveBtnCommand ?? (_saveBtnCommand = new DelegateCommand(
                        SaveBtnCommand_Execute, SaveBtnCommand_CanExecute)
                    .ObservesProperty(() => CurrentDept.Name)
                    .ObservesProperty(() => CurrentDept.Amount));
            }
        }

        private void SaveBtnCommand_Execute()
        {}

        private bool SaveBtnCommand_CanExecute()
        {
            return IsValid;
        }
    }
}
