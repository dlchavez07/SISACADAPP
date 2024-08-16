using SISACADMovil.Models;
using SISACADMovil.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SISACADMovil.ViewModels
{
    public class PeriodosViewModel:BaseViewModel
    {
        public List<EstudianteM> _periodoAcademico;
        public EstudianteM _selectedPeriodo;
        public string _periodo;
        public string identificacion;


     
      
        public PeriodosViewModel(string idConsulta)
        {
            identificacion = idConsulta;
        }



        public List<EstudianteM> PeriodoAcademico
        {
            get { return _periodoAcademico; }
            set { SetValue(ref _periodoAcademico, value); }
        }

        #region PICKER SELECTER
        public EstudianteM SelectedPeriodo
        {
            get { return _selectedPeriodo; }
            set
            {
                if (_selectedPeriodo != value)
                {
                    _selectedPeriodo = value;
                    Periodo = _selectedPeriodo.Periodo_academico;
                }
            }
        }

     
        public string Periodo
        {
            get { return _periodo; }
            set
            {
                if (_periodo != value)
                {
                    _periodo = value;
                    OnpropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
        public async Task MostrarPeriodos()
        {
            try
            {
                var funcion = new PeriodosService();
                PeriodoAcademico = await funcion.GetPeriodos(identificacion);
            }
            catch (Exception ex)
            {


            }
        }
    }
}
