<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ MasterType virtualpath="Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="sectionTitle">
    <asp:Label runat="server" Text="Log In" ID="lblPleaseLogin"></asp:Label>	
</div>
    <p>
        Please enter your username and password. 
        <asp:LinkButton ID="lnkRegister" runat="server" onclick="lnkRegister_Click">Register</asp:LinkButton>&nbsp;if you don't have an account.
    </p>
    


                        <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="LoginUserValidationGroup"/>

    <div class="accountInfo">
    
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName">User Name:</asp:Label>
                                <asp:TextBox ID="txtUserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtUserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword">Password:</asp:Label>
                                <asp:TextBox ID="txtPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvfPasswordRequired" runat="server" ControlToValidate="txtPassword" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>

                        </fieldset>
                        
                        


                        
                        
                        <p class="submitButton">
                            <asp:Button ID="btnLogin" runat="server" CommandName="Log in" Text="Log In" 
                                 ValidationGroup="LoginUserValidationGroup" onclick="btnLogin_Click"/>
                            </p>

                    </div>







</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

