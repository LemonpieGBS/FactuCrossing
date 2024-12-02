using FactuCrossing.Estructuras;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Globalization;
using static FactuCrossing.SistemaCentral;

namespace FactuCrossing.Formularios.Administrador
{
    public partial class MenuAdministrador : Form
    {
        private enum OpcionesDeMuestreo
        {
            HOY,
            MESSELECCIONADO,
            FECHAESPECIFICA,
            RANGODEFECHAS,
            HISTORICO
        }

        private enum Orden
        {
            RESUMEN,
            CRONOLOGICO
        }

        private OpcionesDeMuestreo opcionDeMuestreo = OpcionesDeMuestreo.HOY;
        private Cuenta? filtroPersonal = null; // Si es null se muestra todo el personal
        private Orden ordenDeFiltro = Orden.RESUMEN;

        /// <summary>
        /// Lista de accesos por cuenta
        /// </summary>
        private List<Tuple<int, int, DateTime>> accesosPorCuenta = new List<Tuple<int, int, DateTime>>() { };

        /// <summary>
        /// Información de fecha
        /// </summary>
        private int mes = DateTime.Now.Month;
        private int año = DateTime.Now.Year;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public MenuAdministrador()
        {
            // Inicializamos los componentes del formulario
            InitializeComponent();
            // Aplicamos el estilo de fuente del programa
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            // Actualizamos el data grid
            ActualizarDataGrid();
            // Actualizamos los labels
            ActualizarLabels();
            // Actualizamos los tiempos de sesion
            SistemaCentral.Cuentas.CalcularTiempoDeSesion();
            // Actualizamos combobox
            ActualizarComboBox();
        }

        /// <summary>
        /// Método para actualizar el combobox
        /// </summary>
        private void ActualizarComboBox()
        {
            // Limpiamos el combobox
            cmbPersonal.Items.Clear();
            // Añadimos los nombres de los usuarios
            foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
            {
                cmbPersonal.Items.Add(cuenta.NombreDisplay);
            }
        }

        /// <summary>
        /// Método para calcular los accesos por cuenta
        /// </summary>
        private void CalcularAccesosPorCuenta()
        {
            // Limpiamos la lista
            accesosPorCuenta.Clear();
            // Recorremos los accesos
            foreach (Acceso acceso in FiltrarAccesos())
            {
                // Variable por si encontramos el índice
                bool indiceEncontrado = false;
                // Buscamos si la cuenta ya está en la lista
                for (int i = accesosPorCuenta.Count - 1; i >= 0; i--)
                {
                    // Variable para hacer todo más ameno
                    Tuple<int, int, DateTime> tupleIdAcceso = accesosPorCuenta[i];
                    // Si si está, actualizamos el indice a editar
                    if (tupleIdAcceso.Item1 == acceso.IdDeCuenta)
                    {
                        // Revisamos la fecha
                        DateTime masReciente = DateTime.Compare(tupleIdAcceso.Item3, acceso.TiempoDeAcceso) < 0
                            ? acceso.TiempoDeAcceso : tupleIdAcceso.Item3;
                        // Asignamos el índice
                        accesosPorCuenta[i] =
                            new Tuple<int, int, DateTime>(tupleIdAcceso.Item1, tupleIdAcceso.Item2 + 1, masReciente);
                        // Marcamos el índice como encontrado
                        indiceEncontrado = true;
                        break;
                    }
                }

                // Si no se encontró el indice
                if (!indiceEncontrado)
                {
                    // Agregamos la entrada
                    accesosPorCuenta.Add(new Tuple<int, int, DateTime>(acceso.IdDeCuenta, 1, acceso.TiempoDeAcceso));
                }
            }
        }

