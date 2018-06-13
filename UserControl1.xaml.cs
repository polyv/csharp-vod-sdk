using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Timers;
using System.ComponentModel;

namespace PolyvPlayerDemoWinform
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        static Application app;

        public delegate void OnMediaOpenedHandler(TimeSpan durationPositon);
        public event OnMediaOpenedHandler OnMediaOpened;
        public bool isMouseDown = false;
        public UserControl1()
        {
            InitializeComponent();
           
            if (null == System.Windows.Application.Current)
            {
                app = new System.Windows.Application();
                app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            try
            {
                //ffmpeg库路径，不能为空
                PolyvPlayerSDK.MediaElement.FFmpegDirectory = @".\ffmpeg";

            }
            catch
            {
            }


            //播放初始化账号信息,不能为空
            string userId = "";
            string secretkey = "";
            string readToken = "";
            Media.initAccountInfo(userId, secretkey, readToken);

            //初始化观看者信息，观看者的ip,不能为空
            string viewerIp = "1.1.1.1";
            string viewerId = "";
            string viewerName = "";
            Media.initPlayerInfo(viewerIp, viewerId, viewerName);


            Media.MediaOpening += Media_MediaOpening;
            Media.MediaFailed += Media_MediaFailed;
            Media.MediaEnded += MediaEndedEvent;
            Media.MediaOpened += Media_MediaOpened;


        }


        private void MediaEndedEvent(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
        private void Media_MediaOpening(object sender, PolyvPlayerSDK.MediaOpeningRoutedEventArgs e)
        {
            Media.mylog.Debug("Media_MediaOpening");
        }
        private void Media_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            OnMediaOpened(Media.NaturalDuration.TimeSpan);
            isMouseDown = false;
            Media.mylog.Debug("Media_MediaOpened");
        }

        private void Media_MediaFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            Media.mylog.Error("Media_MediaFailed");
        }

        public int open(string vid,int playType,int bitRate)
        {
            System.Windows.Thickness nes = scrollingTextControl1.Margin;
            nes.Left = 100;
            nes.Top = 0;
            nes.Right = 0;
            nes.Bottom = 0;
            scrollingTextControl1.Margin = nes;
            //string directPaths = "C:/";
            //Directory.SetCurrentDirectory(directPaths);
            int ret = Media.PlayVideo(vid, bitRate, playType, "C:/");

            return ret;
        }

        public void close()
        {
            Media.Close();
        }
        public void pause()
        {
            Media.Pause();
        }
        public void play()
        {
            Media.Play();
        }
        public void setVolume(double vol)
        {
            Media.Volume = vol;
        }

        public void setSpeedRatio(double speed)
        {
            Media.SpeedRatio = speed;
        }

        public void setPosition(long time)
        {
            Media.Position =  TimeSpan.FromTicks((long)(TimeSpan.TicksPerSecond * (long)time));
        }

        public void closeWpfControl()
        {
            if (null == System.Windows.Application.Current)
            {
              new System.Windows.Application();
            }
            Application.Current.Shutdown();
        }
        public void runWpfControl()
        {
            //if (null == System.Windows.Application.Current)
            //{
            //    new System.Windows.Application();
            //}
            //Application.Current.Run();
            if (null == System.Windows.Application.Current)
            {
                new System.Windows.Application();
                Application.Current.Run();

            }
        }
    }

    /// <summary>
    /// Label走马灯自定义控件
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(Label))] //设置工具箱中显示的图标
    public class ScrollingTextControl : Label
    {
        /// <summary>
        /// 定时器
        /// </summary>
        Timer MarqueeTimer = new Timer();
        /// <summary>
        /// 滚动文字源
        /// </summary>
        String _TextSource = "滚动文字源";
        /// <summary>
        /// 输出文本
        /// </summary>
        String _OutText = string.Empty;
        /// <summary>
        /// 过度文本存储
        /// </summary>
        string _TempString = string.Empty;
        /// <summary>
        /// 文字的滚动速度
        /// </summary>
        double _RunSpeed = 1000;

        DateTime _SignTime;
        bool _IfFirst = true;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        int _StopSecond = 3000;

        /// <summary>
        /// 滚动一循环字幕停留的秒数,单位为毫秒,默认值停留3秒
        /// </summary>
        public int StopSecond
        {
            get { return _StopSecond; }
            set
            {
                _StopSecond = value;
            }
        }

        /// <summary>
        /// 滚动的速度
        /// </summary>
        [Description("文字滚动的速度")]　//显示在属性设计视图中的描述
        public double RunSpeed
        {
            get { return _RunSpeed; }
            set
            {
                _RunSpeed = value;
                MarqueeTimer.Interval = _RunSpeed;
            }
        }
        
        /// <summary>
        /// 滚动文字源
        /// </summary>
        [Description("文字滚动的Text")]
        public string TextSource
        {
            get { return _TextSource; }
            set
            {
                _TextSource = value;
                _TempString = _TextSource + "   ";
                _OutText = _TempString;
            }
        }

        private string SetContent
        {
            get { return Content.ToString(); }
            set
            {
                Content = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ScrollingTextControl()
        {
            MarqueeTimer.Interval = _RunSpeed;//文字移动的速度
            MarqueeTimer.Enabled = true;      //开启定时触发事件
            MarqueeTimer.Elapsed += new ElapsedEventHandler(MarqueeTimer_Elapsed);//绑定定时事件
            this.Loaded += new RoutedEventHandler(ScrollingTextControl_Loaded);//绑定控件Loaded事件
        }


        void ScrollingTextControl_Loaded(object sender, RoutedEventArgs e)
        {
            _TextSource = SetContent;
            _TempString = _TextSource + "   ";
            _OutText = _TempString;
            _SignTime = DateTime.Now;
        }


        void MarqueeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (string.IsNullOrEmpty(_OutText)) return;

            if (_OutText.Substring(1) + _OutText[0] == _TempString)
            {
                if (_IfFirst)
                {
                    _SignTime = DateTime.Now;
                }

                if ((DateTime.Now - _SignTime).TotalMilliseconds > _StopSecond)
                {
                    _IfFirst = true; ;
                }
                else
                {
                    _IfFirst = false;
                    return;
                }
            }

            _OutText = _OutText.Substring(1) + _OutText[0];


            Dispatcher.BeginInvoke(new Action(() =>
            {
                SetContent = _OutText;
            }));


        }

    }
}
