using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

using System.Threading;
using galaEatAPI.Services;

namespace MRP.BL
{
    public class BaseADOPG
    {
        //private static ILog log = LogManager.GetLogger(typeof(BaseADO));
        private static string _connString;// = "User ID = postgres; Password=wincom;Host=localhost;Port=5432;Database=posserver;Pooling=true;";// IConfiguration.GetConnectionString("DataAccessPostgreSqlProvider");

        public static string ConnectionString
        {
            get { return _connString; }
            set { _connString = value; }
        }

        protected static SqlConnection _connObj = null;
        protected static SqlConnection _connObjEx = null;


        #region protected Methods


        protected static bool OpenConnection()
        {
            bool _connected = false;

            //MOK 5-Nov-2012
            // Enhance the open connection method.

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    _connObj = new SqlConnection(_connString);
                    _connObj.Open();
                    _connected = true;
                }
                catch (Exception ex)
                {
                    // //log.Error(ex.Message, ex);
                    CloseConnection();
                }
                i++;
                if (_connected)
                {
                    i = 10; //just to break;
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    ////log.Debug("try open connection again.." + i.ToString());
                }
            }

            return _connected;
        }

        protected static bool OpenConnectionEx()
        {
            bool _connected = false;

            //MOK 5-Nov-2012
            // Enhance the open connection method.

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    _connObjEx = new SqlConnection(_connString);
                    _connObjEx.Open();
                    _connected = true;
                }
                catch (Exception ex)
                {
                    //log.Error(ex.Message, ex);
                    CloseConnectionEx();
                }
                i++;
                if (_connected)
                {
                    i = 10; //just to break;
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    ////log.Debug("try open connection again.." + i.ToString());
                }
            }

            return _connected;
        }

        protected static void CloseConnectionEx()
        {
            //MOK 5-Nov-2012
            // Enhance the close connection method.
            try
            {
                if (_connObjEx != null && _connObj.State != System.Data.ConnectionState.Closed)
                {
                    _connObjEx.Close();
                    _connObjEx.Dispose();
                    _connObjEx = null;
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message, ex);
                _connObj = null;
            }
        }

        protected static bool OpenConnectionTrx(ref SqlConnection trxconn)
        {
            bool _connected = false;

            //MOK 5-Nov-2012
            // Enhance the open connection method.

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    trxconn = new SqlConnection(_connString);
                    trxconn.Open();
                    _connected = true;
                }
                catch (Exception ex)
                {
                    //log.Error(ex.Message, ex);
                    CloseConnectionTrx(ref trxconn);
                }
                i++;
                if (_connected)
                {
                    i = 10; //just to break;
                    break;
                }
                else
                {
                    Thread.Sleep(200);
                    //log.Debug("try open connection again.." + i.ToString());
                }
            }

            return _connected;
        }

        protected static void CloseConnection()
        {
            //MOK 5-Nov-2012
            // Enhance the close connection method.
            try
            {
                if (_connObj != null && _connObj.State != System.Data.ConnectionState.Closed)
                {
                    _connObj.Close();
                    _connObj.Dispose();
                    _connObj = null;
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message, ex);
                _connObj = null;
            }
        }

        protected static void CloseConnectionTrx(ref SqlConnection trxconn)
        {
            //MOK 5-Nov-2012
            // Enhance the close connection method.
            try
            {
                if (trxconn != null && trxconn.State != System.Data.ConnectionState.Closed)
                {
                    trxconn.Close();
                    trxconn.Dispose();
                    trxconn = null;
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message, ex);
                trxconn = null;
            }
        }
        //protected static bool OpenConnection()
        //{
        //    bool _connected = false;

        //    try
        //    {
        //        _connObj = new SqlConnection(_connString);
        //        _connObj.Open();
        //        _connected = true;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return _connected;
        //}

        //protected static void CloseConnection()
        //{
        //    if (_connObj != null && _connObj.State != System.Data.ConnectionState.Closed)
        //    {
        //        _connObj.Close();
        //        _connObj.Dispose();
        //    }
        //}

        protected static DataTable GetData(SqlCommand cmd)
        {
            DataTable _dt = new DataTable();
            SqlConnection trxconn = null;
            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {
                    cmd.Connection = trxconn;
                    using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                    {
                        _da.Fill(_dt);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);

            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
            }


            return _dt;
        }

        public static DataTable GetData(string sqlStr)
        {
            DataTable _dt = new DataTable();
            SqlConnection trxconn = null;
            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {
                    SqlCommand cmd = new SqlCommand(sqlStr, trxconn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;

                    using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                    {
                        DataTable t = new DataTable();
                        _da.Fill(_dt);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);
                //log.Debug("GetData " + sqlStr);
            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
            }

            return _dt;
        }

        public static DataTable GetData(string sqlStr, ref SqlDataAdapter da, ref SqlConnection trxconn)
        {
            DataTable _dt = new DataTable();

            try
            {
                SqlCommand cmd = new SqlCommand(sqlStr, trxconn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                da = new SqlDataAdapter(cmd);

                DataTable t = new DataTable();
                da.Fill(_dt);


            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);
                //log.Debug("GetData " + sqlStr);
            }

            return _dt;
        }

        public static void ExceuteSql(string sqlStr)
        {
            DataTable _dt = new DataTable();
            SqlConnection trxconn = null;
            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {
                    SqlCommand cmd = new SqlCommand(sqlStr, trxconn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);
                //log.Debug("GetData " + sqlStr);
            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
            }


        }

        public static void ExceuteSqlEx(string sqlStr, SqlConnection trxconn)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlStr, trxconn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);
                //log.Debug("GetData " + sqlStr);
            }


        }

        public static void ExceuteSql(string sqlStr, SqlConnection trxconn, SqlTransaction trans)
        {
            DataTable _dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlStr, trxconn, trans);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

        }

        protected static DataSet GetDatas(string sqlStr)
        {
            DataSet _dt = new DataSet();
            SqlConnection trxconn = null;
            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {
                    SqlCommand cmd = new SqlCommand(sqlStr, trxconn);
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;

                    using (SqlDataAdapter _da = new SqlDataAdapter(cmd))
                    {
                        _da.Fill(_dt);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Debug("GetData " + ex.Message);
                //log.Debug("GetData " + sqlStr);
            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
            }



            return _dt;
        }

        protected static bool ExecuteQuery(SqlCommand cmd)
        {
            bool _pass = false;
            SqlConnection trxconn = null;

            if (OpenConnectionTrx(ref trxconn))
            {
                SqlTransaction sqlTrans = trxconn.BeginTransaction();
                cmd.Connection = trxconn;
                cmd.Transaction = sqlTrans;
                try
                {
                    cmd.ExecuteNonQuery();
                    sqlTrans.Commit();
                    _pass = true;
                }
                catch (Exception ex)
                {
                    sqlTrans.Rollback();
                    CloseConnectionTrx(ref trxconn);
                    throw (new Exception(ex.Message, ex));
                }

            }

            CloseConnectionTrx(ref trxconn);

            return _pass;
        }

        protected static object ExecuteScalar(SqlCommand cmd)
        {
            object _obj = null;

            SqlConnection trxconn = null;
            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {

                    cmd.Connection = trxconn;
                    _obj = cmd.ExecuteScalar();

                }
            }
            catch (Exception ex)
            {
                //log.Debug("ExecuteScalar " + ex.Message);
                //log.Debug("ExecuteScalar " + cmd.CommandText);
            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
            }



            return _obj;
        }
        #endregion

        protected static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = (pi.GetValue(rec, null) == null) ? DBNull.Value : pi.GetValue(rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        //protected static void LogError(ILog log, Exception ex)
        //{
        //    //log.Error(ex.Message, ex);
        //    throw new Exception(ex.Message, ex);
        //}

        public static bool UpdateTable(ref DataTable tablename, ref SqlDataAdapter da)
        {
            bool success = false;
            SqlConnection trxconn = null;
            SqlTransaction sqlTrans = null;

            try
            {
                if (OpenConnectionTrx(ref trxconn))
                {
                    sqlTrans = trxconn.BeginTransaction();
                    // da.RowUpdated += da_RowUpdated;
                    da.UpdateCommand.Connection = trxconn;
                    da.InsertCommand.Connection = trxconn;
                    da.DeleteCommand.Connection = trxconn;

                    da.InsertCommand.Transaction = sqlTrans;
                    da.UpdateCommand.Transaction = sqlTrans;
                    da.DeleteCommand.Transaction = sqlTrans;

                    da.Update(tablename);
                    da.UpdateCommand.Parameters.Clear();
                    sqlTrans.Commit();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                //log.Debug("UpdateTable " + ex.Message);
                // CCommon.CreateMessageAlert(this, "Error saving data, please contact your system admistrator! " + ex.Message.Replace("'", "\\'").Replace("&nbsp;", ""), "CONNECTION_EXCEPTION");
                sqlTrans.Rollback();
                success = false;
                throw new Exception(ex.Message, ex);

            }
            finally
            {
                CloseConnectionTrx(ref trxconn);
                // da.RowUpdated -= da_RowUpdated;
            }

            return success;
        }

        static void da_RowUpdated(object sender, SqlRowUpdatedEventArgs args)
        {
            if (args.Status == UpdateStatus.ErrorsOccurred)
            {
                //log.Error("UpdateTable da_RowUpdated " + args.Errors.Message);
                //log.Error("UpdateTable da_RowUpdated " + args.Command.CommandText);
                args.Row.RowError = args.Errors.Message;
                args.Status = UpdateStatus.ErrorsOccurred;
            }
        }
    }
}