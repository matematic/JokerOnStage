using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Jokes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Session["UsersID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (Convert.ToInt32(Session["SelectedUserID"]) > 0)
                {
                    lblJokes.Text = DataBaseOps.SelectedCategory(Convert.ToInt32(Session["Categories"]));

                    gvJokes.DataSource = DataBaseOps.JokesList(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["Categories"]));
                    gvJokes.DataBind();
                }

            }
        }


    }
    protected void gvJokes_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void gvJokes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "imgJokeDelete")
        {
            DataBaseOps.JokeDelete(Convert.ToInt32(e.CommandArgument));
            Response.Redirect("~/Default.aspx");
            //gvJokes.DataSource = DataBaseOps.JokesList(Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["Categories"]));
            //gvJokes.DataBind();

        }
    }
    protected void gvJokes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton imgJokeDelete = (ImageButton)e.Row.FindControl("imgJokeDelete");
            if (Convert.ToInt32(Session["UsersID"]) == Convert.ToInt32(Session["SelectedUserID"]))
            {
                imgJokeDelete.OnClientClick = String.Format("return confirm('Delete joke?');");
                imgJokeDelete.Visible = true;
            }
            else
            {
                imgJokeDelete.Visible = false;
            }

        }
    }

    protected void gvJokes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvJokes.PageIndex = e.NewPageIndex;
        gvJokes.DataSource = DataBaseOps.JokesList(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["Categories"]));
        gvJokes.DataBind();
    }





    

}