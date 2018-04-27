using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
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

            //初始化账号信息,不能为空
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
}
