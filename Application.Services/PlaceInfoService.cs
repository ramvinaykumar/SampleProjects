using Application.DTO;
using Application.Interface;
using Application.Helper;
using System;
using System.Collections.Generic;
using System.Data;

namespace Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceInfoService : IPlaceInfoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeInfo"></param>
        /// <returns></returns>
        public int Add(PlaceInfo placeInfo)
        {
            string sQry = "INSERT INTO [PlaceInfo] ([Place],[About],[City],[State],[Country]) " +
                "VALUES('" + placeInfo.Place + "','" + placeInfo.About + "','" + placeInfo.City + "','" +
                placeInfo.State + "','" + placeInfo.Country + "')";
            int retVal = ADOHelper.ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        public int AddRange(IEnumerable<PlaceInfo> places)
        {
            string sQry = "INSERT INTO [PlaceInfo] ([Place],[About],[City],[State],[Country]) VALUES";
            string sVal = "";
            foreach (var place in places)
                sVal += "('" + place.Place + "','" + place.About + "','" + place.City + "','" + place.State + "','" + place.Country + "'),";
            sVal = sVal.TrimEnd(',');
            sQry = sQry + sVal;
            int retVal = ADOHelper.ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlaceInfo Find(int id)
        {
            PlaceInfo placeInfo = null;
            string sQry = "SELECT * FROM [PlaceInfo] WHERE [Id]=" + id;
            DataTable dtPlaceInfo = ADOHelper.ExecuteQuery(sQry);
            if (dtPlaceInfo != null)
            {
                DataRow dr = dtPlaceInfo.Rows[0];
                placeInfo = ADOHelper.GetPlaceInfoByRow(dr);
            }
            return placeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlaceInfo> GetAll()
        {
            List<PlaceInfo> placeInfos = null;
            string sQry = "SELECT * FROM [PlaceInfo]";
            DataTable dtPlaceInfo = ADOHelper.ExecuteQuery(sQry);
            if (dtPlaceInfo != null)
            {
                placeInfos = new List<PlaceInfo>();
                foreach (DataRow dr in dtPlaceInfo.Rows)
                    placeInfos.Add(ADOHelper.GetPlaceInfoByRow(dr));
            }
            return placeInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remove(int id)
        {
            string sQry = "DELETE FROM [PlaceInfo] WHERE [Id]=" + id;
            int retVal = ADOHelper.ExecuteCRUDByQuery(sQry);
            return retVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeInfo"></param>
        /// <returns></returns>
        public int Update(PlaceInfo placeInfo)
        {
            string sQry = "UPDATE [PlaceInfo] SET [Place]='" + placeInfo.Place + "',[About]='" + placeInfo.About + "',[City]='" + placeInfo.City + "',[State]='" + placeInfo.State + "',[Country]='" + placeInfo.Country + "' WHERE [Id]=" + placeInfo.Id;
            int retVal = ADOHelper.ExecuteCRUDByQuery(sQry);
            return retVal;
        }


        
    }
}
