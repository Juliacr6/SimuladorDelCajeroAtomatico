using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorDelCajeroAtomatico
{
    internal class Program
    {
        static double saldo = 1000;

        // Lista global que guarda un registro de cada operación realizada
        static List<string> historialOperaciones = new List<string>();

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            bool activo = true;

            while (activo)
            {
                Console.WriteLine("\n===== CAJERO AUTOMÁTICO =====");
                Console.WriteLine("1. Consultar saldo");
                Console.WriteLine("2. Depositar");
                Console.WriteLine("3. Retirar");
                Console.WriteLine("4. Mostrar operaciones realizadas");
                Console.WriteLine("5. Salir");
                Console.Write("Elija una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ConsultarSaldo();
                        break;
                    case "2":
                        Depositar();
                        break;
                    case "3":
                        Retirar();
                        break;
                    case "4":
                        MostrarOperaciones();
                        break;
                    case "5":
                        activo = false;
                        Console.WriteLine("Gracias por usar el cajero. ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }

        // 1. Consultar saldo
        static void ConsultarSaldo()
        {
            Console.WriteLine("Su saldo actual es: S/" +saldo);
            historialOperaciones.Add("Consulta de saldo");
        }

        // 2. Depositar (con validación en bucle hasta ingresar un monto válido)
        static void Depositar()
        {
            double monto;
            do
            {
                Console.Write("Ingrese el monto a depositar: S/ ");
            } while (!double.TryParse(Console.ReadLine(), out monto) || monto <= 0);

            saldo += monto;
            Console.WriteLine("Depósito exitoso. Nuevo saldo: S/ "+saldo);
            historialOperaciones.Add("Depósito de S/:" +monto);
        }
        // 3. Retirar (con validación en bucle hasta ingresar un monto válido)
        static void Retirar()
        {
            double monto;
            do
            {
                Console.Write("Ingrese el monto a retirar: S/ ");
            } while (!double.TryParse(Console.ReadLine(), out monto) || monto <= 0);

            if (monto > saldo)
            {
                Console.WriteLine("Fondos insuficientes. No puede retirar más de su saldo.");
                historialOperaciones.Add("Intento de retiro fallido (S/ "+monto+ "fondos insuficientes");
            }
            else
            {
                saldo -= monto;
                Console.WriteLine("Retiro exitoso. Nuevo saldo: S/" +saldo);
                historialOperaciones.Add("Retiro de S/:" +monto);
            }
        }

        // 4. Mostrar operaciones: recorre y CUENTA los movimientos por tipo usando for
        static void MostrarOperaciones()
        {
            if (historialOperaciones.Count == 0)
            {
                Console.WriteLine("Aún no ha realizado ninguna operación.");
                return;
            }

            int contadorDepositos = 0;
            int contadorRetiros = 0;
            int contadorConsultas = 0;

            Console.WriteLine("\n===== MOVIMIENTOS REALIZADOS =====");

            for (int i = 0; i < historialOperaciones.Count; i++)
            {
                string operacion = historialOperaciones[i];
                Console.WriteLine((i + 1)+ "" +operacion);

                if (operacion.Contains("Depósito"))
                {
                    contadorDepositos++;
                }
                else if (operacion.Contains("Retiro"))
                {
                    contadorRetiros++;
                }
                else if (operacion.Contains("Consulta"))
                {
                    contadorConsultas++;
                }
            }

            Console.WriteLine("\n===== RESUMEN DE OPERACIONES =====");
            Console.WriteLine("Depósitos realizados: "+contadorDepositos);
            Console.WriteLine("Retiros realizados: "+contadorRetiros);
            Console.WriteLine("Consultas de saldo: "+contadorConsultas);
            Console.WriteLine("Total de operaciones: "+(contadorDepositos+contadorRetiros+contadorConsultas));

            Console.ReadKey();

        }
        
        
    }
}
