using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Page.IsPostBack == false)
        {

            ScriptManager1.RegisterAsyncPostBackControl(rptCategories);

            if (Convert.ToInt32(Session["UsersID"]) < 1)
            {
                lnkLogin.Text = "Login";
                lblUserName.Text = null;
                Session["UsersID"] = 0;
                lnkFriendshipRequest.Visible = false;
                lnkNewJoke.Visible = false;
                lnkSearchForFriends.Visible = false;
                lblFirstNameLastName.Text = null;
                lnkAcceptFriendship.Visible = false;
                lnkDenyFriendship.Visible = false;
                lnkStopFriendship.Visible = false;
                lnkLoginFromMainMenu.Visible = true;
                rptCategories.DataSource =DataBaseOps.CategoriesList(0, 0);
                rptCategories.DataBind();
                rptFriendshipRequests.DataSource = DataBaseOps.FriendshipRequestsList(0);
                rptFriendshipRequests.DataBind();
                rptFriends.DataSource = DataBaseOps.FriendsList(0);
                rptFriends.DataBind();

            }
            else
            {
                lnkLogin.Text = "Logout";
                lblUserName.Text = "Welcome ";
                lnkUserName.Text = Convert.ToString(Session["FirstNameLastName"]);
                rptCategories.DataSource = DataBaseOps.CategoriesList(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]));
                lblFirstNameLastName.Text = DataBaseOps.FirstNameLastName(Convert.ToInt32(Session["SelectedUserID"]));
                rptCategories.DataBind();
                rptFriendshipRequests.DataSource = DataBaseOps.FriendshipRequestsList(Convert.ToInt32(Session["UsersID"]));
                rptFriendshipRequests.DataBind();
                rptFriends.DataSource = DataBaseOps.FriendsList(Convert.ToInt32(Session["UsersID"]));
                rptFriends.DataBind();
                lnkNewJoke.Visible = true;
                lnkSearchForFriends.Visible = true;
                lnkLoginFromMainMenu.Visible = false;

                int UsersID = Convert.ToInt32(Session["UsersID"]);
                int SelectedUserID = Convert.ToInt32(Session["SelectedUserID"]);

                if (UsersID == SelectedUserID)
                {
                    lnkFriendshipRequest.Visible = false;
                    lnkAcceptFriendship.Visible = false;
                    lnkDenyFriendship.Visible = false;
                    lnkStopFriendship.Visible = false;
                }
                else
                {
                    if (DataBaseOps.FriedshipCheck(UsersID, SelectedUserID, false, 0) > 0)
                    {
                        lnkFriendshipRequest.Visible = false;
                        lnkAcceptFriendship.Visible = true;
                        lnkDenyFriendship.Visible = true;
                        lnkStopFriendship.Visible = false;
                        lnkAcceptFriendship.OnClientClick = String.Format("return confirm('Confirm friendship?');");
                        lnkDenyFriendship.OnClientClick = String.Format("return confirm('Deny friendship?');");
                    }
                    else if (DataBaseOps.FriedshipCheck(UsersID, SelectedUserID, true, 0) > 0)
                    {
                        lnkFriendshipRequest.Visible = false;
                        lnkAcceptFriendship.Visible = false;
                        lnkDenyFriendship.Visible = false;
                        lnkStopFriendship.Visible = true;
                        lnkStopFriendship.OnClientClick = String.Format("return confirm('End friendship?');");
                    }

                    else
                    {
                        lnkFriendshipRequest.Visible = true;
                        lnkAcceptFriendship.Visible = false;
                        lnkDenyFriendship.Visible = false;
                        lnkStopFriendship.Visible = false;
                        lnkFriendshipRequest.OnClientClick = String.Format("return confirm('Ask for friendship?');");
                    }



                }

            }

        }


        //if ((int)Session["UsersID"] == 0)
        //{
        //    hypLogin.Text = "Login";
        //}

    }




    // Public property that will be used to manipulate control on Master page
    public LinkButton lnkLoginMaster
    {
        get
        {
            // Get value of control on master page
            return lnkLogin;
        }
        set
        {
            // Set new value for control on master page
            lnkLogin = value;
        }
    }


    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        if (lnkLogin.Text == "Login")
        {
            Response.Redirect("~/Login.aspx");
        }
        if (lnkLogin.Text == "Logout")
        {
            Session["UsersID"] = null;
            lnkLogin.Text = "Login";
            Response.Redirect("~/Default.aspx");
        }

    }

    protected void lnkHome_Click(object sender, EventArgs e)
    {

        Session["SelectedUserID"] = Session["UsersID"];
        Response.Redirect("~/Default.aspx");
    }

    protected void lnkFriendshipRequest_Click(object sender, EventArgs e)
    {
        DataBaseOps.FriendshipRequest(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]), false, 0);
        DataBaseOps.FriendshipRequest(Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["UsersID"]), false, 1);
        Session["SelectedUserID"] = Session["UsersID"];
        Response.Redirect("~/Default.aspx");
    }
    protected void rptFriendshipRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Session["SelectedUserID"] = e.CommandArgument;
        Response.Redirect("~/Default.aspx");

    }
   
    protected void rptFriends_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "lnkFriendFirstNameLastName")
        {
            Session["SelectedUserID"] = e.CommandArgument;
            Response.Redirect("~/Default.aspx");
        }

    }

    //protected void rptFriends_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{

    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        ImageButton imgFriendshipDelete = (ImageButton)e.Item.FindControl("imgFriendshipDelete");
    //        imgFriendshipDelete.OnClientClick = String.Format("return confirm('Cancel friendship?');");
    //    }

    //}

    //protected void rptFriendshipRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        ImageButton imgFriendshipRequestsYes = (ImageButton)e.Item.FindControl("imgFriendshipRequestsYes");
    //        imgFriendshipRequestsYes.OnClientClick = String.Format("return confirm('Accept friendship?');");

    //        ImageButton imgFriendshipRequestsNo = (ImageButton)e.Item.FindControl("imgFriendshipRequestsNo");
    //        imgFriendshipRequestsNo.OnClientClick = String.Format("return confirm('Deny friendship?');");
    //    }
    //}

    protected void rptCategories_ItemCommand(object source, RepeaterCommandEventArgs e)
    {


 
        Session["Categories"] = e.CommandArgument;
        if (Request.ServerVariables["SCRIPT_NAME"].ToString() == "/JokerOnStage/Joker.aspx")
        {


            ((UpdatePanel)MainContent.FindControl("UpdatePanel1")).Update();
            GridView gvJokes = ((GridView)MainContent.FindControl("gvJokes"));





        }
        else
        {
            Response.Redirect("~/Jokes.aspx");
        }

    }
    protected void lnkNewJoke_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/NewJoke.aspx");
    }
    protected void lnkSearchForFriends_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx");
    }

    protected void lnkAcceptFriendship_Click(object sender, EventArgs e)
    {
        DataBaseOps.FriendshipAccept(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]));
        DataBaseOps.FriendshipAccept(Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["UsersID"]));
        Session["SelectedUserID"] = Session["UsersID"];
        Response.Redirect("~/Default.aspx");
    }

    protected void lnkDenyFriendship_Click(object sender, EventArgs e)
    {
        DataBaseOps.FriendshipDenyDelete(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]));
        DataBaseOps.FriendshipDenyDelete(Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["UsersID"]));
        Session["SelectedUserID"] = Session["UsersID"];
        Response.Redirect("~/Default.aspx");
    }
    protected void lnkStopFriendship_Click(object sender, EventArgs e)
    {
        DataBaseOps.FriendshipDenyDelete(Convert.ToInt32(Session["UsersID"]), Convert.ToInt32(Session["SelectedUserID"]));
        DataBaseOps.FriendshipDenyDelete(Convert.ToInt32(Session["SelectedUserID"]), Convert.ToInt32(Session["UsersID"]));
        Session["SelectedUserID"] = Session["UsersID"];
        Response.Redirect("~/Default.aspx");
    }



    



    




   
    protected void lnkLoginFromMainMenu_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }
    protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptCategories.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }
    protected void rptFriendshipRequests_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptFriendshipRequests.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }
    protected void rptFriends_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptFriends.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }
    protected void lnkUserName_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Register.aspx");
    }
}
