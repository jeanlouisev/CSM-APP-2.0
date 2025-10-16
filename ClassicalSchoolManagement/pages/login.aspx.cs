using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using Telerik.Web.UI;
using Telerik.Web;
using Utilities;


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //for telerik activation purpose
        HttpContext.Current.Items["RadControlRandomNumber"] = 0;

        // verify mac adress
        if (!Universal.MACAddressCompatible())
        {
            Response.Redirect("~/WrongServerError.aspx");
        }
        //
        if (!IsPostBack)
        {
            string loginStatus = Request.QueryString["action"];
            txtUserName.Focus();
            if (loginStatus == "logout")
            {
                Users user = Session["user"] as Users;
                // write logout logs
                Users userLog = new Users();
                userLog.staff_code = user.staff_code;
                userLog.url_path = HttpContext.Current.Request.Url.AbsolutePath;
                userLog.log_status = "logout";
                Users.writeUserLogs(userLog);

                // clear login session
                Session.Remove("user");
                Session["user"] = null;
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Value.Trim().Length <= 0 ? null : txtUserName.Value.Trim().ToUpper();
        string password = txtPassword.Value.Trim().Length <= 0 ? null : txtPassword.Value.Trim();

        if (userName == null || password == null)
        {
            MessBox.Show("Nom utilisateur ou mot-de-passe invalide !");
        }
        else
        {
            // get list user info
            List<Users> listUsers = Users.getListUsersByPseudo(userName);
            if (listUsers == null || listUsers.Count <= 0)
            {
                MessBox.Show("Nom utilisateur ou mot-de-passe invalide !");
            }
            else
            {
                // check if password is compatible
                if (listUsers[0].password == Hash.EncodePasswordSH1(password))
                {
                    // check if account is unlocked
                    if (listUsers[0].locked == 0)
                    {
                        // check if account is expired
                        if (listUsers[0].expiry_date > DateTime.Now)
                        {
                            Users newUserInfo = new Users();
                            foreach (Users user in listUsers)
                            {
                                newUserInfo.id = user.id;
                                newUserInfo.username = user.username;
                                newUserInfo.password = user.password;
                                newUserInfo.role_id = user.role_id;
                                //newUserInfo.requester_code = user.requester_code;
                                //newUserInfo.staff_code = user.staff_code;
                                newUserInfo.transaction_date = user.transaction_date;
                                newUserInfo.locked = user.locked;
                                newUserInfo.locked_time = user.locked_time;
                                newUserInfo.log_error = user.log_error;
                                newUserInfo.expiry_date = user.expiry_date;
                                newUserInfo.fullname = user.fullname;
                                //newUserInfo.login_user = user.login_user;
                                newUserInfo.group_name = user.group_name;
                                newUserInfo.phone = user.phone;
                                newUserInfo.image_path = user.image_path;
                            }
                            // unlock user account
                            Users.unlockUserAccount(newUserInfo.username);
                            Session["user"] = newUserInfo;
                            //
                            // write login logs
                            Users userLog = new Users();
                            userLog.id = newUserInfo.id;
                            userLog.staff_code = newUserInfo.username;
                            userLog.url_path = HttpContext.Current.Request.Url.AbsolutePath;
                            userLog.log_status = "login";
                            Users.writeUserLogs(userLog);

                            Response.Redirect("~/Pages/Home.aspx");

                            //Response.Redirect("~/Home.aspx?menu=Acceuil");
                        }
                        else
                        {
                            //MessageAlert.RadAlert("Desolé, votre Compte a expiré !", 350, 150, "Error", null, "/images/warning.png");
                            MessBox.Show("Desolé, votre Compte a expiré !");
                        }
                    }
                    else
                    {
                        //MessageAlert.RadAlert("Desolé, votre Compte est bloqué !", 350, 150, "Error", null, "/images/warning.png");
                        MessBox.Show("Desolé, votre Compte est bloqué !");
                    }
                }
                else
                {
                    // get current log_error
                    int logErrorCounter = listUsers[0].log_error;
                    // get how many trials are left before acccount get locked
                    int trialCounter = 4 - logErrorCounter;
                    // increase log_error anytime user enter wrong password
                    Users.increaseLogError(logErrorCounter + 1, userName);

                    List<Users> listUsers2 = Users.getListUsersByPseudo(userName);
                    // check if user is unlocked
                    if (listUsers2[0].locked == 0)
                    {
                        //MessageAlert.RadAlert("Votre compte sera bloqué après (" + trialCounter + ") essai !", 350, 150, "Error", null, "/images/warning.png");
                        MessBox.Show("Votre compte sera bloqué après (" + trialCounter + ") essai !");
                    }
                    else
                    {
                        //MessageAlert.RadAlert("Desolé, votre Compte est bloqué !", 350, 150, "Error", null, "/images/warning.png");
                        MessBox.Show("Desolé, votre Compte est bloqué !");
                    }
                }
            }
        }
    }

}