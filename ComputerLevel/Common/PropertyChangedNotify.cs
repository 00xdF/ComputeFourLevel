using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerLevel.Common
{
    internal class PropertyChangedNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //提醒UI发生了改变,使用下面的注解可以增加容错性
        public void DoNotify([CallerMemberName]string propName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
