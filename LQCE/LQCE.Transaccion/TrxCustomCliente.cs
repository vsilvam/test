using System;
using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
    public partial class TrxCLIENTE
    {
        public CLIENTE GetByIdWithFullReferences(int ID)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    return repositorio.GetByIdWithFullReferences(ID);
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public int GetByFilterWithFullReferencesCount(string RUT, string NOMBRE, int? ID_REGION, int? ID_COMUNA, int? ID_TIPO_PRESTACION, int? ID_CONVENIO)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    return (from f in repositorio.GetByFilterWithFullReferences(RUT, NOMBRE, ID_REGION, ID_COMUNA, ID_TIPO_PRESTACION, ID_CONVENIO)
                            select f).Count();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        }

        public List<CLIENTE> GetByFilterWithFullReferences(string RUT, string NOMBRE, int? ID_REGION, int? ID_COMUNA, int? ID_TIPO_PRESTACION, int? ID_CONVENIO, int PageIndex, int PageSize)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioCLIENTE repositorio = new RepositorioCLIENTE(context);
                    return (from f in repositorio.GetByFilterWithFullReferences(RUT, NOMBRE, ID_REGION, ID_COMUNA, ID_TIPO_PRESTACION, ID_CONVENIO)
                            .OrderBy(f => f.NOMBRE)
                            .Skip((PageIndex - 1) * PageSize).Take(PageSize)
                            select f).ToList();
                }
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Error = ex.Message;
                throw ex;
            }
        } 
    }
}
