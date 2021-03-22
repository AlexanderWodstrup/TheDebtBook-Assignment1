using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TheDebtBook_Assignment1.Models
{
    public class DeptHistoryModel : BindableBase
    {
        private string name;
        private int amount;
        private string date;

        public DeptHistoryModel()
        {

        }
        public DeptHistoryModel(string dName, int dAmount, string dDate)
        {
            name = dName;
            amount = dAmount;
            //date = DateTime.Now.ToString("dd/mm");
            date = dDate;
        }
        public DeptHistoryModel Clone()
        {
            return this.MemberwiseClone() as DeptHistoryModel;
        }

        public string Name
        {
            get { return name;}
            set { SetProperty(ref name, value); }
        }
        public string Date
        {
            get { return date;}
            set { SetProperty(ref date, value); }
        }
        public int Amount
        {
            get { return amount;}
            set { SetProperty(ref amount, value); }
        }
    }
}
