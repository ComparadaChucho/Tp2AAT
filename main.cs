using System;

namespace Tp2AAT {
    class Program {
        public static void Main(string[] args) {
            Tienda miTienda = new Tienda();
            Console.WriteLine("(La caja de la tienda inicia con un valor de $10000)");

            while (true) {
                Console.WriteLine("\nTienda:");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Borrar producto");
                Console.WriteLine("3. Lista de productos");
                Console.WriteLine("4. Agregar al carrito");
                Console.WriteLine("5. Eliminar del carrito");
                Console.WriteLine("6. Mostrar carrito (subtotal)");
                Console.WriteLine("7. Cobrar");
                Console.WriteLine("8. Salir");
                Console.WriteLine("Eliga una opcion: ");

                int entrada;
                if (!int.TryParse(Console.ReadLine(), out entrada)) {
                    Console.WriteLine("Entrada no valida. Intentalo de nuevo.");
                    continue;
                }

                switch (entrada) {
                    case 1:
                        miTienda.MenuAgregarProducto();
                        break;
                    case 2:
                        miTienda.MenuEliminarProducto();
                        break;
                    case 3:
                        miTienda.MostrarProductos();
                        break;
                    case 4:
                        miTienda.MenuAgregarAlCarrito();
                        break;
                    case 5:
                        miTienda.MenuEliminarDelCarrito();
                        break;
                    case 6:
                        miTienda.ObtenerCarrito().MostrarCarrito();
                        break;
                    case 7:
                        miTienda.MenuCobrar();
                        break;
                    case 8:
                        Console.WriteLine("Chau, que tenga buen dia!");
                        return;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        break;
                }
            }
        }
    }
}
