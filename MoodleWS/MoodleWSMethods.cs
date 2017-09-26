using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using MoodleWS.Common.Enums;
using MoodleWS.Common.Exceptions;
using Newtonsoft.Json;

namespace MoodleWS
{
    public class MoodleWSMethods
    {
        private dynamic CallMoodleFunction(string functionName, dynamic postData)
        {
            try
            {
                string createRequest = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", ConfigEnum.MoodleBaseURL, ConfigEnum.MoodleRestServerPath, 
                    ConfigEnum.MoodleWSTokenParameter, ConfigEnum.MoodleWSToken, ConfigEnum.MoodleWSFunctionNameParameter, 
                    functionName, ConfigEnum.MoodleRestServiceFormatParameter, ConfigEnum.MoodleRestServiceFormat);
                           
                // Call Moodle REST Service
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(createRequest);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
 
                // Encode the parameters as form data:
                byte[] formData = UTF8Encoding.UTF8.GetBytes(postData);
                request.ContentLength = formData.Length;
 
                // Write out the form Data to the request:
                using (Stream post = request.GetRequestStream())
                {
                    post.Write(formData, 0, formData.Length);
                }           
  
                // Get the Response
                HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
                Stream resStream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string contents = reader.ReadToEnd();
 
                dynamic response = JsonConvert.DeserializeObject<dynamic>(contents);

                try
                {
                    if (response.exception != null)
                    {
                        throw new MoodleWSException(contents);
                    }
                }
                catch (RuntimeBinderException)
                {
                    // DO NOTHING. No hay excepción desde el webservice de Moodle.
                } 

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetUserByUsername(string username)
        { 
            dynamic response;

            string postData = string.Format("&field=username&values[0]={0}", username);

            response = CallMoodleFunction("core_user_get_users_by_field", postData);

            return response;
        }

        public dynamic GetUserByIdNumber(int idNumber)
        {
            dynamic response;

            string postData = string.Format("&field=idnumber&values[0]={0}", idNumber);

            response = CallMoodleFunction("core_user_get_users_by_field", postData);

            return response;
        }

        public dynamic GetUserByCriteria(string key, string value)
        { 
            dynamic response;

            string postData = string.Format("&criteria[0][key]={0}&criteria[0][value]={1}", key, value);

            response = CallMoodleFunction("core_user_get_users", postData);

            return response;
        }

        public dynamic GetUserByTwoCriteria(string key1, string value1, string key2, string value2)
        {
            dynamic response;

            string postData = string.Format("&criteria[0][key]={0}&criteria[0][value]={1}&criteria[1][key]={2}&criteria[1][value]={3}", key1, value1, key2, value2);

            response = CallMoodleFunction("core_user_get_users", postData);

            return response;
        }

        public dynamic GetUserById(long id)
        {
            dynamic response;

            string postData = string.Format("&field=id&values[0]={0}", id);

            response = CallMoodleFunction("core_user_get_users_by_field", postData);

            return response;
        }

        public dynamic CreateUser(dynamic user)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (user == null) throw new Exception("Usuario null");
            if (user.username == null) throw new Exception("Username null"); 
                else sb.Append("&").Append("users[0][username]").Append("=").Append(user.username);
            if (user.firstname == null) throw new Exception("First Name null");
                else sb.Append("&").Append("users[0][firstname]").Append("=").Append(user.firstname);
            if (user.lastname == null) throw new Exception("Last Name null");
                else sb.Append("&").Append("users[0][lastname]").Append("=").Append(user.lastname);
            if (user.email == null) throw new Exception("Email null"); 
                else sb.Append("&").Append("users[0][email]").Append("=").Append(user.email);
            if (user.auth == null) throw new Exception("Auth null");
                else sb.Append("&").Append("users[0][auth]").Append("=").Append(user.auth);
            if (user.password == null) throw new Exception("Password null");
                else sb.Append("&").Append("users[0][password]").Append("=").Append(user.password);
            if (user.idnumber == null) throw new Exception("idnumber null");
                else sb.Append("&").Append("users[0][idnumber]").Append("=").Append(user.idnumber);  

            string postData = sb.ToString();

            response = CallMoodleFunction("core_user_create_users", postData);

            return response;
        }

        public dynamic UpdateUser(dynamic user)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (user == null) throw new Exception("Usuario null");
            if (user.id == null) throw new Exception("id null");
            else sb.Append("&").Append("users[0][id]").Append("=").Append(user.id);
            /*if (user.username == null) throw new Exception("Username null");
            else sb.Append("&").Append("users[0][username]").Append("=").Append(user.username);
            if (user.firstname == null) throw new Exception("First Name null");
            else sb.Append("&").Append("users[0][firstname]").Append("=").Append(user.firstname);
            if (user.lastname == null) throw new Exception("Last Name null");
            else sb.Append("&").Append("users[0][lastname]").Append("=").Append(user.lastname);
            if (user.email == null) throw new Exception("Email null");
            else sb.Append("&").Append("users[0][email]").Append("=").Append(user.email);
            if (user.auth == null) throw new Exception("Auth null");
            else sb.Append("&").Append("users[0][auth]").Append("=").Append(user.auth);
            if (user.password == null) throw new Exception("Password null");
            else sb.Append("&").Append("users[0][password]").Append("=").Append(user.password);*/
            if (user.idnumber == null) throw new Exception("idnumber null");
            else sb.Append("&").Append("users[0][idnumber]").Append("=").Append(user.idnumber);

            string postData = sb.ToString();

            response = CallMoodleFunction("core_user_update_users", postData);

            return response;
        }


