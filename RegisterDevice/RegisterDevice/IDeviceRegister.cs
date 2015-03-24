using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RegisterDevice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IDeviceRegister
    {

        [WebInvoke(UriTemplate = "/AddDevice",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json, Method = "POST")]
        string AddDevice(DeviceData device);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class DeviceData
    {
        int deviceType;// = true;
        string registrationId;//= "Hello ";

        [DataMember]
        public int DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        [DataMember]
        public string RegistrationID
        {
            get { return registrationId; }
            set { registrationId = value; }
        }
    }
}
