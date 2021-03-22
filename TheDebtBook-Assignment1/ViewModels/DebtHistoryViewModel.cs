using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Baml2006;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using TheDebtBook_Assignment1.Models;
using TheDebtBook_Assignment1.ViewModels;
using TheDebtBook_Assignment1.Views;


namespace TheDebtBook_Assignment1.ViewModels
{
    public class DebtHistoryViewModel : BindableBase
    {
        public ObservableCollection<DeptHistoryModel> deptsHistory;

        public DebtHistoryViewModel()
        {
            CurrentDept = new Dept();
        }
        public DebtHistoryViewModel(Dept b1)
        {
            CurrentDept = b1;
            List<DeptHistoryModel> calledList = b1.GetList();
            DeptsHistory = new ObservableCollection<DeptHistoryModel>();
            for (int i = 0; i < calledList.Count; i++)
            {
                if (calledList[i].Amount != 0 && calledList[i] != null)
                {
                    DeptsHistory.Add(new DeptHistoryModel(calledList[i].Name, calledList[i].Amount, calledList[i].Date));
                }
            }
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

        ICommand _addValueBtnCommand;
        public ICommand AddValueBtnCommand
        {
            get
            {
                return _addValueBtnCommand ?? (_addValueBtnCommand = new DelegateCommand(() =>
                    {
                        var newDept = new DeptHistoryModel();
                        newDept.Name = CurrentDept.Name;
                        newDept.Amount = CurrentDept.Amount;
                        newDept.Date = DateTime.Today.ToString("D");
                        deptsHistory.Add(newDept);
                        CurrentDept._deptHistory.Add(newDept);
                    }));
            }
        }
    }
}
