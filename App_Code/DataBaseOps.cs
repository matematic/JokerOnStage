using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.OleDb;

using System.Web.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for DataBaseOps
/// </summary>
public class DataBaseOps
{
    public DataBaseOps()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable NewJokesList(int UsersID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;

        myCommand.CommandText = "SELECT TOP 50 Jokes.UsersID, Jokes.Joke, Jokes.DateStamp, FirstName + ' ' + LastName AS FirstNameLastName FROM (Friendships INNER JOIN Jokes ON Friendships.SelectedUserID = Jokes.UsersID) INNER JOIN Users ON Jokes.UsersID = Users.UsersID WHERE (((Friendships.UsersID)=[@UsersID]) AND ((Friendships.Accepted)=True));";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        prmUsersID.Value = UsersID;


        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();

        return myDataTable;

    }




    public static DataTable JokesList(int LogonUserID, int SelectedUserID, int CategoriesID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;

        if (LogonUserID == SelectedUserID || IsThereAcceptedFriend(LogonUserID, SelectedUserID) == true || IsThereAcceptedFriend(SelectedUserID, LogonUserID) == true)
        {
            myCommand.CommandText = "SELECT * FROM Jokes WHERE UsersID=@SelectedUserID AND CategoriesID=@CategoriesID;";
        }
        else
        {
            myCommand.CommandText = "SELECT * FROM Jokes WHERE UsersID=@SelectedUserID AND CategoriesID=@CategoriesID AND Visibility=True;";
        }


        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        OleDbParameter prmCategoriesID = myCommand.Parameters.Add("@CategoriesID", OleDbType.Integer);
        prmSelectedUserID.Value = SelectedUserID;
        prmCategoriesID.Value = CategoriesID;

        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();

        return myDataTable;

    }

