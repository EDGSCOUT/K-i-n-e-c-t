using System;
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

namespace WpfSV
{
    /// <summary>
    /// MicroPhoneTest.xaml 的交互逻辑
    /// </summary>
    public partial class MicroPhoneTest : UserControl
    {
        private WaveInRecorder _recorder;
        private byte[] _recorderBuffer;
        private WaveOutPlayer _player;
        private byte[] _playerBuffer;
        private FifoStream _stream;
        private WaveFormat _waveFormat;
        private AudioFrame _audioFrame;
        private int _audioSamplesPerSecond = 22000;
        private int _audioFrameSize = 16384;
        private byte _audioBitsPerSample = 16;
        private byte _audioChannels = 2;
        private bool _isPlayer = false;
        private bool _isTest = false;
        public int PageNum;
        public MicroPhoneTest(int PageNum)
        {
            this.PageNum = PageNum;
            InitializeComponent();
            this.formhost1.Visibility = Visibility.Hidden;
            this.textBox1.Visibility = Visibility.Hidden;
            if (WaveNative.waveInGetNumDevs() == 0)
            {
                textBox1.AppendText(DateTime.Now.ToString() + " : There are no audio devices available\r\n");
            }
            else
            {
                if (_isPlayer == true)
                    _stream = new FifoStream();
                _audioFrame = new AudioFrame(_isTest);
                Start();
            }


        }

        private void Start()
        {
            Stop();
            try
            {
                _waveFormat = new WaveFormat(_audioFrameSize, _audioBitsPerSample, _audioChannels);
                _recorder = new WaveInRecorder(0, _waveFormat, _audioFrameSize * 2, 3, new BufferDoneEventHandler(DataArrived));
                if (_isPlayer == true)
                    _player = new WaveOutPlayer(-1, _waveFormat, _audioFrameSize * 2, 3, new BufferFillEventHandler(Filler));
                textBox1.AppendText(DateTime.Now.ToString() + " : Audio device initialized\r\n");
                textBox1.AppendText(DateTime.Now.ToString() + " : Audio device polling started\r\n");
                textBox1.AppendText(DateTime.Now + " : Samples per second = " + _audioSamplesPerSecond.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Frame size = " + _audioFrameSize.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Bits per sample = " + _audioBitsPerSample.ToString() + "\r\n");
                textBox1.AppendText(DateTime.Now + " : Channels = " + _audioChannels.ToString() + "\r\n");
            }
            catch (Exception ex)
            {
                textBox1.AppendText(DateTime.Now + " : Audio exception\r\n" + ex.ToString() + "\r\n");
            }
        }

        public void Stop()
        {

            if (_isPlayer == true)
            {
                if (_player != null)
                    try
                    {
                        _player.Dispose();
                    }
                    finally
                    {
                        _player = null;
                    }
                _stream.Flush(); // clear all pending data

            }

            if (_recorder != null)
                try
                {
                    _recorder.Dispose();

                }
                finally
                {
                    _recorder = null;
                }


        }

        private void Filler(IntPtr data, int size)
        {
            if (_isPlayer == true)
            {
                if (_playerBuffer == null || _playerBuffer.Length < size)
                    _playerBuffer = new byte[size];
                if (_stream.Length >= size)
                    _stream.Read(_playerBuffer, 0, size);
                else
                    for (int i = 0; i < _playerBuffer.Length; i++)
                        _playerBuffer[i] = 0;
                System.Runtime.InteropServices.Marshal.Copy(_playerBuffer, 0, data, size);
            }
        }

        private void DataArrived(IntPtr data, int size)
        {
            if (_recorderBuffer == null || _recorderBuffer.Length < size)
                _recorderBuffer = new byte[size];
            if (_recorderBuffer != null)
            {
                System.Runtime.InteropServices.Marshal.Copy(data, _recorderBuffer, 0, size);
                if (_isPlayer == true)
                    _stream.Write(_recorderBuffer, 0, _recorderBuffer.Length);
                _audioFrame.Process(ref _recorderBuffer);
                _audioFrame.RenderTimeDomain(ref userCtrl);
                _audioFrame.RenderFrequencyDomain(ref userCtrl1);
            }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(userCtrl.Handle.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Stop();
            this.NextPage();
        }

        private void NextPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.NextPage(PageNum);
        }

        private void PreviousPage()
        {
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.PreviousPage(PageNum);
        }

        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            Stop();
            Voice.MainWindow win = Window.GetWindow(this) as Voice.MainWindow;
            win.ShutDownTopwindow = false;
            win.Close();
        }
    }
}
