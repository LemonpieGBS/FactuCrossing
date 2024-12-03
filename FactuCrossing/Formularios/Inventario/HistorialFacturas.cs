using FactuCrossing.Estructuras;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FactuCrossing.Formularios.Inventario
{
    public partial class HistorialFacturas : Form
    {
        public string Busqueda = "";
        public List<Factura> facturasMostradas = new List<Factura>();
        public Factura? facturaSeleccionada = null;

        public HistorialFacturas()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
            ActualizarDataGrid();
        }

        public void ActualizarDataGrid()
        {
            // Creamos el data table
            DataTable dt = new();
            dt.Columns.AddRange(new DataColumn[] { new("#"), new("Fecha"), new("Total"), new("Facturista"), new("Nombre Factura") });
            facturasMostradas.Clear();
            // Iteramos sobre las facturas en memoria
            foreach (var factura in SistemaCentral.Facturas.facturasEnMemoria)
            {
                // Si hay una búsqueda activa buscamos si coincide con alguno de los atributos
                if (!string.IsNullOrEmpty(Busqueda))
                {
                    if (!factura.NumFactura.ToString().Contains(Busqueda) ||
                        !factura.FechaFactura.ToString("yyyy-MM-dd").Contains(Busqueda) ||
                        !factura.Total.ToString("0.00").Contains(Busqueda) ||
                        !factura.Facturista.Contains(Busqueda) ||
                        !factura.NombreFactura.Contains(Busqueda))
                    {
                        continue;
                    }
                    
                }

                // Agregamos una fila al data table
                dt.Rows.Add(new object[] { factura.NumFactura, factura.FechaFactura.ToString("yyyy-MM-dd"),
                    factura.Total.ToString("0.00") + "$", factura.Facturista, factura.NombreFactura });
                facturasMostradas.Add(factura);
            }

            dgvFactuas.DataSource = dt;
        }

        private void dgvFactuas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            // Obtenemos la factura seleccionada
            Factura factura = facturasMostradas[e.RowIndex];
            facturaSeleccionada = factura;
            // Creamos un nuevo formulario de DetallesFactura
            dgvFactura.DataSource = factura.ToDataTable();

            toolStripStatusLabel1.Text = $"Factura seleccionada: {factura.NumFactura}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Imprimimos como reporte
            if (facturaSeleccionada is not null)
            {
                // Creamos el reporte
                // Creamos el DataTable (lit no hay necesidad de crear una clase)
                DataTable dt = facturaSeleccionada.ToReportDataTable();
                // Creamos el DataSource
                ReportDataSource rds = new ReportDataSource("DsVenta", dt);
                // Damos la locación del RDLC
                string embedLocation = "FactuCrossing.Reportes.RptFactura.rdlc";
                // Parametros
                List<ReportParameter> listaParametros = new List<ReportParameter>()
            {
                new ReportParameter("FechaFactura", facturaSeleccionada.FechaFactura.ToString("dd/MM/yyyy") ),
                new ReportParameter("Total", $"{facturaSeleccionada.Total:0.00}$"),
                new ReportParameter("NombreFactura", facturaSeleccionada.NombreFactura),
                new ReportParameter("SucursalFactura", facturaSeleccionada.Sucursal),
                new ReportParameter("NumeroFactura", $"F{facturaSeleccionada.NumFactura:0000000}"),
                new ReportParameter("Facturista", facturaSeleccionada.Facturista),
                new ReportParameter("Subtotal", $"{facturaSeleccionada.Subtotal:0.00}$"),
                new ReportParameter("Descuento", $"{facturaSeleccionada.Descuento:0.00}$")
            };

                if (facturaSeleccionada.DescuentoGlobal is not null)
                    listaParametros.Add(new ReportParameter("DescuentoGlobal", $"{facturaSeleccionada.DescuentoGlobal.Nombre}, {facturaSeleccionada.DescuentoGlobal.Porcentaje}%"));
                // Creamos el reporte
                Report reporteFactura = new Report(embedLocation, [rds], listaParametros);
                // Abrimos el dialogo
                new VistaPreviaReporte(reporteFactura).ShowDialog();
            } else
            {
                MessageBox.Show("Por favor seleccione una factura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Busqueda = txtBuscar.Text;
            ActualizarDataGrid();
        }
    }
}
