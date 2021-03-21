using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;
using TheDebtBook_Assignment1.ViewModels;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class DebtHistoryViewModel : BindableBase
    {
        public ObservableCollection<DeptHistoryModel> deptsHistory;
        private string name;
        int amount;
        private string date;
        public DebtHistoryViewModel()
        {

        }
        public DebtHistoryViewModel(Dept b1)
        {
            //Dept b1 = new Dept();
            List<DeptHistoryModel> calledList = b1.GetList();
            deptsHistory = new ObservableCollection<DeptHistoryModel>();
            foreach (var VARIABLE in calledList)
            {
                deptsHistory.Add(new DeptHistoryModel(VARIABLE.Name, VARIABLE.Amount, "21-03-2021"));
            }
            
            CurrentDept = null;
        }

        public ObservableCollection<DeptHistoryModel> DeptsHistory
        {
            get { return deptsHistory; }
            set { SetProperty(ref deptsHistory, value); }
        }

        Dept currentDept = null;

        public Dept CurrentDept
        {
            get { return currentDept; }
            set { SetProperty(ref currentDept, value); }
        }
        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }
        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                SetProperty(ref amount, value);
            }
        }

        public string Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }
    }
}