        /// <summary>
        /// Método para filtrar los accesos
        /// </summary>
        /// <returns>Lista con los accesos en el filtro</returns>
        private List<Acceso> FiltrarAccesos()
        {
            // Crear la lista de retorno
            List<Acceso> accesosFiltrados = SistemaCentral.Accesos.accesosEnMemoria;

            // Filtrar por personal
            if (filtroPersonal != null)
            {
                accesosFiltrados = accesosFiltrados.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
            }

            // Filtrar por datos de muestreo
            DateTime hoy = DateTime.Today;
            switch (opcionDeMuestreo)
            {
                case OpcionesDeMuestreo.HOY:
                    accesosFiltrados = accesosFiltrados.Where(a => a.TiempoDeAcceso.Date == hoy).ToList();
                    break;
                case OpcionesDeMuestreo.MESSELECCIONADO:
                    accesosFiltrados = accesosFiltrados.Where(a => a.TiempoDeAcceso.Year == año && a.TiempoDeAcceso.Month == mes).ToList();
                    break;
                case OpcionesDeMuestreo.FECHAESPECIFICA:
                    DateTime fechaEspecifica = dtpEspecifica.Value.Date;
                    accesosFiltrados = accesosFiltrados.Where(a => a.TiempoDeAcceso.Date == fechaEspecifica).ToList();
                    break;
                case OpcionesDeMuestreo.RANGODEFECHAS:
                    DateTime fechaInicio = dtpFecha1.Value.Date;
                    DateTime fechaFin = dtpFecha2.Value.Date;
                    accesosFiltrados = accesosFiltrados.Where(a => a.TiempoDeAcceso.Date >= fechaInicio && a.TiempoDeAcceso.Date <= fechaFin).ToList();
                    break;
                case OpcionesDeMuestreo.HISTORICO:
                    // No se aplica filtro adicional
                    break;
            }

            return accesosFiltrados;
        }

        /// <summary>
        /// Método para filtrar los tiempos de sesión
        /// </summary>
        /// <param name="cuenta">Cuenta a la que filtrar</param>
        /// <returns>Cantidad de segundos filtrados</returns>
        private TiempoSesion FiltrarTiemposDeSesion(Cuenta cuenta)
        {
            // Conseguir los tiempos de la cuenta
            TiempoSesion ts = cuenta.TiempoSesion;

            // Crear la lista de retorno
            Dictionary<DateTime, double> tiemposFiltrados = new Dictionary<DateTime, double>();

            // Función pequeña para filtrar los tiempos
            void AgregarSiExiste(Dictionary<DateTime, double> lista, DateTime dt)
            {
                if (ts.tiempoPorDia.TryGetValue(dt.Date, out double segundos))
                {
                    lista.Add(dt.Date, segundos);
                }
            }

            // Filtrar por datos de muestreo
            DateTime hoy = DateTime.Today;
            switch (opcionDeMuestreo)
            {
                case OpcionesDeMuestreo.HOY:
                    // Agregar hoy si existe
                    AgregarSiExiste(tiemposFiltrados, DateTime.Now);
                    break;

                case OpcionesDeMuestreo.MESSELECCIONADO:
                    {
                        // Conseguir las fechas del mes
                        List<DateTime> dateTimes = DateHelper.GetDates(año, mes);
                        // Ciclar para ver si existen
                        foreach (DateTime dateTime in dateTimes)
                        {
                            AgregarSiExiste(tiemposFiltrados, dateTime);
                        }
                    }
                    break;

                case OpcionesDeMuestreo.FECHAESPECIFICA:
                    // Agregar hoy si existe
                    AgregarSiExiste(tiemposFiltrados, dtpEspecifica.Value);
                    break;

                case OpcionesDeMuestreo.RANGODEFECHAS:
                    {
                        DateTime fechaInicio = dtpFecha1.Value;
                        DateTime fechaFin = dtpFecha2.Value;

                        // Conseguir las fechas del mes
                        List<DateTime> dateTimes = DateHelper.GetDates(fechaInicio, fechaFin);
                        // Ciclar para ver si existen
                        foreach (DateTime dateTime in dateTimes)
                        {
                            AgregarSiExiste(tiemposFiltrados, dateTime);
                        }
                    }
                    break;

                case OpcionesDeMuestreo.HISTORICO:
                    {
                        // Agregar todos los tiempos
                        foreach (KeyValuePair<DateTime, double> kvp in ts.tiempoPorDia)
                        {
                            tiemposFiltrados.Add(kvp.Key, kvp.Value);
                        }
                    }
                    break;
            }

            // Retornar el tiempo de sesión filtrado
            return new TiempoSesion(tiemposFiltrados);
        }

