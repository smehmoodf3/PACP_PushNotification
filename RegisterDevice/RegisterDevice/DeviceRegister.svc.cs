using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using log4net;

namespace RegisterDevice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class DeviceRegister : IDeviceRegister
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string AddDevice(DeviceData device)
        {
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

                string msg;
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
                return msg;
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


        }
    }
}
