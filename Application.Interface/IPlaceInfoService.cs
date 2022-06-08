using Application.DTO;
using System;
using System.Collections.Generic;

namespace Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPlaceInfoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeInfo"></param>
        /// <returns></returns>
        int Add(PlaceInfo placeInfo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        int AddRange(IEnumerable<PlaceInfo> places);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<PlaceInfo> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PlaceInfo Find(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Remove(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="placeInfo"></param>
        /// <returns></returns>
        int Update(PlaceInfo placeInfo);
    }
}
