using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input.Preview.Injection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestKeys
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.UI.Input.Preview.Injection.InputInjector _injector = InputInjector.TryCreate();
                /*
                            List<InjectedInputKeyboardInfo> inputs = new List<InjectedInputKeyboardInfo>();
                            InjectedInputKeyboardInfo _keyF1down = new InjectedInputKeyboardInfo();
                            _keyF1down.VirtualKey = 0x70; //F1
                            _keyF1down.KeyOptions = InjectedInputKeyOptions.None;
                            _keyF1down.ScanCode = 0x00;
                            inputs.Add(_keyF1down);

                            InjectedInputKeyboardInfo _keyF1up = new InjectedInputKeyboardInfo();
                            _keyF1up.VirtualKey = 0x70; //F1
                            _keyF1up.KeyOptions = InjectedInputKeyOptions.KeyUp;
                            _keyF1up.ScanCode = 0x00;
                            inputs.Add(_keyF1up);

                            //_injector.InjectKeyboardInput(inputs);
                */
                List<InjectedInputKeyboardInfo> inputs = getKeyInputs(new Windows.System.VirtualKey[] {
                Windows.System.VirtualKey.F1,
                Windows.System.VirtualKey.F2,
                Windows.System.VirtualKey.F3,
                Windows.System.VirtualKey.F4,
                Windows.System.VirtualKey.F5,
                Windows.System.VirtualKey.F6,
                //Windows.System.VirtualKey.F7, //unsupported value?!
                //Windows.System.VirtualKey.F8,
                //Windows.System.VirtualKey.F9,
                //Windows.System.VirtualKey.F10
                //Windows.System.VirtualKey.A,
                //Windows.System.VirtualKey.Enter,
                });
                _injector.InjectKeyboardInput(inputs);
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        List<InjectedInputKeyboardInfo> getKeyInputs(Windows.System.VirtualKey[] vkeys)
        {
            List<InjectedInputKeyboardInfo> inputs = new List<InjectedInputKeyboardInfo>();

            foreach(ushort u in vkeys)
            {
                InjectedInputKeyboardInfo _keydown = new InjectedInputKeyboardInfo();
                _keydown.VirtualKey = (ushort)u;
                _keydown.KeyOptions = InjectedInputKeyOptions.None;
                _keydown.ScanCode = 0x00;
                inputs.Add(_keydown);

                InjectedInputKeyboardInfo _keyup = new InjectedInputKeyboardInfo();
                _keyup.VirtualKey = (ushort)u;
                _keyup.KeyOptions = InjectedInputKeyOptions.KeyUp;
                _keyup.ScanCode = 0x00;
                inputs.Add(_keyup);
            }
            return inputs;
        }

        class vkey
        {
            public ushort value { get; set; }
            public ushort scan { get; set; }
            public vkey(ushort keyval, ushort keyscan)
            {
                value = keyval;
                scan = keyscan;
            }
        }

        private void Grid_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Keydown: " + e.Key.ToString());
        }

        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Keyup: " + e.Key.ToString());
        }
    }
}
