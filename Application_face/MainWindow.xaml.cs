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

        private int Num = 0;

        private Double startTime,endTime;

        FileStream fs = null;
        StreamWriter sw = null;

        FrameDescription fd;
        Byte[] colordata;
        WriteableBitmap bitmap;

        private List<Ellipse> _points = new List<Ellipse>();


        private List<List<double>> totaldata=new List<List<double>>();
        private List<List<Tuple<FaceShapeAnimations, float>>> audata=new List<List<Tuple<FaceShapeAnimations, float>>>();
        //private List<int> MostVariancePointIndex = new List<int>() { };

        private Dictionary<String, int> res = new Dictionary<String, int>();
        public MainWindow()
        {
            InitializeComponent();
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
                _sensor.Open();
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

        private void FaceReader_FrameArrived(object sender, HighDefinitionFaceFrameArrivedEventArgs e)
        {
            using (HighDefinitionFaceFrame frame = e.FrameReference.AcquireFrame())
            {
                if (frame != null && frame.IsFaceTracked)
                {
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
        private void UpdateFacePoints(HighDefinitionFaceFrame frame)
        {
            if (_faceModel == null) return;

            var vertices = _faceModel.CalculateVerticesForAlignment(_faceAlignment);
            IReadOnlyDictionary<FaceShapeAnimations,float> _faceShapeAnimations=_faceAlignment.AnimationUnits;
            List<Tuple<FaceShapeAnimations, float>> au = new List<Tuple<FaceShapeAnimations, float>>();

            CameraSpacePoint head = _faceAlignment.HeadPivotPoint;

            foreach (FaceShapeAnimations f in _faceShapeAnimations.Keys)
            {
                au.Add(new Tuple<FaceShapeAnimations,float>(f, _faceShapeAnimations[f]));
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
                        canvas.Children.Add(ellipse);
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
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                double time = Convert.ToInt64(ts.TotalSeconds);
                data.Add(time);
                data.Add(frame.RelativeTime.TotalMilliseconds);
                totaldata.Add(data);
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


        private void button_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Num = Convert.ToInt32(this.textbox.Text);
                this.canvas1.Children.Remove(button);
                this.canvas1.Children.Remove(textbox);
                this.label.Content = "被试编号：" + Num;
                this.btn_start.IsEnabled = true;
            }
            catch(Exception E)
            {
                MessageBox.Show("请正确输入编号");  
            }
        }


        private void save(Double startTime,Double endTime)
        {
            string filePath = "" + Disk + ":\\FaceData\\" + Num;    //被试者文件夹

            // 创建目录
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            fs = new FileStream(filePath + "\\PointsData.csv", FileMode.Create);
            sw = new StreamWriter(fs);

            foreach (List<double> pointd in this.totaldata)
            {
                foreach (double da in pointd)
                {
                    sw.Write(da + ",");

                }
                sw.WriteLine();
            }
            sw.Close();
            fs.Close();


            fs = new FileStream(filePath + "\\time.csv", FileMode.Create);

            sw = new StreamWriter(fs);

            sw.WriteLine(startTime);
            sw.WriteLine(endTime);
            
            sw.Close();
            fs.Close();

            //FileStream fs;// = new FileStream("F:\\Jing_Data\\PointsData.csv", FileMode.Create);
            //StreamWriter sw;// = new StreamWriter(fs);

            //打开文件
            string filePath_ = "" + Disk + ":\\FaceData";
            FileStream fs_1 = new FileStream(filePath_ + "\\"+Num+".csv", FileMode.Create);
            StreamWriter sw_1 = new StreamWriter(fs_1);

            double t;
            foreach (List<double> data in this.totaldata)
            {

                t = data[4044];
                if (t >= Convert.ToDouble(startTime) && t <= Convert.ToDouble(endTime))
                {

                    foreach (double d in data)
                    {
                        sw_1.Write(d + ",");
                    }
                    sw_1.WriteLine();
                    //sw.Close();
                    //fs.Close();
                }
            }
            sw_1.Close();
            fs_1.Close();

            //
            /*fs = new FileStream(filePath+"\\time.csv", FileMode.Create);

            sw = new StreamWriter(fs);

            foreach (String tl in timeList)
            {
                sw.WriteLine(tl);
            }
            sw.Close();
            fs.Close();*/


            fs = new FileStream(filePath + "\\AnimationUnits.csv", FileMode.Create);
            sw = new StreamWriter(fs);

            foreach (Tuple<FaceShapeAnimations, float> unit in audata[0])
            {
                sw.Write(unit.Item1.ToString() + ",");
            }
            sw.WriteLine();
            foreach (List<Tuple<FaceShapeAnimations, float>> unit in audata)
            {
                foreach (Tuple<FaceShapeAnimations, float> a in unit)
                {
                    sw.Write(a.Item2 + ",");
                }
                sw.WriteLine();
            }      

        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            this.btn_start.IsEnabled = false;
            this.btn_save.IsEnabled = true;

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.startTime = Convert.ToInt64(ts.TotalSeconds);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            this.btn_save.IsEnabled = false;

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            this.endTime = Convert.ToInt64(ts.TotalSeconds);
            save(startTime,endTime); 
        }

    }
}