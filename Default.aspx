<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType virtualpath="Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="sectionTitle">
    <asp:Label runat="server" Text="" ID="lblPleaseLoginNewjoke"></asp:Label>	
</div>
<asp:GridView ID="gvNewJokes" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CssClass="gvJokes" EnableModelValidation="True" 
                onrowcommand="gvNewJokes_RowCommand" onrowdatabound="gvNewJokes_RowDataBound" 
                onselectedindexchanged="gvNewJokes_SelectedIndexChanged" ShowHeader="False" 
                onpageindexchanging="gvNewJokes_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJoke" runat="server" Text='<%# Eval("Joke") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                        <table id="tblJoke">
                        <tr>
                        <td id="tblJokeLeftTd" colspan="2">
                        <asp:Label ID="lblJoke" runat="server" Text='<%# Eval("Joke").ToString().Replace(Environment.NewLine,"<br />") %>'></asp:Label>
                        </td>
                        </tr>
                        
                        <tr>
                        <td width="50%">
                            <asp:LinkButton ID="lnkSelectedUser" CommandName="lnkSelectedUser" runat="server" CommandArgument='<%# Eval("UsersID") %>'><%# Eval("FirstNameLastName")%></asp:LinkButton>  

                        </td>
                        <td width="50%">
                            <asp:Label ID="lnkDateStamp" runat="server"><%# Eval("DateStamp") %></asp:Label>
                         </td>
                        </tr>

                        </table>
                            
                            <hr />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>



    
 
</asp:Content>

