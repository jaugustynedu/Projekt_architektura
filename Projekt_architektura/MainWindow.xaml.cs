using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt_architektura
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public List<Register> registers = new List<Register>();
        public MainWindow()
        {
            InitializeComponent();

            registers.Add(new Register { Name = "AH", Value = "00" });
            registers.Add(new Register { Name = "BH", Value = "00" });
            registers.Add(new Register { Name = "CH", Value = "00" });
            registers.Add(new Register { Name = "DH", Value = "00" });
            registers.Add(new Register { Name = "AL", Value = "00" });
            registers.Add(new Register { Name = "BL", Value = "00" });
            registers.Add(new Register { Name = "CL", Value = "00" });
            registers.Add(new Register { Name = "DL", Value = "00" });

            firstRegisterList.ItemsSource = registers;
            secondRegisterList.ItemsSource = registers;
        }

        private void Button_Registry(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            
        }

        private void MOV_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            object input = FindName("result"+firstReg.Name);
            TextBlock inputChild = input as TextBlock;
            inputChild.Text = secondReg.Value;

            firstReg.Value = secondReg.Value;
        }
        private void XCHG_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            object firstOutput = FindName("result" + firstReg.Name);
            object secondOutput = FindName("result" + secondReg.Name);
            TextBlock firstOutputChild = firstOutput as TextBlock;
            TextBlock secondOutputChild = secondOutput as TextBlock;

            string temp = firstReg.Value;
            firstOutputChild.Text = secondReg.Value;
            secondOutputChild.Text = temp;

            firstReg.Value = secondReg.Value;
            secondReg.Value = temp;

        }
        private void MOVALL_Operation(object sender, RoutedEventArgs e)
        {
            foreach (var reg in registers)
            {
                object input = FindName(reg.Name);
                TextBox inputChild = input as TextBox;

                object output = FindName("result" + reg.Name);
                TextBlock outputChild = output as TextBlock;
                
                if (!String.IsNullOrWhiteSpace(inputChild.Text))
                {
                    if (Register.HexValidator(inputChild.Text.ToUpper()))
                    {
                        inputChild.BorderBrush = System.Windows.Media.Brushes.Green;
                        inputChild.BorderThickness = new System.Windows.Thickness(2);

                        if (inputChild.Text.Length == 1)
                        {
                            inputChild.Text = "0" + inputChild.Text.ToUpper();
                        }
                        reg.Value = inputChild.Text.ToUpper();
                        outputChild.Text = inputChild.Text.ToUpper();

                    }
                    else
                    {
                        inputChild.BorderBrush = System.Windows.Media.Brushes.Red;
                        inputChild.BorderThickness = new System.Windows.Thickness(1.5);
                    }
                }
                else
                {
                    outputChild.Text = "00";
                }

            }
        }
        public void CLEAR_Operation(object sender, RoutedEventArgs e)
        {
            foreach(var reg in registers)
            {
                object input = FindName(reg.Name);
                TextBox inputChild = input as TextBox;
                inputChild.Text = "";
                inputChild.ClearValue(Border.BorderBrushProperty);
                inputChild.ClearValue(Border.BorderThicknessProperty);
            }
        }
        public void RANDOM_Operation(object sender, RoutedEventArgs e)
        {
            foreach (var reg in registers)
            {
                object input = FindName(reg.Name);
                TextBox inputChild = input as TextBox;
                inputChild.ClearValue(Border.BorderBrushProperty);
                inputChild.ClearValue(Border.BorderThicknessProperty);
                inputChild.Text = Register.RandomHexGenerator();
            }
        }
    }

    public class Register
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public static bool HexValidator(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!((input[i] >= '0' && input[i] <= '9') || input[i] >= 'A' && input[i] <= 'F'))
                {
                    return false;
                }
            }
            return true;
        }
        public static string RandomHexGenerator()
        {
            const string chars = "0123456789ABCDEF";
            var rand = new Random();
            return new string(Enumerable.Repeat(chars, 2).Select(s => s[rand.Next(s.Length)]).ToArray());
        }
    }
}
