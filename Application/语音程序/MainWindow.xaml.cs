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
namespace Voice
{
    public partial class MainWindow : Window
    {
        private string Disk = "e";  //存放视频和数据的盘符  修改这里

        private KinectSensor _sensor = null;

        private BodyFrameSource _bodySource = null;

        private BodyFrameReader _bodyReader = null;

        private HighDefinitionFaceFrameSource _faceSource = null;

        private HighDefinitionFaceFrameReader _faceReader = null;

        private FaceAlignment _faceAlignment = null;

        private FaceModel _faceModel = null;

        public int UserId = 0;

        public int CurrentPage = 1;
        public int MaxPageNum = 25;
        public int EmotionType1, EmotionType2, EmotionType3;
        public double[] StartTime, EndTime;

        private ShowFace.ShowFace show;
        private WpfSV.MicroPhoneTest microphonetester;

        private Saving.Saving saving;

        public bool CanShowFace = false;

        public bool[] OK,JUMP;

        public String[,] FileName;
        public bool[,] FileUsable;

        private Record.SoundRecord recorder;
        public string wavfile = "C:\\Users\\ad\\Desktop\\new.wav";
        public string filepath = "e:\\文本\\";
        public string currentVideo;
        public string retryVideo;
        public int retryVideoType;
        public int readtime = 4;
        public bool ShutDownTopwindow = true;
        public bool RecordSwitch = false;
        FileStream fs = null;
        StreamWriter sw = null;

        FrameDescription fd;
        Byte[] colordata;
        WriteableBitmap bitmap;

        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        private List<Ellipse> _points = new List<Ellipse>();


        private List<List<double>> totaldata=new List<List<double>>();
        private List<List<Tuple<FaceShapeAnimations, float,double>>> audata=new List<List<Tuple<FaceShapeAnimations, float,double>>>();
        //private List<int> MostVariancePointIndex = new List<int>() { };

        private Dictionary<String, int> res = new Dictionary<String, int>();
        public MainWindow(int StartLocation)
        {
            InitializeComponent();
            OK = new bool[4] {false,false,false,false};
            JUMP = new bool[4] { false, false, false, false };
            FileName = new String[4,4];
            FileUsable = new bool[4,4];
            StartTime = new Double[4] { 0, 0, 0, 0};
            EndTime = new Double[4] { 0, 0, 0, 0};

            for (int i = 0; i <= 3; ++i)
            {
                for (int j = 0; j <= 3; ++j)
                {
                    if(j == 1)
                        FileUsable[i, j] = false;
                    else
                        FileUsable[i, j] = true;
                }     
            }

            
            //show = new ShowFace.ShowFace();
            //ContentControl.Content = show;
            //CanShowFace = true;

            //ContentControl.Content = new Player.VideoPlayer("e:\\video\\anger.avi");
            //ContentControl.Content = new ShowPicture.ShowPicture(5, 1, 1, "e:\\文本\\q11.png", "" + Disk + ":\\VoiceData\\" + UserId, false, false);
            //ContentControl.Content = new EmotionTest.EmotionTest(1);

            //ContentControl.Content = new GuideInterview.GuideInterview(1,3);
            //ContentControl.Content = new QuestionnaireDemography.QuestionnaireDemography_1();  
            //ContentControl.Content = new QuestionnaireDemography.QuestionnaireDemography_2();
            
            //ContentControl.Content = new REMIT.REMIT();  
            //ContentControl.Content = new QuestionnairePSQI.QuestionnairePSQI();
            //ContentControl.Content = new Login.Login(1);
            this.show = new ShowFace.ShowFace(1);

            switch (StartLocation)
            {
                case 1: ContentControl.Content = new Guide.Guide(1, 1); break;
                case 2: ContentControl.Content = new Guide.Guide(23, 2);break;
                case 3: ContentControl.Content = new Guide.Guide(37, 3); break;
                case 4: ContentControl.Content = new Guide.Guide(53, 4); break;
            }
            
            
            
            //ContentControl.Content = new MakeSure.MakeSure(1,1);
            //ContentControl.Content = new VideoGuide.VideoGuide(1);
            //ContentControl.Content = new Relex.Relex(1);
            //ContentControl.Content = new Report.Report(1,OK1,OK2,OK3);
            //ContentControl.Content = new End.End(1);
            //ContentControl.Content = new GuideBackupVideo.GuideBackupVideo();
            //ContentControl.Content = new BDI.BDI(1);
            //ContentControl.Content = new VHI.VHI(1);
            //ContentControl.Content = new Log.Log(1);
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
                _bodyReader.FrameArrived += BodyReader_FrameArrived;

                _faceSource = new HighDefinitionFaceFrameSource(_sensor);

                _faceReader = _faceSource.OpenReader();
                _faceReader.FrameArrived += FaceReader_FrameArrived;

                _faceModel = new FaceModel();
                _faceAlignment = new FaceAlignment();

                this.SensorOpen();
                //cfr.FrameArrived += cfr_FrameArrived;
                //_sensor.Open();
            }
        }

