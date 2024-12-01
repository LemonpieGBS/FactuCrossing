using FactuCrossing.Properties;
using Microsoft.Reporting.WinForms;

namespace FactuCrossing
{
    // No me gusta usar clases como Data Sources asi que decido usar DataTables tanto para
    // los reportes como los DataGridView

    // Gracias a Ikosson en GitHub por proporcionar el port de ReportViewer a .NET Core
    // Link: https://github.com/lkosson/reportviewercore/blob/master/ReportViewerCore.Sample.WinForms
    // Esta es mi propia implementación de la clase Report por motivos de escalabilidad, la clase Report se crea
    // y se pasa a VistaPreviaReporte para crear un formulario con la vista previa del mismo!!!

    /// <summary>
    /// Clase Reporte que se pasa
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Ubicación del recurso incrustado del reporte
        /// </summary>
        private string embedLocation;

        /// <summary>
        /// Lista de fuentes de datos del reporte
        /// </summary>
        private List<ReportDataSource> dataSources;

        /// <summary>
        /// Lista de parámetros del reporte
        /// </summary>
        private List<ReportParameter>? reportParameters;

        /// <summary>
        /// 
        /// </summary>
        public string? Nombre { get; set; }


        /// <summary>
        /// Constructor que inicializa un nuevo reporte con parámetros
        /// </summary>
        /// <param name="embedLocation">Ubicación del recurso incrustado del reporte</param>
        /// <param name="dataSources">Lista de fuentes de datos del reporte</param>
        /// <param name="reportParameters">Lista de parámetros del reporte</param>
        /// <exception cref="ArgumentNullException">Lanzada si embedLocation o dataSources son nulos</exception>
        public Report(string embedLocation, List<ReportDataSource> dataSources, List<ReportParameter> reportParameters)
        {
            // Asigna la ubicación del recurso incrustado del reporte
            this.embedLocation = embedLocation ?? throw new ArgumentNullException(nameof(embedLocation));
            // Asigna la lista de fuentes de datos del reporte
            this.dataSources = dataSources ?? throw new ArgumentNullException(nameof(dataSources));
            // Asigna la lista de parámetros del reporte
            this.reportParameters = reportParameters ?? throw new ArgumentNullException(nameof(reportParameters));
        }

        /// <summary>
        /// Constructor que inicializa un nuevo reporte sin parámetros
        /// </summary>
        /// <param name="embedLocation">Ubicación del recurso incrustado del reporte</param>
        /// <param name="dataSources">Lista de fuentes de datos del reporte</param>
        /// <exception cref="ArgumentNullException">Lanzada si embedLocation o dataSources son nulos</exception>
        public Report(string embedLocation, List<ReportDataSource> dataSources)
        {
            // Asigna la ubicación del recurso incrustado del reporte
            this.embedLocation = embedLocation ?? throw new ArgumentNullException(nameof(embedLocation));
            // Asigna la lista de fuentes de datos del reporte
            this.dataSources = dataSources ?? throw new ArgumentNullException(nameof(dataSources));
            // Inicializa la lista de parámetros del reporte como null
            this.reportParameters = null;
        }

        /// <summary>
        /// Carga el reporte en un objeto LocalReport
        /// </summary>
        /// <param name="report">Objeto LocalReport en el que se cargará el reporte</param>
        public void Load(LocalReport report)
        {
            // Establece la ubicación del recurso incrustado del reporte
            report.ReportEmbeddedResource = embedLocation;
            // Añade cada fuente de datos al reporte
            foreach (ReportDataSource ds in dataSources) report.DataSources.Add(ds);
            // Si hay parámetros del reporte, los establece en el reporte
            if (reportParameters is not null) report.SetParameters(reportParameters.ToArray());
            // Asignar nombre
            if (Nombre is not null) report.DisplayName = Nombre;
        }
    }

    /// <summary>
    /// Clase para la vista previa del reporte
    /// </summary>
    public class VistaPreviaReporte : Form
    {
        private readonly ReportViewer reportViewer;
        private readonly Report passedReport;

        /// <summary>
        /// Constructor que inicializa una nueva vista previa del reporte
        /// </summary>
        /// <param name="reportToLoad">Reporte a cargar</param>
        public VistaPreviaReporte(Report reportToLoad)
        {
            // Establece el título del formulario
            Text = "Vista Previa";
            // Establece el estado de la ventana a maximizado
            // WindowState = FormWindowState.Maximized;
            // Crea una nueva instancia de ReportViewer
            reportViewer = new ReportViewer();
            // Establece el acoplamiento del ReportViewer para llenar el formulario
            reportViewer.Dock = DockStyle.Fill;
            // Establece el color de fondo del ReportViewer
            reportViewer.BackColor = Color.LightGray;
            // Establecemos el tamaño del formulario
            this.Size = new Size(800, 600);
            // Establecemos el icono
            this.Icon = (Icon)Resources.AppIcon;
            // Asigna el reporte pasado al formulario
            passedReport = reportToLoad;
            // Añade el ReportViewer a los controles del formulario
            Controls.Add(reportViewer);
        }

        /// <summary>
        /// Evento que se ejecuta cuando se carga el formulario
        /// </summary>
        /// <param name="e">Argumentos del evento</param>
        protected override void OnLoad(EventArgs e)
        {
            // Carga el reporte en el ReportViewer
            passedReport.Load(reportViewer.LocalReport);
            // Refresca el ReportViewer para mostrar el reporte
            reportViewer.RefreshReport();
            // Llama al método OnLoad de la clase base
            base.OnLoad(e);
        }
    }
}