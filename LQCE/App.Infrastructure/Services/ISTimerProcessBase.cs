using System;
using System.Timers;
using App.Infrastructure.Base;
using App.Infrastructure.Runtime;

namespace App.Infrastructure.Services
{
    public abstract class ISTimerProcessBase
    {
        private bool _autoContinueProcess = true;
        private Double _interval;
        private object _objState;
        private Timer _timerProcess;

        public object ObjState
        {
            get { return _objState; }
        }

        public bool AutoContinueProcess
        {
            get { return _autoContinueProcess; }
            set { _autoContinueProcess = value; }
        }

        protected abstract void OnProcess();

        public bool Start(double intervalSecons, object objState)
        {
            _interval = intervalSecons;
            _objState = objState;

            ISUtil.ClearMemory();

            //SE CREA LA INSTANCIA DEL RELOJ
            _interval = _interval*1000;
            if (_interval < 1000)
            {
                string error =
                    "Error al inciar el servicio: El intervalo de ejecucción no puede ser inferior a 1 segundo.";
                ISException.RegisterExcepcion(error);
                Stop(error);
                return false;
            }

            _timerProcess = new Timer();
            _timerProcess.Elapsed += _timerProcess_Elapsed;

            try
            {
                _timerProcess.Interval = _interval;
                _timerProcess.Enabled = true;

                ISException.WriteInformationLog("Servicio iniciado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                ISException.RegisterExcepcion(ex);
                Stop(ex.Message);
                return false;
            }
        }

        public void Stop()
        {
            //SE ELIMINA DE LA MEMORIA LA INSTANCIA DEL RELOJ
            if (_timerProcess != null)
            {
                _timerProcess.Enabled = false;
                _timerProcess.Dispose();
                _timerProcess = null;
            }

            ISException.WriteInformationLog("Servicio detenido correctamente.");

            ISUtil.ClearMemory();
        }

        protected void Stop(string strCause)
        {
            //SE ELIMINA DE LA MEMORIA LA INSTANCIA DEL RELOJ
            if (_timerProcess != null)
            {
                _timerProcess.Enabled = false;
                _timerProcess.Dispose();
                _timerProcess = null;
            }

            if (!string.IsNullOrEmpty(strCause))
            {
                strCause = " Motivo: " + strCause;
            }
            ISException.WriteInformationLog("Servicio detenido correctamente." + strCause);

            ISUtil.ClearMemory();
        }

        private void _timerProcess_Elapsed(object sender, ElapsedEventArgs e)
        {
            //SE DESACTIVA EL RELOJ MIENTRAS PROCESA LA OPERACION
            _timerProcess.Enabled = false;

            OnProcess();

            //SE REACTIVA EL RELOJ
            if (_autoContinueProcess)
            {
                _timerProcess.Enabled = true;
            }
        }

        protected void Pause()
        {
            if (_timerProcess != null)
            {
                _timerProcess.Enabled = false;
            }
        }

        protected void Continue()
        {
            if (_timerProcess != null)
            {
                _timerProcess.Enabled = true;
            }
        }
    }
}