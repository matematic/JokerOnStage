<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="Jokes.aspx.cs" Inherits="Jokes" %>
<%@ MasterType virtualpath="Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        <div class="sectionTitle">
    <asp:Label runat="server" Text="Jokes" ID="lblJokes"></asp:Label>	
</div>

            <asp:GridView ID="gvJokes" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CssClass="gvJokes" EnableModelValidation="True" 
                onrowcommand="gvJokes_RowCommand" onrowdatabound="gvJokes_RowDataBound" 
                onselectedindexchanged="gvJokes_SelectedIndexChanged" ShowHeader="False" 
                onpageindexchanging="gvJokes_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtJoke" runat="server" Text='<%# Eval("Joke") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                        <table id="tblJoke">
                        <tr>
                        <td id="tblJokeLeftTd">
                        <asp:Label ID="lblJoke" runat="server" Text='<%# Eval("Joke").ToString().Replace(Environment.NewLine,"<br />") %>'></asp:Label>
                        </td>
                        <td id="tblJokeRightTd">
                        <asp:ImageButton ID="imgJokeDelete" runat="server" 
                                CommandArgument='<%# Bind("JokesID") %>' CommandName="imgJokeDelete" 
                                ImageUrl="~/Images/Del.png" />
                        </td>
                        </tr>
                        </table>
                            
                            <hr />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>


 <br />
    
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

