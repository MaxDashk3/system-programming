using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5_1
{
    public class DeskLamp
    {
        // 4 властивості різних типів
        public string Brand { get; set; }   
        public double Power { get; set; }     
        public bool IsOn { get; set; }
        public List<string> Modes { get; set; }

        // 2 конструктори
        public DeskLamp()
        {
            Brand = "Невідомо";
            Power = 10.0;
            IsOn = false;
            Modes = new List<string> { "Стандартний" };
        }
        public DeskLamp(string brand, double power, List<string> modes)
        {
            Brand = brand;
            Power = power;
            IsOn = false;
            Modes = modes;
        }

        // 3 методи
        public void Toggle()
        {
            IsOn = !IsOn;
        }
        public string GetStatus()
        {
            return IsOn ? "Світить" : "Вимкнена";
        }
        public void AddMode(string newMode)
        {
            Modes.Add(newMode);
        }
    }
}
