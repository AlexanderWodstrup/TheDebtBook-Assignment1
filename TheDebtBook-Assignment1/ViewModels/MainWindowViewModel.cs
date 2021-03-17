using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Dept> depts;
        private string AppTitle = "The Dept Book - Assignment1";

        public MainWindowViewModel()
        {
            depts = new ObservableCollection<Dept>
            {
                #if DEBUG
                new Dept("Patrik", 500),
                new Dept("Søren", 50)
                #endif
            };
            AppTitle = "Unnamed - " + AppTitle;
        }

        public ObservableCollection<Dept> Depts
        {
            get { return depts; }
            set { SetProperty(ref depts, value); }
        }

        private Dept currentDept = null;

        public Dept CurrentDept
        {
            get { return currentDept; }
            set { SetProperty(ref currentDept, value); }
        }

        private int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { SetProperty(ref currentIndex, value); }
        }

        
    }
}
