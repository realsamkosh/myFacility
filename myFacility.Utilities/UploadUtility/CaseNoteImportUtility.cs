using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace myFacility.Utilities.UploadUtility
{
    // <summary>
    /// Summary description for CaseNoteImportUtility
    /// </summary>
    public class CaseNoteImportUtility
    {
        public CaseNoteImportUtility()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //public static ImportedCaseNote GetCaseNoteFromArchive(String PatientOldHospitalNo, String PatientFullName)
        //    {
        //        try
        //        {
        //            ImportedCaseNote fileToSend = new ImportedCaseNote();
        //            fileToSend = null;
        //            Common.WriteDebugLog("Searching Scan Archive for :" + PatientOldHospitalNo + " : " + PatientFullName);
        //            String SearchPath = ConfigurationManager.AppSettings["OfflineArchivePhysicalPath"];
        //            String VirtualPath = ConfigurationManager.AppSettings["OfflineArchiveVirtualPath"];
        //            var locatedfiles = Directory.EnumerateFiles(SearchPath, "*" + PatientOldHospitalNo + "*.pdf", SearchOption.AllDirectories);
        //            // var locatedfiles2 = Directory.EnumerateFiles(SearchPath, "*" + PatientOldHospitalNo + "*.pdf", SearchOption.AllDirectories);
        //            Common.WriteDebugLog("Searching inside :" + SearchPath);
        //            int FileCount = locatedfiles.ToList().Count;
        //            Common.WriteDebugLog(String.Format("found {0} possible entries:", FileCount));
        //            Common.WriteDebugLog("Entries :" + locatedfiles.ToJson());
        //            if (FileCount > 0)
        //            {
        //                //we found some files
        //                if (FileCount == 1)
        //                {
        //                    FileInfo file = new FileInfo(locatedfiles.ToList()[0].ToString());
        //                    String FullPath = file.DirectoryName;

        //                    String SubPath = FullPath.Remove(0, SearchPath.Length);
        //                    fileToSend = new ImportedCaseNote
        //                    {
        //                        CreatedDate = DateTime.Now,
        //                        FileName = file.Name,
        //                        ThumbNail = VirtualPath + SubPath + "//" + file.Name,
        //                        FullVPath = file.FullName,
        //                        Extension = System.IO.Path.GetExtension(file.FullName),
        //                        Title = "SCANNED MEDICAL RECORD",
        //                        UploadedBy = "myFacility"
        //                    };

        //                }
        //                else if (FileCount > 1)
        //                {
        //                    //apply another filter to reduce the file count to one in case of patients with similar names
        //                    var newLocFiles = locatedfiles.ToList().FindAll(a => a.Contains(PatientOldHospitalNo.Replace('/', '_')));
        //                    if (newLocFiles.Count == 1)
        //                    {
        //                        FileInfo file = new FileInfo(newLocFiles[0].ToString());
        //                        String FullPath = file.DirectoryName;
        //                        String SubPath = FullPath.Remove(0, SearchPath.Length);
        //                        fileToSend = new ImportedCaseNote
        //                        {
        //                            CreatedDate = DateTime.Now,
        //                            FileName = file.Name,
        //                            ThumbNail = VirtualPath + SubPath + "//" + file.Name,
        //                            FullVPath = file.FullName,
        //                            Extension = System.IO.Path.GetExtension(file.FullName),
        //                            Title = "SCANNED MEDICAL RECORD",
        //                            UploadedBy = "myFacility"
        //                        };

        //                    }
        //                }

        //            }
        //            Common.WriteDebugLog("Found: " + fileToSend.ToJson());
        //            return fileToSend;

        //        }
        //        catch (Exception ex)
        //        {
        //            Common.WriteLog(ex);
        //            return null;
        //        }
        //    }


        //}




        public class ImportedCaseNote
        {
            public String FileName { get; set; }
            public String ThumbNail { get; set; }
            public String FullVPath { get; set; }
            public String Title { get; set; }
            public String UploadedBy { get; set; }
            public DateTime CreatedDate { get; set; }
            public String Extension { get; set; }

        }
    }
}
