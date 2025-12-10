using System;

namespace MollysCare.Formularios
{
    public partial class AjaxDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request["ajax"] == "1")
            {
                ProcesarAjax();
            }
        }

        private void ProcesarAjax()
        {
            string nombre = Request["nombre"];
            string mensaje;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                mensaje = "Hola, invitado. Esta respuesta vino del servidor usando AJAX.";
            }
            else
            {
                mensaje = $"Hola, {nombre}. Esta respuesta vino del servidor usando AJAX.";
            }

            Response.Clear();
            Response.ContentType = "text/plain; charset=utf-8";
            Response.Write(mensaje);
            Response.End();
        }
    }
}
