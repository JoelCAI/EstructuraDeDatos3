using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EstructuraDeDatos3
{
	internal class UsuarioAdministrador : Usuario
	{
		protected List<Empleado> _empleado;

		public List<Empleado> Empleado
		{
			get { return this._empleado; }
			set { this._empleado = value; }
		}

		public UsuarioAdministrador(string nombre, List<Empleado> empleado) : base(nombre)
		{
			this._empleado = empleado;
		}

		public void MenuAdministrador(List<Empleado> auto)
		{

			Empleado = auto;

			int opcion;
			do
			{

				Console.Clear();
				Console.WriteLine(" Bienvenido Usuario: *" + Nombre + "* ");
				opcion = Validador.PedirIntMenu("\n Menú de Registro de nuevos Empleados: " +
									   "\n [1] Crear Empleado" +
									   "\n [2] Grabar Empleado" +
									   "\n [3] Leer Empleado" +
									   "\n [4] Salir del Sistema.", 1, 4);

				switch (opcion)
				{
					case 1:
						DarAltaEmpleado();
						break;
					case 2:
						GrabarEmpleado();
						break;
					case 3:
						LeerEmpleado();
						break;
					case 4:

						break;

				}
			} while (opcion != 4);
		}

		public int BuscarEmpleadoLegajo(int legajo)
		{
			for (int i = 0; i < this._empleado.Count; i++)
			{
				if (this._empleado[i].Legajo == legajo)
				{
					return i;
				}
			}
			/* si no encuentro el producto retorno una posición invalida */
			return -1;
		}

		Dictionary<int, Empleado> empleadoLista = new Dictionary<int, Empleado>();


		protected override void DarAltaEmpleado()
		{

			int legajo;
			string nombre;
			string apellido;
			DateTime añoIngreso;

			DateTime fechaActual = DateTime.Now;

			string opcion;

			Console.Clear();
			legajo = Validador.PedirIntMenu(" Ingrese el legajo del Empleado" +
											  "\n El documento debe estar entre este rango.", 100000,999999);
			if (BuscarEmpleadoLegajo(legajo) == -1)
			{
				VerPersona();
				Console.WriteLine("\n ¡En hora buena! Puede utilizar este Código para crear un Nuevo Empleado");
				nombre = Validador.PedirCaracterString("\n Ingrese el nombre del Empleado", 0, 30);
				Console.Clear();
				apellido = Validador.PedirCaracterString("\n Ingrese el apellido del Empleado", 0, 30);
				añoIngreso = Validador.ValidarFechaIngresada("\n Ingrese el año que ingresa",fechaActual);
				

				opcion = ValidarSioNoPersonaNoCreada("\n Está seguro que desea crear este Empleado? ", nombre, apellido);


				if (opcion == "SI")
				{
					Empleado p = new Empleado(legajo,nombre,apellido,añoIngreso);
					AddPersona(p);
					empleadoLista.Add(legajo, p);
					VerPersona();
					VerPersonaDiccionario();
					Console.WriteLine("\n Empleado con Nombre *" + legajo + "* agregado exitósamente");
					Validador.VolverMenu();
				}
				else
				{
					VerPersona();
					Console.WriteLine("\n Como puede verificar no se creo ningún Empleado");
					Validador.VolverMenu();

				}

			}
			else
			{
				VerPersona();
				Console.WriteLine("\n Usted digitó el legajo *" + legajo + "*");
				Console.WriteLine("\n Ya existe un Empleado con ese legajo");
				Console.WriteLine("\n Será direccionado nuevamente al Menú para que lo realice correctamente");
				Validador.VolverMenu();

			}

		}

		public void AddPersona(Empleado persona)
		{
			this._empleado.Add(persona);
		}


		protected override void GrabarEmpleado()
		{
			using (var archivoLista = new FileStream("archivoLista.txt", FileMode.Create))
			{
				using (var archivoEscrituraAgenda = new StreamWriter(archivoLista))
				{
					foreach (var persona in empleadoLista.Values)
					{

						var linea =
									"\n Legajo del Empleado: " + persona.Legajo +
									"\n Nombre del Empleado: " + persona.Nombre +
									"\n Apellido del Empleado: " + persona.Apellido +
									"\n Antiguedad del Empleado: " + persona.Antiguedad +
									"\n Fecha de Ingreso del Empleado: " + persona.FechaIngreso;

						archivoEscrituraAgenda.WriteLine(linea);

					}

				}
			}
			VerPersona();
			Console.WriteLine("Se ha grabado los datos de los Empleados correctamente");
			Validador.VolverMenu();

		}

		protected override void LeerEmpleado()
		{
			Console.Clear();
			Console.WriteLine("\n Empleados: ");
			using (var archivoLista = new FileStream("archivoLista.txt", FileMode.Open))
			{
				using (var archivoLecturaAgenda = new StreamReader(archivoLista))
				{
					foreach (var persona in empleadoLista.Values)
					{


						Console.WriteLine(archivoLecturaAgenda.ReadToEnd());


					}

				}
			}
			Validador.VolverMenu();

		}


		protected string ValidarSioNoPersonaNoCreada(string mensaje, string marca, string modelo)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Si esta seguro de ello escriba *" + "si" + "* sin los asteriscos" +
									  "\n De lo contrario escriba " + "*" + "no" + "* sin los asteriscos";
			string mensajeError = "\n Por favor ingrese el valor solicitado y que no sea vacio. ";

			do
			{
				VerPersona();

				Console.WriteLine(
								  "\n Nombre Empleado: " + marca +
								  "\n Apellido Empleado: " + modelo);

				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeError);
				Console.WriteLine(mensajeValidador);
				opcion = Console.ReadLine().ToUpper();
				string opcionC = "SI";
				string opcionD = "NO";

				if (opcion == "" || (opcion != opcionC) & (opcion != opcionD))
				{
					continue;

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}


		protected string ValidarStringNoVacioNombre(string mensaje)
		{

			string opcion;
			bool valido = false;
			string mensajeValidador = "\n Por favor ingrese el valor solicitado y que no sea vacio.";


			do
			{
				VerPersona();
				Console.WriteLine(mensaje);
				Console.WriteLine(mensajeValidador);

				opcion = Console.ReadLine().ToUpper();

				if (opcion == "")
				{

					Console.Clear();
					Console.WriteLine("\n");
					Console.WriteLine(mensajeValidador);

				}
				else
				{
					valido = true;
				}

			} while (!valido);

			return opcion;
		}

		public void VerPersona()
		{
			Console.Clear();
			Console.WriteLine("\n Empleado");
			Console.WriteLine(" #\t\tLegajo.\t\tNombre.\t\tApellido.");
			for (int i = 0; i < Empleado.Count; i++)
			{
				Console.Write(" " + (i + 1));

				Console.Write("\t\t");
				Console.Write(Empleado[i].Legajo);
				Console.Write("\t\t");
				Console.Write(Empleado[i].Nombre);
				Console.Write("\t\t");
				Console.Write(Empleado[i].Apellido);
				Console.Write("\t\t");

				Console.Write("\n");
			}

		}

		public void VerPersonaDiccionario()
		{
			Console.WriteLine("\n Empleados en el Diccionario");
			for (int i = 0; i < empleadoLista.Count; i++)
			{
				KeyValuePair<int, Empleado> persona = empleadoLista.ElementAt(i);

				Console.WriteLine("\n Legajo: " + persona.Key);
				Empleado personaValor = persona.Value;

				
				Console.WriteLine(" Nombre del Empleado: " + personaValor.Nombre);
				Console.WriteLine(" Apellido del Empleado: " + personaValor.Apellido);
				Console.WriteLine(" Fecha de ingreso del Empleado " + personaValor.FechaIngreso);


			}


		}



	}
}