    public static void JokeDelete(int JokesID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);

        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "DELETE * FROM Jokes WHERE JokesID=@JokesID";
        OleDbParameter prmJokesID = myCommand.Parameters.Add("@JokesID", OleDbType.Integer);
        prmJokesID.Value = JokesID;
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }

    public static bool IsThereAcceptedFriend(int LogonUserID, int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT Accepted FROM Friendships WHERE UsersID=[@LogonUserID] AND SelectedUserID=[@SelectedUserID];";
        OleDbParameter prmLogonUserID = myCommand.Parameters.Add("@LogonUserID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmLogonUserID.Value = LogonUserID;
        prmSelectedUserID.Value = SelectedUserID;
        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        bool TherIsAFriend = Convert.ToBoolean(myCommand.ExecuteScalar());
        myConnection.Close();
        myConnection.Dispose();
        return TherIsAFriend;
    }



    public static string SelectedCategory(int CategoryID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT CatNames FROM Categories WHERE CategoriesID=[@CategoriesID];";
        OleDbParameter prmCategoriesID = myCommand.Parameters.Add("@CategoriesID", OleDbType.Integer);
        prmCategoriesID.Value = CategoryID;
        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        string CatNames = Convert.ToString(myCommand.ExecuteScalar());
        myConnection.Close();
        myConnection.Dispose();
        return CatNames;
    }

    public static bool LoginUser(string UserName, String Password)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT * FROM Users WHERE UserName=@UserName";
        OleDbParameter prmUserName = myCommand.Parameters.Add("@UserName", OleDbType.VarChar);
        prmUserName.Value = UserName;

        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();
        if (myDataTable.Rows.Count == 1)
        {
            string Salt = myDataTable.Rows[0]["Salt"].ToString();
            string PassHash = Convert.ToString(SHA256(Password));
            string PassHashSalt = Convert.ToString(SHA256(Salt + PassHash));
            if (PassHashSalt == myDataTable.Rows[0]["PassHashSalt"].ToString())
            {
                HttpContext.Current.Session["UsersID"] = myDataTable.Rows[0]["UsersID"].ToString();
                HttpContext.Current.Session["Rights"] = Convert.ToInt32(myDataTable.Rows[0]["Rights"]);
                HttpContext.Current.Session["UserName"] = Convert.ToString(myDataTable.Rows[0]["UserName"]);
                HttpContext.Current.Session["FirstNameLastName"] = Convert.ToString(myDataTable.Rows[0]["FirstName"]) + " " + Convert.ToString(myDataTable.Rows[0]["LastName"]);
                if (Convert.ToInt32(HttpContext.Current.Session["Rights"]) == 3)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public static string Echo(string s)
    {
        return s;
    }

    public static int LosaHashFunkcija(string s)
    {
        int suma = 0;
        foreach (Char c in s)
            suma += c;
        return suma;
    }

    public static int FNV_PIN(string s)
    {
        int hash = 123; // FNV_offset_basis
        const int FNV_prime = 13;

        foreach (Char c in s)
        {
            hash *= FNV_prime;
            hash ^= c;
        }
        return hash;
    }

    public static string SHA256(string input)
    {
        SHA256 sha256 = new SHA256Managed();

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = sha256.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
            sb.Append(hash[i].ToString("X2"));
        return sb.ToString();
    }


    public static void CreateAccount(string UserName, string FirstName, string LastName, string Email, string Password)
    {

        //string myConnectionString= "Provider=Microsoft.Jet.Oledb.4.0; Data Source='/App_Data/HzzArhiv.mdb'";

        Random SlucajniBroj = new Random(System.DateTime.Now.Millisecond);
        string Salt = SlucajniBroj.Next().ToString(); // Salt se generira za svakog usera i pise se u bazu u isti redak sa hashiranim passwordom
        string ZaporkaHash = Convert.ToString(SHA256(Password)); // Hashiranje passworda
        string PassHashSalt = Convert.ToString(SHA256(Salt + ZaporkaHash)); //ovo se sprema u bazu

        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand("INSERT INTO Users (UserName, FirstName, LastName, Email, PassHashSalt, Salt, Rights, DateStamp, RemoteAddr, LogonUser) VALUES(@UserName, @FirstName, @LastName, @Email, @PassHashSalt, @Salt, @Rights, @DateStamp, @RemoteAddr, @LogonUser);", myConnection);// Instanciranje SqlCommand koja predstavlja SQL koji će se izvršiti na SQL Serveru
        OleDbParameter prmUserName = myCommand.Parameters.Add("@UserName", OleDbType.VarChar);
        OleDbParameter prmFirstName = myCommand.Parameters.Add("@FirstName", OleDbType.VarChar);
        OleDbParameter prmLastName = myCommand.Parameters.Add("@LastName", OleDbType.VarChar);
        OleDbParameter prmEmail = myCommand.Parameters.Add("@Email", OleDbType.VarChar);
        OleDbParameter prmPassHashSalt = myCommand.Parameters.Add("@PassHashSalt", OleDbType.VarChar);
        OleDbParameter prmSalt = myCommand.Parameters.Add("@Salt", OleDbType.VarChar);
        OleDbParameter prmRights = myCommand.Parameters.Add("@Rights", OleDbType.Integer);
        OleDbParameter prmDateStamp = myCommand.Parameters.Add("@ateStamp", OleDbType.Date);
        OleDbParameter prmRemoteAddr = myCommand.Parameters.Add("@RemoteAddr", OleDbType.VarChar);
        OleDbParameter prmLogonUser = myCommand.Parameters.Add("@LogonUser", OleDbType.VarChar);

        prmUserName.Value = UserName;
        prmFirstName.Value = FirstName;
        prmLastName.Value = LastName;
        prmEmail.Value = Email;
        prmPassHashSalt.Value = PassHashSalt;
        prmSalt.Value = Salt;
        prmRights.Value = 3;
        prmDateStamp.Value = DateTime.Now;
        prmRemoteAddr.Value = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        prmLogonUser.Value = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        myCommand.ExecuteNonQuery();
        myConnection.Close();
        myConnection.Dispose();

    }


    public static void ChangeAccount(string UserName, string FirstName, string LastName, string Email, string Password, int UsersID)
    {

        //string myConnectionString= "Provider=Microsoft.Jet.Oledb.4.0; Data Source='/App_Data/HzzArhiv.mdb'";

        Random SlucajniBroj = new Random(System.DateTime.Now.Millisecond);
        string Salt = SlucajniBroj.Next().ToString(); // Salt se generira za svakog usera i pise se u bazu u isti redak sa hashiranim passwordom
        string ZaporkaHash = Convert.ToString(SHA256(Password)); // Hashiranje passworda
        string PassHashSalt = Convert.ToString(SHA256(Salt + ZaporkaHash)); //ovo se sprema u bazu

        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand("UPDATE Users SET UserName=@UserName, FirstName=@FirstName, LastName=@LastName, Email=@Email, PassHashSalt=@PassHashSalt, Salt=@Salt, Rights=@Rights, DateStamp=@DateStamp, RemoteAddr=@RemoteAddr, LogonUser=@LogonUser WHERE UsersID=@UsersID;", myConnection);// Instanciranje SqlCommand koja predstavlja SQL koji će se izvršiti na SQL Serveru
        OleDbParameter prmUserName = myCommand.Parameters.Add("@UserName", OleDbType.VarChar);
        OleDbParameter prmFirstName = myCommand.Parameters.Add("@FirstName", OleDbType.VarChar);
        OleDbParameter prmLastName = myCommand.Parameters.Add("@LastName", OleDbType.VarChar);
        OleDbParameter prmEmail = myCommand.Parameters.Add("@Email", OleDbType.VarChar);
        OleDbParameter prmPassHashSalt = myCommand.Parameters.Add("@PassHashSalt", OleDbType.VarChar);
        OleDbParameter prmSalt = myCommand.Parameters.Add("@Salt", OleDbType.VarChar);
        OleDbParameter prmRights = myCommand.Parameters.Add("@Rights", OleDbType.Integer);
        OleDbParameter prmDateStamp = myCommand.Parameters.Add("@ateStamp", OleDbType.Date);
        OleDbParameter prmRemoteAddr = myCommand.Parameters.Add("@RemoteAddr", OleDbType.VarChar);
        OleDbParameter prmLogonUser = myCommand.Parameters.Add("@LogonUser", OleDbType.VarChar);
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);

        prmUserName.Value = UserName;
        prmFirstName.Value = FirstName;
        prmLastName.Value = LastName;
        prmEmail.Value = Email;
        prmPassHashSalt.Value = PassHashSalt;
        prmSalt.Value = Salt;
        prmRights.Value = 3;
        prmDateStamp.Value = DateTime.Now;
        prmRemoteAddr.Value = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        prmLogonUser.Value = HttpContext.Current.Request.ServerVariables["LOGON_USER"];
        prmUsersID.Value = UsersID;

        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        myCommand.ExecuteNonQuery();
        myConnection.Close();
        myConnection.Dispose();

    }

    public static string ReadUserData(string ColumnName)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT * FROM Users WHERE UsersID=@UsersID";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        prmUsersID.Value = Convert.ToInt32(HttpContext.Current.Session["UsersID"]);

        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();
        if (myDataTable.Rows.Count == 1)
        {


            if (ColumnName == "UserName")
            {
                return myDataTable.Rows[0]["UserName"].ToString();
            }
            else if (ColumnName == "FirstName")
            {
                return myDataTable.Rows[0]["FirstName"].ToString();
            }
            else if (ColumnName == "LastName")
            {
                return myDataTable.Rows[0]["LastName"].ToString();
            }
            else if (ColumnName == "Email")
            {
                return myDataTable.Rows[0]["Email"].ToString();
            }
            else
            {
                return null;
            }
            //txtUserName.Text = myDataTable.Rows[0]["UserName"].ToString();
            //txtFirstName.Text = myDataTable.Rows[0]["FirstName"].ToString();
            //txtLastName.Text = myDataTable.Rows[0]["LastName"].ToString();
            //txtEmail.Text = myDataTable.Rows[0]["Email"].ToString();
        }
        else
        {
            return null;
        }
    }

    public static DataTable SearchForFriends(string SearchText)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT *, FirstName + ' ' + LastName AS FirstNameLastName FROM Users WHERE FirstName + ' ' + LastName Like '%' + @SearchText + '%'";
        OleDbParameter prmSearchText = myCommand.Parameters.Add("@SearchText", OleDbType.VarChar);
        prmSearchText.Value =SearchText ;

        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();

        return myDataTable;
    }





    public static void CreateUser(string UserName, string FirstName, string LastName, string Email, string Password)
    {

        //string myConnectionString= "Provider=Microsoft.Jet.Oledb.4.0; Data Source='/App_Data/HzzArhiv.mdb'";

        Random SlucajniBroj = new Random(System.DateTime.Now.Millisecond);
        string Salt = SlucajniBroj.Next().ToString(); // Salt se generira za svakog usera i pise se u bazu u isti redak sa hashiranim passwordom
        string ZaporkaHash = SHA256(Password); // Hashiranje passworda
        string PassHashSalt = SHA256(Salt + ZaporkaHash); //ovo se sprema u bazu

        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand("INSERT INTO Users (UserName, FirstName, LastName, Email, PassHashSalt, Salt, Rights, DateStamp, RemoteAddr, LogonUser) VALUES(@UserName, @FirstName, @LastName, @Email, @PassHashSalt, @Salt, @Rights, @DateStamp, @RemoteAddr, @LogonUser);", myConnection);// Instanciranje SqlCommand koja predstavlja SQL koji će se izvršiti na SQL Serveru
        OleDbParameter prmUserName = myCommand.Parameters.Add("@UserName", OleDbType.VarChar);
        OleDbParameter prmFirstName = myCommand.Parameters.Add("@FirstName", OleDbType.VarChar);
        OleDbParameter prmLastName = myCommand.Parameters.Add("@LastName", OleDbType.VarChar);
        OleDbParameter prmEmail = myCommand.Parameters.Add("@Email", OleDbType.VarChar);
        OleDbParameter prmPassHashSalt = myCommand.Parameters.Add("@PassHashSalt", OleDbType.VarChar);
        OleDbParameter prmSalt = myCommand.Parameters.Add("@Salt", OleDbType.VarChar);
        OleDbParameter prmRights = myCommand.Parameters.Add("@Rights", OleDbType.Integer);
        OleDbParameter prmDateStamp = myCommand.Parameters.Add("@ateStamp", OleDbType.Date);
        OleDbParameter prmRemoteAddr = myCommand.Parameters.Add("@RemoteAddr", OleDbType.VarChar);
        OleDbParameter prmLogonUser = myCommand.Parameters.Add("@LogonUser", OleDbType.VarChar);

        prmUserName.Value = UserName;
        prmFirstName.Value = FirstName;
        prmLastName.Value = LastName;
        prmEmail.Value = Email;
        prmPassHashSalt.Value = PassHashSalt;
        prmSalt.Value = Salt;
        prmRights.Value = 3;
        prmDateStamp.Value = DateTime.Now;
        prmRemoteAddr.Value = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        prmLogonUser.Value = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        myCommand.ExecuteNonQuery();
        myConnection.Close();
        myConnection.Dispose();




    }

    public static DataTable CategoriesList(int LogonUserID, int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;

        if (LogonUserID == SelectedUserID || IsThereAcceptedFriendship(LogonUserID, SelectedUserID) == true)
        {
            myCommand.CommandText = "SELECT Categories.CategoriesID, Categories.CatNames, Count(Categories.CategoriesID) AS CountOfCategoriesID FROM Categories INNER JOIN Jokes ON Categories.CategoriesID = Jokes.CategoriesID WHERE (((Jokes.UsersID)=@SelectedUserID)) GROUP BY Categories.CategoriesID, Categories.CatNames, Categories.CatSort ORDER BY Categories.CatSort;";
        }
        else
        {
            myCommand.CommandText = "SELECT Categories.CategoriesID, Categories.CatNames, Count(Categories.CategoriesID) AS CountOfCategoriesID FROM Categories INNER JOIN Jokes ON Categories.CategoriesID = Jokes.CategoriesID WHERE (((Jokes.UsersID)=@SelectedUserID)) AND Visibility=True GROUP BY Categories.CategoriesID, Categories.CatNames, Categories.CatSort ORDER BY Categories.CatSort;";
        }

        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmSelectedUserID.Value = SelectedUserID;

        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();

        return myDataTable;


    }

    public static void FriendshipRequest(int UsersID, int SelectedUserID, bool Accepted, int RequestAnswer)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);

        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "INSERT INTO Friendships (UsersID, SelectedUserID, Accepted, RequestAnswer, DateStamp, RemoteAddr, LogonUser) VALUES (@UsersID, @SelectedUserID, @Accepted, @RequestAnswer, @DateStamp, @RemoteAddr, @LogonUser)";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        OleDbParameter prmAccepted = myCommand.Parameters.Add("@Accepted", OleDbType.Boolean);
        OleDbParameter prmRequestAnswer = myCommand.Parameters.Add("@RequestAnswer", OleDbType.Integer);
        OleDbParameter prmDateStamp = myCommand.Parameters.Add("@DateStamp", OleDbType.Date);
        OleDbParameter prmRemoteAddr = myCommand.Parameters.Add("@RemoteAddr", OleDbType.VarChar);
        OleDbParameter prmLogonUser = myCommand.Parameters.Add("@LogonUser", OleDbType.VarChar);

        prmUsersID.Value = UsersID;
        prmSelectedUserID.Value = SelectedUserID;
        prmAccepted.Value = Accepted;
        prmRequestAnswer.Value = RequestAnswer;
        prmDateStamp.Value = DateTime.Now;
        prmRemoteAddr.Value = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        prmLogonUser.Value = HttpContext.Current.Request.ServerVariables["LOGON_USER"];

        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();


    }


    public static DataTable FriendshipRequestsList(int UsersID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT TOP 20 Friendships.SelectedUserID, FirstName+' '+LastName AS FirstNameLastName FROM Users INNER JOIN Friendships ON Users.UsersID = Friendships.SelectedUserID WHERE (((Friendships.UsersID)=[@UsersID]) AND ((Friendships.Accepted)=False) AND ((Friendships.RequestAnswer)=1));";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        prmUsersID.Value = UsersID;
        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();
        return myDataTable;
    }


    public static DataTable FriendsList(int UsersID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT TOP 20 Friendships.SelectedUserID, FirstName+' '+LastName AS FirstNameLastName FROM Users INNER JOIN Friendships ON Users.UsersID = Friendships.SelectedUserID WHERE Friendships.UsersID=[@UsersID] AND Friendships.Accepted=True;";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        prmUsersID.Value = UsersID;
        DataTable myDataTable = new DataTable();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        myAdapter.SelectCommand = myCommand;
        myAdapter.Fill(myDataTable);
        myConnection.Close();
        return myDataTable;
    }

    public static void FriendshipDenyDelete(int UsersID, int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);

        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "DELETE * FROM Friendships WHERE (UsersID=@UsersID AND SelectedUserID=@SelectedUserID) OR (UsersID=@SelectedUserID AND SelectedUserID=@UsersID)";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmUsersID.Value = SelectedUserID;
        prmSelectedUserID.Value = UsersID;
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }


    public static void FriendshipAccept(int UsersID, int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);

        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "UPDATE Friendships SET Accepted=True WHERE UsersID=@UsersID AND SelectedUserID=@SelectedUserID";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmUsersID.Value = SelectedUserID; // osoba koja je odabirana je bila SelectedUserID
        prmSelectedUserID.Value = UsersID; // osoba koja je unosila je bila UserID
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myConnection.Close();
    }




    public static String FirstNameLastName(int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT FirstName + ' ' + LastName As FirstNameLastName FROM Users WHERE UsersID=@SelectedUserID;";
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmSelectedUserID.Value = SelectedUserID;
        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        string FirstNameLastName = Convert.ToString(myCommand.ExecuteScalar());
        myConnection.Close();
        myConnection.Dispose();
        return FirstNameLastName;
    }






    public static int FriedshipCheck(int UsersID, int SelectedUserID, bool Accepted, int RequestAnswer)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT Users.UsersID FROM Users INNER JOIN Friendships ON Users.UsersID = Friendships.UsersID WHERE Friendships.UsersID=[@UsersID] AND Friendships.SelectedUserID=[@SelectedUserID] AND Friendships.Accepted=@Accepted AND RequestAnswer=@RequestAnswer;";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        OleDbParameter prmAccepted = myCommand.Parameters.Add("@Accepted", OleDbType.Boolean);
        OleDbParameter prmRequestAnswer = myCommand.Parameters.Add("@RequestAnswer", OleDbType.Boolean);
        prmUsersID.Value = SelectedUserID;
        prmSelectedUserID.Value = UsersID;
        prmAccepted.Value = Accepted;
        prmRequestAnswer.Value = RequestAnswer;

        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        int TherIsAFriend = Convert.ToInt32(myCommand.ExecuteScalar());
        myConnection.Close();
        myConnection.Dispose();
        return TherIsAFriend;
    }

    public static bool IsThereAcceptedFriendship(int UsersID, int SelectedUserID)
    {
        string myConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + HttpContext.Current.Server.MapPath("App_Data/JokerOnStage.mdb");
        OleDbConnection myConnection = new OleDbConnection(myConnectionString);
        OleDbCommand myCommand = new OleDbCommand();
        myCommand.Connection = myConnection;
        myCommand.CommandType = System.Data.CommandType.Text;
        myCommand.CommandText = "SELECT Accepted FROM Friendships WHERE UsersID=[@UsersID] AND SelectedUserID=[@SelectedUserID];";
        OleDbParameter prmUsersID = myCommand.Parameters.Add("@UsersID", OleDbType.Integer);
        OleDbParameter prmSelectedUserID = myCommand.Parameters.Add("@SelectedUserID", OleDbType.Integer);
        prmUsersID.Value = UsersID;
        prmSelectedUserID.Value = SelectedUserID;
        myConnection.Open(); // Otvaranje konekcije, izvršavanje SQL naredbe i zatvaranje konekcije
        bool TherIsAFriend = Convert.ToBoolean(myCommand.ExecuteScalar());
        myConnection.Close();
        myConnection.Dispose();
        return TherIsAFriend;
    }







}