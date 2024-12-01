using System.ComponentModel;
using System.Reflection;

namespace FactuCrossing
{
    // Gracias a Derek W en Stack Overflow, pueden ver su respuesta en como hacer una barra de progreso que no se laguea:
    // https://stackoverflow.com/questions/6071626/progressbar-is-slow-in-windows-forms
    public static class ExtensionMethods
    {
        /// <summary>
        /// Sets the progress bar value, without using 'Windows Aero' animation.
        /// This is to work around a known WinForms issue where the progress bar 
        /// is slow to update. 
        /// </summary>
        public static void SetProgressNoAnimation(this ProgressBar pb, int value)
        {
            // To get around the progressive animation, we need to move the 
            // progress bar backwards.
            if (value == pb.Maximum)
            {
                // Special case as value can't be set greater than Maximum.
                pb.Maximum = value + 1;     // Temporarily Increase Maximum
                pb.Value = value + 1;       // Move past
                pb.Maximum = value;         // Reset maximum
            }
            else
            {
                pb.Value = value + 1;       // Move past
            }
            pb.Value = value;               // Move to correct value
        }
    }

    /// <summary>
    /// Clase con funciones que ayudan al manejo de un form
    /// </summary>
    public static class FormHelper
    {
        // Estas funciones las voy a usar despues, consiguen recursivamente todos los controles de un formulario
        // Incluyendo los controles que albergan mas controles, como los GroupBoxes

        /// <summary> Función para obtener todos los descendientes de un control (<paramref name="c"/>)
        /// <para>
        /// Esta función usa recursión para conseguir los hijos de sus hijos también<br/>
        /// El retorno de esta función es una lista que NO incluye el control pasado como argumento<br/><br/>
        /// Si quieres obtener todos los controles de un formulario te puede interesar <seealso cref="GetControls(Form)"/>
        /// </para>
        /// </summary>
        /// <param name="c">Control el cual se buscan obtener sus descendientes</param>
        /// <returns>Lista que contiene todos los descendientes de un control</returns>
        public static List<Control> GetControls(Control c)
        {
            var controls = new List<Control>();
            foreach (Control child in c.Controls)
            {
                controls.Add(child);
                if (child.HasChildren)
                {
                    controls.AddRange(GetControls(child));
                }
            }
            return controls;
        }

        /// <summary> Función para obtener todos los controles (incluyendo hijos y descendientes) de un formulario (<paramref name="f"/>)
        /// <para>
        /// Esta función consigue todos los controles en un formulario<br/>
        /// Un ejemplo de como usar la función es aplicar una tipografía a todo un formulario:<br/><br/>
        /// <code> public static void ApplyFont(FontFamily ff, Form form)
        /// {
        ///     foreach(Control c in GetControls(form))
        ///     c.Font = new Font(ff, c.Font.Size, c.Font.Style);
        /// } </code>
        /// Si quieres obtener todos los descendientes de un control te puede interesar <seealso cref="GetControls(Control)"/>
        /// </para>
        /// </summary>
        /// <param name="f">Formulario del cual se busca obtener todos sus controles</param>
        /// <returns>Lista que contiene todos los controles en un formulario</returns>
        public static List<Control> GetControls(Form f)
        {
            List<Control> returnList = new List<Control>() { };
            foreach (Control c in f.Controls)
            {
                returnList.Add(c);
                if (c.HasChildren) returnList.AddRange(GetControls(c));
            }
            return returnList;
        }
    }

    /// <summary>
    /// Clase con funciones que ayudan al manejo de enumeradores
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Función para obtener el nombre de un enumerador
        /// </summary>
        /// <param name="e">Enumerador a obtener su nombre</param>
        /// <returns>Nombre del enumerador</returns>
        public static string GetEnumName(Enum e)
        {
            return GetDescription(e) ?? e.ToString();
        }

        /// <summary>
        /// Función para obtener la descripción de un enumerador
        /// Gracias a Thomas Levesque en stack overflow por la función
        /// https://stackoverflow.com/questions/1415140/can-my-enums-have-friendly-names
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string? GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo? field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute? attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }

    public static class FileHelper
    {
        /// <summary>
        /// Camino a AppData por consistencia de la aplicación
        /// </summary>
        public static readonly string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        /// <summary>
        /// Camino de AppData a la aplicación
        /// </summary>
        public static readonly string SaveDataPath = AppDataPath + "\\FactuCrossing\\";
    }

    public static class DateHelper
    {
        // Ya le saben, gracias a Ani en Stack Overflow
        // https://stackoverflow.com/questions/3849975/how-to-get-all-dates-in-a-given-month-in-c-sharp
        public static List<DateTime> GetDates(int year, int month)
        {
            var dates = new List<DateTime>();

            // Loop from the first day of the month until we hit the next month, moving forward a day at a time
            for (var date = new DateTime(year, month, 1); date.Month == month; date = date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }
    }

    public static class StringHelper
    {
        // Código para hacer que la primera letra de un string esté en mayúscula
        public static string PrimeraLetraMayuscula(string str)
        {
            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);
            else return str.ToUpper();
        }
    }
}
