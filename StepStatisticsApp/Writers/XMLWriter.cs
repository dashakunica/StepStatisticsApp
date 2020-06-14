using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using StepStatisticsApp.Models;

namespace StepStatisticsApp
{
    public class XMLUserWriter : IDisposable
    {
        private readonly XmlTextWriter xmlTextWriter;
        private bool disposed = false;

        public XMLUserWriter(StreamWriter writer)
        {
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = true
            };

            this.xmlTextWriter = new XmlTextWriter(writer ?? throw new ArgumentNullException(nameof(writer)));
            this.xmlTextWriter.Formatting = Formatting.Indented;
            this.xmlTextWriter.WriteStartDocument();
            this.xmlTextWriter.WriteStartElement("Users");
        }

        public void Write(User user)
        {
            if (user != null)
            {
                this.xmlTextWriter.WriteStartElement("User");
                this.xmlTextWriter.WriteAttributeString("Name", user.Name.ToString(CultureInfo.InvariantCulture));
                foreach (var step in user.StepStatistics)
                {
                    var rank = user.RankStatistics[step.Key];
                    var status = user.StatusStatistics[step.Key];

                    this.xmlTextWriter.WriteStartElement("day" + step.Key.ToString(CultureInfo.InvariantCulture));
                    this.xmlTextWriter.WriteAttributeString("steps", step.Value.ToString(CultureInfo.InvariantCulture));
                    this.xmlTextWriter.WriteAttributeString("rank", rank.ToString(CultureInfo.InvariantCulture));
                    this.xmlTextWriter.WriteAttributeString("status", status.ToString(CultureInfo.InvariantCulture));
                    this.xmlTextWriter.WriteEndElement();
                }
                this.xmlTextWriter.WriteEndElement();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.xmlTextWriter.WriteEndElement();

            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.xmlTextWriter.Dispose();
            }

            this.disposed = true;
        }
    }
}
