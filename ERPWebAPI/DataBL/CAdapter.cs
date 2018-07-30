
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PriceSetAPI.DataBL
{
    public class CAdapter
    {
        public static void GenerateAdUserCommand(ref SqlDataAdapter da)
        {
            SqlParameter p;
            SqlCommand com;
            // update
            com = new SqlCommand();

            com.Parameters.Add("@id", SqlDbType.VarChar, 10, "id");
            com.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            com.Parameters.Add("@pword", SqlDbType.VarChar, 50, "pword");
            com.Parameters.Add("@email", SqlDbType.VarChar, 30, "email");

            p = com.Parameters.Add("@oldid", SqlDbType.VarChar, 10, "id");
            p.SourceVersion = DataRowVersion.Original;

            com.CommandText = "UPDATE aduser SET pword = @pword WHERE id = @oldid";
            da.UpdateCommand = com;

            com = new SqlCommand();

            com.Parameters.Add("@id", SqlDbType.VarChar, 10, "id");
            com.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            com.Parameters.Add("@pword", SqlDbType.VarChar, 50, "pword");
            com.Parameters.Add("@email", SqlDbType.VarChar, 30, "email");



            com.CommandText = "Insert into aduser (id, name, pword, email) VALUES (@id, @name,@pword,@email)";
            da.InsertCommand = com;

            com = new SqlCommand();

            com.Parameters.Add("@id", SqlDbType.VarChar, 10, "id");
            com.Parameters.Add("@name", SqlDbType.VarChar, 30, "name");
            com.Parameters.Add("@pword", SqlDbType.VarChar, 50, "pword");
            com.Parameters.Add("@email", SqlDbType.VarChar, 30, "email");

            p = com.Parameters.Add("@oldid", SqlDbType.VarChar, 10, "id");
            p.SourceVersion = DataRowVersion.Original;
            com.CommandText = "delete aduser where id=@oldid";
            da.DeleteCommand = com;
        }


    }
}
