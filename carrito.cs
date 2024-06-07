using System;
using System.Collections.Generic;

namespace Tp2AAT {
    class ItemCarrito {
        public Producto Producto { get; private set; }
        public int Cantidad { get; set; }

        public ItemCarrito(Producto producto, int cantidad) {
            Producto = producto;
            Cantidad = cantidad;
        }
    }

    class Carrito {
        private List<ItemCarrito> items;

        public Carrito() {
            items = new List<ItemCarrito>();
        }

        public void AgregarProducto(Producto producto, int cant){
            if (cant <= producto.Stock){
                var item = items.Find(i => i.Producto.Nombre == producto.Nombre);
                if (item != null) {
                    item.Cantidad += cant;
                } else {
                    items.Add(new ItemCarrito(producto, cant));
                }
                producto.CambiaStock(cant);
                Console.WriteLine($"Producto '{producto.Nombre}' x '{cant}' agregado al carrito.");
                MostrarSubtotal();
            } else {
                Console.WriteLine($"No hay suficiente stock disponible. Stock disponible: {producto.Stock}");
                Console.Write("Ingrese una nueva cantidad: ");
                if (int.TryParse(Console.ReadLine(), out int nuevaCantidad)){
                    AgregarProducto(producto, nuevaCantidad);
                } else {
                    Console.WriteLine("Cantidad no válida. Producto no agregado al carrito.");
                }
            }
        }

        public void EliminarProducto(Producto producto) {
            var item = items.Find(i => i.Producto.Nombre == producto.Nombre);
            if (item != null) {
                items.Remove(item);
                producto.AumentarStock(item.Cantidad);
                Console.WriteLine($"Producto '{producto.Nombre}' eliminado del carrito.");
            } else {
                Console.WriteLine("Producto no encontrado en el carrito.");
            }
        }

        public void MostrarCarrito() {
            if (items.Count == 0) {
                Console.WriteLine("El carrito está vacío.");
                return;
            }
            Console.WriteLine("Productos en el carrito:");
            foreach (var item in items) {
                Console.WriteLine($"{item.Producto.Nombre} - Cantidad: {item.Cantidad} - Precio: {item.Producto.PrecioVenta}");
            }
            MostrarSubtotal();
        }

        public void MostrarSubtotal() {
            int subtotal = CalcularTotal();
            Console.WriteLine($"Subtotal: {subtotal}");
        }

        public int CalcularTotal() {
            int total = 0;
            foreach (var item in items) {
                total += item.Cantidad * item.Producto.PrecioVenta;
            }
            return total;
        }

        public void Vaciar() {
            items.Clear();
        }

        public ItemCarrito BuscarProducto(string nombre) {
            return items.Find(i => i.Producto.Nombre == nombre);
        }

        public bool EstaVacio() {
            return items.Count == 0;
        }
    }
}