        /// <summary>
        /// Método para filtrar las acciones
        /// </summary>
        /// <returns>Lista de acciones como filtrar</returns>
        private List<Accion> FiltrarAcciones()
        {
            List<Accion> accionesFiltradas = SistemaCentral.Acciones.accionesEnMemoria;

            // Filtrar por personal
            if (filtroPersonal != null)
            {
                accionesFiltradas = accionesFiltradas.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
            }

            // Filtrar por datos de muestreo
            DateTime hoy = DateTime.Today;
            switch (opcionDeMuestreo)
            {
                case OpcionesDeMuestreo.HOY:
                    accionesFiltradas = accionesFiltradas.Where(a => a.TiempoDeAccion.Date == hoy.Date).ToList();
                    break;
                case OpcionesDeMuestreo.MESSELECCIONADO:
                    // DateTime mesSeleccionado = dtpEspecifica.Value;
                    accionesFiltradas = accionesFiltradas.Where(a => a.TiempoDeAccion.Year == año && a.TiempoDeAccion.Month == mes).ToList();
                    break;
                case OpcionesDeMuestreo.FECHAESPECIFICA:
                    DateTime fechaEspecifica = dtpEspecifica.Value.Date;
                    accionesFiltradas = accionesFiltradas.Where(a => a.TiempoDeAccion.Date == fechaEspecifica).ToList();
                    break;
                case OpcionesDeMuestreo.RANGODEFECHAS:
                    DateTime fechaInicio = dtpFecha1.Value.Date;
                    DateTime fechaFin = dtpFecha2.Value.Date;
                    accionesFiltradas = accionesFiltradas.Where(a => a.TiempoDeAccion.Date >= fechaInicio && a.TiempoDeAccion.Date <= fechaFin).ToList();
                    break;
                case OpcionesDeMuestreo.HISTORICO:
                    // No se aplica filtro adicional
                    break;
            }

            return accionesFiltradas;
        }

