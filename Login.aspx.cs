using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx";
 


        if (Page.IsPostBack == false)
        {
            if (Session["UsersID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else 
            {
                Session["UsersID"] = 0;
            }
        }




    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool login;
        login = Convert.ToBoolean(DataBaseOps.LoginUser(txtUserName.Text, txtPassword.Text));
        //LoginUser(txtUserName.Text, txtPassword.Text);
        if (login == true)
        {
            Session["SelectedUserID"] = Session["UsersID"];
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            // Prikaz pogreške
            RequiredFieldValidator req = new RequiredFieldValidator();
            req.ValidationGroup = "LoginUserValidationGroup";
            req.ErrorMessage = "Failure!";
            req.IsValid = false;
            Page.Form.Controls.Add(req);
            req.Visible = false;
        }
    }

    
   
    protected void lnkRegister_Click(object sender, EventArgs e)
    {
        //Session["UsersID"] = null;
        Response.Redirect("~/Register.aspx");
    }
}