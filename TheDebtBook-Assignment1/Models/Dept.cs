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
        private string name;
        private int amount;

        public Dept()
        {

        }

        public Dept(string dName, int dAmount)
        {
            name = dName;
            amount = dAmount;
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

    }
}
