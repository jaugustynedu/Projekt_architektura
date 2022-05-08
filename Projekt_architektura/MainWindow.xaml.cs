using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text;

namespace Projekt_architektura
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public List<Register> registers = new List<Register>();
        public List<Memory> memoryAdresses = new List<Memory>();
        public MainWindow()
        {
            InitializeComponent();
            var registerNames = new string[] { "AH", "BH", "CH", "DH", "AL", "BL", "CL", "DL" };
            for (int i = 0; i < registerNames.Length; i++)
            {
                registers.Add(new Register { Name = registerNames[i], Value = "00" });
            }

            firstRegisterList.ItemsSource = registers;
            secondRegisterList.ItemsSource = registers;
            singleRegisterList.ItemsSource = registers;

            for (int i = 0; i < 65536; i++) //65 536 => 64Kb
            {
                memoryAdresses.Add(new Memory { Name = i.ToString("X"), Value = "0000" });
            }
        }

        private void MOV_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object input = FindName("result" + firstReg.Name);
                TextBlock inputChild = input as TextBlock;
                inputChild.Text = secondReg.Value;

                firstReg.Value = secondReg.Value;
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }
        private void XCHG_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
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
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }


        private void AND_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object firstOutput = FindName("result" + firstReg.Name);
                object secondOutput = FindName("result" + secondReg.Name);
                TextBlock firstOutputChild = firstOutput as TextBlock;
                TextBlock secondOutputChild = secondOutput as TextBlock;

                var firstRegBinary = String.Join(String.Empty, firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = String.Join(String.Empty, secondReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (int i = 0; i < firstRegBinary.Length; i++)
                {
                    if (firstRegBinary[i] == '1' && secondRegBinary[i] == '1')
                        result.Append("1");
                    else
                        result.Append("0");
                }

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstReg.Value = resultHex;

                if (resultHex.Length == 1)
                {
                    firstOutputChild.Text = "0" + resultHex.ToUpper();
                    firstReg.Value = "0" + resultHex.ToUpper();
                }
                else 
                { 
                    firstOutputChild.Text = resultHex.ToUpper();
                    firstReg.Value = resultHex.ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");

            }
        }
        private void OR_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object firstOutput = FindName("result" + firstReg.Name);
                object secondOutput = FindName("result" + secondReg.Name);
                TextBlock firstOutputChild = firstOutput as TextBlock;
                TextBlock secondOutputChild = secondOutput as TextBlock;

                var firstRegBinary = String.Join(String.Empty, firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = String.Join(String.Empty, secondReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (int i = 0; i < firstRegBinary.Length; i++)
                {
                    if (!(firstRegBinary[i] == '0' && secondRegBinary[i] == '0'))
                        result.Append("1");
                    else
                        result.Append("0");
                }

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstReg.Value = resultHex;

                if (resultHex.Length == 1)
                {
                    firstOutputChild.Text = "0" + resultHex.ToUpper();
                    firstReg.Value = "0" + resultHex.ToUpper();
                }
                else
                {
                    firstOutputChild.Text = resultHex.ToUpper();
                    firstReg.Value = resultHex.ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");

            }
        }
        private void XOR_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object firstOutput = FindName("result" + firstReg.Name);
                object secondOutput = FindName("result" + secondReg.Name);
                TextBlock firstOutputChild = firstOutput as TextBlock;
                TextBlock secondOutputChild = secondOutput as TextBlock;

                var firstRegBinary = String.Join(String.Empty, firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = String.Join(String.Empty, secondReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (int i = 0; i < firstRegBinary.Length; i++)
                {
                    if ((firstRegBinary[i] == '1' && secondRegBinary[i] == '0') || (firstRegBinary[i] == '0' && secondRegBinary[i] == '1'))
                        result.Append("1");
                    else
                        result.Append("0");
                }

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstReg.Value = resultHex;

                if (resultHex.Length == 1)
                {
                    firstOutputChild.Text = "0" + resultHex.ToUpper();
                    firstReg.Value = "0" + resultHex.ToUpper();
                }
                else
                {
                    firstOutputChild.Text = resultHex.ToUpper();
                    firstReg.Value = resultHex.ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");

            }
        }
        private void ADD_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object firstOutput = FindName("result" + firstReg.Name);
                object secondOutput = FindName("result" + secondReg.Name);
                TextBlock firstOutputChild = firstOutput as TextBlock;
                TextBlock secondOutputChild = secondOutput as TextBlock;

                var firstRegInt = Convert.ToInt32(firstReg.Value, 16);
                var secondRegInt = Convert.ToInt32(secondReg.Value, 16);

                var result = new StringBuilder();

                int sum = (firstRegInt + secondRegInt) < 255 ? (firstRegInt + secondRegInt) : (firstRegInt + secondRegInt) % 256;
                result.Append(sum.ToString("x"));

                firstReg.Value = result.ToString();

                if (result.Length == 1)
                {
                    firstOutputChild.Text = "0" + result.ToString().ToUpper();
                    firstReg.Value = "0" + result.ToString().ToUpper();
                }
                else
                {
                    firstOutputChild.Text = result.ToString().ToUpper();
                    firstReg.Value = result.ToString().ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");

            }
        }
        private void SUB_Operation(object sender, RoutedEventArgs e)
        {
            Register firstReg = (Register)firstRegisterList.SelectedItem;
            Register secondReg = (Register)secondRegisterList.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                object firstOutput = FindName("result" + firstReg.Name);
                object secondOutput = FindName("result" + secondReg.Name);
                TextBlock firstOutputChild = firstOutput as TextBlock;
                TextBlock secondOutputChild = secondOutput as TextBlock;

                var firstRegInt = Convert.ToInt32(firstReg.Value, 16);
                var secondRegInt = Convert.ToInt32(secondReg.Value, 16);

                var result = new StringBuilder();

                int diff = (firstRegInt - secondRegInt) >= 0 ? (firstRegInt - secondRegInt) : 256 + (firstRegInt - secondRegInt); // 256 + diff, because diff has minus sign
                result.Append(diff.ToString("x"));

                firstReg.Value = result.ToString();

                if (result.Length == 1)
                {
                    firstOutputChild.Text = "0" + result.ToString().ToUpper();
                    firstReg.Value = "0" + result.ToString().ToUpper();
                }
                else
                {
                    firstOutputChild.Text = result.ToString().ToUpper();
                    firstReg.Value = result.ToString().ToUpper();
                }
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");

            }
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
            foreach (var reg in registers)
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
        public void INC_Operation(object sender, RoutedEventArgs e)
        {

            Register singleReg = (Register)singleRegisterList.SelectedItem;

            if (singleReg != null)
            {
                object input = FindName("result" + singleReg.Name);
                TextBlock inputChild = input as TextBlock;
                int intFromHex = int.Parse(singleReg.Value, System.Globalization.NumberStyles.HexNumber) + 1;

                if(intFromHex == 256)
                    singleReg.Value = "0";
                else
                    singleReg.Value = intFromHex.ToString("X").ToString();

                if (singleReg.Value.Length == 1)
                    inputChild.Text = "0" + singleReg.Value.ToUpper();
                else
                    inputChild.Text = singleReg.Value.ToUpper();
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }
        public void DEC_Operation(object sender, RoutedEventArgs e)
        {
            Register singleReg = (Register)singleRegisterList.SelectedItem;

            if (singleReg != null)
            {
                object input = FindName("result" + singleReg.Name);
                TextBlock inputChild = input as TextBlock;
                int intFromHex = int.Parse(singleReg.Value, System.Globalization.NumberStyles.HexNumber) - 1;

                if (intFromHex == -1)
                    singleReg.Value = "FF";
                else
                    singleReg.Value = intFromHex.ToString("X").ToString();

                if (singleReg.Value.Length == 1)
                    inputChild.Text = "0" + singleReg.Value.ToUpper();
                else
                    inputChild.Text = singleReg.Value.ToUpper();
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }
        public void NOT_Operation(object sender, RoutedEventArgs e)
        {
            Register singleReg = (Register)singleRegisterList.SelectedItem;

            if (singleReg != null)
            {
                object input = FindName("result" + singleReg.Name);
                TextBlock inputChild = input as TextBlock;
                string binarystring = String.Join(String.Empty, singleReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                string BinaryNot = string.Concat(binarystring.Select(x => x == '0' ? '1' : '0'));

                singleReg.Value = Convert.ToInt32(BinaryNot, 2).ToString("X");

                if (singleReg.Value.Length == 1)
                    inputChild.Text = "0" + singleReg.Value.ToUpper();
                else
                    inputChild.Text = singleReg.Value.ToUpper();
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }
        public void NEG_Operation(object sender, RoutedEventArgs e)
        {
            Register singleReg = (Register)singleRegisterList.SelectedItem;

            if (singleReg != null)
            {
                object input = FindName("result" + singleReg.Name);
                TextBlock inputChild = input as TextBlock;

                NOT_Operation(sender, e);
                INC_Operation(sender, e);
                MessageBox.Show(singleReg.Value);

                if (singleReg.Value.Length == 1)
                    inputChild.Text = "0" + singleReg.Value.ToUpper();
                else
                    inputChild.Text = singleReg.Value.ToUpper();
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }
        /*public void checkMemoryAddress(object sender, RoutedEventArgs e)
        {
            object input = memoryAdressName;
            TextBlock inputChild = input as TextBlock;

            Memory test = (Memory)memoryAdresses.Select(x => x.Name);
            var test2 = from entry in memoryAdresses select entry.Name;
            MessageBox.Show(test.Value);
            memoryAdresses.First(l => l.Name == "nazwa")

        }*/

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
        public class Memory
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}