        /// <summary>
        /// Método para actualizar el DataGrid con los productos facturados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActualizarDataGrid()
        {
            // Calculamos los accesos por cuenta
            // CalcularAccesosPorCuenta();
            // Limpiamos el DataGrid
            dgvAccesos.DataSource = null;
            // Creamos un DataTable
            DataTable dt = new();
            // Conseguimos las acciones filtradas
            List<Acceso> accesosFiltrados = FiltrarAccesos();
            List<Accion> accionesFiltradas = FiltrarAcciones();

            if (ordenDeFiltro == Orden.RESUMEN)
            {
                // Generamos el data table
                // Añadimos las columnas
                dt.Columns.AddRange(new DataColumn[] { new("Usuario"), new("# de Accesos"), new("Tiempo en Sesión"), new DataColumn("# de Acciones"),
                new("Ultimo Acceso (Fecha)"), new("Ultimo Acceso (Hora)") });
                // Añadimos las filas
                foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
                {
                    // Saltamos si hay un filtro de personal y esta cuenta no es el personal seleccionado
                    if (filtroPersonal is not null) { if (cuenta.Id != filtroPersonal.Id) continue; }

                    // Cargamos la fecha
                    DateTime ultimoAcceso;
                    // Accesos de la cuenta
                    int accesosDeCuenta = accesosFiltrados.Where(a => a.IdDeCuenta == cuenta.Id).Count();
                    // Calculamos el ultimo acceso
                    if (accesosDeCuenta > 0)
                    {
                        // Conseguimos el último acceso
                        ultimoAcceso = accesosFiltrados.Where(a => a.IdDeCuenta == cuenta.Id).Max(a => a.TiempoDeAcceso);
                    }
                    // Si no, retornamos
                    else continue;

                    // Conseguimos la cantidad de tiempo
                    double segundos = FiltrarTiemposDeSesion(cuenta).ObtenerTiempo();
                    // TimeSpan
                    TimeSpan tiempo = TimeSpan.FromSeconds(segundos);
                    // # de Acciones
                    int acciones = accionesFiltradas.Where(a => a.IdDeCuenta == cuenta.Id).Count();
                    // Añadimos la fila
                    dt.Rows.Add(new object[] { cuenta.NombreDisplay, accesosDeCuenta, tiempo.ToString(@"hh\:mm\:ss"), acciones,
                        ultimoAcceso.ToString("yyyy-MM-dd"), ultimoAcceso.ToString("hh:mm tt")});
                }

                // Asignamos el DataTable al DataGrid
                dgvAccesos.DataSource = dt;
            }
            else if (ordenDeFiltro == Orden.CRONOLOGICO)
            {
                // Añadimos las columnas
                dt.Columns.AddRange(new DataColumn[] { new("Tiempo"), new("Usuario"), new("Fecha"), new("Hora"), new("Evento"), new("Detalle") });

                List<EventoGenerico> eventosCronologicos = new List<EventoGenerico>();

                // Combinamos accesos, acciones y tiempos de sesión en una lista de eventos
                foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
                {
                    // Saltamos si hay un filtro de personal y esta cuenta no es el personal seleccionado
                    if (filtroPersonal is not null) { if (cuenta.Id != filtroPersonal.Id) continue; }

                    // Añadimos los timespans
                    foreach (KeyValuePair<DateTime, double> pareja in FiltrarTiemposDeSesion(cuenta).tiempoPorDia)
                    {
                        eventosCronologicos.Add(new EventoGenerico(cuenta.NombreDisplay, pareja.Key,
                            "Tiempo de Sesión", "", (TimeSpan.FromSeconds(pareja.Value)).ToString(@"hh\:mm\:ss")));
                    }
                }

                // Si hay un filtro personal filtramos los accesos y las acciones
                if (filtroPersonal is not null)
                {
                    accesosFiltrados = accesosFiltrados.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
                    accionesFiltradas = accionesFiltradas.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
                }

                // Ahora los agregamos a los eventos genericos
                foreach (Acceso acceso in accesosFiltrados)
                {
                    Cuenta? cuentaAcceso = SistemaCentral.Cuentas.ObtenerCuentaPorId(acceso.IdDeCuenta);
                    if (cuentaAcceso is null)
                    {
                        eventosCronologicos.Add(new EventoGenerico("Desconocido", acceso.TiempoDeAcceso, "Acceso", "El usuario entró", ""));
                        continue;
                    }
                    eventosCronologicos.Add(new EventoGenerico(cuentaAcceso.NombreDisplay, acceso.TiempoDeAcceso, "Acceso", "El usuario entró", ""));
                }

                foreach (Accion accion in accionesFiltradas)
                {
                    Cuenta? cuentaAccion = SistemaCentral.Cuentas.ObtenerCuentaPorId(accion.IdDeCuenta);
                    if (cuentaAccion is null)
                    {
                        eventosCronologicos.Add(new EventoGenerico("Desconocido", accion.TiempoDeAccion, "Accion", accion.Mensaje, ""));
                        continue;
                    }
                    eventosCronologicos.Add(new EventoGenerico(cuentaAccion.NombreDisplay, accion.TiempoDeAccion, "Accion", accion.Mensaje, ""));
                }

                // Ahora ordenamos
                eventosCronologicos = eventosCronologicos.OrderBy(e => e.Tiempo).ToList();

                // Y ahora lo añadimos al DataTable
                foreach (EventoGenerico evento in eventosCronologicos)
                {
                    dt.Rows.Add(new object[] { evento.TiempoSesion, evento.Accionario, evento.Tiempo.ToString("yyyy-MM-dd"), evento.Tiempo.ToString("hh:mm tt"),
                        evento.Tipo, evento.Mensaje });
                }

                // Asignamos el DataTable al DataGrid
                dgvAccesos.DataSource = dt;

                DataGridViewColumn column = dgvAccesos.Columns[5];
                column.Width = 200;
            }
            
        }

