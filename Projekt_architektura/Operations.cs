using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projekt_architektura
{
    public partial class MainWindow
    {
        #region Double registers operations

        private void MovOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var input = FindName("Result" + firstReg.Name);
                if (input is TextBlock inputChild) inputChild.Text = secondReg.Value;

                firstReg.Value = secondReg.Value.PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        private void XchgOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var secondOutput = FindName("Result" + secondReg.Name);
                var firstOutputChild = firstOutput as TextBlock;
                var secondOutputChild = secondOutput as TextBlock;

                var temp = firstReg.Value;
                if (firstOutputChild != null) firstOutputChild.Text = secondReg.Value;
                if (secondOutputChild != null) secondOutputChild.Text = temp;

                firstReg.Value = secondReg.Value.PadLeft(2, '0'); ;
                secondReg.Value = temp.PadLeft(2, '0'); ;
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }


        private void AndOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var firstOutputChild = firstOutput as TextBlock;

                var firstRegBinary = string.Join(string.Empty,
                    firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = string.Join(string.Empty,
                    secondReg.Value.Select(c =>
                        Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (var i = 0; i < firstRegBinary.Length; i++)
                    if (firstRegBinary[i] == '1' && secondRegBinary[i] == '1')
                        result.Append("1");
                    else
                        result.Append("0");

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstOutputChild.Text = resultHex.ToUpper().PadLeft(2, '0');
                firstReg.Value = resultHex.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        private void OrOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var firstOutputChild = firstOutput as TextBlock;

                var firstRegBinary = string.Join(string.Empty,
                    firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = string.Join(string.Empty,
                    secondReg.Value.Select(c =>
                        Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (var i = 0; i < firstRegBinary.Length; i++)
                    if (!(firstRegBinary[i] == '0' && secondRegBinary[i] == '0'))
                        result.Append("1");
                    else
                        result.Append("0");

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstOutputChild.Text = resultHex.ToUpper().PadLeft(2, '0');
                firstReg.Value = resultHex.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        private void XorOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var firstOutputChild = firstOutput as TextBlock;

                var firstRegBinary = string.Join(string.Empty,
                    firstReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var secondRegBinary = string.Join(string.Empty,
                    secondReg.Value.Select(c =>
                        Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var result = new StringBuilder();

                for (var i = 0; i < firstRegBinary.Length; i++)
                    if (firstRegBinary[i] == '1' && secondRegBinary[i] == '0' ||
                        firstRegBinary[i] == '0' && secondRegBinary[i] == '1')
                        result.Append("1");
                    else
                        result.Append("0");

                var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                firstOutputChild.Text = resultHex.ToUpper().PadLeft(2, '0');
                firstReg.Value = resultHex.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        private void AddOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var firstOutputChild = firstOutput as TextBlock;

                var firstRegInt = Convert.ToInt32(firstReg.Value, 16);
                var secondRegInt = Convert.ToInt32(secondReg.Value, 16);
                var result = new StringBuilder();

                var sum = firstRegInt + secondRegInt < 255
                    ? firstRegInt + secondRegInt
                    : (firstRegInt + secondRegInt) % 256;
                result.Append(sum.ToString("x"));
                
                firstOutputChild.Text = result.ToString().ToUpper().PadLeft(2, '0');
                firstReg.Value = result.ToString().ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        private void SubOperation(object sender, RoutedEventArgs e)
        {
            var firstReg = (Register) TwoRegistersOperationListFirst.SelectedItem;
            var secondReg = (Register) TwoRegistersOperationListSecond.SelectedItem;

            if (firstReg != null && secondReg != null)
            {
                var firstOutput = FindName("Result" + firstReg.Name);
                var firstOutputChild = firstOutput as TextBlock;

                var firstRegInt = Convert.ToInt32(firstReg.Value, 16);
                var secondRegInt = Convert.ToInt32(secondReg.Value, 16);

                var result = new StringBuilder();

                var diff = firstRegInt - secondRegInt >= 0
                    ? firstRegInt - secondRegInt
                    : 256 + (firstRegInt - secondRegInt); // 256 + diff, because diff has minus sign
                result.Append(diff.ToString("x"));

                firstOutputChild.Text = result.ToString().ToUpper().PadLeft(2, '0');
                firstReg.Value = result.ToString().ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz oba rejestry!");
            }
        }

        #endregion

        #region Single Register operations

        public void IncOperation(object sender, RoutedEventArgs e) // increase
        {
            var singleReg = (Register) OneRegisterOperationList.SelectedItem;

            if (singleReg != null)
            {
                var input = FindName("Result" + singleReg.Name);
                var inputChild = input as TextBlock;
                var intFromHex = int.Parse(singleReg.Value, NumberStyles.HexNumber) + 1;

                if (intFromHex == 256)
                    singleReg.Value = "00";
                else
                    singleReg.Value = intFromHex.ToString("X").PadLeft(2, '0');

                inputChild.Text = singleReg.Value.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }

        public void DecOperation(object sender, RoutedEventArgs e) //decrease
        {
            var singleReg = (Register) OneRegisterOperationList.SelectedItem;

            if (singleReg != null)
            {
                var input = FindName("Result" + singleReg.Name);
                var inputChild = input as TextBlock;
                var intFromHex = int.Parse(singleReg.Value, NumberStyles.HexNumber) - 1;

                if (intFromHex == -1)
                    singleReg.Value = "FF";
                else
                    singleReg.Value = intFromHex.ToString("X").PadLeft(2, '0');

                inputChild.Text = singleReg.Value.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }

        public void NotOperation(object sender, RoutedEventArgs e)
        {
            var singleReg = (Register) OneRegisterOperationList.SelectedItem;

            if (singleReg != null)
            {
                var input = FindName("Result" + singleReg.Name);
                var inputChild = input as TextBlock;
                var binaryString = string.Join(string.Empty,
                    singleReg.Value.Select(c =>
                        Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var binaryNot = string.Concat(binaryString.Select(x => x == '0' ? '1' : '0'));

                singleReg.Value = Convert.ToInt32(binaryNot, 2).ToString("X").PadLeft(2, '0');
                inputChild.Text = singleReg.Value.ToUpper().PadLeft(2, '0');
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }

        public void NegOperation(object sender, RoutedEventArgs e)
        {
            var singleReg = (Register) OneRegisterOperationList.SelectedItem;

            if (singleReg != null)
            {
                var input = FindName("Result" + singleReg.Name);
                var inputChild = input as TextBlock;

                NotOperation(sender, e);
                IncOperation(sender, e);

                inputChild.Text = singleReg.Value.ToUpper().PadLeft(2, '0');
                ;
            }
            else
            {
                MessageBox.Show("Wybierz rejestr!");
            }
        }

        #endregion

        #region All registers random movall clear

        private void MovAllOperation(object sender, RoutedEventArgs e)
        {
            foreach (var reg in Registers)
            {
                var input = FindName(reg.Name);
                var inputChild = input as TextBox;

                var output = FindName("Result" + reg.Name);
                var outputChild = output as TextBlock;

                if (inputChild != null && !string.IsNullOrWhiteSpace(inputChild.Text))
                {
                    if (Utils.HexValidator(inputChild.Text.ToUpper()))
                    {
                        inputChild.BorderBrush = Brushes.Green;
                        inputChild.BorderThickness = new Thickness(2);

                        if (inputChild.Text.Length == 1) inputChild.Text = "0" + inputChild.Text.ToUpper();
                        reg.Value = inputChild.Text.ToUpper();
                        if (outputChild != null) outputChild.Text = inputChild.Text.ToUpper();
                    }
                    else
                    {
                        inputChild.BorderBrush = Brushes.Red;
                        inputChild.BorderThickness = new Thickness(1.5);
                    }
                }
                else
                {
                    if (outputChild != null) outputChild.Text = "00";
                }
            }
        }

        public void ClearOperation(object sender, RoutedEventArgs e)
        {
            foreach (var reg in Registers)
            {
                // reg.Value = "00";
                var input = FindName(reg.Name);
                var inputChild = input as TextBox;
                if (inputChild == null) continue;
                inputChild.Text = "";
                inputChild.ClearValue(Border.BorderBrushProperty);
                inputChild.ClearValue(Border.BorderThicknessProperty);
            }
        }

        public void RandomOperation(object sender, RoutedEventArgs e)
        {
            foreach (var reg in Registers)
            {
                var input = FindName(reg.Name);
                var inputChild = input as TextBox;
                if (inputChild == null) continue;
                inputChild.ClearValue(Border.BorderBrushProperty);
                inputChild.ClearValue(Border.BorderThicknessProperty);
                inputChild.Text = Utils.RandomHexGenerator8Bit();
            }
        }


        #endregion

        #region Memory operations

        public void CheckMemoryAddress(object sender, RoutedEventArgs e)
        {
            var input = ChooseMemoryAddressName;
            var mem = MemoryAddresses.First(l => l.Name == input.Text.ToUpper().PadLeft(4, '0'));
            MemoryAddressNameResult.Text = mem.Name + ":";
            MemoryAddressValueResult.Text = mem.Value;
        }

        public void PerformDirectAddressingToRegister(object sender, RoutedEventArgs e)
        {
            var toReg = (Register) DirectAddressingToRegister.SelectedItem;
            var fromMemoryObj = FindName("DirectAddressingFromMemory");
            var fromMemory = fromMemoryObj as TextBox;

            if (toReg == null || string.IsNullOrWhiteSpace(fromMemory.Text)) return;
            if (Utils.HexValidator(fromMemory.Text.ToUpper()))
            {
                fromMemory.BorderBrush = Brushes.DarkGray;
                fromMemory.BorderThickness = new Thickness(1);

                var memoryAddress = MemoryAddresses.First(x => x.Name == fromMemory.Text);
                toReg.Value = memoryAddress.Value;

                var input = FindName("Result" + toReg.Name);
                if (input is TextBlock inputChild) inputChild.Text = memoryAddress.Value;
            }
            else
            {
                fromMemory.BorderBrush = Brushes.Red;
                fromMemory.BorderThickness = new Thickness(1.5);
            }
        }

        public void PerformDirectAddressingToMemory(object sender, RoutedEventArgs e)
        {
            var fromReg = (Register) DirectAddressingFromRegister.SelectedItem;
            var toMemoryObj = FindName("DirectAddressingToMemory");
            var toMemory = toMemoryObj as TextBox;

            if (fromReg == null || string.IsNullOrWhiteSpace(toMemory.Text)) return;
            if (Utils.HexValidator(toMemory.Text.ToUpper()))
            {
                toMemory.BorderBrush = Brushes.DarkGray;
                toMemory.BorderThickness = new Thickness(1);

                var memoryAddress = MemoryAddresses.First(x => x.Name == toMemory.Text.PadLeft(4, '0'));
                memoryAddress.Value = fromReg.Value;
            }
            else
            {
                toMemory.BorderBrush = Brushes.Red;
                toMemory.BorderThickness = new Thickness(1.5);
            }
        }

        public void PerformIndirectAddressingToRegister(object sender, RoutedEventArgs e)
        {
            var toReg = IndirectAddressingToRegister.SelectionBoxItem.ToString();
            var fromMemoryObj = FindName("IndirectAddressingFromMemory");
            var fromMemory = fromMemoryObj as TextBox;

            if (toReg == null || string.IsNullOrWhiteSpace(fromMemory.Text)) return;
            if (Utils.HexValidator(fromMemory.Text.ToUpper()))
            {
                fromMemory.BorderBrush = Brushes.DarkGray;
                fromMemory.BorderThickness = new Thickness(1);

                if (toReg == "BX")
                {
                    var bh = Registers.First(x => x.Name == "Bh");
                    var bl = Registers.First(x => x.Name == "Bl");
                    var bhTestBlock = FindName("ResultBh") as TextBlock;
                    var blTestBlock = FindName("ResultBl") as TextBlock;

                    var mem = MemoryAddresses.First(x => x.Name == fromMemory.Text.PadLeft(4, '0'));
                    var intValue = Convert.ToInt32(fromMemory.Text, 16);
                    var mem2name = (intValue + 1).ToString("X");
                    if (mem2name == "10000") mem2name = "0";
                    var mem2 = MemoryAddresses.First(x => x.Name == mem2name.PadLeft(4, '0'));
                    bh.Value = mem.Value;
                    bl.Value = mem2.Value;
                    if (bhTestBlock != null) bhTestBlock.Text = mem.Value;
                    if (blTestBlock != null) blTestBlock.Text = mem2.Value;
                }
                else
                {
                    var regObj = FindName(toReg);
                    var reg = regObj as TextBox;
                    reg.Text = fromMemory.Text.PadLeft(4, '0');
                }
            }
            else
            {
                fromMemory.BorderBrush = Brushes.Red;
                fromMemory.BorderThickness = new Thickness(1.5);
            }
        }

        public void PerformIndirectAddressingToMemory(object sender, RoutedEventArgs e)
        {
            var fromReg = IndirectAddressingFromRegister.SelectionBoxItem.ToString();
            var toMemoryObj = FindName("IndirectAddressingToMemory");
            var toMemory = toMemoryObj as TextBox;

            if (fromReg == null || string.IsNullOrWhiteSpace(toMemory.Text)) return;
            if (Utils.HexValidator(toMemory.Text.ToUpper()))
            {
                toMemory.BorderBrush = Brushes.DarkGray;
                toMemory.BorderThickness = new Thickness(1);
                var mem = MemoryAddresses.First(x => x.Name == toMemory.Text.PadLeft(4, '0'));
                var intValue = Convert.ToInt32(toMemory.Text, 16);
                var mem2name = (intValue + 1).ToString("X");
                if (mem2name == "10000") mem2name = "0";
                var mem2 = MemoryAddresses.First(x => x.Name == mem2name.PadLeft(4, '0'));

                if (fromReg == "BX")
                {
                    var bh = Registers.First(x => x.Name == "Bh");
                    var bl = Registers.First(x => x.Name == "Bl");

                    mem.Value = bh.Value;
                    mem2.Value = bl.Value;
                }
                else
                {
                    var regObj = FindName(fromReg);
                    var reg = regObj as TextBox;

                    mem.Value = reg.Text[..2];
                    mem2.Value = reg.Text[2..4];
                }
            }
            else
            {
                toMemory.BorderBrush = Brushes.Red;
                toMemory.BorderThickness = new Thickness(1.5);
            }
        }

        public void PerformIndexedAddressing(object sender, RoutedEventArgs e)
        {
            var si = FindName("SI") as TextBox;
            var di = FindName("DI") as TextBox;
            var disp = FindName("DISP") as TextBox;
            var memName = "";

            if (Sidisp.IsChecked == true)
            {
                if (si != null && si.Text != "")
                {
                    var intSi = Convert.ToInt32(si.Text, 16);
                    if (disp != null && disp.Text != "")
                    {
                        var intDisp = Convert.ToInt32(disp.Text, 16);
                        memName = ((intSi + intDisp) % 10000).ToString("X");
                    }
                }
            }
            else if (Didisp.IsChecked == true)
            {
                if (di != null && di.Text != "")
                {
                    var intDi = Convert.ToInt32(di.Text, 16);
                    if (disp != null && disp.Text != "")
                    {
                        var intDisp = Convert.ToInt32(disp.Text, 16);
                        memName = ((intDi + intDisp) % 10000).ToString("X");
                    }
                }
            }

            var mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));

            var binaryString = string.Join(string.Empty,
                mem.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            var binaryNot = string.Concat(binaryString.Select(x => x == '0' ? '1' : '0'));

            mem.Value = Convert.ToInt32(binaryNot, 2).ToString("X").PadLeft(2, '0').ToUpper();
            var intFromHex = int.Parse(mem.Value, NumberStyles.HexNumber) + 1;

            if (intFromHex == 256)
                mem.Value = "0".PadLeft(2, '0');
            else
                mem.Value = intFromHex.ToString("X").PadLeft(2, '0');

        }

        public void PerformBasedAddressing(object sender, RoutedEventArgs e)
        {
            var bp = FindName("BP") as TextBox;
            var disp = FindName("DISP") as TextBox;
            var toReg = (Register)BasedAddressingRegister.SelectedItem;
            var bh = Registers.First(x => x.Name == "Bh");
            var bl = Registers.First(x => x.Name == "Bl");
            var memName = "";

            if (toReg is null) return;
            if (Bxdisp.IsChecked == true)
            {
                var intBhl = Convert.ToInt32(bh.Value + bl.Value, 16);
                var intDisp = Convert.ToInt32(disp.Text, 16);
                memName = ((intBhl + intDisp) % 10000).ToString("X");
            }
            else if (Bpdisp.IsChecked == true)
            {
                var intBp = Convert.ToInt32(bp.Text, 16);
                var intDisp = Convert.ToInt32(disp.Text, 16);
                memName = ((intBp + intDisp) % 10000).ToString("X");
            }
            
            var mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));
            var reg = FindName("Result" + toReg.Name) as TextBlock;
            reg.Text = mem.Value;
            toReg.Value = mem.Value;
        }
        public void PerformIndexBasedAddressing(object sender, RoutedEventArgs e)
        {
            var si = FindName("SI") as TextBox;
            var di = FindName("DI") as TextBox;
            var bp = FindName("BP") as TextBox;
            var disp = FindName("DISP") as TextBox;
            var toReg = (Register)IndexBasedAddressingRegister.SelectedItem;
            var bh = Registers.First(x => x.Name == "Bh");
            var bl = Registers.First(x => x.Name == "Bl");
            var memName = "";
            var intFromHex = 0;
            Memory mem = null;
            // Operacje na pamięcie DEC i NOT
            if (toReg is null) return;
            if (Sibxdisp.IsChecked == true)
            {
                if (si != null && si.Text != "")
                {
                    var intSi = Convert.ToInt32(si.Text, 16);
                    if (bp != null && bp.Text != "")
                    {
                        var intBp = Convert.ToInt32(bp.Text, 16);
                        if (disp != null && disp.Text != "")
                        {
                            var intDisp = Convert.ToInt32(disp.Text, 16);
                            memName = ((intSi + intBp + intDisp) % 10000).ToString("X");
                        }
                    }
                }

                mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));
                var binaryString = string.Join(string.Empty, mem.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                var binaryNot = string.Concat(binaryString.Select(x => x == '0' ? '1' : '0'));
                mem.Value = Convert.ToInt32(binaryNot, 2).ToString("X").PadLeft(2, '0').ToUpper();
                intFromHex = int.Parse(mem.Value, NumberStyles.HexNumber);
                if (intFromHex == 256)
                    mem.Value = "0".PadLeft(2, '0');
                else
                    mem.Value = intFromHex.ToString("X").PadLeft(2, '0');
            }
            else if (Sibpdisp.IsChecked == true)
            {
                if (si != null && si.Text != "")
                {
                    var intSi = Convert.ToInt32(si.Text, 16);
                    var intBhl = Convert.ToInt32(bh.Value + bl.Value, 16);
                    if (disp != null && disp.Text != "")
                    {
                        var intDisp = Convert.ToInt32(disp.Text, 16);
                        memName = ((intSi + intBhl + intDisp) % 10000).ToString("X");
                    }
                }

                mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));
                intFromHex = int.Parse(mem.Value, NumberStyles.HexNumber) - 1;
                if (intFromHex == 256)
                    mem.Value = "0".PadLeft(2, '0');
                else
                    mem.Value = intFromHex.ToString("X").PadLeft(2, '0');
            }
            

            // Operations AND and XOR on registers
            if (Dibpdisp.IsChecked == true)
            {
                if (di != null && di.Text != "")
                {
                    var intDi = Convert.ToInt32(di.Text, 16);
                    if (bp != null && bp.Text != "")
                    {
                        var intBp = Convert.ToInt32(bp.Text, 16);
                        if (disp != null && disp.Text != "")
                        {
                            var intDisp = Convert.ToInt32(disp.Text, 16);
                            memName = ((intDi + intBp + intDisp) % 10000).ToString("X");
                            mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));
                            
                            var regBinary = string.Join(string.Empty, toReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                            var memBinary = string.Join(string.Empty, mem.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                            var result = new StringBuilder();

                            for (var i = 0; i < regBinary.Length; i++)
                                if (regBinary[i] == '1' && memBinary[i] == '1')
                                    result.Append("1");
                                else
                                    result.Append("0");

                            var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                            toReg.Value = resultHex.PadLeft(2, '0');
                            var reg = FindName("Result" + toReg.Name) as TextBlock;
                            reg.Text = resultHex.PadLeft(2, '0');
                        }
                    }
                }
            }
            else if (Dibxdisp.IsChecked == true)
            {
                if (di != null && di.Text != "")
                {
                    var intDi = Convert.ToInt32(di.Text, 16);

                    if (disp != null && disp.Text != "")
                    {
                        var intDisp = Convert.ToInt32(disp.Text, 16);
                        var intBhl = Convert.ToInt32(bh.Value + bl.Value, 16);
                        memName = ((intDi + intBhl + intDisp) % 10000).ToString("X");
                        mem = MemoryAddresses.First(x => x.Name == memName.PadLeft(4, '0'));

                        var regBinary = string.Join(string.Empty, toReg.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                        var memBinary = string.Join(string.Empty, mem.Value.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                        var result = new StringBuilder();
                        
                        for (var i = 0; i < regBinary.Length; i++)
                            if (regBinary[i] == '1' && memBinary[i] == '0' ||
                                regBinary[i] == '0' && memBinary[i] == '1')
                                result.Append("1");
                            else
                                result.Append("0");

                        var resultHex = Convert.ToInt32(result.ToString(), 2).ToString("X");
                        toReg.Value = resultHex.PadLeft(2, '0');
                        var reg = FindName("Result" + toReg.Name) as TextBlock;
                        reg.Text = resultHex.PadLeft(2, '0');
                    }
                }
            }
        }
        #endregion
    }
}
