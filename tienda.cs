using System;
using System.Collections.Generic;

namespace Tp2AAT {
    class Tienda {
        private List<Producto> listaProductos;
        private Carrito carrito;
        private int dineroEnCaja;

        public Tienda() {
            listaProductos = new List<Producto>();
            carrito = new Carrito();
            dineroEnCaja = 10000;
        }

        public void AgregarProducto(string nombre, int costo, int stock) {
            if (string.IsNullOrWhiteSpace(nombre)) {
                Console.WriteLine("El nombre del producto no puede estar vacío.");
                return;
            }
            if (costo <= 0) {
                Console.WriteLine("El costo debe ser mayor que cero.");
                return;
            }
            if (stock < 0) {
                Console.WriteLine("El stock no puede ser negativo.");
                return;
            }

            Producto nuevoProducto = new Producto(nombre, costo, stock);
            listaProductos.Add(nuevoProducto);
            Console.WriteLine($"Producto '{nombre}' agregado a la lista de productos.");
        }

        public void MostrarProductos() {
            Console.WriteLine("Lista de productos disponibles:");
            foreach (var producto in listaProductos) {
                Console.WriteLine($"{producto.Nombre} - Stock: {producto.Stock} - Precio Venta: {producto.PrecioVenta}");
            }
        }

        public Producto BuscarProducto(string nombre) {
            return listaProductos.Find(p => p.Nombre == nombre);
        }

        public void EliminarProducto(string nombre) {
            Producto producto = BuscarProducto(nombre);
            if (producto != null) {
                listaProductos.Remove(producto);
                Console.WriteLine($"Producto '{nombre}' eliminado de la lista de productos.");
            } else {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public Carrito ObtenerCarrito() {
            return carrito;
        }

        public void Cobrar() {
            int total = carrito.CalcularTotal();
            while (true) {
                Console.WriteLine($"Total a pagar: {total}");
                Console.Write("Ingrese la cantidad con la que el cliente va a pagar: ");
                if (!int.TryParse(Console.ReadLine(), out int pago)) {
                    Console.WriteLine("Cantidad no válida.");
                    continue;
                }
                if (pago >= total) {
                    int vuelto = pago - total;
                    if (vuelto > 0) {
                        dineroEnCaja -= vuelto;
                    }
                    dineroEnCaja += total;
                    Console.WriteLine($"Pago recibido: {pago}");
                    Console.WriteLine($"Vuelto: {vuelto}");
                    Console.WriteLine($"Dinero en caja: {dineroEnCaja}");
                    carrito.Vaciar();
                    break;
                } else {
                    Console.WriteLine("Pago insuficiente. Por favor, ingrese una nueva cantidad.");
                }
            }
        }

        public void MenuAgregarProducto() {
            Console.Write("Ingrese nombre del producto: ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre)) {
                Console.WriteLine("El nombre del producto no puede estar vacío.");
                return;
            }
            Console.Write("Ingrese costo del producto: ");
            if (!int.TryParse(Console.ReadLine(), out int costo) || costo <= 0) {
                Console.WriteLine("El costo debe ser un número mayor que cero.");
                return;
            }
            Console.Write("Ingrese stock inicial del producto: ");
            if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 0) {
                Console.WriteLine("El stock debe ser un número no negativo.");
                return;
            }
            AgregarProducto(nombre, costo, stock);
        }

        public void MenuEliminarProducto() {
            MostrarProductos();
            Console.Write("Ingrese el nombre del producto que desea borrar: ");
            string nombreProducto = Console.ReadLine();
            EliminarProducto(nombreProducto);
        }

        public void MenuAgregarAlCarrito() {
            MostrarProductos();
            Console.Write("Ingrese el nombre del producto que desea agregar al carrito: ");
            string nombreProducto = Console.ReadLine();
            Producto productoSeleccionado = BuscarProducto(nombreProducto);
            if (productoSeleccionado != null) {
                Console.Write("Ingrese la cantidad que desea agregar al carrito: ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0) {
                    Console.WriteLine("Cantidad no válida.");
                    return;
                }
                carrito.AgregarProducto(productoSeleccionado, cantidad);
            } else {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void MenuEliminarDelCarrito() {
            carrito.MostrarCarrito();
            Console.Write("Ingrese el nombre del producto que desea eliminar del carrito: ");
            string nombreProducto = Console.ReadLine();
            var itemCarrito = carrito.BuscarProducto(nombreProducto);
            if (itemCarrito != null) {
                carrito.EliminarProducto(itemCarrito.Producto);
            } else {
                Console.WriteLine("Producto no encontrado en el carrito.");
            }
        }

        public void MenuCobrar() {
            if (carrito.EstaVacio()) {
                Console.WriteLine("El carrito está vacío. No se puede cobrar.");
            } else {
                carrito.MostrarCarrito();
                Cobrar();
            }
        }
    }
}
