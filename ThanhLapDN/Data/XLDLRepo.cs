using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ThanhLapDN.Data
{
    public class XLDLRepo
    {
        public static string StrCnn = ConfigurationManager.ConnectionStrings["db_thanhlapctyConnectionString"].ConnectionString;
        static SqlConnection Cnn;
        public static DataTable ReadData(string comm)
        {
            DataTable dt = new DataTable();
            try
            {
                if (Cnn == null) Cnn = new SqlConnection(StrCnn);
                SqlDataAdapter da = new SqlDataAdapter(comm, Cnn);
                da.FillSchema(dt, SchemaType.Mapped);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public static DataTable ReadStore(int _year, string _txt, int _iBegin, int _iEnd)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = StrCnn;
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SP_LoadCongNo", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@nam", _year);
                    sqlCommand.Parameters.AddWithValue("@tenkh", _txt);
                    sqlCommand.Parameters.AddWithValue("@mst", _txt);
                    sqlCommand.Parameters.AddWithValue("@content", _txt);
                    sqlCommand.Parameters.AddWithValue("@iBegin", _iBegin);
                    sqlCommand.Parameters.AddWithValue("@iEnd", _iEnd);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlCommand;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public static int DataCommand(string comm)
        {
            try
            {
                if (Cnn == null) Cnn = new SqlConnection(StrCnn);
                if (Cnn.State == ConnectionState.Closed) Cnn.Open();
                SqlCommand cmd = new SqlCommand(comm, Cnn);
                int kq = cmd.ExecuteNonQuery();
                Cnn.Close();
                return kq;
            }
            catch (SqlException)
            {
                return -1;
            }
        }
    }
}
