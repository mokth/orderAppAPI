using System;
using System.Data;
using System.Data.SqlClient;


namespace POS.DATA
{
    /// <summary>
    /// Summary description for CSys.
    /// </summary>
    public class CSys
    {

        #region "SESSION NAME"
        internal static string SESSION_CONN = "Connection";
        #endregion

        public static string strCon;// = "User ID = postgres; Password=wincom;Host=localhost;Port=5432;Database=posserver;Pooling=true;" ;// IConfiguration.GetConnectionString("DataAccessPostgreSqlProvider");
                                    // public static string strConAcc = ConfigurationManager.AppSettings["ConnectionStringAcc"];
        public static string strAppUpdate = "";
        public static bool bExitNow = false;
        public static string UserID = "";
        public static string Message = "";
        //  public static string receiptPrinter = ConfigurationManager.AppSettings["ReceiptPrinter"];

        public CSys()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static bool OpenCon(ref SqlConnection con)
        {
            try
            {


                if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
                {
                    con.ConnectionString = strCon;
                    con.Open();
                }

                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                if (e.Message.IndexOf("Login failed") != -1)
                {
                    // System.Web.HttpContext.Current.Response.Redirect("~/sqlerror.html");
                }
                Message = e.Message;
                return false;
            }
        }
        public static void CloseCon(ref SqlConnection con)
        {
            try
            {
                con.Close();
                con.Dispose();
            }
            catch
            {
            }
            return;
        }



        public static DataTable OpenTablex(string sqlstr)
        {
            SqlConnection sqlcon = new SqlConnection();
            DataTable table = new DataTable();

            if (!OpenCon(ref sqlcon))
            {
                throw new Exception("Error opening database connection.");
            }
            try
            {
                SqlCommand cmd = new SqlCommand(sqlstr, sqlcon);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                ad.Fill(table);
            }
            catch (Exception)
            {
                throw new Exception("Error opening table connection." + sqlstr);
            }
            finally
            {
                CloseCon(ref sqlcon);
            }

            return table;
        }
    }
}

