using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Text;
using log4net;


namespace Pacp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class PacpService : IPacp
    {

        /*  IMP NOTE FOR DATA ENTITY FRAMEWORK
         *  in case if you do any modification in Table or if you changes the source of Db, 
         *  you need to re create Enity model by deleting previous one.
         *  You need to delete Connection string in App.Config file and Once you create Connection String in App.Config you need 
         *  to Place same in Web Config file. Once you done with this, Create object of Connection string Name and call the Method.
         *  Note Data Entity Framework allow your to use LINQ. 
         * 
         * */
        //PacpDbEntities entobj;
        PacpDbEntities entobj;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
      //  PacpDbEntities1 entobj;
        int i = 0;



        //[Sarim]: overloaded for returning Alert ID
        public String GetInsertAlertResponse(ClsAlerts item)
        {
            log.Debug("Pacp.GetInsertAlertResponse() called. Starting Execution");
            log.Debug("Calling PacpDbEntities()");
            entobj = new PacpDbEntities();

            log.Debug("Creating AlertObject and adding to Database");

            Pacp_tbl_Alerts Alertobj = new Pacp_tbl_Alerts();
            try
            {
                Alertobj.Title = item.Title;
                Alertobj.UserId = item.UserId;
                Alertobj.Description = item.Description;
                Alertobj.Image = item.Image;
                Alertobj.Language = item.Language;
                Alertobj.Date = item.Date.HasValue == true ? item.Date.Value : DateTime.Now;
                Alertobj.Status = 1;
                Alertobj.CreatedBy = 1;
                Alertobj.CreatedDate = DateTime.Now.Date;
                Alertobj.Content = item.Content;
                entobj.Pacp_tbl_Alerts.AddObject(Alertobj);
                i = entobj.SaveChanges();
                log.Debug(string.Format("Alert added to Db, AlertId : {0}", Alertobj.Id));
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    log.Error("InnerException: {0}", innerException);
                    innerException = innerException.InnerException;
                }
                throw;
            }

            //string stat = "Failed";
            log.Debug("Creating Response Object");
            string responseString = "";
            try
            {
                SuccessResponse responseObject = new SuccessResponse();
                if (i > 0)
                {
                    ClsAlerts alertData = new ClsAlerts();
                    alertData.Id = Alertobj.Id;
                    alertData.Image = Alertobj.Image;
                    alertData.Language = Alertobj.Language;
                    alertData.Description = Alertobj.Description;
                    alertData.Title = Alertobj.Title;
                    responseObject.Data = alertData;
                    responseObject.Success = true;
                    responseObject.Message = "Alert added successfully";
                    //stat = "Inserted";
                }

                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(responseObject.GetType());
                serializer.WriteObject(ms, responseObject);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                responseString = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    log.Error("InnerException: {0}", innerException);
                    innerException = innerException.InnerException;
                }
                throw;
            }

            log.Debug("Returning from GetInsertAlertResponse()");

            return responseString;
        }


        //[Sarim]: overloaded for returning Alert ID
        public string GetUpdateAlertResponse(ClsAlerts altobj)
        {
            log.Debug("Pacp.GetUpdateAlertResponse() called. Starting Execution");
            log.Debug("Calling PacpDbEntities() to get Database Entities");

            // entobj = new PacpDbEntities();
            string responseString = "";
            entobj = new PacpDbEntities();
            var item = (from x in entobj.Pacp_tbl_Alerts where x.Id == altobj.Id && x.Status == 1 select x).FirstOrDefault();
            Pacp_tbl_Alerts Alertobj = new Pacp_tbl_Alerts();

            log.Debug("Updating the alert record in Database");
            if (item != null)
            {
                Alertobj.Id = altobj.Id;
                Alertobj.Title = altobj.Title;
                Alertobj.UserId = item.UserId;
                Alertobj.Description = altobj.Description;
                Alertobj.Image = altobj.Image;
                Alertobj.Language = altobj.Language;
                Alertobj.Date = altobj.Date.HasValue == true? altobj.Date.Value: DateTime.Now;
                Alertobj.CreatedBy = 1;
                Alertobj.CreatedDate = DateTime.Now.Date;
                Alertobj.ModifiedBy = 1;
                Alertobj.ModifiedDate = DateTime.Now.Date;
                Alertobj.Status = 1;
                Alertobj.Content = item.Content;

                entobj.Pacp_tbl_Alerts.Detach(item);
                entobj.Pacp_tbl_Alerts.Attach(Alertobj);
                entobj.ObjectStateManager.ChangeObjectState(Alertobj, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }

            SuccessResponse responseObject = new SuccessResponse();

            try
            {
                if (i > 0)
                {
                    log.Debug(string.Format("Alert Updated, AlertId:  {0}", Alertobj.Id));
                    responseObject.Success = true;
                    responseObject.Message = "Updated Successfully";
                    ClsAlerts alertData = new ClsAlerts();
                    alertData.Id = Alertobj.Id;
                    alertData.Image = Alertobj.Image;
                    alertData.Language = Alertobj.Language;
                    alertData.Description = Alertobj.Description;
                    alertData.Title = Alertobj.Title;
                    responseObject.Data = alertData;
                    //stat = "Updated";
                }

                log.Debug("Converting Json To Response Object");
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(responseObject.GetType());
                serializer.WriteObject(ms, responseObject);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                responseString = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    log.Error("InnerException: {0}", innerException);
                    innerException = innerException.InnerException;
                }
                throw;
            }

            log.Debug("Returning from GetUpdateAlertResponse()");

            return responseString;
        }



        //[Sarim]: overloaded for returning Alert ID
        public string GetDeleteAlertResponse(int Id)
        {
            // entobj = new PacpDbEntities();
            log.Debug("Pacp.GetUpdateAlertResponse() called. Starting Execution");
            log.Debug("Calling PacpDbEntities() to get Database Entities");

            entobj = new PacpDbEntities();
            var obj1 = (from x in entobj.Pacp_tbl_Alerts where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            string stat = "Failed";
            string responseString = "";
            DeleteAlertResponse responseObject = new DeleteAlertResponse();
            log.Debug("Deleting the alert record in Database");

            try
            {
                if (obj1 != null)
                {
                    obj1.Status = 0;
                    obj1.ModifiedBy = 1;
                    obj1.ModifiedDate = DateTime.Now.Date;

                    entobj.Pacp_tbl_Alerts.Detach(obj1);
                    entobj.Pacp_tbl_Alerts.Attach(obj1);
                    entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                    i = entobj.SaveChanges();
                }
                else
                {
                    responseObject.Success = false;
                    responseObject.Message = "Alert does not exists";
                    //stat = "Alert does not exists";
                }

                if (i > 0)
                {
                    responseObject.Success = true;
                    responseObject.Message = "Deleted successfully";
                    responseObject.Data = new DeleteAlertResponse.deletedItem(obj1.Id);
                    //stat = "Deleted";
                }

                log.Debug("Converting json to Response object");
                MemoryStream ms = new MemoryStream();
                var objType = responseObject.GetType();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(responseObject.GetType());
                serializer.WriteObject(ms, responseObject);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);
                responseString = sr.ReadToEnd();
            }
            catch (Exception ex)
            {

            }

            log.Debug("Returning from GetDeleteAlertResponse");
            return responseString;
        }
        

        public List<ClsAlerts> GetAllAlerts()
        {
            entobj = new PacpDbEntities();
            entobj.CommandTimeout = int.MaxValue;
         //   entobj = new PacpDbEntities();
            var AlertObj = from x in entobj.Pacp_tbl_Alerts where x.Status == 1 select x;
            List<ClsAlerts> alertList = new List<ClsAlerts>();
            foreach (Pacp_tbl_Alerts item in AlertObj)
            {
                ClsAlerts uobj = new ClsAlerts();
                uobj.Id = item.Id;
                uobj.UserId = item.UserId;
                uobj.Title = item.Title;
                uobj.Description = item.Description;
                uobj.Image = item.Image;
                uobj.Language = item.Language;
                uobj.Date = item.Date;
                alertList.Add(uobj);
            }
            return alertList;
        }

        public ClsAlerts GetAlertById(int? Id)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var item = (from x in entobj.Pacp_tbl_Alerts where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            ClsAlerts uobj = new ClsAlerts();
            if (item != null)
            {
                uobj.Id = item.Id;
                uobj.UserId = item.UserId;
                uobj.Title = item.Title;
                uobj.Description = item.Description;
                uobj.Image = item.Image;
                uobj.Language = item.Language;
                uobj.Date = item.Date;
            }
            return uobj;
        }

        public string InsertAlert(ClsAlerts item)
        {
            //entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            Pacp_tbl_Alerts Alertobj = new Pacp_tbl_Alerts();
            Alertobj.Title = item.Title;
            Alertobj.UserId = item.UserId;
            Alertobj.Description = item.Description;
            Alertobj.Image = item.Image;
            Alertobj.Language = item.Language;
            Alertobj.Date = item.Date.Value;
            Alertobj.Status = 1;
            Alertobj.CreatedBy = 1;
            Alertobj.CreatedDate = DateTime.Now.Date;
            Alertobj.Content = item.Content;
            entobj.Pacp_tbl_Alerts.AddObject(Alertobj);
            i = entobj.SaveChanges();
            string stat = "Failed";
            if (i > 0)
            {
                stat = "Inserted";
            }
            return stat;
        }

        public string UpdateAlert(ClsAlerts altobj)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var item = (from x in entobj.Pacp_tbl_Alerts where x.Id == altobj.Id && x.Status == 1 select x).FirstOrDefault();

            if (item != null)
            {
                Pacp_tbl_Alerts Alertobj = new Pacp_tbl_Alerts();
                Alertobj.Id = altobj.Id;
                Alertobj.Title = altobj.Title;
                Alertobj.UserId = item.UserId;
                Alertobj.Description = altobj.Description;
                Alertobj.Image = altobj.Image;
                Alertobj.Language = altobj.Language;
                Alertobj.Date = altobj.Date.Value;
                Alertobj.CreatedBy = 1;
                Alertobj.CreatedDate = DateTime.Now.Date;
                Alertobj.ModifiedBy = 1;
                Alertobj.ModifiedDate = DateTime.Now.Date;
                Alertobj.Status = 1;
                Alertobj.Content = altobj.Content;

                entobj.Pacp_tbl_Alerts.Detach(item);
                entobj.Pacp_tbl_Alerts.Attach(Alertobj);
                entobj.ObjectStateManager.ChangeObjectState(Alertobj, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }
            string stat = "Failed";
            if (i > 0)
            {
                stat = "Updated";
            }
            return stat;
        }

        public string DeleteAlert(int Id)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var obj1 = (from x in entobj.Pacp_tbl_Alerts where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            string stat = "Failed";

            if (obj1 != null)
            {
                obj1.Status = 0;
                obj1.ModifiedBy = 1;
                obj1.ModifiedDate = DateTime.Now.Date;

                entobj.Pacp_tbl_Alerts.Detach(obj1);
                entobj.Pacp_tbl_Alerts.Attach(obj1);
                entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }
            else
            {
                stat = "Alert does not exists";
            }
            if (i > 0)
            {
                stat = "Deleted";
            }
            return stat;
        }

        public List<ClsAbout> GetAllAbout()
        {
          //  entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var AlertObj = from x in entobj.Pacp_tbl_abt where x.Status == 1 select x;
            List<ClsAbout> alertList = new List<ClsAbout>();
            foreach (Pacp_tbl_abt item in AlertObj)
            {
                ClsAbout uobj = new ClsAbout();
                uobj.Id = item.Id;
                uobj.Title = item.Title;
                uobj.Text = item.Text;
                uobj.AText = item.AText;
                alertList.Add(uobj);
            }
            return alertList;
        }

        public ClsAbout GetAboutById(int? Id)
        {
          //  entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var item = (from x in entobj.Pacp_tbl_abt where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            ClsAbout uobj = new ClsAbout();
            if (item != null)
            {
                uobj.Id = item.Id;
                uobj.Title = item.Title;
                uobj.Text = item.Text;
                uobj.AText = item.AText;
            }
            return uobj;
        }

        public string InsertAbout(ClsAbout item)
        {
            //entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            Pacp_tbl_abt Abtobj = new Pacp_tbl_abt();
            Abtobj.Title = item.Title;
            Abtobj.Text = item.Text;
            Abtobj.AText = item.AText;
            Abtobj.Status = 1;
            Abtobj.CreatedBy = 1;
            Abtobj.CreatedDate = DateTime.Now.Date;
            entobj.Pacp_tbl_abt.AddObject(Abtobj);
            i = entobj.SaveChanges();
            string stat = "Failed";
            if (i > 0)
            {
                stat = "Inserted";
            }
            return stat;
        }

        public string UpdateAbout(ClsAbout obj)
        {
         //   entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var item = (from x in entobj.Pacp_tbl_abt where x.Id == obj.Id && x.Status == 1 select x).FirstOrDefault();

            if (item != null)
            {
                Pacp_tbl_abt Abtobj = new Pacp_tbl_abt();
                Abtobj.Id = item.Id;
                Abtobj.Title = obj.Title;
                Abtobj.Text = obj.Text;
                Abtobj.AText = obj.AText;
                Abtobj.ModifiedBy = 1;
                Abtobj.CreatedBy = 1;
                Abtobj.ModifiedDate = DateTime.Now.Date;
                Abtobj.CreatedDate = DateTime.Now.Date;
                Abtobj.Status = 1;

                entobj.Pacp_tbl_abt.Detach(item);
                entobj.Pacp_tbl_abt.Attach(Abtobj);
                entobj.ObjectStateManager.ChangeObjectState(Abtobj, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }
            string stat = "Failed";
            if (i > 0)
            {
                stat = "Updated";
            }
            return stat;
        }

        public string DeleteAbout(int Id)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var obj1 = (from x in entobj.Pacp_tbl_abt where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            string stat = "Failed";

            if (obj1 != null)
            {
                obj1.Status = 0;
                obj1.ModifiedBy = 1;
                obj1.ModifiedDate = DateTime.Now.Date;

                entobj.Pacp_tbl_abt.Detach(obj1);
                entobj.Pacp_tbl_abt.Attach(obj1);
                entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }
            else
            {
                stat = "About record does not exists";
            }
            if (i > 0)
            {
                stat = "Deleted";
            }
            return stat;
        }


        public string ForgotPwd(int? Id)
        {
            string stat = string.Empty;

            //entobj = new PacpDbEntities();

            //var temp = (from x in entobj.Pacp_tbl_Users where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            //stat = "Invalid User Id";
            //if (temp != null)
            //{
            //    string email = temp.Email;
            //    if (!email.Contains('@'))
            //    {
            //        stat = "Invalid Email Id";
            //    }
            //    else
            //    {
            //        MailAddress fromAddress = new MailAddress("noreply@myiton.com", "srinivas");
            //        MailAddress toAddress = new MailAddress(email, temp.Name);
            //        StringBuilder str = new StringBuilder();

            //        str.Append("<table style=width:100%;><tr><td>Dear " + temp.Name + ", </td> </tr><tr> <td style=text-align: left><table align=center cellpadding=5 cellspacing=0 style=width: 50%;> <tr> <td style=text-align: left>Your recently requested for forgot password.</td> </tr> <tr> <td>Password for your User name (" + temp.UserName + ") is : " + temp.Password + "</td> </tr> <tr> <td>Please login with this password to access your account </td> </tr> </table> </td> </tr> <tr> <td>&nbsp;</td> </tr>");
            //        str.Append("<tr> <td>Thank You </td> </tr> <tr> <td>ITON Technologies </td> </tr> </table>");

            //        //str.Append(" Dear " +temp.Name +", <br><br>");
            //        //str.AppendLine(" Your recently requested for forgot password.<br>");
            //        //str.AppendLine(" Password for your User name ("+temp.UserName +") is : "+temp.Password +"<br>");
            //        //str.AppendLine(" Please login with this password to access your account <br>");
            //        //str.AppendLine(" Thank You <br>");
            //        //str.AppendLine(" ITON Technologies");
            //        //mail.Body = str.ToString();

            //        const string subject = "IContacts - Forgot Passord";
            //        string body = str.ToString();

            //        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(fromAddress.Address, toAddress.Address, subject, body);
            //        msg.IsBodyHtml = true;

            //        //var client = new SmtpClient("smtp.gmail.com", 587)
            //        var client = new SmtpClient("mail.myiton.com", 587)
            //        {
            //            Credentials = new NetworkCredential("noreply@myiton.com", "iton@123"),
            //            //Credentials = new NetworkCredential("srinivas.pasumarthi@myiton.com", "ITON@123"),
            //            // EnableSsl = true  
            //        };
            //        try
            //        {
            //            client.Send(msg);
            //            stat = "Mail Sent Successfully";
            //        }
            //        catch (Exception ex)
            //        {
            //            //Console.WriteLine(ex.ToString());
            //            stat = ex.ToString();
            //        }
            //    }
            //}
            return stat;
        }

        public List<clsUser> GetAllUsers()
        {
         //   entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var UserObj = from x in entobj.Pacp_tbl_Users where x.Status == 1 select x;
            List<clsUser> usersList = new List<clsUser>();
            foreach (Pacp_tbl_Users item in UserObj)
            {
                clsUser uobj = new clsUser();
                uobj.Id = item.Id;
                uobj.Name = item.Name;
                uobj.UserName = item.UserName;
                uobj.Password = item.Password;
                uobj.UserType = item.UserType;
                uobj.Country = item.Country;
                uobj.Email = item.Email;
                usersList.Add(uobj);
            }
            return usersList;
        }

        public clsUser GetUserById(int? Id)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var UserObj = (from x in entobj.Pacp_tbl_Users where x.Id == Id && x.Status == 1 select x).FirstOrDefault();
            clsUser uobj = new clsUser();
            if (UserObj != null)
            {
                uobj.Id = UserObj.Id;
                uobj.Name = UserObj.Name;
                uobj.UserName = UserObj.UserName;
                uobj.Password = UserObj.Password;
                uobj.UserType = UserObj.UserType;
                uobj.Country = UserObj.Country;
                uobj.Email = UserObj.Email;
            }
            return uobj;
        }

        public clsUser GetUserByNamePwd(string UserName, string Password,string UserType)
        {
            //entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();

            var UserObj = (from x in entobj.Pacp_tbl_Users where x.UserName == UserName && x.Password == Password && x.UserType == UserType && x.Status == 1 select x).FirstOrDefault();
            clsUser uobj = new clsUser();
            if (UserObj != null)
            {
                uobj.Id = UserObj.Id;
                uobj.Name = UserObj.Name;
                uobj.UserName = UserObj.UserName;
                uobj.Password = UserObj.Password;
                uobj.UserType = UserObj.UserType;
                uobj.Country = UserObj.Country;
                uobj.Email = UserObj.Email;
            }
            return uobj;
        }

        public string InsertUser(clsUser obj)
        {
            //entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            bool Status = CheckUserName(obj.UserName);
            string stat = string.Empty;
            stat = Status == true ? "User Name is already exists" : stat;

            if (Status == false)
            {
                Pacp_tbl_Users Userobj = new Pacp_tbl_Users();
                //Userobj.Id = 3;
                Userobj.Name = obj.Name;
                Userobj.UserName = obj.UserName;
                Userobj.Password = obj.Password;
                Userobj.UserType = "User";
                Userobj.Country = obj.Country;
                Userobj.Email = obj.Email;
                Userobj.Status = 1;
                Userobj.CreatedBy = 0;
                Userobj.CreatedDate = DateTime.Now.Date;
                entobj.Pacp_tbl_Users.AddObject(Userobj);
                i = entobj.SaveChanges();
            }
           
            return (i > 0 ? "Inserted" : stat);
        }

        public string UpdateUser(clsUser obj)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            string stat = "Failed";

            bool Status = CheckUserName(obj.UserName);

            stat = Status == true ? "User Name is already exists" : stat;

            if (Status == false)
            {
                var Userobj = (from x in entobj.Pacp_tbl_Users where x.Id == obj.Id && x.Status == 1 select x).FirstOrDefault();
                if (Userobj != null)
                {
                    Pacp_tbl_Users obj1 = new Pacp_tbl_Users();
                    obj1.Id = Userobj.Id;
                    obj1.Name = obj.Name;
                    obj1.UserName = obj.UserName;
                    obj1.Password = obj.Password;
                    //obj1.UserType = obj.UserType;
                    obj1.Country = obj.Country;
                    obj1.Status = 1;
                    obj1.Email = obj.Email;
                    obj1.ModifiedBy = Userobj.Id;
                    obj1.ModifiedDate = DateTime.Now.Date;

                    entobj.Pacp_tbl_Users.Detach(Userobj);
                    entobj.Pacp_tbl_Users.Attach(obj1);
                    entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                    i = entobj.SaveChanges();
                    stat = i > 0 ? "Updated" : string.Empty;
                }
            }
            return stat;
        }

        private bool CheckUserName(string UserName)
        {
            //entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var Userobj = (from x in entobj.Pacp_tbl_Users where x.UserName == UserName && x.Status == 1 select x).FirstOrDefault();

            return Userobj == null ? false : true;
        }

        public string DeleteUser(int Id)
        {
           // entobj = new PacpDbEntities();
            entobj = new PacpDbEntities();
            var obj1 = (from x in entobj.Pacp_tbl_Users where x.Id == Id && x.Status == 1 select x).FirstOrDefault();

            string stat = "Failed";

            if (obj1 != null)
            {
                obj1.Status = 0;
                //obj1.ModifiedBy = obj1.UserId;
                obj1.ModifiedDate = DateTime.Now.Date;

                entobj.Pacp_tbl_Users.Detach(obj1);
                entobj.Pacp_tbl_Users.Attach(obj1);
                entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                i = entobj.SaveChanges();
            }
            else
            {
                stat = "User does not exists";
            }
            return i > 0 ? "Deleted" : stat;
        }

        public clsUser ValidateUserCredentials(string UserName, string Pwd, string UserType)
        {
           return GetUserByNamePwd(UserName,Pwd,UserType);
        }
     
        public List<ClsAlerts> getAlerts(string title, string desc)
        {
            List<ClsAlerts> listAlerts = new List<ClsAlerts>();
            entobj = new PacpDbEntities();
            var alertVar = from x in entobj.Pacp_tbl_Alerts where x.Description.Contains(desc) && x.Title.Contains(title) && x.Status == 1 select x;
            //var alertVar = from x in entobj.Pacp_tbl_Alerts where x.Description.Contains(desc) && x.Title.Contains(title) && x.Status == 1 select x;
            foreach (Pacp_tbl_Alerts item in alertVar)
            {
                listAlerts.Add(new ClsAlerts { 
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    Date = item.Date,
                    Description = item.Description,
                    Id = item.Id,
                    Image =item.Image,
                    Language = item.Language,
                    ModifiedBy = Convert.ToInt32(item.ModifiedBy.ToString()),
                    ModifiedDate = item.ModifiedDate,
                    Status = item.Status,
                    Title = item.Title,
                    UserId = item.UserId
                    


                });
            }




            return listAlerts;
        }


        //folio3 changes
        public List<Device> GetAllDevices()
        {
            entobj = new PacpDbEntities();
            List<Device> deviceList = new List<Device>();

            var q =
                from a in entobj.Pacp_tbl_Registered_Devices//.GetTable<DeviceRegistration>()
                select a;
            foreach (var item in q)
            {
                Device d = new Device(); ;
                d.deviceRegistrationId = item.RegistrationID;
                d.deviceType = item.DeviceType;
                deviceList.Add(d);
            }
            return deviceList;
        }


        public List<Device> GetDevicesByType(int deviceType)
        {
            //DeviceRegistrationClassDataContext dc = new DeviceRegistrationClassDataContext();
            entobj = new PacpDbEntities();
            List<Device> deviceList = new List<Device>();

            var q =
                from a in entobj.Pacp_tbl_Registered_Devices//.GetTable<DeviceRegistration>()
                where a.DeviceType == deviceType
                select a;
            foreach (var item in q)
            {
                Device d = new Device();
                d.deviceRegistrationId = item.RegistrationID;
                d.deviceType = item.DeviceType;
                deviceList.Add(d);
            }
            return deviceList;
        }

        public Device GetDevice(string deviceRegistrationId)
        {

            //DeviceRegistrationClassDataContext dc = new DeviceRegistrationClassDataContext();
            entobj = new PacpDbEntities();
            Device device = new Device();
            var q =
            from a in entobj.Pacp_tbl_Registered_Devices//.GetTable<DeviceRegistration>()
            where a.RegistrationID == deviceRegistrationId
            select a;
            if (q != null)
            {
                device.deviceRegistrationId = q.FirstOrDefault().RegistrationID;
                device.deviceType = q.FirstOrDefault().DeviceType;
                return device;
            }
            else
            {
                return null;
            }


        }


        public string insertDevice(Device device)
        {
            try
            {
                //DeviceRegistrationClassDataContext dc = new DeviceRegistrationClassDataContext();
                entobj = new PacpDbEntities();
                //DeviceRegistration dr = new DeviceRegistration();
                Pacp_tbl_Registered_Devices dr = new Pacp_tbl_Registered_Devices();
                dr.DeviceType = device.deviceType;
                dr.RegistrationID = device.deviceRegistrationId;
                dr.Status = 1;
                dr.CreatedBy = 0;
                dr.CreatedDate = DateTime.Now.Date;
                //dc.DeviceRegistrations.InsertOnSubmit(dr);
                //dc.SubmitChanges();
                entobj.Pacp_tbl_Registered_Devices.AddObject(dr);
                i = entobj.SaveChanges();
                string msg = i > 0 ? "Device is Successfully Registered" : string.Empty;
                //string msg = "Device is Successfully Registered";
                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        public string updateDevice(string deviceRegistrationId, int deviceType)
        {
            try
            {

                //DeviceRegistrationClassDataContext dc = new DeviceRegistrationClassDataContext();
                entobj = new PacpDbEntities();
                Pacp_tbl_Registered_Devices dev = entobj.Pacp_tbl_Registered_Devices.Single(d => d.RegistrationID == deviceRegistrationId && d.Status == 1);
                //DeviceRegistration dev = dc.DeviceRegistrations.Single(d => d.RegistrationID == deviceRegistrationId);
                if (dev != null)
                {
                    Pacp_tbl_Registered_Devices device = new Pacp_tbl_Registered_Devices();
                    device.RegistrationID = dev.RegistrationID;
                    device.DeviceType = deviceType;
                    device.ModifiedBy = 1;
                    device.CreatedBy = 1;
                    device.ModifiedDate = DateTime.Now.Date;
                    device.CreatedDate = DateTime.Now.Date;
                    device.Status = 1;
                    entobj.Pacp_tbl_Registered_Devices.Detach(dev);
                    entobj.Pacp_tbl_Registered_Devices.Attach(device);
                    entobj.ObjectStateManager.ChangeObjectState(device, System.Data.EntityState.Modified);
                    i = entobj.SaveChanges();
                }
                string msg = String.Empty;
                if (i > 0)
                {
                    msg = "Device is Successfully Updated";
                }
                
                return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string deleteDevice(string deviceRegistrationId)
        {
            try
            {

                //DeviceRegistrationClassDataContext dc = new DeviceRegistrationClassDataContext();
                //DeviceRegistration dev = dc.DeviceRegistrations.Single(d => d.RegistrationID == deviceRegistrationId);
                entobj = new PacpDbEntities();
                var obj1 = (from x in entobj.Pacp_tbl_Registered_Devices where x.RegistrationID == deviceRegistrationId && x.Status == 1 select x).FirstOrDefault();
                string msg = "Failed";

                if (obj1 != null)
                {
                    obj1.Status = 0;
                    obj1.ModifiedBy = 1;
                    obj1.ModifiedDate = DateTime.Now.Date;

                    entobj.Pacp_tbl_Registered_Devices.Detach(obj1);
                    entobj.Pacp_tbl_Registered_Devices.Attach(obj1);
                    entobj.ObjectStateManager.ChangeObjectState(obj1, System.Data.EntityState.Modified);
                    i = entobj.SaveChanges();
                }
                else
                {
                    msg = "Device does not exists";
                }
                if (i > 0)
                {
                    msg = "Deleted";
                }
                return msg;

                //dc.DeviceRegistrations.DeleteOnSubmit(dev);
                //dc.SubmitChanges();
                //string msg = "Device is Successfully Updated";
                //return msg;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
