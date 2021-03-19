using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TheDebtBook_Assignment1.Models
{
    public class Dept : BindableBase
    {
        string name;
        List<DeptHistoryModel> _deptHistory = new List<DeptHistoryModel>();
        int amount;

        public Dept()
        {

        }

        public Dept(string dName, int dAmount)
        {
            name = dName;
            //amount = dAmount;
            string date = DateTime.Now.ToString("dd/mm");
            _deptHistory.Add(new DeptHistoryModel() {Name = name, Date = date, Amount = dAmount });
        }

        public Dept Clone()
        {
            return this.MemberwiseClone() as Dept;
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
                foreach (var VARIABLE in _deptHistory)
                {
                    amount = amount + VARIABLE.Amount;
                }
                return amount;
            }
            set
            {
                SetProperty(ref amount, value);
            }
        }
    }
}
