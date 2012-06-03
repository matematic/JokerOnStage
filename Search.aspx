<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<%@ MasterType virtualpath="Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <div class="sectionTitle">
    Search	
</div>

     <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="SearchValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="SearchValidationGroup"/>

    <div class="wideContainer">
    
                        <fieldset class="register">
                            <legend>Account Information</legend>

                                <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch">Search:</asp:Label>
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="SearchRequired" runat="server" ControlToValidate="txtSearch" 
                                     CssClass="failureNotification" ErrorMessage="Required." ToolTip="Required." 
                                     ValidationGroup="SearchValidationGroup">*</asp:RequiredFieldValidator>
                   
                            <asp:Button ID="btnSearch" runat="server" CommandName="Search" Text="Search" 
                                 ValidationGroup="SearchValidationGroup" onclick="btnSearch_Click" />
    
       <div ID="divSearch">
             <asp:Repeater ID="rptSearch" Runat="server" 
                 onitemcommand="rptSearch_ItemCommand" >
                <ItemTemplate>
                    <li>
                      <asp:LinkButton ID="lnkSearch" runat="server" CommandArgument='<%# Bind("UsersID") %>' Text='<%# Bind("FirstNameLastName") %>'></asp:LinkButton>
                    </li>
                </ItemTemplate>
            </asp:Repeater>      
        </div>
              

                        </fieldset>
                        
                        



                        
                        


                    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

