using System;
using System.Collections.Generic;

namespace Tp2AAT{
    class Producto {
        public string Nombre { get; private set; }
        public int Costo { get; private set; }
        public int PrecioVenta { get; private set; }
        public int Stock { get; set; }

        public Producto(string nombre, int costo, int stock) {
            Nombre = nombre;
            Costo = costo;
            Stock = stock;
            CalcularPrecioVenta();
        }

        private void CalcularPrecioVenta() {
            PrecioVenta = (int)Math.Round(Costo * 1.3);
        }

        public void CambiaStock(int cant) {
            Stock -= cant;
        }

        public void AumentarStock(int cant) {
            Stock += cant;
        }
    }
}