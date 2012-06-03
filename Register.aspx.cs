using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Session["UsersID"] == null)
            {
                Response.Redirect("Default.aspx");
                ViewState["CreateChangeAccount"] = 0;
            }
            else if (Convert.ToInt32(Session["UsersID"]) > 0)
            {
                //Session["UsersID"] = 0;
                lblCreateChangeDeleteAccount.Text = "Change Account";
                btnCreateChangeAccount.Text = "Change";
                txtUserName.Text = DataBaseOps.ReadUserData("UserName");
                ViewState["CreateChangeAccount"] = 2;
            }
            else
            {
                lblCreateChangeDeleteAccount.Text = "Create a New Account";
                btnCreateChangeAccount.Text = "Create";
                ViewState["CreateChangeAccount"] = 1;
            }
        }

    }


    protected void btnCreateChangeAccount_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtPassword.Text == txtConfirmPassword.Text && txtPassword.Text != "") // Da li su passwordi isti
            {
                if (Convert.ToInt32(ViewState["CreateChangeAccount"]) == 1)
                {
                   DataBaseOps.CreateAccount(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text);
                }
                else if (Convert.ToInt32(ViewState["CreateChangeAccount"]) == 2)
                {
                   DataBaseOps.ChangeAccount(txtUserName.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPassword.Text, Convert.ToInt32(Session["UsersID"]));
                }
                bool login;
                login = Convert.ToBoolean(DataBaseOps.LoginUser(txtUserName.Text, txtPassword.Text));
                if (login == true)
                {
                    Response.Redirect("~/Default.aspx");
                }


            }
            else
            {
                throw new Exception(string.Format("Passwords are not equal {0}!", this.GetType()));
            }



        }
        catch (Exception ex)
        {
            // Prikaz pogreške
            RequiredFieldValidator req = new RequiredFieldValidator();
            req.ValidationGroup = "RegisterUserValidationGroup";
            req.ErrorMessage = ex.Message;
            req.IsValid = false;
            Page.Form.Controls.Add(req);
            req.Visible = false;

        }

    }





   

   


   
}