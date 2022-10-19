using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WikiApp.Services
{
    public class GetSearch
    {



        /*public void Search(TextBox textbox, Label label)
        {
            WebClient client = new WebClient();
            var searchString = client.DownloadString("https://en.wikipedia.org/w/api.php?action=opensearch&search=" + textbox.Text );
            var resp = Newtonsoft.Json.JsonConvert.DeserializeObject(searchString);
            string[] respString = resp.ToString().Split('[');
            var i = respString[3];
            label.Text = i;
        }
        */
        public string SearchArticle(string articulo)
        {
            WebClient wc = new WebClient();
            string archivo = wc.DownloadString("https://es.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + articulo + "&redirects=true");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(archivo);

            XmlNode nodo = xml.GetElementsByTagName("extract")[0];
            try
            {
                string texto = nodo.InnerText;
                Regex rx = new Regex("\\<[^\\>]*\\>");
                texto = rx.Replace(texto, "");

                texto = texto.Replace("Ã", "í");
                texto = texto.Replace("í³", "ó");
                texto = texto.Replace("í©", "é");
                texto = texto.Replace("í±", "ñ");
                texto = texto.Replace("í¡", "á");
                texto = texto.Replace("íº", "ú");
                texto = texto.Replace("â€”", "—");
                texto = texto.Replace("Â", "");
                texto = texto.Replace("â€‹", "");

                return texto;
            }
            catch (Exception ex) { return "El artículo no existe"; }
        }

    }
}
