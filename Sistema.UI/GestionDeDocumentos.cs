using Sistema.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.UI
{
    public class GestionDeDocumentos
    {

        private static string rutaAGuardar = "D:\\Archivos";

        public static void CargarDocumentos(List<Documento> listaDocumentos ,ref List<Documento>  listaDocumentosCreados)
        {

            foreach (var documento in listaDocumentos)
            {
                if (documento.Documento1 != null) {

                    string nombreDeArchivo = $"{documento.IdDocumento.ToString()}-{documento.Nombre}{documento.Extension}";
                    if (ValidacionDeCreacionDeArchivo(rutaAGuardar, nombreDeArchivo))
                    {
                        try
                        {
                            string ruta = Path.Combine(rutaAGuardar, nombreDeArchivo);
                            using (var fileStream = new FileStream(ruta, FileMode.Create))
                            {
                                fileStream.Write(documento.Documento1, 0, documento.Documento1.Length);
                            }

                            documento.RutaPc = ruta;
                            listaDocumentosCreados.Add(documento);
                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }

           

        }

        public static string GenerarRutaPorDefecto(string nombreArchivo) {

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
