<%@ Page Title="" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true" CodeFile="NewJoke.aspx.cs" Inherits="NewJoke" %>
<%@ MasterType virtualpath="Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


 <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="NewJokeValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="NewJokeValidationGroup"/>
                    <asp:Panel ID="panNewJoke" runat="server">
    
                    
                      <div class="sectionTitle">
                        
                            New Joke
                            </div>
                            
                            <p>
                                <asp:Label ID="lblJoke" runat="server" AssociatedControlID="txtJoke">Joke:</asp:Label><br />
                                <asp:TextBox ID="txtJoke" runat="server" CssClass="textarea" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="JokeRequired" runat="server" ControlToValidate="txtJoke" 
                                     CssClass="failureNotification" ErrorMessage="No Joke!" ToolTip="Joke is required." 
                                     ValidationGroup="NewJokeValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            

                                <p>
                                <asp:Label ID="lblCategories" runat="server" AssociatedControlID="ddlCategories">Category:</asp:Label><br />

    <asp:DropDownList ID="ddlCategories" runat="server" DataSourceID="adsCategories" DataTextField="CatNames" 
                                DataValueField="CategoriesID" >
    </asp:DropDownList>
                            </p>



                                <p>
                                <asp:Label ID="lblVisibility" runat="server" AssociatedControlID="rblVisibility">Visibility:</asp:Label>
<asp:RadioButtonList ID="rblVisibility" runat="server" Width="302px" RepeatDirection ="Horizontal">
                                <asp:ListItem Value="0">Visible to my friends</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Visible to everyone</asp:ListItem>
                            </asp:RadioButtonList>
                            
                            </p>




<asp:AccessDataSource ID="adsCategories" runat="server" 
        DataFile="App_Data/JokerOnStage.mdb" 
        SelectCommand="SELECT [CategoriesID], [CatNames], [CatSort], [CatActive] FROM [Categories] WHERE ([CatActive] = True) ORDER BY [CatNames]">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="True" Name="CatActive" Type="Boolean" />
                    </SelectParameters>
    </asp:AccessDataSource>

                        
                        <p class="submitButton">
                            <asp:Button ID="btnInsertJoke" runat="server" CommandName="MoveNext" Text="Submit" 
                                 ValidationGroup="NewJokeValidationSummary" 
                                style="height: 26px" onclick="btnInsertJoke_Click"/>
                        </p>
                    
    
    				</asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

