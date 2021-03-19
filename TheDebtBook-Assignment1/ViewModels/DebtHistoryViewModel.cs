using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class DebtHistoryViewModel : BindableBase
    {
        
        public DebtHistoryViewModel(string title, Dept dept)
        {
            Title = title;
            CurrentDept = dept;
        }

        string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private Dept currentDept;

        public Dept CurrentDept
        {
            get { return currentDept; }
            set { SetProperty(ref currentDept, value); }
        }
    }
}
