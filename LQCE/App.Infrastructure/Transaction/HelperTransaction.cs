using System;
using System.Transactions;

namespace App.Infrastructure.Transaction
{
    public class HelperTransaction : IDisposable
    {
        private TransactionScope _transactionScope;

        /// <summary>
        /// Inicializa el helper de transacciones con MSDTC.
        /// </summary>
        public HelperTransaction()
        {
            var transactionOptions = new TransactionOptions();
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Completa la transacción actual.
        /// </summary>
        public void Commit()
        {
            _transactionScope.Complete();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_transactionScope != null)
                {
                    _transactionScope.Dispose();
                    _transactionScope = null;
                }
            }
        }
    }
}