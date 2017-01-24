using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.SqlClient;
using System.Text;

namespace CamadaDeControle.Models
{
    public class InsertUpdateInterceptor : IDbCommandInterceptor
    {
        public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            logCommand(command);
        }

        public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            logCommand(command);
        }

        public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            logCommand(command);
        }

        public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            logCommand(command);
        }

        public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            logCommand(command);
        }

        public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            logCommand(command);
        }

        private void logCommand(DbCommand dbCommand)
        {
            StringBuilder commandText = new StringBuilder();

            commandText.AppendLine("-- New statement generated: " + System.DateTime.Now.ToString());
            commandText.AppendLine();

            // as the command has a bunch of parameters, we need to declare
            // those parameters here so the SQL will execute properly

            foreach (DbParameter param in dbCommand.Parameters)
            {
                var sqlParam = (SqlParameter)param;

                commandText.AppendLine(String.Format("DECLARE {0} {1} {2}",
                                                        sqlParam.ParameterName,
                                                        sqlParam.SqlDbType.ToString().ToLower(),
                                                        getSqlDataTypeSize(sqlParam)));

                var escapedValue = sqlParam.SqlValue;
                commandText.AppendLine(String.Format("SET {0} = '{1}'", sqlParam.ParameterName, escapedValue));
                commandText.AppendLine();
            }

            commandText.AppendLine(dbCommand.CommandText);
            commandText.AppendLine("GO");
            commandText.AppendLine();
            commandText.AppendLine();

            System.IO.File.AppendAllText("outputfile.sql", commandText.ToString());
        }

        private string getSqlDataTypeSize(SqlParameter param)
        {
            if (param.Size == 0)
            {
                return "";
            }

            if (param.Size == -1)
            {
                return "(MAX)";
            }

            return "(" + param.Size + ")";
        }
    }
}