using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFTest.ServiceReference1;

namespace WCFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PacpClient pacpClient = new ServiceReference1.PacpClient();
            ClsAlerts alert = new ClsAlerts();
            alert.Content = Encoding.ASCII.GetBytes("testContent");
            alert.Title = "testtitle";
            alert.Image = "testimage";
            alert.Description = "testDescription";
            alert.Language = "English";


            pacpClient.GetInsertAlertResponse(alert);
            Console.Read();
        }
    }
}
