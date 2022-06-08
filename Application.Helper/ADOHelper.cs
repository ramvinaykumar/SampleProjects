using Application.DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Application.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public static class ADOHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteCRUDByQuery(string strSql)
        {
            string sConStr = "Data Source=.\\SQLExpress;Initial Catalog=YourDB;Integrated Security=True";
            SqlConnection conn = null;
            int iR = 0;
            try
            {
                conn = new SqlConnection(sConStr);
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //Execute the command  
                iR = cmd.ExecuteNonQuery();
            }
            catch { iR = 0; }
            finally { if (conn.State != 0) conn.Close(); }
            return iR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string strSql)
        {
            string sConStr = "Data Source=.\\SQLExpress;Initial Catalog=YourDB;Integrated Security=True";
            SqlConnection conn = null;
            DataTable dt = null;
            try
            {
                conn = new SqlConnection(sConStr);
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                dt = new DataTable();
                //Fill the dataset  
                da.Fill(dt);
                if (!(dt.Rows.Count > 0)) dt = null;
            }
            catch { dt = null; }
            finally { if (conn.State != 0) conn.Close(); }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static PlaceInfo GetPlaceInfoByRow(DataRow dr)
        {
            PlaceInfo placeInfo = new PlaceInfo();
            placeInfo.Id = Convert.ToInt32(dr["Id"]);
            placeInfo.Place = dr["Place"].ToString();
            placeInfo.About = dr["About"].ToString();
            placeInfo.City = dr["City"].ToString();
            placeInfo.State = dr["State"].ToString();
            placeInfo.Country = dr["Country"].ToString();
            return placeInfo;
        }
    }
}
