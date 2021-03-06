﻿using System;
using System.Collections.Generic;
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
using Microsoft.Windows.Controls.Ribbon;
using System.IO.Ports;

namespace Layout_Sistema_Supervisorio
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SerialPort SerialCom = new SerialPort();

        private void buttonBuscar_Click(object sender, RoutedEventArgs e)
        {
            // Apaga o conteúdo das combobox evitando duplicidade de valores 
            #region
            comboBoxSerialPortName.Items.Clear();
            comboBoxBaudRate.Items.Clear();
            comboBoxParity.Items.Clear();
            comboBoxDataBits.Items.Clear();
            comboBoxStopBits.Items.Clear();

            comboBoxSerialPortName.Text = "";
            comboBoxBaudRate.Text = "";
            comboBoxParity.Text = "";
            comboBoxDataBits.Text = "";
            comboBoxStopBits.Text = "";
            #endregion

            if (checkBoxSerialUsb.IsChecked == true)
            {               

                // Carrega os nomes das Portas seriais disponíveis no PC
                #region
                string[] serialPortName = SerialPort.GetPortNames();

                for (int i = 0; i < serialPortName.Length; i++)
                {
                    comboBoxSerialPortName.Items.Add(serialPortName[i]);
                }
                if (serialPortName.Length != 0)
                {
                    comboBoxSerialPortName.SelectedIndex = 0;
                }
                #endregion


                // Carrega o Baud Rate na combobox
                #region
                if (serialPortName.Length != 0)
                {
                    comboBoxBaudRate.Items.Add(300);
                    comboBoxBaudRate.Items.Add(600);
                    comboBoxBaudRate.Items.Add(1200);
                    comboBoxBaudRate.Items.Add(2400);
                    comboBoxBaudRate.Items.Add(4800);
                    comboBoxBaudRate.Items.Add(9600);
                    comboBoxBaudRate.Items.Add(14400);
                    comboBoxBaudRate.Items.Add(19200);
                    comboBoxBaudRate.Items.Add(38400);
                    comboBoxBaudRate.Items.Add(56000);
                    comboBoxBaudRate.Items.Add(57600);
                    comboBoxBaudRate.Items.Add(115200);
                    comboBoxBaudRate.Items.Add(128000);
                    comboBoxBaudRate.Items.Add(230400);
                    comboBoxBaudRate.Items.Add(256000);

                    comboBoxBaudRate.SelectedIndex = 5;
                }
                #endregion

                // Carrega Parity na combobox
                #region            
                if (serialPortName.Length != 0)
                {
                    int j = 0;
                    foreach (string str in Enum.GetNames(typeof(Parity)))
                    {
                        comboBoxParity.Items.Add(str);

                        if (str == "None") comboBoxParity.SelectedIndex = j;
                        j++;
                    }
                }
                #endregion

                // Carrega Data Bits
                #region           
                if (serialPortName.Length != 0)
                {
                    comboBoxDataBits.Items.Add(5);
                    comboBoxDataBits.Items.Add(6);
                    comboBoxDataBits.Items.Add(7);
                    comboBoxDataBits.Items.Add(8);

                    comboBoxDataBits.SelectedIndex = 3;
                }
                #endregion

                // Carrega Stop Bits
                #region
                if (serialPortName.Length != 0)
                {
                    int h = 0;
                    foreach (string str in Enum.GetNames(typeof(StopBits)))
                    {
                        comboBoxStopBits.Items.Add(str);

                        if (str == "One") comboBoxStopBits.SelectedIndex = h;
                        h++;
                    }
                }
                #endregion

                // abilita as comandos
                #region
                if (serialPortName.Length != 0)
                {
                    comboBoxSerialPortName.IsEnabled = true;
                    comboBoxBaudRate.IsEnabled = true;
                    comboBoxParity.IsEnabled = true;
                    comboBoxDataBits.IsEnabled = true;
                    comboBoxStopBits.IsEnabled = true;

                    buttonConectar.IsEnabled = true;
                }
                else
                {
                    comboBoxSerialPortName.IsEnabled = !true;
                    comboBoxBaudRate.IsEnabled = !true;
                    comboBoxParity.IsEnabled = !true;
                    comboBoxDataBits.IsEnabled = !true;
                    comboBoxStopBits.IsEnabled = !true;

                    buttonConectar.IsEnabled = !true;
                }
                #endregion

            }
        }

        private void buttonConectar_Click(object sender, RoutedEventArgs e)
        {
            // Verifica se a porta já esta aberta, caso esta já esteja sera fechada 
            if (SerialCom.IsOpen == true) SerialCom.Close();

            // Seta os paraêmetros da serial
            SerialCom.PortName = comboBoxSerialPortName.Text;
            SerialCom.BaudRate = Int32.Parse(comboBoxBaudRate.Text);
            SerialCom.Parity = (Parity)comboBoxParity.SelectedIndex;
            SerialCom.DataBits = Int32.Parse(comboBoxDataBits.Text);
            SerialCom.StopBits = (StopBits)comboBoxStopBits.SelectedIndex;

        }

        private void buttonDesconectar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSair_Click(object sender, RoutedEventArgs e)
        {

        }

         

        private void checkBoxSerialUsb_Click(object sender, RoutedEventArgs e)
        {
            switch (checkBoxSerialUsb.IsChecked)
            {
                case true:
                    checkBoxWifi.IsChecked = false;
                    checkBoxBluetooth.IsChecked = false;
                    tabControlComunicacao.SelectedIndex = 0;
                    buttonBuscar.IsEnabled = true;

                    break;
                case false:
                    buttonBuscar.IsEnabled = false;
                    break;
            }
        }

        private void checkBoxWifi_Click(object sender, RoutedEventArgs e)
        {
            switch (checkBoxWifi.IsChecked)
            {
                case true:
                    checkBoxSerialUsb.IsChecked = false;
                    checkBoxBluetooth.IsChecked = false;
                    tabControlComunicacao.SelectedIndex = 1;
                    buttonBuscar.IsEnabled = true;

                    break;
                case false:
                    buttonBuscar.IsEnabled = false;
                    break;
            }

        }

        private void checkBoxBluetooth_Click(object sender, RoutedEventArgs e)
        {
            switch (checkBoxBluetooth.IsChecked)
            {
                case true:
                    checkBoxSerialUsb.IsChecked = false;
                    checkBoxWifi.IsChecked = false;
                    tabControlComunicacao.SelectedIndex = 2;
                    buttonBuscar.IsEnabled = true;

                    break;
                case false:
                    buttonBuscar.IsEnabled = false;
                    break;
            }

        }
    }
}