        private void GenerarReporte()
        {
            // Calculamos los accesos por cuenta
            // CalcularAccesosPorCuenta();

            // Creamos un DataTable
            DataTable dt = new();
            // Conseguimos las acciones filtradas
            List<Acceso> accesosFiltrados = FiltrarAccesos();
            List<Accion> accionesFiltradas = FiltrarAcciones();
            ReportDataSource rds = new ReportDataSource();
            string embeddedPath = "";

            if (ordenDeFiltro == Orden.RESUMEN)
            {
                // Generamos el data table
                // Añadimos las columnas
                dt.Columns.AddRange(new DataColumn[] { new("Usuario"), new("Accesos"), new("TiempoSesion"), new DataColumn("Acciones"),
                new("UltimaFecha"), new("UltimaHora") });
                // Añadimos las filas
                foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
                {
                    // Saltamos si hay un filtro de personal y esta cuenta no es el personal seleccionado
                    if (filtroPersonal is not null) { if (cuenta.Id != filtroPersonal.Id) continue; }

                    // Cargamos la fecha
                    DateTime ultimoAcceso;
                    // Accesos de la cuenta
                    int accesosDeCuenta = accesosFiltrados.Where(a => a.IdDeCuenta == cuenta.Id).Count();
                    // Calculamos el ultimo acceso
                    if (accesosDeCuenta > 0)
                    {
                        // Conseguimos el último acceso
                        ultimoAcceso = accesosFiltrados.Where(a => a.IdDeCuenta == cuenta.Id).Max(a => a.TiempoDeAcceso);
                    }
                    // Si no, retornamos
                    else continue;

                    // Conseguimos la cantidad de tiempo
                    double segundos = FiltrarTiemposDeSesion(cuenta).ObtenerTiempo();
                    // TimeSpan
                    TimeSpan tiempo = TimeSpan.FromSeconds(segundos);
                    // # de Acciones
                    int acciones = accionesFiltradas.Where(a => a.IdDeCuenta == cuenta.Id).Count();
                    // Añadimos la fila
                    dt.Rows.Add(new object[] { cuenta.NombreDisplay, accesosDeCuenta, tiempo, acciones,
                        ultimoAcceso.ToString("yyyy-MM-dd"), ultimoAcceso.ToString("hh:mm tt")});
                }
                embeddedPath = "FactuCrossing.Reportes.rptAdmin1.rdlc";
                rds = new ReportDataSource("DsInfo", dt);

            } else if(ordenDeFiltro == Orden.CRONOLOGICO)
            {
                // Añadimos las columnas
                dt.Columns.AddRange(new DataColumn[] { new("Tiempo"), new("Usuario"), new("Fecha"), new("Hora"), new("Evento"), new("Detalle") });

                List<EventoGenerico> eventosCronologicos = new List<EventoGenerico>();

                // Combinamos accesos, acciones y tiempos de sesión en una lista de eventos
                foreach (Cuenta cuenta in SistemaCentral.Cuentas.cuentasEnMemoria)
                {
                    // Saltamos si hay un filtro de personal y esta cuenta no es el personal seleccionado
                    if (filtroPersonal is not null) { if (cuenta.Id != filtroPersonal.Id) continue; }

                    // Añadimos los timespans
                    foreach (KeyValuePair<DateTime, double> pareja in FiltrarTiemposDeSesion(cuenta).tiempoPorDia)
                    {
                        eventosCronologicos.Add(new EventoGenerico(cuenta.NombreDisplay, pareja.Key,
                            "Tiempo de Sesión", "", (TimeSpan.FromSeconds(pareja.Value)).ToString(@"hh\:mm\:ss")));
                    }
                }

                // Si hay un filtro personal filtramos los accesos y las acciones
                if (filtroPersonal is not null)
                {
                    accesosFiltrados = accesosFiltrados.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
                    accionesFiltradas = accionesFiltradas.Where(a => a.IdDeCuenta == filtroPersonal.Id).ToList();
                }

                // Ahora los agregamos a los eventos genericos
                foreach (Acceso acceso in accesosFiltrados)
                {
                    Cuenta? cuentaAcceso = SistemaCentral.Cuentas.ObtenerCuentaPorId(acceso.IdDeCuenta);
                    if (cuentaAcceso is null)
                    {
                        eventosCronologicos.Add(new EventoGenerico("Desconocido", acceso.TiempoDeAcceso, "Acceso", "El usuario entró", ""));
                        continue;
                    }
                    eventosCronologicos.Add(new EventoGenerico(cuentaAcceso.NombreDisplay, acceso.TiempoDeAcceso, "Acceso", "El usuario entró", ""));
                }

                foreach (Accion accion in accionesFiltradas)
                {
                    Cuenta? cuentaAccion = SistemaCentral.Cuentas.ObtenerCuentaPorId(accion.IdDeCuenta);
                    if (cuentaAccion is null)
                    {
                        eventosCronologicos.Add(new EventoGenerico("Desconocido", accion.TiempoDeAccion, "Accion", accion.Mensaje, ""));
                        continue;
                    }
                    eventosCronologicos.Add(new EventoGenerico(cuentaAccion.NombreDisplay, accion.TiempoDeAccion, "Accion", accion.Mensaje, ""));
                }

                // Ahora ordenamos
                eventosCronologicos = eventosCronologicos.OrderBy(e => e.Tiempo).ToList();

                // Y ahora lo añadimos al DataTable
                foreach (EventoGenerico evento in eventosCronologicos)
                {
                    dt.Rows.Add(new object[] { evento.TiempoSesion, evento.Accionario, evento.Tiempo.ToString("yyyy-MM-dd"), evento.Tiempo.ToString("hh:mm tt"),
                        evento.Tipo, evento.Mensaje });
                }
                embeddedPath = "FactuCrossing.Reportes.rptAdmin2.rdlc";
                rds = new ReportDataSource("DsCrono", dt);
            }

            List<ReportParameter> parametros = new();
            parametros.Add(new ReportParameter("Generador", SistemaCentral.Cuentas.cuentaEnSesion?.NombreDisplay));

            switch(opcionDeMuestreo)
            {
                case OpcionesDeMuestreo.HOY:
                    parametros.Add(new ReportParameter("RangoFecha1", $"Hoy, " +
                        $"{DateTime.Now.ToString("dd/MM/yyyy")}"));
                    break;
                case OpcionesDeMuestreo.MESSELECCIONADO:
                    parametros.Add(new ReportParameter("RangoFecha1", $"Mes Seleccionado: " +
                        $"{StringHelper.PrimeraLetraMayuscula(new DateTime(año, mes, 1).ToString("MMMM yyyy"))}"));
                    break;
                case OpcionesDeMuestreo.FECHAESPECIFICA:
                    parametros.Add(new ReportParameter("RangoFecha1", $"Fecha Seleccionada: " +
                        $"{dtpEspecifica.Value.ToString("dd/MM/yyyy")}"));
                    break;
                case OpcionesDeMuestreo.RANGODEFECHAS:
                    parametros.Add(new ReportParameter("RangoFecha1", $"Rango de Fechas: " +
                        $"{dtpFecha1.Value.ToString("dd/MM/yyyy")} - {dtpFecha2.Value.ToString("dd/MM/yyyy")}"));
                    break;
                case OpcionesDeMuestreo.HISTORICO:
                    parametros.Add(new ReportParameter("RangoFecha1", $"Histórico"));
                    break;
            }

            Report report = new Report(embeddedPath, new List<ReportDataSource>(){ rds }, parametros);

            // Abrimos el dialogo
            new VistaPreviaReporte(report).ShowDialog();
        }

