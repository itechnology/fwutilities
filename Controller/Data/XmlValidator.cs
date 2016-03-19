/******************************************************************************
*    File Watcher Utilities / File Watcher Controller
*    Copyright (c) 2006-2013 Jussi Hiltunen
*
*    This program is free software; you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation; either version 2 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*
*    You should have received a copy of the GNU General Public License
*    along with this program; if not, write to the Free Software
*    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
******************************************************************************/

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides XML Schema validation for XML file.
    /// </summary>
    public sealed class XmlValidator
    {
        /// <summary>
        /// Occurs when validator encounters validation errors.
        /// </summary>
        public event EventHandler<ValidationEventArgs> Validation;

        /// <summary>
        /// Initializes a new instance of the XmlValidator class.
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file to validate.</param>
        /// <param name="xmlSchemaFilePath">Path of the XML Schema file.</param>
        /// <exception cref="FileNotFoundException">XML file not found.</exception>
        /// <exception cref="FileNotFoundException">XML Schema file not found.</exception>
        /// <exception cref="ArgumentNullException">xmlFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaFilePath is null.</exception>
        public XmlValidator(string xmlFilePath,
                            string xmlSchemaFilePath)
        {
            if (xmlFilePath == null)
            {
                throw new ArgumentNullException("xmlFilePath",
                                                Resources.ArgumentNullException);
            }
            if (xmlSchemaFilePath == null)
            {
                throw new ArgumentNullException("xmlSchemaFilePath",
                                                Resources.ArgumentNullException);
            }
            if (File.Exists(xmlFilePath))
            {
                _xmlFilePath = xmlFilePath;
            }
            else
            {
                throw new FileNotFoundException(Resources.FileNotFoundException,
                                                xmlFilePath);
            }
            if (File.Exists(xmlSchemaFilePath))
            {
                _xmlSchemaFilePath = xmlSchemaFilePath;
            }
            else
            {
                throw new FileNotFoundException(Resources.FileNotFoundException,
                                                xmlSchemaFilePath);
            }
        }

        /// <summary>
        /// Performs validation of the XML file.
        /// </summary>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        public void Validate()
        {
            XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();

            xmlSchemaSet.Add(null,
                             _xmlSchemaFilePath);

            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();

            xmlReaderSettings.ValidationType = ValidationType.Schema;
            xmlReaderSettings.Schemas = xmlSchemaSet;
            xmlReaderSettings.ValidationEventHandler +=
                new ValidationEventHandler(XmlValidationCallBack);

            using (XmlReader xmlReader = XmlReader.Create(_xmlFilePath,
                                                          xmlReaderSettings))
            {
                while (xmlReader.Read())
                {
                }
            }
        }

        /// <summary>
        /// XML validation callback.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void XmlValidationCallBack(object sender,
                                           ValidationEventArgs e)
        {
            if (Validation != null)
            {
                Validation(sender,
                           e);
            }
        }

        /// <summary>
        /// Contains the path of the XML Schema file.
        /// </summary>
        private readonly string _xmlSchemaFilePath;

        /// <summary>
        /// Contains the path of the XML file.
        /// </summary>
        private readonly string _xmlFilePath;
    }
}