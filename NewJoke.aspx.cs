using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OleDb;

public partial class NewJoke : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UsersID"] == null || Convert.ToInt32(Session["UsersID"]) == 0)
        {
            if (Page.IsPostBack == false)
            {
                if (Session["UsersID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
            }

            Response.Redirect("Default.aspx");

            //// Prikaz pogreške
            //RequiredFieldValidator req = new RequiredFieldValidator();
            //req.ValidationGroup = "NewJokeValidationGroup";
            //req.ErrorMessage = "Only for registered users.";
            //req.IsValid = false;
            //Page.Form.Controls.Add(req);
            //req.Visible = false;
            //panNewJoke.Enabled = false;
        }
        else
        {
            panNewJoke.Enabled = true;
        }
    }


    protected void btnInsertJoke_Click(object sender, EventArgs e)
    {

        try
        {
           InsertJoke( txtJoke.Text, Convert.ToInt32( ddlCategories.SelectedValue),  Convert.ToInt32(rblVisibility.SelectedValue));
           Session["SelectedUserID"] = Session["UsersID"];
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
            // Prikaz pogreške
            RequiredFieldValidator req = new RequiredFieldValidator();
            req.ValidationGroup = "NewJokeValidationGroup";
            req.ErrorMessage = ex.Message;
            req.IsValid = false;
            Page.Form.Controls.Add(req);
            req.Visible = false;
            panNewJoke.Enabled = false;
        }

    }

    public static void InsertJoke(string txtJokeText, int ddlCategoriesSelectedValue, int rblVisibilitySelectedValue)
    {

        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);

        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "INSERT INTO Jokes (UsersID, Joke, CategoriesID, Visibility, DateStamp, RemoteAddr, LogonUser) VALUES (@UsersID, @Joke, @CategoriesID, @Visibility, @DateStamp, @RemoteAddr, @LogonUser)";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmJoke = myCommand.Parameters.Add("@prmJoke", OleDbType.LongVarChar);
        OleDbParameter prmCategoriesID = myCommand.Parameters.Add("@CategoriesID", OleDbType.Integer);
        OleDbParameter prmVisibility = myCommand.Parameters.Add("@Visibility", OleDbType.Integer);
        OleDbParameter prmDateStamp = myCommand.Parameters.Add("@DateStamp", OleDbType.VarChar);
        OleDbParameter prmRemoteAddr = myCommand.Parameters.Add("@RemoteAddr", OleDbType.VarChar);
        OleDbParameter prmLogonUser = myCommand.Parameters.Add("@LogonUser", OleDbType.VarChar);

        prmUsersID.Value = HttpContext.Current.Session["UsersID"];
        prmJoke.Value = txtJokeText;
        prmCategoriesID.Value = ddlCategoriesSelectedValue;
        prmVisibility.Value = rblVisibilitySelectedValue;
        prmDateStamp.Value = DateTime.Now;
        prmRemoteAddr.Value = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        prmLogonUser.Value = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();


    }
}