        public dynamic CreateCourse(dynamic course)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (course == null) throw new Exception("Curso null");
            if (course.fullname == null) throw new Exception("fullname null");
                else sb.Append("&").Append("courses[0][fullname]").Append("=").Append(course.fullname);
            if (course.shortname == null) throw new Exception("shortname null");
                else sb.Append("&").Append("courses[0][shortname]").Append("=").Append(course.shortname);
            if (course.categoryid == null) throw new Exception("categoryid null");
                else sb.Append("&").Append("courses[0][categoryid]").Append("=").Append(course.categoryid);
            if (course.summary == null) throw new Exception("summary null");
                else sb.Append("&").Append("courses[0][summary]").Append("=").Append(course.summary);
            if (course.format == null) throw new Exception("format null");
                else sb.Append("&").Append("courses[0][format]").Append("=").Append(course.format);
            if (course.idnumber == null) throw new Exception("idnumber null");
                else sb.Append("&").Append("courses[0][idnumber]").Append("=").Append(course.idnumber);
            if (course.startdate == null) throw new Exception("startdate null");
                else sb.Append("&").Append("courses[0][startdate]").Append("=").Append(course.startdate);
            if (course.numsections == null) throw new Exception("numsections null");
                else
                {
                    sb.Append("&").Append("courses[0][courseformatoptions][0][name]").Append("=").Append("numsections");
                    sb.Append("&").Append("courses[0][courseformatoptions][0][value]").Append("=").Append(course.numsections);
                }
            if (course.coursedisplay == null) throw new Exception("coursedisplay null");
                else
                {
                    sb.Append("&").Append("courses[0][courseformatoptions][1][name]").Append("=").Append("coursedisplay");
                    sb.Append("&").Append("courses[0][courseformatoptions][1][value]").Append("=").Append(course.coursedisplay);
                }

            string postData = sb.ToString();

            response = CallMoodleFunction("core_course_create_courses", postData);

            return response;
        }

        public dynamic CreateGroup(dynamic group)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (group == null) throw new Exception("Grupo null");
            if (group.courseid == null) throw new Exception("courseid null");
                else sb.Append("&").Append("groups[0][courseid]").Append("=").Append(group.courseid);
            if (group.name == null) throw new Exception("name null");
                else sb.Append("&").Append("groups[0][name]").Append("=").Append(group.name);
            if (group.description == null) throw new Exception("description null");
                else sb.Append("&").Append("groups[0][description]").Append("=").Append(group.description);
            //if (group.idnumber == null) throw new Exception("idnumber null");
            //    else sb.Append("&").Append("groups[0][idnumber]").Append("=").Append(group.idnumber);

            string postData = sb.ToString();

            response = CallMoodleFunction("core_group_create_groups", postData);

            return response;
        }

        public dynamic EnrolUser(dynamic enrolment)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (enrolment == null) throw new Exception("Matriculacion null");
            if (enrolment.roleid == null) throw new Exception("roleid null");
                else sb.Append("&").Append("enrolments[0][roleid]").Append("=").Append(enrolment.roleid);
            if (enrolment.userid == null) throw new Exception("userid null");
                else sb.Append("&").Append("enrolments[0][userid]").Append("=").Append(enrolment.userid);
            if (enrolment.courseid == null) throw new Exception("courseid null");
                else sb.Append("&").Append("enrolments[0][courseid]").Append("=").Append(enrolment.courseid);

            string postData = sb.ToString();

            response = CallMoodleFunction("enrol_manual_enrol_users", postData);

            return response;
        }

        public dynamic AddGroupMember(dynamic groupMember)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (groupMember == null) throw new Exception("groupMember null");
            if (groupMember.groupid == null) throw new Exception("groupid null");
                else sb.Append("&").Append("members[0][groupid]").Append("=").Append(groupMember.groupid);
            if (groupMember.userid == null) throw new Exception("userid null");
                else sb.Append("&").Append("members[0][userid]").Append("=").Append(groupMember.userid);

            string postData = sb.ToString();

            response = CallMoodleFunction("core_group_add_group_members", postData);

            return response;
        }

        public dynamic DeleteGroupMember(dynamic groupMember)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (groupMember == null) throw new Exception("groupMember null");
            if (groupMember.groupid == null) throw new Exception("groupid null");
            else sb.Append("&").Append("members[0][groupid]").Append("=").Append(groupMember.groupid);
            if (groupMember.userid == null) throw new Exception("userid null");
            else sb.Append("&").Append("members[0][userid]").Append("=").Append(groupMember.userid);

            string postData = sb.ToString();

            response = CallMoodleFunction("core_group_delete_group_members", postData);

            return response;
        }

        public dynamic UnenrolUser(dynamic unenrolment)
        {
            dynamic response;

            StringBuilder sb = new StringBuilder();

            if (unenrolment == null) throw new Exception("Desmatriculacion null");
            if (unenrolment.userid == null) throw new Exception("userid null");
                else sb.Append("&").Append("enrolments[0][userid]").Append("=").Append(unenrolment.userid);
            if (unenrolment.courseid == null) throw new Exception("courseid null");
                else sb.Append("&").Append("enrolments[0][courseid]").Append("=").Append(unenrolment.courseid);

            string postData = sb.ToString();

            response = CallMoodleFunction("enrol_manual_unenrol_users", postData);

            return response;
        }

        public dynamic GetCategoryByCriteria(string key, string value)
        {
            dynamic response;

            string postData = string.Format("&criteria[0][key]={0}&criteria[0][value]={1}", key, value);

            response = CallMoodleFunction("core_course_get_categories", postData);

            return response;
        }

        public dynamic GetCourseByName(string courseName)
        {
            dynamic response;

            string postData = string.Format("&criterianame=search&criteriavalue={0}", courseName);

            response = CallMoodleFunction("core_course_search_courses", postData);

            return response;
        }
    }
}
