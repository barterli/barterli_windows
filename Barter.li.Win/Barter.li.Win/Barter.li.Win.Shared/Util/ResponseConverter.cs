//----------------------------------------------------------------------------------------------
// <copyright file="ResponseConverter.cs" company="BarterLi">
// Copyright (c) BarterLi.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------------
namespace Barter.Li.Win.Util
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Response (JSON) to Object and Vice-versa converter
    /// </summary>
    public class ResponseConverter
    {
        /// <summary>
        /// Convert JOSN string to Object
        /// </summary>
        /// <typeparam name="T">Expecting type</typeparam>
        /// <param name="value">string response</param>
        /// <returns>Object of Expecting type</returns>
        public static async Task<T> Deserialize<T>(string value)
        {
            try
            {
                return await Task.Factory.StartNew<T>(() => Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value));                
            }
            catch (Exception e)
            {
                throw e;
            }           
        }

        /// <summary>
        /// Convert object to string JSON
        /// </summary>
        /// <param name="value">Object - Model</param>
        /// <returns>JSON string</returns>
        public static async Task<string> Serialize(object value)
        {            
            try
            {
                return await Task.Factory.StartNew<string>(() => Newtonsoft.Json.JsonConvert.SerializeObject(value));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