        /// <summary>
        /// Método que se ejecuta cuando se muestra el formulario
        /// </summary>
        private void OnShow()
        {
            // Si no se encuentra una cuentaEnSesion en el sistema central, cerrar el formulario
            if (SistemaCentral.Cuentas.cuentaEnSesion is null)
            {
                // Mostramos un mensaje diciendo que hubo un problema de autenticación
                MessageBox.Show("Hubo un problema de autenticación, por favor inicie sesión de nuevo", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Cerramos el formulario
                this.Close();
                return;
            }
            // Actualizamos el data grid
            ActualizarDataGrid();
            // Asignamos la cuenta en sesión
            this.Enabled = true;
        }

        /// <summary>
        /// Método para abrir un formulario y ocultar el actual
        /// </summary>
        /// <param name="frm">Formulario a abrir</param>
        private void AbrirDeshabilitarDelegar(Form frm)
        {
            // Ocultamos el formulario actual
            this.Enabled = false;
            // Mostramos el nuevo formulario
            frm.Show();
            // Cuando el nuevo formulario se cierra, volvemos a mostrar el formulario actual
            frm.FormClosed += delegate { this.OnShow(); };
        }

        /// <summary>
        /// Evento para abrir el formulario de Resetear Contraseñas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetear_Click(object sender, EventArgs e)
        {
            AbrirDeshabilitarDelegar(new Administrador.ResetearContraseñas());
        }

        /// <summary>
        /// Evento para abrir el formulario de Administrador de Personal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdministrar_Click(object sender, EventArgs e)
        {

            AbrirDeshabilitarDelegar(new Administrador.AdministrarPersonal());
        }

        /// <summary>
        /// Evento para abrir el formulario de Generador de Reportes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }

        private void btnDerecha_Click(object sender, EventArgs e)
        {
            // Si la fecha es mayor a la actual, retornar
            if (mes >= DateTime.Now.Month && año >= DateTime.Now.Year) return;

            // Si estamos en Diciembre pasamos a Enero del proximo año
            if (mes == 12) { mes = 1; año++; }
            // Si no, simplemente aumentamos el mes
            else mes++;

            // Actualizamos los labels
            ActualizarLabels();

            // Actualizar Data Grid View
            ActualizarDataGrid();
        }

        private void btnIzquierda_Click(object sender, EventArgs e)
        {
            // Si estamos en Enero pasamos a Diciembre del año pasado
            if (mes == 1) { mes = 12; año--; }
            // Si no, simplemente aumentamos el mes
            else mes--;

            // Actualizamos los labels
            ActualizarLabels();

            // Actualizar Data Grid View
            ActualizarDataGrid();
        }
        private void ActualizarLabels()
        {
            // Gracias a Darin Dimitrov en StackOverflow
            // https://stackoverflow.com/questions/3184121/get-month-name-from-month-number

            // Conseguimos el nombre del mes
            string fullMonthName = new DateTime(año, mes, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("es"));
            // Actualizamos el label
            lblMes.Text =
                $"{StringHelper.PrimeraLetraMayuscula(fullMonthName)} {año}";

            // Listar al personal contado
            List<int> personalConAcceso = new List<int>();

            // # de Accesos
            int accesos = 0;

            // Buscamos los accesos en el mes
            foreach (Acceso acceso in SistemaCentral.Accesos.accesosEnMemoria)
            {
                // Si el acceso tiene el mismo mes y año que 
                if ((acceso.TiempoDeAcceso.Month, acceso.TiempoDeAcceso.Year) == (mes, año))
                {
                    // Si el personal no se ha visto
                    if (!personalConAcceso.Contains(acceso.IdDeCuenta))
                    {
                        // Lo agregamos
                        personalConAcceso.Add(acceso.IdDeCuenta);
                    }
                    // Agregamos al numero de accesos
                    accesos++;
                }
            }

            // Actualizamos el label de accesos
            lblAccesos.Text = accesos.ToString();
            // Actualizamos el label de personal
            lblPersonal.Text = personalConAcceso.Count.ToString();
        }

        private void MenuAdministrador_Load(object sender, EventArgs e)
        {

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarDataGrid();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) opcionDeMuestreo = OpcionesDeMuestreo.HOY;
            ActualizarDataGrid();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) opcionDeMuestreo = OpcionesDeMuestreo.MESSELECCIONADO;
            ActualizarDataGrid();
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton5.Checked) opcionDeMuestreo = OpcionesDeMuestreo.FECHAESPECIFICA;
            dtpEspecifica.Enabled = radioButton5.Checked;
            ActualizarDataGrid();
        }

        private void dtpFecha1_ValueChanged(object sender, EventArgs e)
        {
            ActualizarDataGrid();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked) opcionDeMuestreo = OpcionesDeMuestreo.RANGODEFECHAS;
            dtpFecha1.Enabled = radioButton6.Checked;
            dtpFecha2.Enabled = radioButton6.Checked;
            ActualizarDataGrid();
        }

        private void AplicarFiltro()
        {
            Cuenta cuenta =
                    SistemaCentral.Cuentas.cuentasEnMemoria.First(m => m.NombreDisplay == cmbPersonal.Text);
            filtroPersonal = cuenta;
            ActualizarDataGrid();
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked && cmbPersonal.Text != string.Empty)
            {
                AplicarFiltro();
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked) opcionDeMuestreo = OpcionesDeMuestreo.HISTORICO;
            ActualizarDataGrid();
        }

        private void cmbPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked && cmbPersonal.Text != string.Empty)
            {
                AplicarFiltro();
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked) filtroPersonal = null;
            ActualizarDataGrid();
        }

        private void dtpEspecifica_ValueChanged(object sender, EventArgs e)
        {
            ActualizarDataGrid();
        }

        private void dtpFecha2_ValueChanged(object sender, EventArgs e)
        {
            ActualizarDataGrid();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked) ordenDeFiltro = Orden.RESUMEN;
            ActualizarDataGrid();
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked) ordenDeFiltro = Orden.CRONOLOGICO;
            ActualizarDataGrid();
        }
    }
}
