using JdSoft.Apple.Apns.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string p12FileName = @"D:\Certs\Certificates.p12"; // change this to reflect your own certificate
            if (File.Exists(p12FileName))
            {
                string p12Password = ""; // change this
                bool sandBox = true;
                int numConnections = 1; // you can change the number of connections here
                var notificationService = new NotificationService(sandBox, p12FileName, p12Password, numConnections);
                var deviceToken = "e74a78b88bfde84aae905a844df8a0401bf39002a628b25a7048dccb188644ce"; // put in your device token here
                var notification = new Notification(deviceToken);
                notification.Payload.Alert.Body = "Some message 12313213";
                notification.Payload.Sound = "beep.wav";
                notification.Payload.Badge = 1;
                if (notificationService.QueueNotification(notification))
                {
                    // queued the notification
                }
                else
                {
                    // failed to queue
                }
                // This ensures any queued notifications get sent befor the connections are closed
                notificationService.Close();
                notificationService.Dispose();
            }
            
        }
    }
}
