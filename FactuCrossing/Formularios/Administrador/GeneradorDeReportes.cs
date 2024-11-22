namespace FactuCrossing.Formularios.Administrador
{
    public partial class Generador_de_Reportes : Form
    {
        public Generador_de_Reportes()
        {
            InitializeComponent();
            if (Program.mFont is not null) Program.ApplyFont(Program.mFont, this);
        }
    }
}
