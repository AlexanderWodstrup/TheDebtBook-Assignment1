using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using TheDebtBook_Assignment1.Models;
using TheDebtBook_Assignment1.Views;
using TheDebtBook_Assignment1.Data;
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
using System.Xml.Serialization;

namespace TheDebtBook_Assignment1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public ObservableCollection<Dept> depts;
        private string filePath = "";
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

        #region Properties

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

        #endregion

        #region Commands

        ICommand _newCommand;
        public ICommand AddNewDeptCommand
        {
            get
            {
                return _newCommand ?? (_newCommand = new DelegateCommand(() =>
                {
                    var newDept = new Dept();
                    var vm = new DeptViewModel(newDept);
                    var dlg = new AddDept
                    {
                        DataContext = vm
                    };
                    
                    if (dlg.ShowDialog() == true)
                    {
                        bool alreadyExists = false;
                        foreach (var depts in Depts)
                        {
                            if (depts.Name.Equals(newDept.Name))
                            {
                                depts._deptHistory.Add(new DeptHistoryModel(depts.Name, newDept.Amount, "Dato"));
                                depts.Amount = depts.Amount + newDept.Amount;
                                alreadyExists = true;
                            }
                        }

                        if (alreadyExists == false)
                        {
                            Depts.Add(newDept);
                            CurrentDept = newDept;
                            CurrentDept._deptHistory.Add(new DeptHistoryModel(newDept.Name, newDept.Amount, "Dato"));
                        }
                    }
                }));
            }
        }

        ICommand _editCommand;
        public ICommand EditDeptCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new DelegateCommand(() =>
                    {
                        var tmpDept = CurrentDept.Clone();
                        var vm = new DebtHistoryViewModel(tmpDept);

                        var dlg = new DebtHistory();
                        dlg.DataContext = vm;
                        dlg.Owner = App.Current.MainWindow;
                        if (dlg.ShowDialog() == true)
                        {
                            // Copy values back
                            //CurrentDept.Name = tmpDept.Name;
                            int lastAmount = 0;

                            for (int i = 0; i < CurrentDept._deptHistory.Count; i++)
                            {
                                if (CurrentDept._deptHistory[i].Amount != 0 && CurrentDept._deptHistory[i] != null)
                                {
                                    lastAmount += CurrentDept._deptHistory[i].Amount;
                                }
                            }
                            CurrentDept.Amount = lastAmount;
                        }
                    }
                ));
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

        ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                return _closeAppCommand ?? (_closeAppCommand = new DelegateCommand(() =>
                {
                    App.Current.MainWindow.Close();
                }));
            }
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Agent assignment documents|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                  .ObservesProperty(() => Depts.Count));
            }
        }

        private void SaveFileCommand_Execute()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Dept>));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, Depts);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Depts.Count > 0);
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, Depts);
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to save file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new DelegateCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Depts.Clear();
                filename = "";
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Dept files|*.agn|All Files|*.*",
                DefaultExt = "agn"
            };
            if (filePath == "")
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    Repository.ReadFile(filePath, out ObservableCollection<Dept> tempDepts);
                    Depts = tempDepts;
                    Dirty = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion
    }
}