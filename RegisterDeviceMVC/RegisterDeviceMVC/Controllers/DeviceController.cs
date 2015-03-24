using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace RegisterDeviceMVC.Controllers
{


    public class DeviceData
    {
        int deviceType;// = true;
        string registrationId;//= "Hello ";


        public int DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }


        public string RegistrationID
        {
            get { return registrationId; }
            set { registrationId = value; }
        }
    }

    public class DeviceController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //
        // GET: /Device/

        [HttpPost]
        public ActionResult Index(DeviceData device)
        {
            string msg;

            try
            {
                log.Debug("AddDevice() called. Starting Execution");
                //PacpClient clntobj = new PacpClient();
                log.Debug("Creating PacpClient");

                PacpDataService.PacpClient pacp = new PacpDataService.PacpClient();
                
                log.Debug("PacpClient created.");
                //DeviceRegistration.DeviceRegistrationServiceClient client = new DeviceRegistration.DeviceRegistrationServiceClient();
                //List<DeviceRegistration.Device> deviceList = client.GetAllDevices();

                log.Debug("Calling PacpClient.GetAllDevices()");
                var deviceList = pacp.GetAllDevices();//client.GetAllDevices();
                log.Debug(string.Format("PacpClient.GetAllDevices() Called, Total {0} devices returned", deviceList.Count()));

                
                bool flag = false;
                if (deviceList.Length > 0)
                {
                    foreach (var dev in deviceList)
                    {
                        if (dev.deviceRegistrationId == device.RegistrationID)
                        {
                            log.Debug("Found the Device");
                            flag = true;
                            break;
                        }

                    }
                    if (!flag)
                    {
                        log.Debug("Device Not Found. Adding to DeviceList in database");
                        //DeviceRegistration.Device dev = new DeviceRegistration.Device();
                        PacpDataService.Device dev = new PacpDataService.Device();
                        dev.deviceRegistrationId = device.RegistrationID;
                        dev.deviceType = device.DeviceType;
                        log.Debug("Calling PacpClient.insertDevice()");
                        msg = pacp.insertDevice(dev);
                        log.Debug(string.Format("PacpClient.insertDevice() Called, meesage: {0}", msg));
                        //pacp.insertDevice(dev);
                        //string msg = "Device is Registered Successfully";

                    }
                    else
                    {
                        log.Debug("Device already Registered");
                        msg = "Device already Registered";
                    }
                }
                else // if device list is empty
                {
                    PacpDataService.Device dev = new PacpDataService.Device();
                    dev.deviceRegistrationId = device.RegistrationID;
                    dev.deviceType = device.DeviceType;
                    log.Debug("Device List is empty. Adding Device via PacpClient.insertDevice()");
                    msg = pacp.insertDevice(dev);
                    log.Debug(string.Format("PacpClient.insertDevice() called. message: {0}", msg));
                }

                //string msg = "Device is Registered Successfully";
                return Json(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                log.Error(ex.Message);
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    log.Error("InnerException: {0}", innerException);
                    innerException = innerException.InnerException;
                }
                throw;
            }

            return Json(msg);
        }

    }
}
