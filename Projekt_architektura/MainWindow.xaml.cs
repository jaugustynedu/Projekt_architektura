using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projekt_architektura
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Memory> MemoryAddresses = new();
        public List<Register> Registers = new();

        public MainWindow()
        {
            InitializeComponent();

            var registerNames = new[] {"Ah", "Bh", "Ch", "Dh", "Al", "Bl", "Cl", "Dl"};
            foreach (var reg in registerNames)
                Registers.Add(new Register {Name = reg, Value = "00"});
            

            for (var i = 0; i < 65536; i++) //65 536 => 64KB
                MemoryAddresses.Add(new Memory {Name = i.ToString("X").PadLeft(4, '0'), Value = "00"});

            OneRegisterOperationList.ItemsSource = Registers;
            TwoRegistersOperationListFirst.ItemsSource = Registers;
            TwoRegistersOperationListSecond.ItemsSource = Registers;
            DirectAddressingToRegister.ItemsSource = Registers;
            DirectAddressingFromRegister.ItemsSource = Registers;
            BasedAddressingRegister.ItemsSource = Registers;
            IndexBasedAddressingRegister.ItemsSource = Registers;
        }

        public class Register
        {
            public string Name { get; init; }
            public string Value { get; set; }
        }

        public class Memory
        {
            public string Name { get; init; }
            public string Value { get; set; }
        }
    }
}
