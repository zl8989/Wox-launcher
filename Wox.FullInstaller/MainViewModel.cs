using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Windows;
using Wox.FullInstaller.Annotations;

namespace Wox.FullInstaller
{
    class MainViewModel : INotifyPropertyChanged
    {
        private string _message = "Checking existing installation.";
        private bool _checked = false;
        private string _python = "Python";
        private bool _installPython = true;
        private string _everything = "Everything";
        private bool _installEverything = true;
        private string _wox = "Wox";

        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                OnPropertyChanged();
            }
        }

        public bool Checked
        {
            get { return _checked; }
            set
            {
                if (value == _checked) return;
                _checked = value;
                OnPropertyChanged();
            }
        }

        public string Python
        {
            get { return _python; }
            set
            {
                if (value == _python) return;
                _python = value;
                OnPropertyChanged();
            }
        }

        public bool InstallPython
        {
            get { return _installPython; }
            set
            {
                if (value == _installPython) return;
                _installPython = value;
                OnPropertyChanged();
            }
        }

        public string Everything
        {
            get { return _everything; }
            set
            {
                if (value == _everything) return;
                _everything = value;
                OnPropertyChanged();
            }
        }

        public bool InstallEverything
        {
            get { return _installEverything; }
            set
            {
                if (value == _installEverything) return;
                _installEverything = value;
                OnPropertyChanged();
            }
        }

        public string Wox
        {
            get { return _wox; }
            set
            {
                if (value == _wox) return;
                _wox = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Task.Run(() =>
            {
                InstallEverything = !EverythingInstalled();
                MessageBox.Show(InstallEverything.ToString());
                Checked = true;
            });
        }

        public bool EverythingInstalled()
        {
            ServiceController sc;
            const string name = "Everything";
            try
            {
                sc = new ServiceController(name);
            }
            catch (ArgumentException)
            {
                return false;
            }
            bool running;
            try
            {
                running = sc.Status == ServiceControllerStatus.Running;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            if (running)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