        /*
        private void cfr_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    FrameDescription fd = frame.FrameDescription;
                    byte[] data =new byte[fd.LengthInPixels*4];
                    if (frame.RawColorImageFormat == ColorImageFormat.Bgra)
                        frame.CopyRawFrameDataToArray(data);
                    else
                        frame.CopyConvertedFrameDataToArray(data, ColorImageFormat.Bgra);
                    bitmap.WritePixels(new Int32Rect(0, 0, fd.Width, fd.Height), data, fd.Width * 4, 0);
                    //this.image.Source = BitmapSource.Create(fd.Width,fd.Height,96,96,PixelFormats.Bgr32,null,data,fd.Width*4);

                }
            }
        }*/

        private void BodyReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            if(this.RecordSwitch)
            {
                using (var frame = e.FrameReference.AcquireFrame())
                {
                    if (frame != null)
                    {
                        Body[] bodies = new Body[frame.BodyCount];
                        frame.GetAndRefreshBodyData(bodies);

                        Body body = bodies.Where(b => b.IsTracked).FirstOrDefault();

                        if (!_faceSource.IsTrackingIdValid)
                        {
                            if (body != null)
                            {
                                _faceSource.TrackingId = body.TrackingId;
                            }
                        }
                    }
                }
            }
        }

        private void FaceReader_FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
        {
            if (this.RecordSwitch)
            {
                using (HighDefinitionFaceFrame frame = e.FrameReference.AcquireFrame())
                {
                    if (frame != null && frame.IsFaceTracked)
                    {
                        if (CanShowFace)
                        {
                            show.label.Content = "已捕捉到面部";
                            show.label.Foreground = new SolidColorBrush(Color.FromRgb(57, 104, 230));
                            show.button.IsEnabled = true;
                            show.cover.Visibility = Visibility.Hidden;
                        }

                        /*TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        String time = Convert.ToInt64(ts.TotalSeconds).ToString();

                        if (res.ContainsKey(time))
                        {
                            res[time] += 1;
                        }
                        else
                        {
                            res.Add(time, 1);
                        }*/
                        frame.GetAndRefreshFaceAlignmentResult(_faceAlignment);
                        UpdateFacePoints(frame);
                    }
                }
            }
        }
        private void UpdateFacePoints(HighDefinitionFaceFrame frame)
        {
            
            if (_faceModel == null) return;
            
            var vertices = _faceModel.CalculateVerticesForAlignment(_faceAlignment);
            IReadOnlyDictionary<FaceShapeAnimations,float> _faceShapeAnimations=_faceAlignment.AnimationUnits;
            List<Tuple<FaceShapeAnimations, float,double>> au = new List<Tuple<FaceShapeAnimations, float,double>>();

            CameraSpacePoint head = _faceAlignment.HeadPivotPoint;
            TimeSpan ts1 = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            double time1 = Convert.ToDouble(ts1.TotalSeconds);
            foreach (FaceShapeAnimations f in _faceShapeAnimations.Keys)
            {
                au.Add(new Tuple<FaceShapeAnimations,float,double>(f, _faceShapeAnimations[f],time1));
                //float x=_faceShapeAnimations[f];
            }

            audata.Add(au);
            List<double> data = new List<double>();
            //sw.WriteLine(vertices.Count);


            if (vertices.Count > 0)
            {
                if (_points.Count == 0)
                {
                    for (int index = 0; index < vertices.Count; index++)
                    {

                        Ellipse ellipse = new Ellipse
                        {
                            Width = 3.0,
                            Height = 3.0,
                            Fill = new SolidColorBrush(Colors.Blue)
                        };
                        _points.Add(ellipse);
                    }

                    foreach (Ellipse ellipse in _points)
                    {
                        if(CanShowFace)
                            show.canvas.Children.Add(ellipse);
                        
                    }
                }

                for (int index = 0; index < vertices.Count; index++)
                {
                    CameraSpacePoint vertice = vertices[index];

                    data.Add(vertice.X);
                    data.Add(vertice.Y);
                    data.Add(vertice.Z);
                    ColorSpacePoint point = _sensor.CoordinateMapper.MapCameraPointToColorSpace(vertice);
                    if (float.IsInfinity(point.X) || float.IsInfinity(point.Y)) return;

                    Ellipse ellipse = _points[index];
                    //if (MostVariancePointIndex.Contains(index))
                    //{
                      //  ellipse.Fill = new SolidColorBrush(Colors.Red);
                     //   ellipse.Width = 5
                     //   ellipse.Height = 5;
                    //}
                    Canvas.SetLeft(ellipse, point.X);
                    Canvas.SetTop(ellipse, point.Y);
                }
                data.Add(head.X);
                data.Add(head.Y);
                data.Add(head.Z);

                //double t = (DateTime.Now - startTime).TotalMilliseconds;
                //long t = (DateTime.Now.ToUniversalTime().Ticks - startTime.Ticks) / 10000;

                //long t = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
                //TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                //double time = Convert.ToInt64(ts.TotalSeconds);
                data.Add(time1);
                data.Add(frame.RelativeTime.TotalMilliseconds);
                totaldata.Add(data);
            }
        }

        private void Window_Closing_1(object sender, CancelEventArgs e)
        {
            if (CurrentPage != 0 && CurrentPage != 1 && CurrentPage != 67)
            {
                string message = "实验进行中，确定退出吗?";
                string title = "提示";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage img = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(message, title, button, img);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;    // 取消退出 
                    return;
                }
            }

            if (CurrentPage == 0)
            {
                this.microphonetester.Stop();
                this.microphonetester = null;
            }

            if (_faceModel != null)
            {
                _faceModel.Dispose();
                _faceModel = null;
            }
            this.show = null;
            GC.SuppressFinalize(this);
            _sensor.Close();
            
           
            if (ShutDownTopwindow)
                this.Owner.Close();
            else
            {
                this.Owner.Left = this.Left;
                this.Owner.Top = this.Top;
                this.Owner.Width = this.Width;
                this.Owner.Height = this.Height;
                this.Owner.WindowState = this.WindowState;
                this.Owner.Show();
            }
            
        }

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);

        private void save_1(string curvideo,int EmotionType)
        {
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(this.saving.ProgressBar.SetValue);

            string filePath = "" + Disk + ":\\VoiceData\\" + UserId;    //被试者文件夹

            if (EmotionType == 1)
            {
                fs = new FileStream(filePath  + "\\time04.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time05.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time06.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }
            else if (EmotionType == 2)
            {
                fs = new FileStream(filePath  + "\\time07.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time08.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time09.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }
            else if (EmotionType == 3)
            {
                fs = new FileStream(filePath  + "\\time10.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time11.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();

                fs = new FileStream(filePath  + "\\time12.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }
            

            

            //打开文件
            FileStream fs_1 = null, fs_2 = null, fs_3 = null;
            StreamWriter sw_1 = null, sw_2 = null, sw_3 = null;

            
            if (EmotionType == 1)
            {
                fs_1 = new FileStream(filePath  + "\\04.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath  + "\\05.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath  + "\\06.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (EmotionType == 2)
            {
                fs_1 = new FileStream(filePath  + "\\07.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath  + "\\08.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath  + "\\09.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (EmotionType == 3)
            {
                fs_1 = new FileStream(filePath  + "\\10.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath  + "\\11.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath  + "\\12.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }


            double t;
            foreach (List<double> data in this.totaldata)
            {

                t = data[4044];
                if (t >= StartTime[1] && t <= EndTime[1])
                {

                    foreach (double d in data)
                    {
                        sw_1.Write(d + ",");
                    }
                    sw_1.WriteLine();
                    
                }
                else if (t >= StartTime[2] && t <= EndTime[2])
                {
                    foreach (double d in data)
                    {
                        sw_2.Write(d + ",");
                    }
                    sw_2.WriteLine();
                    
                }
                else if (t >= StartTime[3] && t <= EndTime[3])
                {
                    foreach (double d in data)
                    {
                        sw_3.Write(d + ",");
                    }
                    sw_3.WriteLine();
                    
                }
             
            }

            sw_1.Close();
            fs_1.Close();

            sw_2.Close();
            fs_2.Close();

            sw_3.Close();
            fs_3.Close();

            this.totaldata.Clear();


            if (EmotionType == 1)
            {
                fs_1 = new FileStream(filePath   + "\\AU04.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath   + "\\AU05.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath   + "\\AU06.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (EmotionType == 2)
            {
                fs_1 = new FileStream(filePath  + "\\AU07.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath  + "\\AU08.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath  + "\\AU09.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (EmotionType == 3)
            {
                fs_1 = new FileStream(filePath  + "\\AU10.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);

                fs_2 = new FileStream(filePath  + "\\AU11.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);

                fs_3 = new FileStream(filePath  + "\\AU12.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }

            

            foreach (Tuple<FaceShapeAnimations, float,double> unit in audata[0])
            {
                sw_1.Write(unit.Item1.ToString() + ",");
            }
            sw_1.Write("timestamp");
            sw_1.WriteLine();

            foreach (Tuple<FaceShapeAnimations, float, double> unit in audata[0])
            {
                sw_2.Write(unit.Item1.ToString() + ",");
            }
            sw_2.Write("timestamp");
            sw_2.WriteLine();

            foreach (Tuple<FaceShapeAnimations, float, double> unit in audata[0])
            {
                sw_3.Write(unit.Item1.ToString() + ",");
            }
            sw_3.Write("timestamp");
            sw_3.WriteLine();

            foreach (List<Tuple<FaceShapeAnimations, float,double>> unit in audata)
            {
                double tm = unit[0].Item3;

                if (tm >= StartTime[1] && tm <= EndTime[1])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_1.Write(a.Item2 + ",");
                    }
                    sw_1.Write(tm);
                    sw_1.WriteLine();
                }
                else if (tm >= StartTime[2] && tm <= EndTime[2])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_2.Write(a.Item2 + ",");
                    }
                    sw_2.Write(tm);
                    sw_2.WriteLine();
                }
                else if (tm >= StartTime[3] && tm <= EndTime[3])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_3.Write(a.Item2 + ",");
                    }
                    sw_3.Write(tm);
                    sw_3.WriteLine();
                }

            }
            sw_1.Close();
            fs_1.Close();

            sw_2.Close();
            fs_2.Close();

            sw_3.Close();
            fs_3.Close();

            this.audata.Clear();

            Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, 100.0 });

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() => 
            { 
                this.saving.label.Content = "        保存完成";
                this.saving.ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(226, 46, 46));
                this.saving.Next.IsEnabled = true;
            }));  
        }

        private void save_2(string curvideo, int emotion1, int emotion2, int emotion3)
        {
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(this.saving.ProgressBar.SetValue);

            string filePath = "" + Disk + ":\\VoiceData\\" + UserId;    //被试者文件夹

            if (emotion1 == 1)
            {
                fs = new FileStream(filePath  + "\\time13.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion1 == 2)
            {
                fs = new FileStream(filePath  + "\\time14.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion1 == 3)
            {
                fs = new FileStream(filePath  + "\\time15.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[1].ToString());
                sw.WriteLine(EndTime[1].ToString());
                sw.Close();
                fs.Close();
            }


            if (emotion2 == 1)
            {
                fs = new FileStream(filePath  + "\\time13.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion2 == 2)
            {
                fs = new FileStream(filePath  + "\\time14.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion2 == 3)
            {
                fs = new FileStream(filePath  + "\\time15.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[2].ToString());
                sw.WriteLine(EndTime[2].ToString());
                sw.Close();
                fs.Close();
            }


            if (emotion3 == 1)
            {
                fs = new FileStream(filePath  + "\\time13.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion3 == 2)
            {
                fs = new FileStream(filePath  + "\\time14.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }
            else if (emotion3 == 3)
            {
                fs = new FileStream(filePath  + "\\time15.csv", FileMode.Create);
                sw = new StreamWriter(fs);
                sw.WriteLine(StartTime[3].ToString());
                sw.WriteLine(EndTime[3].ToString());
                sw.Close();
                fs.Close();
            }

            

            //打开文件
            FileStream fs_1 = null, fs_2 = null, fs_3 = null;
            StreamWriter sw_1 = null, sw_2 = null, sw_3 = null;

            if (emotion1 == 1)
            {
                fs_1 = new FileStream(filePath  + "\\13.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }
            else if (emotion1 == 2)
            {
                fs_1 = new FileStream(filePath  + "\\14.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }
            else if (emotion1 == 3)
            {
                fs_1 = new FileStream(filePath  + "\\15.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }


            if (emotion2 == 1)
            {
                fs_2 = new FileStream(filePath  + "\\13.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }
            else if (emotion2 == 2)
            {
                fs_2 = new FileStream(filePath  + "\\14.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }
            else if (emotion2 == 3)
            {
                fs_2 = new FileStream(filePath  + "\\15.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }



            if (emotion3 == 1)
            {
                fs_3 = new FileStream(filePath  + "\\13.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (emotion3 == 2)
            {
                fs_3 = new FileStream(filePath  + "\\14.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (emotion3 == 3)
            {
                fs_3 = new FileStream(filePath  + "\\15.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            

            
            double t;
            foreach (List<double> data in this.totaldata)
            {

                t = data[4044];
                if (t >= StartTime[1] && t <= EndTime[1])
                {

                    foreach (double d in data)
                    {
                        sw_1.Write(d + ",");
                    }
                    sw_1.WriteLine();

                }
                else if (t >= StartTime[2] && t <= EndTime[2])
                {
                    foreach (double d in data)
                    {
                        sw_2.Write(d + ",");
                    }
                    sw_2.WriteLine();

                }
                else if (t >= StartTime[3] && t <= EndTime[3])
                {
                    foreach (double d in data)
                    {
                        sw_3.Write(d + ",");
                    }
                    sw_3.WriteLine();

                }
                
            }
            sw_1.Close();
            fs_1.Close();

            sw_2.Close();
            fs_2.Close();

            sw_3.Close();
            fs_3.Close();

            this.totaldata.Clear();


            if (emotion1 == 1)
            {
                fs_1 = new FileStream(filePath  + "\\AU13.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }
            else if (emotion1 == 2)
            {
                fs_1 = new FileStream(filePath  + "\\AU14.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }
            else if (emotion1 == 3)
            {
                fs_1 = new FileStream(filePath  + "\\AU15.csv", FileMode.Create);
                sw_1 = new StreamWriter(fs_1);
            }



            if (emotion2 == 1)
            {
                fs_2 = new FileStream(filePath  + "\\AU13.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }
            else if (emotion2 == 2)
            {
                fs_2 = new FileStream(filePath  + "\\AU14.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }
            else if (emotion2 == 3)
            {
                fs_2 = new FileStream(filePath  + "\\AU15.csv", FileMode.Create);
                sw_2 = new StreamWriter(fs_2);
            }



            if (emotion3 == 1)
            {
                fs_3 = new FileStream(filePath  + "\\AU13.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (emotion3 == 2)
            {
                fs_3 = new FileStream(filePath  + "\\AU14.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }
            else if (emotion3 == 3)
            {
                fs_3 = new FileStream(filePath  + "\\AU15.csv", FileMode.Create);
                sw_3 = new StreamWriter(fs_3);
            }

            

            foreach (Tuple<FaceShapeAnimations, float,double> unit in audata[0])
            {
                sw_1.Write(unit.Item1.ToString() + ",");
            }
            sw_1.Write("timestamp");
            sw_1.WriteLine();

            foreach (Tuple<FaceShapeAnimations, float, double> unit in audata[0])
            {
                sw_2.Write(unit.Item1.ToString() + ",");
            }
            sw_2.Write("timestamp");
            sw_2.WriteLine();

            foreach (Tuple<FaceShapeAnimations, float, double> unit in audata[0])
            {
                sw_3.Write(unit.Item1.ToString() + ",");
            }
            sw_3.Write("timestamp");
            sw_3.WriteLine();

            foreach (List<Tuple<FaceShapeAnimations, float,double>> unit in audata)
            {
                double tm = unit[0].Item3;

                if (tm >= StartTime[1] && tm <= EndTime[1])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_1.Write(a.Item2 + ",");
                    }
                    sw_1.Write(tm);
                    sw_1.WriteLine();
                }
                else if (tm >= StartTime[2] && tm <= EndTime[2])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_2.Write(a.Item2 + ",");
                    }
                    sw_2.Write(tm);
                    sw_2.WriteLine();
                }
                else if (tm >= StartTime[3] && tm <= EndTime[3])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw_3.Write(a.Item2 + ",");
                    }
                    sw_3.Write(tm);
                    sw_3.WriteLine();
                }

            }
            sw_1.Close();
            fs_1.Close();

            sw_2.Close();
            fs_2.Close();

            sw_3.Close();
            fs_3.Close();


            this.audata.Clear();

            Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, 100.0 });

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                this.saving.label.Content = "        保存完成";
                this.saving.ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(226, 46, 46));
                this.saving.Next.IsEnabled = true;
            }));
        }

        private void save_3(string curvideo,int pictureType,int EmotionType)
        {
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(this.saving.ProgressBar.SetValue);

            string filePath = "" + Disk + ":\\VoiceData\\" + UserId;    //被试者文件夹

            if (pictureType == 1)
            { 
                if(EmotionType == 1)
                    fs = new FileStream(filePath + "\\time16.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs = new FileStream(filePath + "\\time17.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs = new FileStream(filePath + "\\time18.csv", FileMode.Create);
            }
            else if (pictureType == 2)
            {
                if (EmotionType == 1)
                    fs = new FileStream(filePath + "\\time19.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs = new FileStream(filePath + "\\time20.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs = new FileStream(filePath + "\\time21.csv", FileMode.Create);
            }

            
            sw = new StreamWriter(fs);
            sw.WriteLine(StartTime[1].ToString());
            sw.WriteLine(EndTime[1].ToString());
            sw.Close();
            fs.Close();



            //打开文件
            FileStream fs_1 = null;

            if (pictureType == 1)
            {
                if (EmotionType == 1)
                    fs_1 = new FileStream(filePath + "\\16.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs_1 = new FileStream(filePath + "\\17.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs_1 = new FileStream(filePath + "\\18.csv", FileMode.Create);
            }
            else if (pictureType == 2)
            {
                if (EmotionType == 1)
                    fs_1 = new FileStream(filePath + "\\19.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs_1 = new FileStream(filePath + "\\20.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs_1 = new FileStream(filePath + "\\21.csv", FileMode.Create);
            }

            
            StreamWriter sw_1 = new StreamWriter(fs_1);

            
            double t;
            foreach (List<double> data in this.totaldata)
            {

                t = data[4044];
                if (t >= StartTime[1] && t <= EndTime[1])
                {

                    foreach (double d in data)
                    {
                        sw_1.Write(d + ",");
                    }
                    sw_1.WriteLine();

                }
                
                
            }
            sw_1.Close();
            fs_1.Close();


            this.totaldata.Clear();


            if (pictureType == 1)
            {
                if (EmotionType == 1)
                    fs = new FileStream(filePath + "\\AU16.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs = new FileStream(filePath + "\\AU17.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs = new FileStream(filePath + "\\AU18.csv", FileMode.Create);
            }
            else if (pictureType == 2)
            {
                if (EmotionType == 1)
                    fs = new FileStream(filePath + "\\AU19.csv", FileMode.Create);
                else if (EmotionType == 2)
                    fs = new FileStream(filePath + "\\AU20.csv", FileMode.Create);
                else if (EmotionType == 3)
                    fs = new FileStream(filePath + "\\AU21.csv", FileMode.Create);
            }

            
            sw = new StreamWriter(fs);

            foreach (Tuple<FaceShapeAnimations, float,double> unit in audata[0])
            {
                sw.Write(unit.Item1.ToString() + ",");
            }
            sw.Write("timsatamp");
            sw.WriteLine();
            foreach (List<Tuple<FaceShapeAnimations, float,double>> unit in audata)
            {
                double tm = unit[0].Item3;

                if (tm >= StartTime[1] && tm <= EndTime[1])
                {
                    foreach (Tuple<FaceShapeAnimations, float, double> a in unit)
                    {
                        sw.Write(a.Item2 + ",");
                    }
                    sw.Write(tm);
                    sw.WriteLine();
                }
            }
            sw.Close();
            fs.Close();

            this.audata.Clear();

            Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, 100.0 });

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                this.saving.label.Content = "        保存完成";
                this.saving.ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(226, 46, 46));
                this.saving.Next.IsEnabled = true;
            }));
        }

        public void SensorOpen()
        {
            _sensor.Open();
        }

        public void SensorClose()
        {
            _sensor.Close();
        }

        public string getUserPath()
        {
            return "" + Disk + ":\\VoiceData\\" + UserId;
        }

        public void setStartTime(int index,double StartTime)
        {
            this.StartTime[index] = StartTime;
        }

        public void setEndTime(int index,double EndTime)
        {
            this.EndTime[index] = EndTime;
        }

        public void setUserId(int UserId)
        {
            this.UserId = UserId;
            int usertype = (this.UserId % 1000) % 6;
            string video1 = "", video2 = "", video3 = "";
            switch (usertype)
            {
                case 0: video1 = "负性"; video2 = "中性"; video3 = "正性"; EmotionType1 = 3; EmotionType2 = 2; EmotionType3 = 1; break;
                case 1: video1 = "正性"; video2 = "中性"; video3 = "负性"; EmotionType1 = 1; EmotionType2 = 2; EmotionType3 = 3; break;
                case 2: video1 = "正性"; video2 = "负性"; video3 = "中性"; EmotionType1 = 1; EmotionType2 = 3; EmotionType3 = 2; break;
                case 3: video1 = "中性"; video2 = "负性"; video3 = "正性"; EmotionType1 = 2; EmotionType2 = 3; EmotionType3 = 1; break;
                case 4: video1 = "中性"; video2 = "正性"; video3 = "负性"; EmotionType1 = 2; EmotionType2 = 1; EmotionType3 = 3; break;
                case 5: video1 = "负性"; video2 = "正性"; video3 = "中性"; EmotionType1 = 3; EmotionType2 = 1; EmotionType3 = 2; break;
            }

            FileName[1, 1] = filepath + video1 + "1.png";
            FileName[1, 2] = filepath + video1 + "2.png";
            FileName[1, 3] = filepath + video1 + "3.png";
            FileName[2, 1] = filepath + video2 + "1.png";
            FileName[2, 2] = filepath + video2 + "2.png";
            FileName[2, 3] = filepath + video2 + "3.png";
            FileName[3, 1] = filepath + video3 + "1.png";
            FileName[3, 2] = filepath + video3 + "2.png";
            FileName[3, 3] = filepath + video3 + "3.png";

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
                //访谈
                case 1: CurrentPage = num; ContentControl.Content = new Guide.Guide(CurrentPage, 1); break;
                case 2: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 1, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1,true); break;
                case 3: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 4: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 1, 1, "e:\\文本\\q" + EmotionType1 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 5: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 1, 2, "e:\\文本\\q" + EmotionType1 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 6: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 1, 3, "e:\\文本\\q" + EmotionType1 + "3.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 7: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 1, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, false); break;
                case 8: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); Thread t = new Thread(delegate() { save_1("Interview1", EmotionType1); }); t.Start(); break;
                //case 9: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage,3); break;

                case 9: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 10: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 2, 1, "e:\\文本\\q" + EmotionType2 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 11: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 2, 2, "e:\\文本\\q" + EmotionType2 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 12: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 2, 3, "e:\\文本\\q" + EmotionType2 + "3.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 13: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 1, 2, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType2, true); break;
                case 14: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_1("Interview2", EmotionType2); }); t.Start(); break;
                //case 16: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage,3); break;


                case 15: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 16: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 3, 1, "e:\\文本\\q" + EmotionType3 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 17: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 3, 2, "e:\\文本\\q" + EmotionType3 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 18: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 1, 3, 3, "e:\\文本\\q" + EmotionType3 + "3.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 19: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 1, 3, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType3, true); break;
                case 20: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_1("Interview3", EmotionType3); }); t.Start(); break;
                case 21: CurrentPage = num; ContentControl.Content = new End.End(CurrentPage,1); break;
                case 22: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage,60); break;


                //阅读
                case 23: CurrentPage = num; ContentControl.Content = new Guide.Guide(CurrentPage, 2); break;
                case 24: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 2, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, true); break;
                case 25: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 26: CurrentPage = num; ContentControl.Content = new ShowPictureRead.ShowPicture(CurrentPage, 2, 1, 1, FileName[1, 1], "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 27: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 2, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, false); break;
                //case 30: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;

                case 28: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); break;
                case 29: CurrentPage = num; ContentControl.Content = new ShowPictureRead.ShowPicture(CurrentPage, 2, 2, 2, FileName[2, 1], "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 30: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 2, 2, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType2, true); break;
                //case 34: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;

                case 31: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); break;
                case 32: CurrentPage = num; ContentControl.Content = new ShowPictureRead.ShowPicture(CurrentPage, 2, 3, 3, FileName[3, 1], "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 33: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 2, 3, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType3, true); break;
                case 34: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_2("Read", EmotionType1, EmotionType2, EmotionType3); }); t.Start(); break;
                case 35: CurrentPage = num; ContentControl.Content = new End.End(CurrentPage,2); break;
                case 36: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage,60); break;


                //图片1
                case 37: CurrentPage = num; ContentControl.Content = new Guide.Guide(CurrentPage, 3); break;
                case 38: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, true); break;
                case 39: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 40: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 1, 1, "e:\\文本\\p" + EmotionType1 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 41: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 1, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, false); break;
                case 42: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_3("Picture1", 1, EmotionType1); }); t.Start(); break;
                //case 47: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;


                case 43: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 44: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 2, 1, "e:\\文本\\p" + EmotionType2 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 45: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 2, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType2, true); break;
                case 46: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_3("Picture2", 1, EmotionType2); }); t.Start(); break;
                //case 52: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;


                case 47: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 48: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 3, 1, "e:\\文本\\p" + EmotionType3 + "1.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 49: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 3, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType3, true); break;
                case 50: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_3("Picture3", 1, EmotionType3); }); t.Start(); break;
                case 51: CurrentPage = num; ContentControl.Content = new End.End(CurrentPage,3); break;
                case 52: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage,60); break;

                //图片2
                case 53: CurrentPage = num; ContentControl.Content = new Guide.Guide(CurrentPage, 4); break;
                case 54: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 4, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, true); break;
                case 55: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 56: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 4, 1, "e:\\文本\\p" + EmotionType1 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 57: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 4, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType1, false); break;
                case 58: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_3("Picture4", 2, EmotionType1); }); t.Start(); break;
                //case 65: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;


                case 59: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 60: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 5, 1, "e:\\文本\\p" + EmotionType2 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 61: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 5, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType2, true); break;
                case 62: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); t = new Thread(delegate() { save_3("Picture5", 2, EmotionType2); }); t.Start(); break;
                //case 70: CurrentPage = num; ContentControl.Content = new Relex.Relex(CurrentPage, 3); break;

                case 63: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetCanShowFace(true); this.SetRecordSwitch(true); break;
                case 64: CurrentPage = num; ContentControl.Content = new ShowPicture.ShowPicture(CurrentPage, 3, 6, 1, "e:\\文本\\p" + EmotionType3 + "2.png", "" + Disk + ":\\VoiceData\\" + UserId, true, true); break;
                case 65: CurrentPage = num; ContentControl.Content = new EmotionTest.EmotionTest(CurrentPage, 3, 6, "" + Disk + ":\\VoiceData\\" + UserId, EmotionType3, true); break;
                case 66: CurrentPage = num; this.saving = new Saving.Saving(CurrentPage); ContentControl.Content = this.saving; this.SensorClose(); t = new Thread(delegate() { save_3("Picture6", 2, EmotionType3); }); t.Start(); break;
                case 67: CurrentPage = num; ContentControl.Content = new AllEnd.AllEnd(CurrentPage); break;
            }
        }
        

        public void SetCanShowFace(bool CanShowFace)
        {
            this.CanShowFace = CanShowFace;
        }

        public void SetRecordSwitch(bool RecordSwitch)
        {
            this.RecordSwitch = RecordSwitch;
        }

        public void setWavfile(String filename)
        {
            this.wavfile = filename;
        }

        public void StartRecord()
        {
            recorder = new Record.SoundRecord();
            recorder.SetFileName(wavfile);
            recorder.RecStart();
        }

        public void StopRecord()
        {
            recorder.RecStop();
            recorder = null;
        }

        public void setOK1(bool ok)
        {
            this.OK[1] = ok;
        }

        public void setOK2(bool ok)
        {
            this.OK[2] = ok;
        }

        public void setOK3(bool ok)
        {
            this.OK[3] = ok;
        }

        public void setCurrentVideo(string videopath)
        {
            currentVideo = videopath.Split('\\')[2].Split('.')[0];
        }

        public void refreshShow()
        {
            show.cover.Visibility = Visibility.Visible;
            this.show.label.Content = "未捕捉到面部"; 
            show.label.Foreground = new SolidColorBrush(Color.FromRgb(207, 17, 17)); 
            this.show.button.IsEnabled = false;
        }

        public String getBackupVideo()
        {
            if (OK[1] && OK[2] && OK[3])
                return "";

            string filename = "";

            for (int i = 1; i <= 3; ++i)
            {
                if (OK[i] == true)
                    continue;
                if (JUMP[i] == true)
                    continue;
                for (int j = 2; j <= 3; ++j)
                {
                    if (FileUsable[i, j] == false)
                        continue;
                    filename = FileName[i, j];
                    FileUsable[i, j] = false;
                    this.retryVideoType = i;
                    if (j == 3)
                        JUMP[i] = true;
                    return filename;
                }
            }
            return filename;
        }

    }
}