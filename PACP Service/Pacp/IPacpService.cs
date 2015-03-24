using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Pacp;
namespace Pacp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(Namespace = "http://Pacp.com")]
     interface IPacp
    {
        // for Alerts 
        [OperationContract]
        List<ClsAlerts> GetAllAlerts();

        [OperationContract]
        ClsAlerts GetAlertById(int? Id);

        [OperationContract]
        string InsertAlert(ClsAlerts obj);

        [OperationContract]
        string GetInsertAlertResponse(ClsAlerts obj);

        [OperationContract]
        string UpdateAlert(ClsAlerts obj);

        [OperationContract]
        string GetUpdateAlertResponse(ClsAlerts obj);

        [OperationContract]
        string DeleteAlert(int Id);

        [OperationContract]
        string GetDeleteAlertResponse(int Id);

        // for About 
        [OperationContract]
        List<ClsAbout> GetAllAbout();

        [OperationContract]
        ClsAbout GetAboutById(int? Id);

        [OperationContract]
        string InsertAbout(ClsAbout obj);

        [OperationContract]
        string UpdateAbout(ClsAbout obj);

        [OperationContract]
        string DeleteAbout(int Id);

        // for Users 

        [OperationContract]
        string ForgotPwd(int? Id);

        [OperationContract]
        List<clsUser> GetAllUsers();

        [OperationContract]
        clsUser GetUserById(int? Id);

        [OperationContract]
        clsUser GetUserByNamePwd(string UserName, string Password, string UserType);

        [OperationContract]
        string InsertUser(clsUser obj);

        [OperationContract]
        string UpdateUser(clsUser obj);

        [OperationContract]
        string DeleteUser(int Id);

        [OperationContract]
        List<ClsAlerts> getAlerts(string title, string desc);

        //for Device Registration
        [OperationContract]
        List<Device> GetAllDevices();

        [OperationContract]
        List<Device> GetDevicesByType(int deviceType);

        [OperationContract]
        Device GetDevice(string deviceRegistrationId);

        [OperationContract]
        string insertDevice(Device device);

        [OperationContract]
        string updateDevice(string deviceRegistrationId, int deviceType);

        [OperationContract]
        string deleteDevice(string deviceRegistrationId);

    }
   
}
