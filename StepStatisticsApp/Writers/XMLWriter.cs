using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using StepStatisticsApp.Models;

namespace StepStatisticsApp
{
    public class XMLWriter : IDisposable
    {
        private readonly XmlWriter xmlTextWriter;
        private bool disposed = false;

        public XMLWriter(StreamWriter writer)
        {
            this.xmlTextWriter = new XmlTextWriter(writer ?? throw new ArgumentNullException(nameof(writer)));
            this.xmlTextWriter.WriteStartDocument();
            this.xmlTextWriter.WriteStartElement("Users");
        }

        public void Write(User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} cannot be null.");
            }

            this.xmlTextWriter.WriteStartElement("User");
            this.xmlTextWriter.WriteAttributeString("Name", user.Name.ToString(CultureInfo.InvariantCulture));
            foreach (var step in user.StepStatistics)
            {
                foreach (var rank in user.RankStatistics)
                {
                    foreach (var status in user.StatusStatistics)
                    {
                        this.xmlTextWriter.WriteStartElement("day", rank.Key.ToString(CultureInfo.InvariantCulture));
                        this.xmlTextWriter.WriteAttributeString("steps", step.Value.ToString(CultureInfo.InvariantCulture));
                        this.xmlTextWriter.WriteAttributeString("rank", rank.Value.ToString(CultureInfo.InvariantCulture));
                        this.xmlTextWriter.WriteAttributeString("status", status.Value.ToString(CultureInfo.InvariantCulture));
                        this.xmlTextWriter.WriteEndElement();
                    }
                }
            }
            this.xmlTextWriter.WriteEndElement();
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
