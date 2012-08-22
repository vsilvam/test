using System;
using System.Data;
using System.Data.SqlClient;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.DataAccess
{
    public class HelperCommand : IDisposable
    {
        private SqlCommand _command;
        private SqlConnection _connection;

        /// <summary>
        /// Inicializa el helper de acceso a datos.
        /// </summary>
        /// <param name="connectionString">Cadena o nombre constante de conexión para la fuente de datos.</param>
        public HelperCommand(string connectionString)
        {
            connectionString = ISConfiguration.GetDbConfig(connectionString);

            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand {CommandType = CommandType.StoredProcedure, Connection = _connection};
        }

        public HelperCommand(string connectionString, CommandType commandType)
        {
            connectionString = ISConfiguration.GetDbConfig(connectionString);

            _connection = new SqlConnection(connectionString);
            _command = new SqlCommand {CommandType = commandType, Connection = _connection};
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Método de ejecución de consulta sin retorno de datos.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        /// <param name="parameterCollection">Array de parámetros.</param>
        public void ExecuteNonQuery(string commandText, SqlParameter[] parameterCollection)
        {
            if (parameterCollection != null)
                _command.Parameters.AddRange(parameterCollection);

            _command.CommandText = commandText;

            _connection.Open();
            _command.ExecuteNonQuery();
        }

        /// <summary>
        /// Método de ejecución de consulta que retorna un lector de solo avance.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        /// <param name="parameterCollection">Array de parámetros.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, SqlParameter[] parameterCollection)
        {
            if (parameterCollection != null)
                _command.Parameters.AddRange(parameterCollection);

            _command.CommandText = commandText;

            _connection.Open();
            return _command.ExecuteReader();
        }

        /// <summary>
        /// Método de ejecución de consulta que retorna un matriz 1x1.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        /// <param name="parameterCollection">Array de parámetros.</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, SqlParameter[] parameterCollection)
        {
            if (parameterCollection != null)
                _command.Parameters.AddRange(parameterCollection);

            _command.CommandText = commandText;

            _connection.Open();
            return _command.ExecuteScalar();
        }


        /// <summary>
        /// Método de ejecución de consulta sin retorno de datos.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        public void ExecuteNonQuery(string commandText)
        {
            ExecuteNonQuery(commandText, null);
        }

        /// <summary>
        /// Método de ejecución de consulta que retorna un lector de solo avance.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(commandText, null);
        }

        /// <summary>
        /// Método de ejecución de consulta que retorna un matriz 1x1.
        /// </summary>
        /// <param name="commandText">Texto de consulta.</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(commandText, null);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                    _connection.Dispose();
                    _connection = null;
                }

                if (_command != null)
                {
                    _command.Dispose();
                    _command = null;
                }
            }
        }
    }
}