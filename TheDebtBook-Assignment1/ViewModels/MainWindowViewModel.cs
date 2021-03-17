﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;
using TheDebtBook_Assignment1.Views;
using Microsoft.Win32;
using Prism.Commands;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private ObservableCollection<Dept> depts;
        private string filePath = "";
        private string filter;
        private string AppTitle = "The Dept Book - Assignment1";

        public MainWindowViewModel()
        {
            depts = new ObservableCollection<Dept>
            {
                #if DEBUG
                new Dept("Patrick Nielsen", 500),
                new Dept("Søren", 50)
                #endif
            };
            CurrentDept = null;

        }

        public ObservableCollection<Dept> Depts
        {
            get { return depts; }
            set { SetProperty(ref depts, value); }
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

        ICommand _newCommand;
        public ICommand AddNewDeptCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    var newDept = new Dept();
                    var vm = new DeptViewModel("Add new agent", newDept);
                    var dlg = new AddDept
                    {
                        DataContext = vm
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        Depts.Add(newDept);
                        CurrentDept = newDept;
                        Dirty = true;
                    }
                }));
            }
        }
        private string filename = "";
        public string Filename
        {
            get { return filename; }
            set
            {
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                var s = "";
                if (Dirty)
                    s = "*";
                return Filename + s + " - " + AppTitle;
            }
        }

        private bool dirty = false;
        public bool Dirty
        {
            get { return dirty; }
            set
            {
                SetProperty(ref dirty, value);
                RaisePropertyChanged("Title");
            }
        }

    }
}
