﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class DeptViewModel : BindableBase
    {
        public DeptViewModel(string title, Dept dept)
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
        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDept.Name))
                    isValid = false;
                return isValid;
            }
            //set
            //{
            //    SetProperty(ref isValid, value);
            //}
        }
    }
}