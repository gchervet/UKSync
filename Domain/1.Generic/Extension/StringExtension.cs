using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extension
{
    /// <summary>
    /// Contiene los métodos de extensión para la clase String
    /// </summary>
    public static class StringExtension
    {
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Convierte imagen path
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConvertImagePath(this string value, string name)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : SaveImage(value, name);
        }

        /// <summary>
        /// Convierte path imagen
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertPathImage(this string value, bool makeThumbnail = false)
        {
            string rtn = string.Empty;
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    using (Image img = Image.FromFile(value))
                    {
                        Image finalImage = img;

                        if (makeThumbnail)
                        {
                            finalImage = img.GetThumbnailImage(30, 30, () => false, IntPtr.Zero);
                        }

                        byte[] bArray;
                        using (var ms = new MemoryStream())
                        {
                            finalImage.Save(ms, ImageFormat.Bmp);
                            bArray = ms.ToArray();
                        }

                        rtn = "data:image/" + System.IO.Path.GetExtension(value) + ";base64," + Convert.ToBase64String(bArray);

                        if (makeThumbnail)
                        {
                            finalImage.Dispose();
                        }
                    }
                }
                catch
                {

                }
            }
            return rtn;
        }

        /// <summary>
        /// Guarda una imagen a partir de un base64 más detalles de su formato (Como se usaría en un img src). Devuelve el path absoluto del archivo creado.
        /// </summary>
        /// <param name="decoratedBase64">String que representa a la imagen (ej: data:image/png;base64,iVBORw0KGgoA...)</param>
        /// <param name="filename">Nombre del archivo, sin extensión</param>
        /// <returns>Path absoluto del archivo creado</returns>
        private static string SaveImage(string decoratedBase64, string filename)
        {
            string base64 = decoratedBase64.Substring(decoratedBase64.IndexOf(',') + 1);
            string extension = decoratedBase64.Substring(decoratedBase64.IndexOf('/') + 1, decoratedBase64.IndexOf(';') - (decoratedBase64.IndexOf('/') + 1));
            byte[] bytes = Convert.FromBase64String(base64);

            string path = string.Format("{0}\\Download", AssemblyDirectory);

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            string fullPath = string.Format("{0}\\{1}.{2}", path, filename, extension);

            Image image;

            MemoryStream ms = new MemoryStream(bytes);
            image = Image.FromStream(ms);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            image.Save(fullPath);

            image.Dispose();

            return fullPath;
        }
    }
}
