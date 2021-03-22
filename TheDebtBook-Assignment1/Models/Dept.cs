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
        public List<DeptHistoryModel> _deptHistory = new List<DeptHistoryModel>();
        int amount;
        string date;

        public Dept()
        {

        }
        public Dept(string dName, int dAmount)
        {
            name = dName;
            amount = dAmount;
            string date = DateTime.Today.ToString("D");
            _deptHistory.Add(new DeptHistoryModel() { Name = name, Date = date, Amount = dAmount });
        }

        public Dept(string dName, int dAmount, string dDate)
        {
            name = dName;
            amount = dAmount;
            //date = DateTime.Now.ToString("dd/mm");
            date = dDate;
            _deptHistory.Add(new DeptHistoryModel() {Name = name, Date = date, Amount = dAmount });
        }

        public Dept Clone()
        {
            return this.MemberwiseClone() as Dept;
        }

        public List<DeptHistoryModel> GetList()
        {
            return _deptHistory;
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
                //_deptHistory.Add(new DeptHistoryModel() { Name = name, Date = "date", Amount = amount - lastAmount });
            }
        }
    }
}
