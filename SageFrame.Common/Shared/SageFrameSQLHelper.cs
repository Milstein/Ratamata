﻿#region "Copyright"
/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/
#endregion

#region "References"
using System;
using System.Collections.Generic;
using System.Text;
using SageFrame.Web.Utilities;
using System.Data.SqlClient;
using System.Data;
#endregion

namespace SageFrame.Common
{
    public class SageFrameSQLHelper:SQLHandler
    {
        public SageFrameSQLHelper():base()
        {
        }

        public List<KeyValuePair<int, string>> ExecuteNonQueryWithMultipleOutput(string StroredProcedureName, List<KeyValuePair<string, object>> InputParamColl, List<KeyValuePair<string, object>> OutPutParamColl)
        {
            SqlConnection SQLConn = new SqlConnection(base.connectionString);
            try
            {
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.Connection = SQLConn;
                SQLCmd.CommandText = StroredProcedureName;
                SQLCmd.CommandType = CommandType.StoredProcedure;
                //Loop for Paramets
                foreach (KeyValuePair<string,object> kvp in InputParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    SQLCmd.Parameters.Add(sqlParaMeter);
                }

                foreach (KeyValuePair<string, object> kvp in OutPutParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    sqlParaMeter.Direction = ParameterDirection.InputOutput;
                    sqlParaMeter.Size = 256;
                    SQLCmd.Parameters.Add(sqlParaMeter);
                }               
                SQLConn.Open();
                SQLCmd.ExecuteNonQuery();
                List<KeyValuePair<int, string>> lstRetValues = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < OutPutParamColl.Count; i++)
                {
                    lstRetValues.Add(new KeyValuePair<int, string>(i, SQLCmd.Parameters[InputParamColl.Count+i].Value.ToString()));
                }
                return lstRetValues;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                SQLConn.Close();
            }
        }
        public List<KeyValuePair<int, string>> ExecuteNonQueryWithMultipleOutput(SqlTransaction transaction, CommandType commandType, string StroredProcedureName, List<KeyValuePair<string, object>> InputParamColl, List<KeyValuePair<string, object>> OutPutParamColl)
        {
            try
            {
                //create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();
                SQLHandler.PrepareCommand(cmd, transaction.Connection, transaction, commandType, StroredProcedureName);
                //Loop for Paramets
                foreach (KeyValuePair<string, object> kvp in InputParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    cmd.Parameters.Add(sqlParaMeter);
                }

                foreach (KeyValuePair<string, object> kvp in OutPutParamColl)
                {
                    SqlParameter sqlParaMeter = new SqlParameter();
                    sqlParaMeter.IsNullable = true;
                    sqlParaMeter.ParameterName = kvp.Key;
                    sqlParaMeter.Value = kvp.Value;
                    sqlParaMeter.Direction = ParameterDirection.InputOutput;
                    sqlParaMeter.Size = 256;
                    cmd.Parameters.Add(sqlParaMeter);
                }

                cmd.ExecuteNonQuery();
                List<KeyValuePair<int, string>> lstRetValues = new List<KeyValuePair<int, string>>();
                for (int i = 0; i < OutPutParamColl.Count; i++)
                {
                    lstRetValues.Add(new KeyValuePair<int, string>(i, cmd.Parameters[InputParamColl.Count + i].Value.ToString()));
                }
                return lstRetValues;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
       
        
    }
}
