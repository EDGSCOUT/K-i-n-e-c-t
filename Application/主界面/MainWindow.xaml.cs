    using System;
    using System.IO;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Face;
    using System.Windows.Shapes;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Threading;
    using System.Text.RegularExpressions;
    using Microsoft.DirectX;
    using Microsoft.DirectX.DirectSound;
namespace TopWindow
{
    public partial class MainWindow : Window
    {
       private KinectSensor _sensor = null;

        private BodyFrameSource _bodySource = null;

        private BodyFrameReader _bodyReader = null;

        private HighDefinitionFaceFrameSource _faceSource = null;

        private HighDefinitionFaceFrameReader _faceReader = null;

        private FaceAlignment _faceAlignment = null;

        private FaceModel _faceModel = null;

        public int CurrentPage = 0;

        public int UserId = 0;

        FrameDescription fd;
        Byte[] colordata;
        WriteableBitmap bitmap;

        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        private List<Ellipse> _points = new List<Ellipse>();


        private List<List<double>> totaldata=new List<List<double>>();
        private List<List<Tuple<FaceShapeAnimations, float>>> audata=new List<List<Tuple<FaceShapeAnimations, float>>>();
        //private List<int> MostVariancePointIndex = new List<int>() { };

        private Dictionary<String, int> res = new Dictionary<String, int>();
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ContentControl.Content = new TopWindow.Login(1);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();
            /*ColorFrameReader cfr = _sensor.ColorFrameSource.OpenReader();
            fd = _sensor.ColorFrameSource.FrameDescription;
            colordata=new byte[fd.LengthInPixels*4];
            bitmap = new WriteableBitmap(fd.Width, fd.Height, 96, 96, PixelFormats.Bgr32, null);
            
            this.image.Source = bitmap;*/
            if (_sensor != null)
            {
                _bodySource = _sensor.BodyFrameSource;
                _bodyReader = _bodySource.OpenReader();
                //_bodyReader.FrameArrived += BodyReader_FrameArrived;

                _faceSource = new HighDefinitionFaceFrameSource(_sensor);
                _faceReader = _faceSource.OpenReader();
                //_faceReader.FrameArrived += FaceReader_FrameArrived;

                _faceModel = new FaceModel();
                _faceAlignment = new FaceAlignment();

                //cfr.FrameArrived += cfr_FrameArrived;
                //_sensor.Open();
            }
            
            
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            if (_faceModel != null)
            {
                _faceModel.Dispose();
                _faceModel = null;
            }
            
            GC.SuppressFinalize(this);
            _sensor.Close();
    
        }

        public void NextPage(int num)
        {
            SetCurrentPage(num + 1);

        }

        public void PreviousPage(int num)
        {
            if (num > 1)
            {
                SetCurrentPage(num - 1);
            }
        }

        public void SetCurrentPage(int num)
        {
            switch (num)
            {
                case 1: CurrentPage = num; ContentControl.Content = new TopWindow.Login(CurrentPage); break;
                case 2: CurrentPage = num; ContentControl.Content = new TopWindow.BodyAndVoice(CurrentPage,this.UserId); break;
                case 3: CurrentPage = num; ContentControl.Content = new TopWindow.VoiceAndVideo(CurrentPage, this.UserId); break;
                case 4: CurrentPage = num; ContentControl.Content = new QuestionnaireHealth.QuestionnaireHealth(CurrentPage); break;
                case 5: CurrentPage = num; ContentControl.Content = new BDI.BDI(CurrentPage); break;
                case 6: CurrentPage = num; ContentControl.Content = new VHI.VHI(CurrentPage); break;
                case 7: CurrentPage = num; ContentControl.Content = new Log.Log(CurrentPage, this.UserId); break;
            }
        }

        public void setUserId(int UserId)
        {
            this.UserId = UserId;
            
        }

        public int getUserId()
        {
            return this.UserId;

        }

        public string getUserPath()
        {
            return "e:\\VoiceData\\" + UserId;
        }

        public void Body()
        {
            Microsoft.Samples.Kinect.BodyBasics.MainWindow w2 = new Microsoft.Samples.Kinect.BodyBasics.MainWindow();
            Application.Current.MainWindow = w2;
            w2.WindowStartupLocation = WindowStartupLocation.Manual;
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            w2.Num = this.UserId;
            w2.Left = win.Left;
            w2.Top = win.Top;
            w2.Width = win.Width;
            w2.Height = win.Height;
            w2.WindowState = win.WindowState;
            w2.Owner = win;
            win.Hide();
            w2.Show();
        }

        public void Face()
        {
            Kinect2FaceHD_NET.MainWindow w2 = new Kinect2FaceHD_NET.MainWindow();
            Application.Current.MainWindow = w2;
            w2.WindowStartupLocation = WindowStartupLocation.Manual;
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            w2.setUserId(this.UserId);
            w2.Left = win.Left;
            w2.Top = win.Top;
            w2.Width = win.Width;
            w2.Height = win.Height;
            w2.WindowState = win.WindowState;
            w2.Owner = win;
            win.Hide();
            w2.Show();
        }

        public void Voice(int StartLocation)
        {
            Voice.MainWindow w2 = new Voice.MainWindow(StartLocation);
            Application.Current.MainWindow = w2;
            w2.WindowStartupLocation = WindowStartupLocation.Manual;
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            w2.setUserId(this.UserId);
            w2.Left = win.Left;
            w2.Top = win.Top;
            w2.Width = win.Width;
            w2.Height = win.Height;
            w2.WindowState = win.WindowState;
            w2.Owner = win;
            win.Hide();
            w2.Show();
        }

        public void EmotionStudy()
        {
            EmotionStudy.MainWindow w2 = new EmotionStudy.MainWindow();
            Application.Current.MainWindow = w2;
            w2.WindowStartupLocation = WindowStartupLocation.Manual;
            TopWindow.MainWindow win = Window.GetWindow(this) as TopWindow.MainWindow;
            w2.setUserId(this.UserId);
            w2.Left = win.Left;
            w2.Top = win.Top;
            w2.Width = win.Width;
            w2.Height = win.Height;
            w2.WindowState = win.WindowState;
            w2.Owner = win;
            win.Hide();
            w2.Show();
        }
       
    }
}