using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Modelo;
using System.Xml;
using System.Web.Hosting;
using System.IO;
using System.Xml.Linq;


namespace App.Datos
{
    public class PersonasRepositories
    {
        private static List<Personas> data = new List<Personas>();
        
        public List<Personas> getPersonas()
        {
          // Creamos la lista genérica de Personas
            List<Personas> lista = new List<Personas>();
           // Obtenemos la ruta de archivo XML
          string ruta = HttpContext.Current.Server.MapPath("/Datos/Agenda.xml");

          XDocument doc = XDocument.Load(ruta);

          var people = from p in doc.Descendants("Personas") select p;

          foreach (XElement p in people.Elements("Persona"))
          {
              Personas person = new Personas(
                                                p.Element("ID").Value,
                                                p.Element("Nombres").Value,
                                                p.Element("Apellidos").Value,
                                                p.Element("EmailP").Value,
                                                p.Element("EmailW").Value,
                                                p.Element("TelefonoP").Value,
                                                p.Element("TelefonoW").Value
                  
                                            );
              lista.Add(person);

          }
          
   
         return lista;
        }

        public void add(Personas p)
        {
            data.Add(p);
            WriteXML(data);

        }

        private void WriteXML(List<Personas> list)
        {
            XmlTextWriter xmlwriter = new XmlTextWriter(HttpContext.Current.Server.MapPath("/Datos/Agenda.xml"), System.Text.Encoding.UTF8);
            
            //Inicio XML Documento
            xmlwriter.WriteStartDocument(true);
            xmlwriter.Formatting = Formatting.Indented;
            xmlwriter.Indentation = 2;

            //ROOT Element
            xmlwriter.WriteStartElement("Personas");
            //Call create nodes method
            foreach(Personas p in list)
            {
                xmlwriter.WriteStartElement("Persona");
                xmlwriter.WriteElementString("ID", p.Id);
                xmlwriter.WriteElementString("Nombres", p.Nombres);
                xmlwriter.WriteElementString("Apellidos", p.Apellidos);
                xmlwriter.WriteElementString("EmailP", p.EmailP);
                xmlwriter.WriteElementString("EmailW", p.EmailW);
                xmlwriter.WriteElementString("TelefonoP", p.TelefonoP);
                xmlwriter.WriteElementString("TelefonoW", p.TelefonoW);
                xmlwriter.WriteEndElement();
            }
               
            xmlwriter.WriteEndElement();

            //End XML Document
            xmlwriter.WriteEndDocument();

            //Close Write
            xmlwriter.Close();
        }

    }
}