using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Net.NetworkInformation;
using System.Collections;

namespace PolyvPlayerDemoWinform
{

    public partial class Form1 : Form
    {
        //ThreadTest tt;
       // Thread thread;
       // bool allStart = false;
        TimeSpan DurationPosition = TimeSpan.Zero;
       // Queue<KeyValuePair<string, int>> taskQueue;
        PolyvLocal.DownLoadVideo DownLoadVideo;



        public Form1()
        {
            try
            {
                InitializeComponent();
                DataTable dt = new DataTable();
                dt.Columns.Add("Text");
                dt.Columns.Add("Value");
                DataRow dr1 = dt.NewRow();
                DataRow dr2 = dt.NewRow();
                DataRow dr3 = dt.NewRow();
                dr1["Text"] = "标清";
                dr1["Value"] = 1;
                dr2["Text"] = "高清";
                dr2["Value"] = 2;
                dr3["Text"] = "超清";
                dr3["Value"] = 3;
                dt.Rows.Add(dr1);
                dt.Rows.Add(dr2);
                dt.Rows.Add(dr3);
                this.comboBox.DataSource = dt;
                this.comboBox.DisplayMember = "Text";
                this.comboBox.ValueMember = "Value";
                this.textBox1.Text = "";

                userControl11.OnMediaOpened += OnMediaOpenedEvent;
                // userControl11.OnPositionTime += OnPositionTimeEvent;

                //下载初始化账号信息,不能为空
                string userId = "";
                string secretkey = "";
                string readToken = "";
                DownLoadVideo = new PolyvLocal.DownLoadVideo();
                DownLoadVideo.OnDownloadProgress += DownloadProgressEvent;
                DownLoadVideo.OnDeleteInfo += DeleteInfoEvent;
                DownLoadVideo.OnCurrentBitRate += CurrentBitRate;
                DownLoadVideo.initAccountInfo(userId, secretkey, readToken);
                NetworkChange.NetworkAvailabilityChanged += new NetworkAvailabilityChangedEventHandler(networkchanged);


                this.positionTime.Start();
            }
            catch (Exception ex)
            {

            }

        }

        private void OnPositionTimeEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            TimeSpan time = this.userControl11.Media.Position;
            positionLbl.Text = time.ToString("hh':'mm':'ss''") + "/" + DurationPosition.ToString("hh':'mm':'ss''");
            trackBar.Value = time.Hours * 60 * 60 + time.Minutes * 60 + time.Seconds;
            // OnPositionTime(Media.Position);
        }
        private void OnMediaOpenedEvent(TimeSpan time)
        {
            DurationPosition = time;
            trackBar.Maximum = time.Hours * 60 * 60 + time.Minutes * 60 + time.Seconds;
            //videoSchedule.Value = int.Parse("0" + Position);
            positionLbl.Text = "00:00:00" + "/" + time.ToString("hh':'mm':'ss''");
            //this.Invoke(new Action(()=> {
            //    positionLbl.Text = currentPosition +"/" +totalPosition;
            //}));
        }
        //private void OnPositionTimeEvent(TimeSpan time)
        //{
        //    //trackBar.Maximum = time.Hours * 60 * 60 + time.Minutes * 60 + time.Seconds;
        //    //videoSchedule.Value = int.Parse("0" + Position);
        //    positionLbl.Text = time.ToString("hh':'mm':'ss''") + "/" + DurationPosition.ToString("hh':'mm':'ss''");
        //    //this.Invoke(new Action(()=> {
        //    //    positionLbl.Text = currentPosition +"/" +totalPosition;
        //    //}));
        //}

