namespace PolyvPlayerSDK.Sample
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;
    using System.Threading;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.IO;
    using System.Threading.Tasks;
    using System.Text;
    using System.Text.RegularExpressions;
    using Events;
    using FFmpeg.AutoGen;
    using Common.Shared;
    using PolyvLocal;
    using System.Linq;
    using System.Drawing;
    using System.Timers;
   

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class BitRate
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public partial class MainWindow : Window
    {
        #region Commands
        private string lastSource = "";
        public bool play = false;
        //public bool toAudio = false;
      //  public static bool tranAudio = false;
        TimeSpan currentPosition;
        static string curTime = ""; 
        bool networkAvaiable = true;
        string logPath = System.Environment.CurrentDirectory + "\\log.txt";
        static System.DateTime currentTime = new System.DateTime();
        static TimeSpan curpos;
        #endregion
        DownLoadVideo DownLoadVideo;
        
        public MainWindow()
        {
            try
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                // FFMPEG 库路径不能为空。
                PolyvPlayerSDK.MediaElement.FFmpegDirectory = @"./ffmpeg";  
                InitializeComponent();
                DownLoadVideo = new DownLoadVideo();
                NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(networkchanged);
                System.Windows.Threading.DispatcherTimer infoTimer = new System.Windows.Threading.DispatcherTimer();
                infoTimer.Tick += (obj, e) =>
                {
                    string stayInVideoTime = Media.StayInVideoTime.ToString(@"hh\:mm\:ss");
                    string realPlayVideoTime = Media.RealPlayVideoTime.ToString(@"hh\:mm\:ss");

                    stayInVideoLbl.Content = stayInVideoTime;
                    realPlayVideoLbl.Content = realPlayVideoTime;
                };
                infoTimer.Interval = new TimeSpan(0, 0, 1);
                infoTimer.Start();


                //播放初始化账号信息,不能为空
                 VIDTextBox.Text = "";
                //VIDTextBox.Text = "";
                 string userId = "";
                 string secretkey = "";
                 string readToken = "";


                //初始化必要参数，播放和下载均需要初始化
                DownLoadVideo.initAccountInfo(userId, secretkey, readToken);
                Media.initAccountInfo(userId, secretkey, readToken);

                PolyvPlayerSDK.MediaElement.FFmpegMessageLogged += OnMediaFFmpegMessageLogged;
                //初始化观看者信息，观看者的ip,不能为空
                string viewerIp = "";
                string viewerId = "";
                string viewerName = "";
                Media.initPlayerInfo(viewerIp, viewerId, viewerName);
                Media.MessageLogged += OnMediaMessageLogged;
                Media.MediaOpened += (ms, me) =>
                {
                    int time = Convert.ToInt32(Media.NaturalDuration.TimeSpan.TotalSeconds);
                    string timestr = string.Format("{0:00}:{1:00}:{2:00}", time / 3600, time / 60, time % 60);
                    durationLbl.Content = timestr;
                };
                Media.MediaOpening += Media_MediaOpening;
                Media.MediaOpened += Media_MediaOpened;
                Media.MediaFailed += Media_MediaFailed;
                Media.MediaEnded += MediaEndedEvent;
                Media.SeekingEnded += Media_SeekingEnded;
                Media.BufferingStarted += MediaBufferingStartEvent;
                Media.BufferingEnded += MediaBufferingEndEvent;
                // DownLoadVideo.OnDownloadCompleted += DownloadCompletedEvent;
                DownLoadVideo.OnDownloadProgress += DownloadProgressEvent;
                DownLoadVideo.OnDeleteInfo += DeleteInfoEvent;
                IList<BitRate> customList = new List<BitRate>();
                customList.Add(new BitRate() { ID = 1, Name = "标清" });
                customList.Add(new BitRate() { ID = 2, Name = "高清" });
                customList.Add(new BitRate() { ID = 3, Name = "超清" });
                comboBox.ItemsSource = customList;
                comboBox.DisplayMemberPath = "Name";
                comboBox.SelectedValuePath = "ID";
                comboBox.SelectedValue = 1;

                // 初始化跑马灯
                String content = "保利威视频点播";
                Single fontSize = 14;
                Color fontColor = Color.SkyBlue;
                Media.InitScrollText(content, fontSize, fontColor);
                Media.StartDrawScrollText();
       
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }

        }

        private delegate void outputDelegate(bool e);

        private void output(bool e)
        {
            this.tipsLabel.Dispatcher.Invoke(new outputDelegate(outputAction), e);
        }

        private void outputAction(bool e)
        {
            if (!e)
            {
                tipsLabel.Content = "网络中断";
                networkAvaiable = false;

            }
            else
            {
                tipsLabel.Content = "网络恢复";
                networkAvaiable = true;
                if (Media.DownloadProgress == 0)
                    Media.Position = currentPosition;
            }
        }

        private void OnMediaMessageLogged(object sender, MediaLogMessageEventArgs e)
        {
            if (e.MessageType == MediaLogMessageType.Trace)
                return;

            Debug.WriteLine($"{e.MessageType,10} - {e.Message}");
        }

        private void OnMediaFFmpegMessageLogged(object sender, MediaLogMessageEventArgs e)
        {
            if (e.MessageType != MediaLogMessageType.Warning && e.MessageType != MediaLogMessageType.Error)
                return;

            if (string.IsNullOrWhiteSpace(e.Message) == false && e.Message.Contains("Using non-standard frame rate"))
                return;

            Debug.WriteLine($"{e.MessageType,10} - {e.Message}");
        }

        public void networkchanged(object sender, NetworkAvailabilityEventArgs e)
        {
            output(e.IsAvailable);
        }


        private void MediaBufferingStartEvent(object sender, RoutedEventArgs e)
        {
            tipsLabel.Content = "开始缓冲。。。";
        }

        private void MediaBufferingEndEvent(object sender, RoutedEventArgs e)
        {
            tipsLabel.Content = "缓冲结束";
        }
        private void OnMediaElementClosedEvent(bool status)
        {

        }
        private void MediaEndedEvent(object sender, RoutedEventArgs e)
        {

        }
        //下载视频
        private void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {         
                string videoId = VIDTextBox.Text.Trim();
                //int bitRate = 2;// 1,2,3  1表示标清，2表示高清，3表示超清
                int bitRate = Convert.ToInt32(comboBox.SelectedValue.ToString());
              //  下载路径
                string directPaths = "C:/Workspace/Polyv/";
                int ret = 0;
                Task task = new Task(new Action(() =>
                {
                    ret = DownLoadVideo.downloadVideo(videoId, bitRate, directPaths);//0:成功，-1:参数错误,-2:流量超标，-3:账号过期,-4:视频信息获取失败,-5:没有对应码率,-6:网络异常；
                    string status = "";

                    switch (ret)
                    {
                        case 0: status = "下载完成"; break;
                        case -1: status = "参数错误"; break;
                        case -2: status = "流量超标"; break;
                        case -3: status = "账号过期"; break;
                        case -4: status = "视频信息获取失败"; break;
                        case -5: status = "无对应码率"; break;
                        case -6: status = "key下载失败"; break;
                        case -7: status = "MP4下载失败"; break;
                        case -8: status = "m3u8下载失败"; break;
                        case -9: status = "ts下载失败"; break;
                        case -10: status = "ts网络请求错误"; break;
                        case -11: status = "ts下载不完整"; break;                                  
                    }
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        tipsLabel.Content = status;
                    }));
                }));
                task.Start();



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            DownLoadVideo.pauseDownload();

            //DownLoadVideo
        }
        public void DownloadCompletedEvent(string videoId, long totalBytes, string flags, bool status)
        {
            if (status)
            {
                tipsLabel.Content = flags + "下载完成";

            }
            else
            {
                tipsLabel.Content = flags + "下载失败";

            }
        }

        public void DownloadProgressEvent(string videoId, long receiveBytes, long totalBytes, DownLoadVideo obj)
        //public void DownloadProgressEvent(string videoId, long receiveBytes, long totalBytes)
        {

            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    tipsLabel.Content = "已下载" + ((receiveBytes * 100) / totalBytes).ToString() + "%";
                }));

            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteInfoEvent(bool status, string videoId, int retcode, string msg)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                tipsLabel.Content = msg;
            }));

        }
        private void Media_MediaFailed(object sender, ExceptionRoutedEventArgs ex)
        {
            tipsLabel.Dispatcher.Invoke(new Action(() =>
            {
                if (ex.Equals("BadDeviceId calling waveOutOpen"))
                    tipsLabel.Content = "音频输出设备异常";
                else
                    tipsLabel.Content = ex;
                Media.mylog.Error(VIDTextBox.Text.Trim() + "  PlayVideo" + "  ex:" + ex);
            }));
        }
       
        private void Media_MediaOpening(object sender, MediaOpeningRoutedEventArgs e)
        {

            if (VIDTextBox.Text.StartsWith("udp://"))
                e.Options.VideoFilter = "yadif";

            var videoStream = e.Options.VideoStream;
            if (videoStream != null)
            {
                // Check if the video requires deinterlacing
                var requiresDeinterlace = videoStream.FieldOrder != AVFieldOrder.AV_FIELD_PROGRESSIVE
                    && videoStream.FieldOrder != AVFieldOrder.AV_FIELD_UNKNOWN;

                // Hardwrae device priorities
                var deviceCandidates = new AVHWDeviceType[]
                {
                    AVHWDeviceType.AV_HWDEVICE_TYPE_DXVA2,
                    AVHWDeviceType.AV_HWDEVICE_TYPE_CUDA,
                    AVHWDeviceType.AV_HWDEVICE_TYPE_D3D11VA
                };

                // Hardware device selection
                if (videoStream.FPS <= 30)
                {
                    foreach (var deviceType in deviceCandidates)
                    {
                        var accelerator = videoStream.HardwareDevices.FirstOrDefault(d => d.DeviceType == deviceType);
                        if (accelerator != null)
                        {
                            if (Platform.GuiContext.Current.IsInDebugMode)
                                e.Options.VideoHardwareDevice = accelerator;

                            break;
                        }
                    }
                }

                var videoFilter = new StringBuilder();

                // The yadif filter deinterlaces the video; we check the field order if we need
                // to deinterlace the video automatically
                if (requiresDeinterlace)
                    videoFilter.Append("yadif,");

                // Scale down to maximum 1080p screen resolution.
                if (videoStream.PixelHeight > 1080)
                {
                    // e.Options.VideoHardwareDevice = null;
                    videoFilter.Append($"scale=-1:1080,");
                }

                e.Options.VideoFilter = videoFilter.ToString().TrimEnd(',');
            }
        }
    
        private void Media_MediaOpened(object sender, RoutedEventArgs e)
        {
           
        }

        /// <summary>
        /// 判断视频是否完整 bool IsFileComplete(string videoId, int bitRate, string videoFilePath)
        /// <param name="videoId">视频vid.</param>
        /// <param name="bitRate">码率</param>
        /// <param name="videoFilePath">传入的路径</param>
        /// 返回true标志文件完整，返回false表示文件不完整
        /// </summary>
        private bool IsFileComplete(string videoId, int bitRate, string videoFilePath)
        {
            try
            {
                string videoPoolId = videoId.Substring(0, videoId.Length - 2);
                string directPath = videoFilePath + "video/" + videoId + "/";
                // 判断mp4文件是否存在
                string mp4Path = directPath + videoPoolId + "_" + bitRate + ".mp4";
                if (File.Exists(mp4Path))
                {
                    return true;
                }

                //拼凑m3u8路径
                string m3u8PathStr = directPath + videoPoolId + "_" + bitRate + ".m3u8";
                if (File.Exists(m3u8PathStr))
                {
                    //读取m3u8文件，分析有多少ts片,tsCount表示ts片的数量
                    StreamReader sr = new StreamReader(m3u8PathStr, Encoding.Default);
                    string content = sr.ReadToEnd();
                    Regex rg = new Regex("EXTINF");
                    int tsCount = rg.Matches(content).Count;

                    //判断文件夹内有多少名称带keyName的文件
                    //遍历文件，若所查找文件存在，则记录,count 表示diretPath文件夹下所有名称包含keyName的文件
                    int count = 0;
                    string KeyName = videoPoolId + "_" + bitRate;

                    DirectoryInfo TheFolder = new DirectoryInfo(directPath);

                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        if (NextFile.Name.Contains(KeyName))
                        {
                            count++;
                        }
                    }
                    //ts文件数量加上m3u8和key文件，即为count数量
                    if (count.Equals(tsCount + 2))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
              
            }
            catch (Exception ex)
            {
                Media.mylog.Debug(videoId + "  IsFilecomplete" + "  ex" + ex.Message);
                return false;
            }
            
        }
        //加载视频源
        private void openVideoClicked(object sender, RoutedEventArgs e)
        {        
            string videoId = VIDTextBox.Text.Trim();
            try
            {                                             
                if (Media.MediaState != MediaState.Manual)
                {
                    
                    int bitRate = Convert.ToInt32(comboBox.SelectedValue.ToString());
                    int playType = 0;//0,1  0表示在线播放，1表示本地播放
                                     //播放在线视频获取vid，获取播放地址，分为加密和非加密
                    if ((bool)this.localCheckBox.IsChecked)
                    {
                        playType = 1;
                    }                 
                    string directPaths = "C:/Workspace/Polyv/";
                    int ret = 0;
                    if (playType.Equals(1))
                    {
                        bool IsfileComplete = IsFileComplete(videoId, bitRate, directPaths);
                        if (IsfileComplete)
                            ret = Media.PlayVideo( /*true ,*/videoId, bitRate, playType/*, sPosition*/, directPaths/*, 1.0*/);
                        else
                            ret = -5;
                    }
                    else
                    {
                        ret = Media.PlayVideo( /*true ,*/videoId, bitRate, playType/*, sPosition*/, directPaths/*, 1.0*/);

                    }                 
                    Media.mylog.Debug(videoId + "  PlayVideo" + "  Return Code:" + ret.ToString());
                    if (ret < 0)
                    {
                        Media.Close();
                        durationLbl.Content = "00:00:00";
                        lastSource = "";
                    }
                    else
                    {
                        play = true;
                        playBtn.Content = "pause";
                        // Media.Stop();
                    }
                    System.Diagnostics.Debug.Print("openVideoClicked--ret:" + ret.ToString());

                    string status = "";
                    switch (ret)
                    {
                        case 3: status = "播放超清"; this.comboBox.SelectedValue = 3; break;
                        case 2: status = "播放高清"; this.comboBox.SelectedValue = 2; break;
                        case 1: status = "播放标清"; this.comboBox.SelectedValue = 1; break;
                        case -1: status = "参数错误"; break;
                        case -2: status = "流量超标"; break;
                        case -3: status = "账号过期"; break;
                        case -4: status = "视频信息获取失败"; break;
                        case -5: status = "无对应码率"; break;
                        case -6: status = "网络异常"; break;
                    }
                    this.tipsLabel.Content = status;
                    window.Title = VIDTextBox.Text;
                }
                else
                {
                    this.tipsLabel.Content = "稍等。。。";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);               
                Media.mylog.Error(videoId + "  PlayVideo" + "  play fail1 ex:" + ex.Message);
            }
        }
        //关闭视频源
        private void closeClicked(object sender, RoutedEventArgs e)
        {            
            Media.Close();
            durationLbl.Content = "00:00:00";
            play = false;
            playBtn.Content = "play";
            lastSource = "";
        }
        //播放/暂停
        private void pauseClicked(object sender, RoutedEventArgs e)
        {
            if (Media.IsOpen)
            {
                if (!play)
                {
                    play = true;
                    playBtn.Content = "pause";
                    Media.Play();
                }
                else
                {
                    play = false;
                    playBtn.Content = "play";
                    Media.Pause();
                }
            }
        }
        private void Media_SeekingEnded(object sender, RoutedEventArgs e)
        {
            DateTime dt1 = DateTime.Now;
            Media.mylog.Error( "shijian  :" + dt1 .ToString() );
        }
    
        //停止
        private void stopClicked(object sender, RoutedEventArgs e)
        {
            Media.Stop();
            play = false;
            playBtn.Content = "play";
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string videoId = VIDTextBox.Text.Trim();
            int bitRate = Convert.ToInt32(comboBox.SelectedValue.ToString());
            string directPaths = "C:/Workspace/Polyv/";
            DownLoadVideo.deleteVideo(videoId, bitRate, directPaths);
        }
     
    }
}
