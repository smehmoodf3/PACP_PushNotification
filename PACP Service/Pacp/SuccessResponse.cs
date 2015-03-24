using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Pacp
{
    [DataContract(Namespace = "http://Pacp.com")]
    public class SuccessResponse
    {
        [DataMember]
        public ClsAlerts Data { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; }

        public SuccessResponse()
        {
            Success = false;
            Message = "Failed";
            Data = null;
        }
    }

    [DataContract]
    public class DeleteAlertResponse
    {
        [DataContract(Namespace = "http://Pacp.com")]
        public class deletedItem
        {
            [DataMember]
            public int Id;

            public deletedItem(int id)
            {
                Id = id;
            }
        }

        [DataMember]
        public deletedItem Data { get; set; }

        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public string Message { get; set; }

        public DeleteAlertResponse()
        {
            Data = null;
        }
    }
    
}