        public void networkchanged(object sender, NetworkAvailabilityEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (!e.IsAvailable) tipsLabel.Text = "网络中断";
                else tipsLabel.Text = "网络恢复";

            }));
        }
        void DownloadCompletedEvent(string videoId, long totalBytes, string flags, bool status)
        {
            this.Invoke(new MethodInvoker(delegate
            {


            }));
        }

        void DownloadProgressEvent(string videoId, long receiveBytes, long totalBytes)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                tipsLabel.Text = "已下载" + ((receiveBytes) * 100 / (totalBytes)).ToString() + "%";

            }));
        }

        void DeleteInfoEvent(bool status, string videoId, int bitRate, string msg)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                tipsLabel.Text = msg;

            }));
        }
        void CurrentBitRate(string videoId, int inputBitRate, int realBitRate)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                string status = "";
                switch (realBitRate)
                {
                    case 3: status = "下载超清"; comboBox.SelectedIndex = realBitRate - 1; break;
                    case 2: status = "下载高清"; comboBox.SelectedIndex = realBitRate - 1; break;
                    case 1: status = "下载标清"; comboBox.SelectedIndex = realBitRate - 1; break;
                }
                tipsLabel.Text = status;

            }));

        }

        private void form_Closing(object sender, FormClosingEventArgs e)
        {
            this.positionTime.Stop();
            this.userControl11.closeWpfControl();
            //this.Invoke(new MethodInvoker(delegate
            //{
            //    this.userControl11.closeWpfControl();
            //}));
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            string videoId = this.textBox1.Text.Trim();

            int playType = 0;
            if (checkBox1.Checked)
                playType = 1;
            int ret = this.userControl11.open(videoId, playType, Convert.ToInt32(comboBox.SelectedValue));

            string status = "";
            switch (ret)
            {
                case 3: status = "播放超清"; this.button1.Text = "暂停"; break;
                case 2: status = "播放高清"; this.button1.Text = "暂停"; break;
                case 1: status = "播放标清"; this.button1.Text = "暂停"; break;
                case -1: status = "参数错误"; break;
                case -2: status = "流量超标"; break;
                case -3: status = "账号过期"; break;
                case -4: status = "视频信息获取失败"; break;
                case -5: status = "无对应码率"; break;
                case -6: status = "网络异常"; break;
            }
            tipsLabel.Text = status;
        }

        bool IsPlay = false;
        private void pauseBtn_Clicked(object sender, EventArgs e)
        {
            if (!IsPlay)
            {
                IsPlay = true;
                this.userControl11.pause();
                this.button1.Text = "播放";

            }
            else
            {
                IsPlay = false;
                this.userControl11.play();
                this.button1.Text = "暂停";
            }

        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.userControl11.close();

        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string videoId = this.textBox1.Text.Trim();
                // 1,2,3  1表示标清，2表示高清，3表示超清
                int bitRate = Convert.ToInt32(comboBox.SelectedValue);
                string directPath = "C:/";
                string status = "";
                int ret = 0;
                Task task = new Task(new Action(() =>
                {
                    long rest = DownLoadVideo.getFileSize(videoId, bitRate);
                    ret = DownLoadVideo.downloadVideo(videoId, bitRate, directPath);
                    //2,删除，1:暂停，0:成功，-1:参数错误,-2:流量超标，-3:账号过期,-4:视频信息获取失败,-5:没有对应码率,-6:key下载失败，-7：MP4下载失败，-8:m3u8下载失败,-9:ts下载失败；

                    switch (ret)
                    {
                        case 2: status = "删除下载"; break;
                        case 1:status = "暂停下载"; break;
                        case 0: status = "下载完成"; break;
                        case -1: status = "参数错误"; break;
                        case -2: status = "流量超标"; break;
                        case -3: status = "账号过期"; break;
                        case -4: status = "视频信息获取失败"; break;
                        case -5: status = "无对应码率"; break;
                        case -6: status = "key下载失败"; break;
                        case -7: status = "mp4下载失败"; break;
                        case -8: status = "m3u8下载失败"; break;
                        case -9: status = "ts下载失败"; break;
                    }
                    this.Invoke(new Action(() =>
                    {
                        tipsLabel.Text = status;
                    }));


                }));
                task.Start();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private void pauseDownload_Click(object sender, EventArgs e)
        {
            DownLoadVideo.pauseDownload();

        }
        #region 
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string directPaths = "C:/";
            int bitRate = Convert.ToInt32(comboBox.SelectedValue);
            DownLoadVideo.deleteVideo(textBox1.Text.Trim(), bitRate, directPaths);
        }
        bool res = false;


        private void trackBar_Scroll(object sender, EventArgs e)
        {

            this.userControl11.setPosition(trackBar.Value);
        }
        private void volumTrackBarScroll(object sender, EventArgs e)
        {
            double vol = volumeTrackBar.Value ;
            this.userControl11.setVolume(vol /100);
        }
        private void speedRatioScroll(object sender, EventArgs e)
        {
            double speed = speedRatioTrackBar.Value;
            this.userControl11.setSpeedRatio(speed / 10);
        }
    }

    /* public class ThreadTest
     {
         // private string vid;
         // private int bitRate;
         public List<KeyValuePair<string, int>> list;
         long totalBytes;
         PolyvLocal.DownLoadVideo DownLoadVideo;
         public delegate void OnDownloadCompletedHandler(string videoId, long totalBytes, string flags, bool status);
         public event OnDownloadCompletedHandler OnDownloadCompleted;

         public delegate void OnDownloadProgressHandler(string videoId, long receivedBytes, long totalBytes);
         public event OnDownloadProgressHandler OnDownloadProgress;

         public delegate void OnDequeueHandler(string videoId,int bitRate,long totalBytes);
         public event OnDequeueHandler OnDequeue;
         public ThreadTest()
         {

         }

         public void ThreadProc()
         {
             DownLoadVideo = new PolyvLocal.DownLoadVideo();
             DownLoadVideo.OnDownloadCompleted += DownloadCompletedEvent;
             DownLoadVideo.OnDownloadProgress += DownloadProgressEvent;
             DownLoadVideo.OnErrorInfo += DownloadErrorEvent;
             DownLoadVideo.initAccountInfo(userId, secretkey, readToken);

             bool success = false;
             int tryAgain = 0;
             while (!success && queue.Count>0)
             {
                 var kvp = queue.Peek();
                 int ret = DownLoadVideo.downloadVideo(kvp.Key, kvp.Value);
                 if (ret == 0)
                 {
                     //下载完成移除队列
                         queue.Dequeue();
                    // 发送消息，ui界面将下载完成的任务设置为已完成
                        // OnDequeue(kvp.Key, kvp.Value, totalBytes);
                 }
                 else
                 {
                     //下载未完成
                     if (DownLoadVideo.abort)
                     {
                         //该任务被暂停，移除队列
                         queue.Dequeue();
                     }
                     else if (DownLoadVideo.isDelete)
                     {
                         //该任务被删除，移除队列
                         queue.Dequeue();
                     }
                     else
                     {
                         //下载中途出现错误，需重试
                         //if (tryAgain++ < 3)
                         //{
                         //    continue;
                         //}
                         //else
                         //{

                         //}
                     }

                  }

             }


         }

         public void  Download(string vid,int bitRate)
         {

         }

         public void pauseDownload()
         {
             DownLoadVideo.pauseDownload();
         }
         public void deleteVideo(string video, int bitRate)
         {
             DownLoadVideo.deleteVideo(video, bitRate,"");
         }

         public void DownloadCompletedEvent(string videoId, long totalByte, string flags, bool status)
         {
             //OnDownloadCompleted(videoId, totalBytes, flags, status);
             totalBytes = totalByte;


         }
         public void DownloadErrorEvent(string videoId,int retcode,string msg)
         {

         }
         public void DownloadProgressEvent(string videoId, long receiveBytes, long totalBytes)
         {
             OnDownloadProgress(videoId, receiveBytes, totalBytes);

         }

     }
     */
    #endregion

}

