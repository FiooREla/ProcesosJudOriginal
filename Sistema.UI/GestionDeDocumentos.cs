using Sistema.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema.UI
{
    public class GestionDeDocumentos
    {

        public const  string rutaAGuardar = @"\\10.113.1.45\sis_pj";

        public static void CargarDocumentos(List<Documento> listaDocumentos ,ref List<Documento>  listaDocumentosCreados)
        {

            foreach (var documento in listaDocumentos)
            {
                if (documento.Documento1 != null) {

                    string nombreDeArchivo = $"{documento.IdNEWID}-{documento.Nombre}{documento.Extension}";

                    bool resultadoDeCarga = CargarDocumentosIndividual(nombreDeArchivo, documento);

                   
                    if (resultadoDeCarga)
                    {
                        documento.RutaPc = GenerarRutaPorDefecto(nombreDeArchivo);
                        listaDocumentosCreados.Add(documento);
                    }
                  
                }
            }
        }


        public static bool CargarDocumentosIndividual( string nombreDeArchivo,  Documento documento)
        {

          
            
            if (ValidacionDeCreacionDeArchivo(rutaAGuardar, nombreDeArchivo))
            {
                try
                {
                    string ruta = Path.Combine(rutaAGuardar, nombreDeArchivo);
                    using (var fileStream = new FileStream(ruta, FileMode.Create))
                    {
                        fileStream.Write(documento.Documento1, 0, documento.Documento1.Length);
                    }

                   
                    return true;

                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        public static string GenerarRutaPorDefecto(string nombreArchivo)
        {
            
            return Path.Combine(rutaAGuardar, nombreArchivo);

        }

        private static bool ValidacionDeCreacionDeArchivo(string ruta, string nombreArchivo)
        {

            bool resultadoDeValidacionDeArchivo = false;
            try
            {
                if (!Directory.Exists(ruta))
                {
                    Directory.CreateDirectory(ruta);
                }

                if (File.Exists(nombreArchivo))
                {

                    resultadoDeValidacionDeArchivo = false;

                }
                else
                {
                    resultadoDeValidacionDeArchivo = true;
                }
            }
            catch (Exception)
            {
                resultadoDeValidacionDeArchivo = false;
            }


            return resultadoDeValidacionDeArchivo;



        }

    }
}
