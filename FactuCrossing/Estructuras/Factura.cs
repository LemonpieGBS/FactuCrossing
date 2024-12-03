using System.Data;

namespace FactuCrossing.Estructuras
{
    public class ProductoFacturado
    {
        /// <summary>
        /// Constructor principal de la clase ProductoFacturado
        /// </summary>
        /// <param name="producto">Producto de la clase <seealso cref="Producto"/></param>
        /// <param name="cantidad">Cantidad a facturar</param>
        /// <param name="descuentoAplicado">Descuento aplicado de la clase <seealso cref="FactuCrossing.Estructuras.Descuento"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Algunos de los valores numéricos son 0 o negativos cuando no deberian de serlo</exception>
        /// <exception cref="ArgumentNullException">El nombre del producto es nulo o vacío</exception>
        /// <exception cref="ArgumentException">El descuento proveído no es aplicable al producto <seealso cref="Descuento.ProductoAplicable"/></exception>
        public ProductoFacturado(Producto producto, int cantidad, Descuento? descuentoAplicado = null)
        {
            // Validaciones
            // Validación de ID de inventario
            if (producto.Id < 0)
                // Lanzar excepción si el ID del inventario es negativo
                throw new ArgumentOutOfRangeException(nameof(producto.Id), "El ID del inventario no puede ser negativo");
            // Validación de nombre
            if (string.IsNullOrEmpty(producto.Nombre))
                // Lanzar excepción si el nombre es nulo o vacío
                throw new ArgumentNullException(nameof(producto.Nombre), "El nombre del producto no puede ser nulo o vacío");
            // Validación de nombre
            if (string.IsNullOrEmpty(producto.Proveedor))
                // Lanzar excepción si el nombre es nulo o vacío
                throw new ArgumentNullException(nameof(producto.Proveedor), "El nombre del producto no puede ser nulo o vacío");
            // Validación de precio
            if (producto.Precio < 0)
                // Lanzar excepción si el precio es negativo
                throw new ArgumentOutOfRangeException(nameof(producto.Precio), "El precio del producto no puede ser negativo");
            // Validación de cantidad
            if (cantidad <= 0)
                // Lanzar excepción si la cantidad es 0 o negativa
                throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad del producto no puede ser negativa o 0");

            IDenInventario = producto.Id;
            Nombre = producto.Nombre;
            Proveedor = producto.Proveedor;
            Precio = (double) producto.Precio;
            Cantidad = cantidad;

            // Validación de descuento aplicado
            // Si no hay descuento aplicado, asignar valores por defecto
            if (descuentoAplicado is null)
            {
                DescuentoPorcentaje = 0;
                DescuentoNombre = "";
            }
            // Si hay descuento aplicado
            else
            {
                // Validación de descuento aplicado
                if (descuentoAplicado.ProductoAplicable != IDenInventario)
                {
                    // Si el descuento no es aplicable al producto, lanzar excepción
                    throw new ArgumentException("El descuento aplicado no es aplicable a este producto");
                }
                // Asignar valores del descuento aplicado
                DescuentoPorcentaje = descuentoAplicado.Porcentaje;
                DescuentoNombre = descuentoAplicado.Nombre;
            }
        }
        /// <summary>
        /// Otro constructor pero sin la implementacion de la clase <seealso cref="Producto"/> o a la clase <seealso cref="Descuento"/>
        /// </summary>
        public ProductoFacturado(int id, string nombre, string proveedor, double precio, int cantidad, double descPorcentaje = 0, string descNombre = "")
        {
            // Validaciones
            // Validación de ID de inventario
            if (id < 0)
                // Lanzar excepción si el ID del inventario es negativo
                throw new ArgumentOutOfRangeException(nameof(id), "El ID del inventario no puede ser negativo");
            // Validación de nombre
            if (string.IsNullOrEmpty(nombre))
                // Lanzar excepción si el nombre es nulo o vacío
                throw new ArgumentNullException(nameof(nombre), "El nombre del producto no puede ser nulo o vacío");
            // Validación de nombre
            if (string.IsNullOrEmpty(proveedor))
                // Lanzar excepción si el nombre es nulo o vacío
                throw new ArgumentNullException(nameof(proveedor), "El nombre del producto no puede ser nulo o vacío");
            // Validación de precio
            if (precio < 0)
                // Lanzar excepción si el precio es negativo
                throw new ArgumentOutOfRangeException(nameof(precio), "El precio del producto no puede ser negativo");
            // Validación de cantidad
            if (cantidad <= 0)
                // Lanzar excepción si la cantidad es 0 o negativa
                throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad del producto no puede ser negativa o 0");
            IDenInventario = id;
            Nombre = nombre;
            Proveedor = proveedor;
            Precio = precio;
            Cantidad = cantidad;
            DescuentoPorcentaje = descPorcentaje;
            DescuentoNombre = descNombre;
        }
        /// <summary>
        /// Propiedad de solo lectura para el ID del inventario
        /// </summary>
        public int IDenInventario { get; init; }
        /// <summary>
        /// Propiedad de solo lectura para el nombre del producto
        /// </summary>
        public string Nombre { get; init; }
        /// <summary>
        /// Propiedad para el proveedor
        /// </summary>
        public string Proveedor { get; init; }
        /// <summary>
        /// Propiedad de solo lectura para el precio del producto
        /// </summary>
        public double Precio { get; init; }
        /// <summary>
        /// Propiedad interna para la cantidad del producto
        /// </summary>
        public int Cantidad { get; private set; }
        /// <summary>
        /// Propiedad interna para el descuento aplicado
        /// </summary>
        public double DescuentoPorcentaje { get; private set; } = 0;
        /// <summary>
        /// Propiedad interna para el nombre del descuento aplicado
        /// </summary>
        public string DescuentoNombre { get; private set; } = "";
        /// <summary>
        /// Propiedad de solo lectura para el subtotal del producto
        /// </summary>
        public double Subtotal => Precio * Cantidad;
        /// <summary>
        /// Propiedad de solo lectura para el descuento del producto
        /// </summary>
        public double Descuento => (Subtotal * (DescuentoPorcentaje / 100));
        /// <summary>
        /// Propiedad de solo lectura para el total del producto
        /// </summary>
        public double Total => Subtotal - Descuento;
        /// <summary>
        /// Método para cambiar la cantidad del producto
        /// </summary>
        /// <param name="cant">Cantidad a cambiar</param>
        public void CambiarCantidad(int cant)
        {
            // Validar la cantidad
            if (cant <= 0)
            {
                // Lanzar excepción si la cantidad es 0 o negativa
                throw new ArgumentOutOfRangeException(nameof(cant), "La cantidad del producto no puede ser negativa o 0");
            }
            // Asignar la nueva cantidad
            Cantidad = cant;
        }
        /// <summary>
        /// Método para asignar un descuento al producto
        /// </summary>
        /// <param name="desc">El descuento a aplicar</param>
        public void AsignarDescuento(Descuento desc)
        {
            // Validar descuento
            if (desc.ProductoAplicable != IDenInventario)
            {
                // Lanzar excepción si el descuento no es aplicable al producto
                throw new ArgumentException("El descuento no es aplicable a este producto");
            }

            // Asignar valores del descuento
            DescuentoPorcentaje = desc.Porcentaje;
            DescuentoNombre = desc.Nombre;
        }
    }


