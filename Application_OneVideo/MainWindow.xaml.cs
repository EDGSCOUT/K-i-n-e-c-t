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
namespace Kinect2FaceHD_NET
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

        public double[] StartTime, EndTime;

        private ShowFace.ShowFace show;
        
        private Saving2.Saving saving;

        public bool CanShowFace = false;

        public bool[] OK,JUMP;

        public String[] QuestionPicture;

        public String[,] FileName;
        public bool[,] FileUsable;

        private Record.SoundRecord recorder;
        public string wavfile = "C:\\Users\\ad\\Desktop\\new.wav";
        public string filepath = "e:\\video\\";
        public string filepath2 = "e:\\文本\\";
        public string currentVideo;
        public string retryVideo;
        string video1 = "", video2 = "", video3 = "";
        public int retryVideoType,retryVideoPage;
        public bool ShutDownTopwindow = true;
        public bool OpenVoicewindow = false;
        public bool RecordSwitch = true;
        FileStream fs = null,fs2 = null;
        StreamWriter sw = null, sw2 = null;

        FrameDescription fd;
        Byte[] colordata;
        WriteableBitmap bitmap;

        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
        private List<Ellipse> _points = new List<Ellipse>();


        private List<List<double>> totaldata=new List<List<double>>();
        private List<List<Tuple<FaceShapeAnimations, float,double>>> audata=new List<List<Tuple<FaceShapeAnimations, float,double>>>();
        //private List<int> MostVariancePointIndex = new List<int>() { };

        private Dictionary<String, int> res = new Dictionary<String, int>();
        public MainWindow()
        {
            InitializeComponent();
            OK = new bool[4] {true, true, true, true};
            JUMP = new bool[4] { false, false, false, false };
            FileName = new String[4,4];
            FileUsable = new bool[4,4];
            QuestionPicture = new String[4];

            StartTime = new Double[3] { 0, 0, 0};
            EndTime = new Double[3] { 0, 0, 0};

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

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            //show = new ShowFace.ShowFace();
            //ContentControl.Content = show;
            //CanShowFace = true;

            //ContentControl.Content = new Player.VideoPlayer("e:\\video\\anger.avi");
            //ContentControl.Content = new ShowPicture.ShowPicture(1, 1,1, "e:\\文本\\happy-1.png", "" + Disk + ":\\FaceData\\" + UserId,false,false);
            //ContentControl.Content = new EmotionTest.EmotionTest(1, "" + Disk + ":\\FaceData\\" + UserId);

            //ContentControl.Content = new QuestionnaireHealth.QuestionnaireHealth();
            //ContentControl.Content = new QuestionnaireDemography.QuestionnaireDemography_1();  
            //ContentControl.Content = new QuestionnaireDemography.QuestionnaireDemography_2();
            
            //ContentControl.Content = new REMIT.REMIT();  
            //ContentControl.Content = new QuestionnairePSQI.QuestionnairePSQI();
            //ContentControl.Content = new Top.Top(0);
            //ContentControl.Content = new Login.Login(1);

            //microphonetester = new WpfSV.MicroPhoneTest(1);
            //ContentControl.Content = microphonetester;

            //ContentControl.Content = new MakeSure.MakeSure(1,1);
            ContentControl.Content = new VideoGuide2.VideoGuide(1);
            //ContentControl.Content = new Result.Result(1,1);
            //ContentControl.Content = new Relex.Relex(1);
            //ContentControl.Content = new Report.Report(1,OK1,OK2,OK3);
            //ContentControl.Content = new End.End(1);
            //ContentControl.Content = new GuideBackupVideo.GuideBackupVideo(1);
            //ContentControl.Content = new BDI.BDI();
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

                //cfr.FrameArrived += cfr_FrameArrived;
                //_sensor.Open();
            }
            this.show = new ShowFace.ShowFace(3);
            
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
            if (this.RecordSwitch)
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
                au.Add(new Tuple<FaceShapeAnimations, float, double>(f, _faceShapeAnimations[f], time1));
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
            if (CurrentPage != 1 && CurrentPage != 6)
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

            
            if (_faceModel != null)
            {
                _faceModel.Dispose();
                _faceModel = null;
            }
            this.show = null;
            GC.SuppressFinalize(this);
            _sensor.Close();
            /*
            if (ShutDownTopwindow)
                this.Owner.Close();
            else
            {
                if (OpenVoicewindow == true)
                {
                    Voice.MainWindow w2 = new Voice.MainWindow(1);
                    Application.Current.MainWindow = w2;
                    w2.WindowStartupLocation = WindowStartupLocation.Manual;

                    w2.setUserId(this.UserId);
                    w2.Left = this.Left;
                    w2.Top = this.Top;
                    w2.Width = this.Width;
                    w2.Height = this.Height;
                    w2.WindowState = this.WindowState;
                    w2.Owner = this.Owner;
                    w2.Show();
                }
                else 
                {
                    this.Owner.Left = this.Left;
                    this.Owner.Top = this.Top;
                    this.Owner.Width = this.Width;
                    this.Owner.Height = this.Height;
                    this.Owner.WindowState = this.WindowState;
                    this.Owner.Show();
                }
                
            }*/
    
        }

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);

        private void save(string curvideo)
        {
            UpdateProgressBarDelegate updatePbDelegate = new UpdateProgressBarDelegate(this.saving.ProgressBar.SetValue);

            string filePath = "" + Disk + ":\\VoiceData\\" + UserId;    //被试者文件夹

            
            // 创建目录
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (String.Equals(curvideo, "happy-1"))
            {
                fs = new FileStream(filePath + "\\time01.csv", FileMode.Create);
                fs2 = new FileStream(filePath + "\\time22.csv", FileMode.Create);
            }    
            else if (String.Equals(curvideo, "neutrality-1"))
            {
                fs = new FileStream(filePath + "\\time02.csv", FileMode.Create);
                fs2 = new FileStream(filePath + "\\time23.csv", FileMode.Create);
            }    
            else if (String.Equals(curvideo, "sad-1"))
            {
                fs = new FileStream(filePath + "\\time03.csv", FileMode.Create);
                fs2 = new FileStream(filePath + "\\time24.csv", FileMode.Create);
            }    
            
            sw = new StreamWriter(fs);
            sw2 = new StreamWriter(fs2);

            sw.WriteLine(StartTime[1].ToString());
            sw.WriteLine(EndTime[1].ToString());
            sw2.WriteLine(StartTime[2].ToString());
            sw2.WriteLine(EndTime[2].ToString());

            sw.Close();
            fs.Close();
            sw2.Close();
            fs2.Close();

            //FileStream fs;// = new FileStream("F:\\Jing_Data\\PointsData.csv", FileMode.Create);
            //StreamWriter sw;// = new StreamWriter(fs);

            //打开文件
            FileStream fs_1 = null;
            if (String.Equals(curvideo, "happy-1"))
                fs_1 = new FileStream(filePath + "\\01.csv", FileMode.Create);
            else if (String.Equals(curvideo, "neutrality-1"))
                fs_1 = new FileStream(filePath + "\\02.csv", FileMode.Create);
            else if (String.Equals(curvideo, "sad-1"))
                fs_1 = new FileStream(filePath + "\\03.csv", FileMode.Create);

            
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
                    //sw.Close();
                    //fs.Close();
                }
                else if (t >= StartTime[2] && t <= EndTime[2])
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

            
            //
            /*fs = new FileStream(filePath+"\\time.csv", FileMode.Create);

            sw = new StreamWriter(fs);

            foreach (String tl in timeList)
            {
                sw.WriteLine(tl);
            }
            sw.Close();
            fs.Close();*/

            if (String.Equals(curvideo, "happy-1"))
                fs = new FileStream(filePath + "\\AU01.csv", FileMode.Create);
            else if (String.Equals(curvideo, "neutrality-1"))
                fs = new FileStream(filePath + "\\AU02.csv", FileMode.Create);
            else if (String.Equals(curvideo, "sad-1"))
                fs = new FileStream(filePath + "\\AU03.csv", FileMode.Create);
            
            
            sw = new StreamWriter(fs);

            foreach (Tuple<FaceShapeAnimations, float,double> unit in audata[0])
            {
                sw.Write(unit.Item1.ToString() + ",");
            }
            sw.Write("timestamp");
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
                else if (tm >= StartTime[2] && tm <= EndTime[2])
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

        public void setStartTime(int index, double StartTime)
        {
            this.StartTime[index] = StartTime;
        }

        public void setEndTime(int index, double EndTime)
        {
            this.EndTime[index] = EndTime;
        }

        public void setUserId(int UserId)
        {
            this.UserId = UserId;
            int usertype = (this.UserId % 1000) % 6;
            video1 = ""; 
            video2 = "";
            video3 = "";
            switch (usertype)
            {
                case 0: video1 = "sad"; video2 = "neutrality"; video3 = "happy"; break;
                case 1: video1 = "happy"; video2 = "neutrality"; video3 = "sad"; break;
                case 2: video1 = "happy"; video2 = "sad"; video3 = "neutrality"; break;
                case 3: video1 = "neutrality"; video2 = "sad"; video3 = "happy"; break;
                case 4: video1 = "neutrality"; video2 = "happy"; video3 = "sad"; break;
                case 5: video1 = "sad"; video2 = "happy"; video3 = "neutrality"; break;
            }

            FileName[1, 1] = filepath + video1 + "-1.avi";
            FileName[1, 2] = filepath + video1 + "-2.avi";
            FileName[1, 3] = filepath + video1 + "-3.avi";
            FileName[2, 1] = filepath + video2 + "-1.avi";
            FileName[2, 2] = filepath + video2 + "-2.avi";
            FileName[2, 3] = filepath + video2 + "-3.avi";
            FileName[3, 1] = filepath + video3 + "-1.avi";
            FileName[3, 2] = filepath + video3 + "-2.avi";
            FileName[3, 3] = filepath + video3 + "-3.avi";

            QuestionPicture[1] = filepath2 + video1 + "-1.png";
            QuestionPicture[2] = filepath2 + video2 + "-1.png";
            QuestionPicture[3] = filepath2 + video3 + "-1.png";
        }


        public void NextPage(int num)
        {
            if (num == 6)
            {
                SetCurrentPage(1);
            }
            else
                SetCurrentPage(num + 1);
            
        }

        public void PreviousPage(int num)
        {
            if (num > 0)
            {
                SetCurrentPage(num - 1);
            }
        }
        
        public void SetCurrentPage(int num)
        {
            switch (num)
            {
                case 1: CurrentPage = num; ContentControl.Content = new VideoGuide2.VideoGuide(CurrentPage); break;
                case 2: CurrentPage = num; ContentControl.Content = new Login.Login(CurrentPage); break;
                case 3: CurrentPage = num; refreshShow(); this.show.SetPage(CurrentPage); ContentControl.Content = this.show; this.SetRecordSwitch(true); this.SetCanShowFace(true); this.SensorOpen(); break;
                case 4: CurrentPage = num; ContentControl.Content = new VideoPlayer.VideoPlayer(CurrentPage, "e:\\video\\sad-1.avi"); break;
                case 5: CurrentPage = num; this.saving = new Saving2.Saving(CurrentPage); ContentControl.Content = this.saving; this.SetRecordSwitch(false); Thread t = new Thread(delegate() { save(currentVideo); }); t.Start(); break;
                case 6: CurrentPage = num; ContentControl.Content = new Result.Result(CurrentPage,this.UserId); break;

                case 25: CurrentPage = num; ContentControl.Content = new GuideBackupVideo.GuideBackupVideo(CurrentPage); break;
            }
        }

        public void SetRetryVideoPage(int page)
        {
            this.retryVideoPage = page;
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