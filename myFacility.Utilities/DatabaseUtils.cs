//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace myFacility.Utilities
//{
//   public class DatabaseUtils
//    {
//        public static void SortList<T>(List<T> dataSource, string fieldName, SortDirection sortDirection)
//        {
//            PropertyInfo propInfo = typeof(T).GetProperty(fieldName);
//            Comparison<T> compare = delegate (T a, T b)
//            {
//                bool asc = sortDirection == SortDirection.Ascending;
//                object valueA = asc ? propInfo.GetValue(a, null) : propInfo.GetValue(b, null);
//                object valueB = asc ? propInfo.GetValue(b, null) : propInfo.GetValue(a, null);

//                return valueA is IComparable ? ((IComparable)valueA).CompareTo(valueB) : 0;
//            };
//            dataSource.Sort(compare);
//        }

//        public static void DatatableToExcel(DataTable dt, string Filename)
//        {
//            try
//            {
//                string folder = HttpContext.Current.Server.MapPath("~/tempexcel");
//                if (!Directory.Exists(folder))
//                {
//                    Directory.CreateDirectory(folder);
//                }
//                string tempFilename = string.Format("{0}-{1}.xls", Guid.NewGuid().ToString(), DateTime.Now.Ticks);
//                string fullpath = string.Format("{0}\\{1}", folder, tempFilename);

//                //string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", fullpath);
//                string connStr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;\"", fullpath);

//                //create columns
//                StringBuilder createTableScript = new StringBuilder();
//                createTableScript.Append("CREATE TABLE [Sheet1]("); //create statement
//                foreach (DataColumn column in dt.Columns)
//                {
//                    createTableScript.AppendFormat("[{0}] MEMO,", column.ColumnName.ToUpper(), column.DataType.Name);
//                }

//                //remove trailing comma
//                createTableScript.Remove((createTableScript.Length - 1), 1);
//                createTableScript.Append(")"); //closing bracket

//                using (OleDbConnection conn = new OleDbConnection(connStr))
//                {
//                    conn.Open();
//                    using (OleDbCommand command = new OleDbCommand(createTableScript.ToString(), conn))
//                    {
//                        command.ExecuteNonQuery();
//                        createTableScript = null;
//                        //insert data
//                        int looper = dt.Columns.Count;
//                        StringBuilder insertScript = null;
//                        foreach (DataRow row in dt.Rows)
//                        {
//                            insertScript = new StringBuilder();
//                            insertScript.Append("INSERT INTO [Sheet1] VALUES(");

//                            //populate data
//                            for (int i = 0; i < looper; i++)
//                            {
//                                insertScript.AppendFormat("'{0}',", row[i].ToString().Replace("'", "''"));
//                            }

//                            //remove trailing comma
//                            insertScript.Remove((insertScript.Length - 1), 1);
//                            insertScript.Append(")");

//                            command.CommandText = insertScript.ToString();
//                            command.CommandType = CommandType.Text;

//                            command.ExecuteNonQuery();
//                            insertScript = null;
//                        }

//                    }
//                }

//                ////zip up file
//                //string zipFie = string.Format("{0}.zip", Filename);
//                //string zipFullpath = string.Format("{0}\\{1}", folder, zipFie);
//                //using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipFullpath)))
//                //{
//                //    zipStream.SetLevel(9);

//                //    byte[] buffer = new byte[4096];

//                //    ZipEntry entry = new ZipEntry(string.Format("{0}.xls", Filename));
//                //    entry.DateTime = DateTime.Now;

//                //    zipStream.PutNextEntry(entry);

//                //    using (FileStream fs = File.OpenRead(fullpath))
//                //    {
//                //        // manage memory usage.
//                //        int sourceBytes;
//                //        do
//                //        {
//                //            sourceBytes = fs.Read(buffer, 0, buffer.Length);
//                //            zipStream.Write(buffer, 0, sourceBytes);
//                //        } while (sourceBytes > 0);
//                //    }

//                //    zipStream.Finish();
//                //    zipStream.Close();
//                //}

//                //clean up               
//                byte[] retFile = null;
//                using (FileStream fs = File.OpenRead(fullpath))
//                {
//                    retFile = new byte[fs.Length];
//                    fs.Read(retFile, 0, retFile.Length);
//                }

//                //delete temp file
//                try
//                {
//                    File.Delete(fullpath);
//                    //File.Delete(zipFullpath);
//                }
//                catch (Exception ex)
//                {
//                    Common.WriteLog(ex);
//                }

//                //sent file
//                HttpResponse response = HttpContext.Current.Response;
//                response.Clear();
//                // response.AddHeader("content-disposition", string.Format("attachment;filename={0}.zip", Filename.Replace(" ", "_")));
//                response.AddHeader("content-disposition", string.Format("attachment;filename={0}.xls", Filename.Replace(" ", "_")));
//                response.Charset = "";
//                response.Cache.SetCacheability(HttpCacheability.NoCache);
//                response.ContentType = "application/octet-stream";
//                response.BinaryWrite(retFile);
//                response.End();
//            }
//            catch (Exception ex)
//            {
//                Common.WriteLog(ex);
//            }
//        }

//        public static void DatatableToExcelNoZip(DataTable dt, string Filename, string VirtualFolderPath)
//        {
//            try
//            {
//                string folder = HttpContext.Current.Server.MapPath(VirtualFolderPath);
//                if (!Directory.Exists(folder))
//                {
//                    Directory.CreateDirectory(folder);
//                }
//                string fullpath = string.Format("{0}\\{1}", folder, Filename);

//                string connStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", fullpath);

//                //create columns
//                StringBuilder createTableScript = new StringBuilder();
//                createTableScript.Append("CREATE TABLE [Sheet1]("); //create statement
//                foreach (DataColumn column in dt.Columns)
//                {
//                    createTableScript.AppendFormat("[{0}] MEMO,", column.ColumnName, column.DataType.Name);
//                }

//                //remove trailing comma
//                createTableScript.Remove((createTableScript.Length - 1), 1);
//                createTableScript.Append(")"); //closing bracket

//                using (OleDbConnection conn = new OleDbConnection(connStr))
//                {
//                    conn.Open();
//                    using (OleDbCommand command = new OleDbCommand(createTableScript.ToString(), conn))
//                    {
//                        command.ExecuteNonQuery();
//                        createTableScript = null;
//                        //insert data
//                        int looper = dt.Columns.Count;
//                        StringBuilder insertScript = null;
//                        foreach (DataRow row in dt.Rows)
//                        {
//                            insertScript = new StringBuilder();
//                            insertScript.Append("INSERT INTO [Sheet1] VALUES(");

//                            //populate data
//                            for (int i = 0; i < looper; i++)
//                            {
//                                insertScript.AppendFormat("'{0}',", row[i].ToString().Replace("'", "''"));
//                            }

//                            //remove trailing comma
//                            insertScript.Remove((insertScript.Length - 1), 1);
//                            insertScript.Append(")");

//                            command.CommandText = insertScript.ToString();
//                            command.CommandType = CommandType.Text;

//                            command.ExecuteNonQuery();
//                            insertScript = null;
//                        }

//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                ErrorMgt.WriteLog(ex);
//            }
//        }
//    }
//}
