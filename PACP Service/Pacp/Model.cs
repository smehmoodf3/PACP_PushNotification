using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization ;

namespace Pacp
{
    [DataContract(Namespace = "http://Pacp.com")]
    public class ClsMaster
    {
        // [DataMember]
        public int Status { get; set; }
        // [DataMember]
        public int CreatedBy { get; set; }
        //[DataMember]
        public DateTime? CreatedDate { get; set; }
        //[DataMember]
        public int ModifiedBy { get; set; }
        // [DataMember]
        public DateTime? ModifiedDate { get; set; }
    }
    [DataContract(Namespace = "http://Pacp.com")]
    public class ClsAlerts:ClsMaster
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public byte[] Content { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public System .Nullable<DateTime>  Date { get; set; }
    }
    [DataContract(Namespace = "http://Pacp.com")]
    public class ClsAbout : ClsMaster
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string AText { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
    [DataContract(Namespace = "http://Pacp.com")]
   public class clsUser
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string Country { get; set; }
    }

    [DataContract(Namespace = "http://Pacp.com")]
    public class Device : ClsMaster
    {
        string _deviceRegistrationId;
        int _deviceType;

        [DataMember]
        public string deviceRegistrationId
        {
            get { return _deviceRegistrationId; }
            set { _deviceRegistrationId = value; }
        }

        [DataMember]
        public int deviceType
        {
            get { return _deviceType; }
            set { _deviceType = value; }
        }
    }
}
