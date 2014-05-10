using log4net;
using SolidEdgeContrib.Reader;
using SolidEdgeContrib.Reader.Assembly;
using SolidEdgeContrib.Reader.Draft;
using SolidEdgeContrib.Reader.InsightConnect;
using SolidEdgeContrib.Reader.Native;
using SolidEdgeContrib.Reader.Part;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace QA
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal static bool _testProperties;
        internal static bool _extractEMFs;
        internal static bool _extractThumbnails;
        private static int _maxFiles;

        static void Main(string[] args)
        {
            TimeSpan elapsedTime = new TimeSpan();

            DirectoryInfo rootFolder = new DirectoryInfo(ConfigurationManager.AppSettings["RootFolder"]);
            string[] fileExtensions = ConfigurationManager.AppSettings["FileExtensions"].Split(',');
            _testProperties = bool.Parse(ConfigurationManager.AppSettings["TestProperties"]);
            _extractEMFs = bool.Parse(ConfigurationManager.AppSettings["ExtractEMFs"]);
            _extractThumbnails = bool.Parse(ConfigurationManager.AppSettings["ExtractThumbnails"]);
            _maxFiles = int.Parse(ConfigurationManager.AppSettings["MaxFiles"]);

            log.InfoFormat("Scanning '{0}' for files.", rootFolder.FullName);

            int count = 0;

            foreach (string fileExtension in fileExtensions)
            {
                foreach (FileInfo file in rootFolder.EnumerateFiles("*" + fileExtension, SearchOption.AllDirectories))
                {
                    count++;

                    if (count >= _maxFiles)
                    {
                        break;
                    }

                    string relativePath = file.FullName.Substring(rootFolder.FullName.Length);
                    log.Info(relativePath);

                    try
                    {
                        TimeSpan t1 = TestDocument(file.FullName);
                        log.InfoFormat("t1: {0}", t1.ToString());
                        log.Info(String.Empty);
                        elapsedTime = elapsedTime.Add(t1);
                    }
                    catch (System.Exception ex)
                    {
                        log.Error(ex.Message, ex);
                    }
                }
            }

        }

        static TimeSpan TestDocument(string path)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            using (SolidEdgeDocument document = SolidEdgeDocument.Open(path))
            {
                log.Debug(String.Format("ClassId: '{0}'", document.ClassId));
                log.Debug(String.Format("CreatedVersion: '{0}'", document.CreatedVersion));
                log.Debug(String.Format("LastSavedVersion: '{0}'", document.LastSavedVersion));
                log.Debug(String.Format("Created: '{0}'", document.Created));
                log.Debug(String.Format("LastModified: '{0}'", document.LastModified));
                log.Debug(String.Format("Status: '{0}'", document.Status));

                if (Program._testProperties)
                {
                    foreach (PropertySet propertySet in document.PropertySets)
                    {
                        foreach (Property property in propertySet)
                        {
                            object value = property.Value;
                            log.Debug(String.Format(@"{0}\{1} = '{2}'", propertySet.Name, property.ToString(), value));
                        }
                    }
                }

                if (Program._extractThumbnails)
                {
                    //using (Bitmap bitmap = document.GetThumbnail())
                    //{
                    //    log.Debug(String.Format("Extracting thumbnail"));
                    //    bitmap.Save(path + ".bmp");
                    //}
                }

                switch (document.DocumentType)
                {
                    case DocumentType.AssemblyDocument:
                        TestDocument((AssemblyDocument)document);
                        break;
                    case DocumentType.ConfigFileExtension:
                        TestDocument((ConfigFileExtension)document);
                        break;
                    case DocumentType.DraftDocument:
                        TestDocument((DraftDocument)document);
                        break;
                    case DocumentType.FamilyOfAssembliesDocument:
                        TestDocument((FamilyOfAssembliesDocument)document);
                        break;
                    case DocumentType.MarkupDocument:
                        TestDocument((MarkupDocument)document);
                        break;
                    case DocumentType.PartDocument:
                        TestDocument((PartDocument)document);
                        break;
                    case DocumentType.SheetMetalDocument:
                        TestDocument((SheetMetalDocument)document);
                        break;
                    case DocumentType.SynchronousAssemblyDocument:
                        TestDocument((SynchronousAssemblyDocument)document);
                        break;
                    case DocumentType.SynchronousPartDocument:
                        TestDocument((SynchronousPartDocument)document);
                        break;
                    case DocumentType.SynchronousSheetMetalDocument:
                        TestDocument((SynchronousSheetMetalDocument)document);
                        break;
                    case DocumentType.Unknown:
                        System.Diagnostics.Debugger.Break();
                        break;
                    case DocumentType.WeldmentDocument:
                        TestDocument((WeldmentDocument)document);
                        break;
                }
            }

            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        static void TestDocument(AssemblyDocument document)
        {
        }

        static void TestDocument(ConfigFileExtension document)
        {
            foreach (SolidEdgeContrib.Reader.Assembly.Configuration config in document.Configurations)
            {
                string configName = config.Name;
            }

            foreach (Zone zone in document.Zones)
            {
            }
        }

        static void TestDocument(DraftDocument document)
        {
            if (Program._extractEMFs)
            {
                foreach (Sheet sheet in document.Sheets)
                {
                    log.Debug(String.Format("Extracting sheet '{0}'", sheet.Name));
                    sheet.SaveAsEmf(Path.ChangeExtension(document.FileName, String.Format("{0}.emf", sheet.Name)));
                }
            }
        }

        static void TestDocument(FamilyOfAssembliesDocument document)
        {
        }

        static void TestDocument(MarkupDocument document)
        {
        }

        static void TestDocument(PartDocument document)
        {
        }

        static void TestDocument(SheetMetalDocument document)
        {
        }

        static void TestDocument(SynchronousAssemblyDocument document)
        {
        }

        static void TestDocument(SynchronousPartDocument document)
        {
        }

        static void TestDocument(SynchronousSheetMetalDocument document)
        {
        }

        static void TestDocument(WeldmentDocument document)
        {
        }
    }
}
