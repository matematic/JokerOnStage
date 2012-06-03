using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Convert.ToInt32(Session["UsersID"]) < 1)
        {
            lblPleaseLoginNewjoke.Text = "Please login";
            gvNewJokes.DataSource = DataBaseOps.NewJokesList(0);
            gvNewJokes.DataBind();
        }
        else
        {
            lblPleaseLoginNewjoke.Text = "New jokes";
            gvNewJokes.DataSource = DataBaseOps.NewJokesList(Convert.ToInt32(Session["UsersID"]));
            gvNewJokes.DataBind();
        }
    }

   
    protected void gvNewJokes_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvNewJokes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNewJokes.PageIndex = e.NewPageIndex;
        gvNewJokes.DataSource = DataBaseOps.NewJokesList(Convert.ToInt32(Session["UsersID"]));
        gvNewJokes.DataBind();
    }
    protected void gvNewJokes_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvNewJokes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkSelectedUser")
        {
            Session["SelectedUserID"] = e.CommandArgument;
            Response.Redirect("~/Default.aspx");
        }
    }
}