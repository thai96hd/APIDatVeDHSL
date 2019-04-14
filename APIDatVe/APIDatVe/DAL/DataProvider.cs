using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIDatVe.DAL
{
    public class DataProvider
    {
        private string chuoiketnoi = @"Data Source=.\SqlExpress;Initial Catalog=QLDatVe2;User ID=thai96hd; Password=thaitran1234";
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }
        private DataProvider() { }
        private SqlConnection con = null;
        /// <summary>
        /// Mở kết nối
        /// </summary>
        /// <returns></returns>
        public int open()
        {
            int ret;
            con = new SqlConnection(chuoiketnoi);
            try
            {
                con.Open();
                ret = 1;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;
        }
        public int close()
        {
            int ret;
            if (con == null)
            {
                ret = 1;
            }
            try
            {
                con.Close();
                ret = 1;
            }
            catch (Exception)
            {
                ret = 0;
            }
            return ret;

        }
        /// <summary>
        /// get command
        /// </summary>
        /// <returns></returns>
        public SqlCommand getCommand(String sql)
        {
            open();
            SqlCommand command = new SqlCommand(sql, con);
            return command;
        }

        public SqlTransaction GetSqlTransaction()
        {
            if (con == null)
            {
                open();
            }

            return con.BeginTransaction();


        }



        /// <summary>
        /// Lấy dữ liệu từ database đổ về datatable
        /// </summary>
        /// <param name="querry"></param>
        /// <param name="pa"></param>
        /// <returns></returns>
        public DataTable GetData(string storeName, params SqlParameter[] pa)
        {
            DataTable dt = new DataTable();
            if (open() == 0)
            {
                open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = storeName;
            command.CommandType = CommandType.StoredProcedure;
            if (pa != null)
            {
                command.Parameters.AddRange(pa);
            }
            SqlDataAdapter data = new SqlDataAdapter(command);
            data.Fill(dt);
            close();
            return dt;
        }
        public DataTable GetDataQuerry(string querry)
        {
            DataTable dt = new DataTable();
            if (open() == 0)
            {
                open();
            }
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandText = querry;
            command.CommandType = CommandType.Text;
            SqlDataAdapter data = new SqlDataAdapter(command);
            data.Fill(dt);
            close();
            return dt;
        }
        public int ExecuteNonQuery(string storename, params SqlParameter[] pa)
        {
            int ret = 0;
            if (open() == 0)
            {
                open();
            }
            SqlCommand command = new SqlCommand();
            command.CommandText = storename;
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            if (pa != null) command.Parameters.AddRange(pa);
            try
            {
                ret = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.ToString();
                ret = -1;
            }
            close();
            return ret;
        }
        public int DangNhap(string name_storeproc, SqlParameter[] pa)
        {
            int ret = 0;
            open();
            SqlCommand commd = new SqlCommand();
            commd.CommandText = name_storeproc;
            commd.Connection = con;
            commd.CommandType = CommandType.StoredProcedure;
            if (pa != null)
            {
                commd.Parameters.AddRange(pa);
            }
            try
            {
                ret = (Int32)commd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                return -1;
            }
            close();
            return ret;
        }
    }
}