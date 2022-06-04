using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos3
{
    internal class Empleado
    {
		private int _legajo;
		private string _nombre;
		private string _apellido;
		private int _antiguedad;
		private DateTime _fechaIngreso;
		private DateTime _fechaEgreso;
		public int Legajo
		{
			get { return this._legajo; }
			set { this._legajo = value; }
		}

		public string Nombre
		{
			get { return this._nombre; }
			set { this._nombre = value; }
		}

		public string Apellido
		{
			get { return this._apellido; }
			set { this._apellido = value; }
		}

		public int Antiguedad
		{
			get { return this._antiguedad; }
			set { this._antiguedad = value; }
		}

		public DateTime FechaIngreso
		{
			get { return this._fechaIngreso; }
			set { this._fechaIngreso = value; }
		}

		public DateTime FechaEgreso
		{
			get { return this._fechaEgreso; }
			set { this._fechaEgreso = value; }
		}

		public Empleado(int legajo, string nombre,string apellido, DateTime fechaIngreso)
		{

			this._legajo = legajo;
			this._nombre = nombre;
			this._apellido = apellido;
			this._fechaIngreso = fechaIngreso;
			this._antiguedad = 0;

		}	
	}
}