    public class Factura
    {
        /// <summary>
        /// Constructor principal de la clase Factura
        /// </summary>
        /// <param name="numFactura">Número de la factura (requerido)</param>
        /// <param name="facturista">Cuenta del facturista (requerido)</param>
        /// <param name="nombreFactura"></param>
        /// <param name="sucursal"></param>
        /// <exception cref="ArgumentOutOfRangeException">Lanzada si el número de factura es negativo</exception>
        /// <exception cref="ArgumentNullException">Lanzada si el facturista no se encuentra en el sistema</exception>"
        public Factura(int numFactura, Cuenta facturista, string? nombreFactura, string? sucursal)
        {
            // Validaciones
            // Validación de número de factura
            if (numFactura < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numFactura), "El número de factura no puede ser negativo");
            }
            // Validación de facturista
            if (SistemaCentral.Cuentas.CuentaEnMemoria(facturista) == -1)
            {
                throw new ArgumentNullException(nameof(facturista), "El facturista no es un personal activo del sistema");
            }
            NumFactura = numFactura;

            NumFacturista = facturista.Id;
            Facturista = facturista.NombreDisplay;

            NombreFactura = (string.IsNullOrEmpty(nombreFactura)) ? "Desconocido" : nombreFactura;
            Sucursal = (string.IsNullOrEmpty(sucursal)) ? "Desconocida" : sucursal;
        }

        /// <summary>
        /// Constructor de la clase Factura sin la implementación de <seealso cref="Cuenta"/>
        /// </summary>
        /// <param name="numFactura">Número de la factura (requerido)</param>
        /// <param name="facturista">Cuenta del facturista (requerido)</param>
        /// <param name="nombreFactura"></param>
        /// <param name="sucursal"></param>
        /// <exception cref="ArgumentOutOfRangeException">Lanzada si el número de factura es negativo</exception>
        /// <exception cref="ArgumentNullException">Lanzada si el facturista no se encuentra en el sistema</exception>"
        public Factura(int numFactura, int numFacturista, string nombreFacturista, string? nombreFactura, string? sucursal)
        {
            // Validaciones
            // Validación de número de factura
            if (numFactura < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numFactura), "El número de factura no puede ser negativo");
            }
            // Validación de facturista
            if (SistemaCentral.Cuentas.CuentaEnMemoria(numFacturista) == -1)
            {
                throw new ArgumentNullException(nameof(numFacturista), "El facturista no es un personal activo del sistema");
            }
            NumFactura = numFactura;

            NumFacturista = numFacturista;
            Facturista = nombreFacturista;

            NombreFactura = (string.IsNullOrEmpty(nombreFactura)) ? "Desconocido" : nombreFactura;
            Sucursal = (string.IsNullOrEmpty(sucursal)) ? "Desconocida" : sucursal;
        }

        /// <summary>
        /// Constructor a la hora de cargar una factura
        /// </summary>
        /// <param name="numFactura"></param>
        /// <param name="facturista"></param>
        /// <param name="nombreFactura"></param>
        /// <param name="sucursal"></param>
        /// <param name="productosFacturados"></param>
        /// <param name="descuentosAplicados"></param>
        /// <param name="descuentoGlobal"></param>
        /// <param name="fechaFactura"></param>
        public Factura(int numFactura, int numFacturista, string nombreFacturista, string nombreFactura, string sucursal,
            List<ProductoFacturado> productosFacturados, List<Descuento> descuentosAplicados, Descuento? descuentoGlobal,
            DateTime fechaFactura) : this(numFactura, numFacturista, nombreFacturista, nombreFactura, sucursal)
        {
            // Validaciones
            // Validación de productos facturados
            if (productosFacturados is null || productosFacturados.Count == 0)
            {
                throw new ArgumentNullException(nameof(productosFacturados), "La lista de productos facturados no puede ser nula o vacía");
            }
            // Validación de descuentos aplicados
            if (descuentosAplicados is null)
            {
                throw new ArgumentNullException(nameof(descuentosAplicados), "La lista de descuentos aplicados no puede ser nula");
            }
            // Asignamos los valores
            ProductosFacturados = productosFacturados;
            DescuentosAplicados = descuentosAplicados;
            DescuentoGlobal = descuentoGlobal;
            FechaFactura = fechaFactura;
        }

        /// <summary>
        /// Propiedad de solo lectura para el número de factura
        /// </summary>
        public int NumFactura { get; init; }
        /// <summary>
        /// Propiedad de solo lectura para el número del facturista (en el sistema de cuentas)
        /// </summary>
        public int NumFacturista { get; init; }
        /// <summary>
        /// Nombre del facturista
        /// </summary>
        public string Facturista { get; init; }
        /// <summary>
        /// Nombre de la factura
        /// </summary>
        public string NombreFactura { get; set; }
        /// <summary>
        /// Sucursal en la que se realizó la factura
        /// </summary>
        public string Sucursal { get; set; }
        /// <summary>
        /// Lista de productos facturados
        /// </summary>
        public List<ProductoFacturado> ProductosFacturados { get; private set; } = new List<ProductoFacturado>();
        /// <summary>
        /// Lista de descuentos aplicados
        /// </summary>
        public List<Descuento> DescuentosAplicados { get; set; } = new List<Descuento>();
        /// <summary>
        /// Descuento global aplicado a la factura
        /// </summary>
        public Descuento? DescuentoGlobal { get; private set; } = null;
        /// <summary>
        /// Fecha de la factura
        /// </summary>
        public DateTime FechaFactura { get; set; }
        /// <summary>
        /// Propiedad de solo lectura para el total de la factura
        /// </summary>
        public double Total { get; private set; } = 0;
        /// <summary>
        /// Propiedad de solo lectura para el subtotal de la factura
        /// </summary>
        public double Subtotal { get; private set; } = 0;
        /// <summary>
        /// Propiedad de solo lectura para el descuento total de la factura
        /// </summary>
        public double Descuento { get; private set; } = 0;
        /// <summary>
        /// Método para agregar un producto facturado a la factura
        /// </summary>
        /// <param name="producto">Producto a agregar a la factura</param>
        public void AgregarProductoFacturado(Producto producto, int cantidad)
        {
            // Validar que el producto no esté ya en la factura
            if (ProductosFacturados.Any(p => p.IDenInventario == producto.Id))
            {
                // Si el producto ya está en la factura, actualizar la cantidad
                ProductoFacturado productoExistente = ProductosFacturados.First(p => p.IDenInventario == producto.Id);
                // Cambiar la cantidad del producto
                productoExistente.CambiarCantidad(cantidad);
                return;
            }
            // Agregar el producto a la factura si no está ya
            ProductosFacturados.Add(new ProductoFacturado(producto, cantidad));
        }
        /// <summary>
        /// Método para remover un producto facturado de la factura dado un ID
        /// </summary>
        /// <param name="Id">Id del producto a buscar</param>
        /// <exception cref="ArgumentException">Dar si el producto no está en la factura</exception>
        /// <exception cref="ArgumentOutOfRangeException">Dar si el ID es negativo</exception>
        public void RemoverProductoFacturado(int Id)
        {
            // Validar que el id dado no sea negativo
            if (Id < 0)
            {
                // Lanzar excepción si el ID es negativo
                throw new ArgumentOutOfRangeException(nameof(Id), "El ID del producto no puede ser negativo");
            }
            // Validar que el producto esté en la factura
            if (!ProductosFacturados.Any(p => p.IDenInventario == Id))
            {
                // Si el producto no está en la factura, lanzar excepción
                throw new ArgumentException("El producto no está en la factura");
            }
            // Remover el producto de la factura
            ProductosFacturados.Remove(ProductosFacturados.First(p => p.IDenInventario == Id));
        }

        /// <summary>
        /// Método para actualizar los descuentos de los productos facturados
        /// </summary>
        public void ActualizarDescuentosProductos()
        {
            // Validar que haya descuentos aplicados
            List<Descuento> descuentosRestantes = DescuentosAplicados;
            // Iterar por cada producto facturado
            foreach (ProductoFacturado producto in ProductosFacturados)
            {
                // Validar que haya descuentos restantes
                if (descuentosRestantes.Count <= 0) break;
                // Iterar por cada descuento restante
                foreach (Descuento descuento in descuentosRestantes)
                {
                    // Validar que el descuento sea aplicable al producto
                    if (descuento.ProductoAplicable == producto.IDenInventario)
                    {
                        // Aplicar el descuento al producto
                        producto.AsignarDescuento(descuento);
                        // Remover el descuento de la lista de descuentos restantes
                        descuentosRestantes.Remove(descuento);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Método para agregar un descuento a la factura
        /// </summary>
        public void AgregarDescuento(Descuento descuento)
        {
            // Validaciones
            // Validar que el descuento no esté ya en la factura
            if (DescuentosAplicados.Any(d => d.Id == descuento.Id))
            {
                // Si el descuento ya está en la factura, no hacer nada
                return;
            }
            // Comprobar que el descuento sea aplicable a algún producto
            if (descuento.ProductoAplicable == -1)
            {
                // Si el descuento es aplicable a todos los productos, no hacer nada
                return;
            }
            // Si el producto aplicable ya está en la factura, sobreescribir el descuento
            if (ProductosFacturados.Any(p => p.IDenInventario == descuento.ProductoAplicable))
            {
                // Obtener el producto al que se le aplicará el descuento
                ProductoFacturado producto = ProductosFacturados.First(p => p.IDenInventario == descuento.ProductoAplicable);
                // Asignar el descuento al producto
                producto.AsignarDescuento(descuento);
            }   
            // Agregar el descuento a la factura
            DescuentosAplicados.Add(descuento);
        }

        /// <summary>
        /// Método para remover un descuento de la factura
        /// </summary>
        public void RemoverDescuento(Descuento descuento)
        {
            // Validaciones
            // Validar que el descuento esté en la factura
            if (!DescuentosAplicados.Any(d => d.Id == descuento.Id))
            {
                // Si el descuento no está en la factura, no hacer nada
                return;
            }
            // Remover el descuento de la factura
            DescuentosAplicados.Remove(descuento);
        }

        /// <summary>
        /// Método para establecer el descuento global de la factura
        /// </summary>
        /// <param name="descuento">Descuento a aplicar</param>
        /// <exception cref="ArgumentNullException">El descuento no puede ser nulo</exception>
        public void EstablecerDescuentoGlobal(Descuento descuento)
        {
            // Validaciones
            // Validar que el descuento global no sea nulo
            if (descuento is null)
            {
                // Si el descuento global es nulo, lanzar excepción
                throw new ArgumentNullException(nameof(descuento), "El descuento global no puede ser nulo");
            }
            // Asignar el descuento global
            DescuentoGlobal = descuento;
        }

        /// <summary>
        /// Método para remover el descuento global de la factura
        /// </summary>
        public void RemoverDescuentoGlobal()
        {
            // Remover el descuento global
            DescuentoGlobal = null;
        }

        /// <summary>
        /// Método para calcular los totales de la factura
        /// </summary>
        public void CalcularTotales()
        {
            // Aca se van a calcular totales
            Subtotal = 0;
            Total = 0;
            Descuento = 0;
            // Actualizamos los descuentos de los productos
            ActualizarDescuentosProductos();

            // Iterar por cada producto facturado
            foreach (ProductoFacturado producto in ProductosFacturados)
            {
                // Se agrega el costo del producto al subtotal
                Subtotal += producto.Subtotal;
                // El precio descontado se agrega al descuento
                Descuento += producto.Subtotal * (producto.DescuentoPorcentaje / 100);
            }

            // Si hay un descuento global, se aplica
            if (DescuentoGlobal is not null)
            {
                Descuento += Subtotal * (DescuentoGlobal.Porcentaje / 100);
            }

            // Ahora el total es el subtotal menos el descuento
            Total = Subtotal - Descuento;
        } 
        /// <summary>
        /// Método para convertir la factura a un DataTable
        /// </summary>
        /// <returns>DataTable con los productos facturados</returns>
        public DataTable ToReportDataTable()
        {
            // Actualizamos descuentos de productos
            ActualizarDescuentosProductos();
            // Crear un DataTable
            DataSets.DsVenta.DtVentaDataTable dtVentas = new();
            // Iterar por cada producto facturado
            foreach (ProductoFacturado producto in ProductosFacturados)
            {
                // Agregar una fila al DataTable con los datos del producto
                dtVentas.AddDtVentaRow(producto.Nombre, producto.Proveedor, producto.Cantidad, producto.Precio,
                    producto.Subtotal, producto.Descuento, producto.Total);
            }
            // Retornar el DataTable
            return (DataTable)dtVentas;
        }

        /// <summary>
        /// Método para convertir la factura a un DataTable
        /// </summary>
        /// <returns>DataTable con los productos facturados</returns>
        public DataTable ToDataTable()
        {
            // Actualizamos descuentos de productos
            ActualizarDescuentosProductos();
            // Crear un DataTable
            DataTable dtVentas = new();
            // Crear las columnas
            dtVentas.Columns.AddRange(new DataColumn[]
            {
                new("Nombre"),
                new("Proveedor"),
                new("Cantidad"),
                new("Precio"),
                new("Subtotal"),
                new("Descuento"),
                new("Total")
            });
            // Iterar por cada producto facturado
            foreach (ProductoFacturado producto in ProductosFacturados)
            {
                // Agregar una fila al DataTable con los datos del producto
                dtVentas.Rows.Add(new object[]{producto.Nombre, producto.Proveedor, producto.Cantidad, $"{producto.Precio:0.00}$",
                    $"{producto.Subtotal:0.00}$", $"{producto.Descuento:0.00}", $"{producto.Total:0.00}$"});
            }
            // Retornar el DataTable
            return (DataTable)dtVentas;
        }
    }
}
