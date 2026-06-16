using System;
using System.Collections.Generic;
using System.Text;
using CitasApp.Domain.Interfaces;

namespace CitasApp.Application.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public double Sumar(double a, double b) => a + b;
        public double Restar(double a, double b) => a - b;
        public double Multiplicar(double a, double b) => a * b;
        public double Dividir(double a, double b) => b != 0 ? a / b : throw new DivideByZeroException();
    }
}