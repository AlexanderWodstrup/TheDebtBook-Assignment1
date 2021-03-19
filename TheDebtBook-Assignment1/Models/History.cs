using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TheDebtBook_Assignment1.Models
{
    public class History : BindableBase
    {
        string date;
        int debtValue;

        public History(string hDate, int hDebtValue) 
        {
            date = hDate;
            debtValue = hDebtValue;
        }

        public string Date
        {
            get { return date; }
            set { SetProperty(ref date, value); }
        }

        public int DebtValue
        {
            get { return debtValue; }
            set { SetProperty(ref debtValue, value); }
        }
    }
}
