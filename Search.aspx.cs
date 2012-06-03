using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Search : System.Web.UI.Page
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


        }


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        rptSearch.DataSource = DataBaseOps.SearchForFriends(txtSearch.Text);
        rptSearch.DataBind();
        
    }


   

    protected void rptSearch_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        LinkButton lnkSearch = (LinkButton)rptSearch.Items[0].FindControl("lnkSearch");
        Session["SelectedUserID"] = lnkSearch.CommandArgument;
        Response.Redirect("~/Default.aspx");
    }


}