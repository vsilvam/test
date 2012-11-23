using System;
using System.Linq;
using App.Infrastructure.Runtime;
using LQCE.Modelo;
using LQCE.Repositorio;

namespace LQCE.Transaccion
{
    public partial class TrxREGION
    {
        public int Add(int ID, string NOMBRE)
        {
            Init();
            try
            {
                using (LQCEEntities context = new LQCEEntities())
                {
                    RepositorioREGION repositorio = new RepositorioREGION(context);

                    if (repositorio.GetByFilter(ID, true).Any())
                        throw new Exception("Ya existe una región con este ID");

                    REGION _REGION = null;
                    if (repositorio.GetByFilter(ID, false).Any())
                    {
                        _REGION = repositorio.GetByFilter(ID, false).First();
                        _REGION.NOMBRE = NOMBRE;
                        _REGION.ACTIVO = true;
                    }
                    else
                    {
                        _REGION = new Modelo.REGION();
                        _REGION.ID = ID;
                        _REGION.NOMBRE = NOMBRE;
                        _REGION.ACTIVO = true;
                        context.AddObject("REGION", _REGION);
                    }
                   
                    context.SaveChanges();

                    return _REGION.ID;
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
