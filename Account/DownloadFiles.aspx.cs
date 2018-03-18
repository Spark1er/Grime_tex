using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using GrimeTex;

namespace Account
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Context.User.Identity.IsAuthenticated)
                Response.Redirect("~/Default.aspx");
        }

        protected void Upload(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile) return;
            if (FileUpload1.PostedFile.ContentLength < 3100000)
            {
                var path = Request.PhysicalApplicationPath + "Files\\" + User.Identity.Name;
                Directory.CreateDirectory(path);

                var pathS = path + "\\" + Server.HtmlEncode(FileUpload1.FileName);
                FileUpload1.SaveAs(pathS);

                var nFile = new SourceFile { Name = FileUpload1.FileName, UrlFile = pathS, User = Context.User.Identity.Name };
                using (var DB = new ApplicationDbContext())
                {
                    DB.SourceFiles.Add(nFile);
                    DB.SaveChanges(); //добавление объекта и сохранение изменений в бд
                }
                UploadStatusLabel.CssClass = "label label-success";
                UploadStatusLabel.Text = nFile.Name + "  успешно загружен";
            }
            else
            {
                UploadStatusLabel.CssClass = "label label-danger";
                UploadStatusLabel.Text = "Превышен максимальный размер файла.";
            }
        }


        protected void Compile(object sender, EventArgs e)
        {
            string appFolder = Request.PhysicalApplicationPath + "Files\\" + User.Identity.Name;
            string appPath = appFolder + "\\LaTexFullWork.tex";

            if (File.Exists(appPath)) //существует ли указанный файл
            {
                InProgramUploadStatusLabel.Text = "Происходит загрузка, подождите";

                //Выход во внешнюю программу
                string path = appFolder + "\\start.bat";

                if (File.Exists(path)) // удалить файл, если он вдруг существует
                    File.Delete(path);

                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("latexmk -pdf LaTexFullWork.tex");
                    fs.Write(info, 0, info.Length);
                }

                // Create the ProcessInfo object
                var psi = new ProcessStartInfo("cmd.exe")
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = Request.PhysicalApplicationPath + "Files\\" + User.Identity.Name + "\\"
                };
                // Start the process
                var proc = Process.Start(psi);
                // Open the batch file for reading
                var strm = File.OpenText(path);
                // Attach the output for reading
                if (proc != null)
                {
                    var sOut = proc.StandardOutput;
                    // Attach the in for writing
                    var sIn = proc.StandardInput;
                    // Write each line of the batch file to standard input
                    while (strm.Peek() != -1)
                    {
                        sIn.WriteLine(strm.ReadLine());
                    }
                    strm.Close();
                    // Exit CMD.EXE
                    var stEchoFmt = "# {0} run successfully. Exiting";
                    sIn.WriteLine(stEchoFmt, path);
                    sIn.WriteLine("EXIT");
                    // Close the process
                    proc.Close();
                    // Read the sOut to a string.
                    var trim = sOut.ReadToEnd().Trim();
                    // Close the io Streams;
                    sIn.Close();
                    sOut.Close();
                }


                string pdfPath = appFolder + "\\LaTexFullWork.pdf";
                string file = fileName.Text + ".pdf";
                string readyFileTrack = Request.PhysicalApplicationPath + "PDF\\" + User.Identity.Name + "\\";
                string url = "PDF/" + User.Identity.Name + "/" + fileName.Text + ".pdf";

                if (File.Exists(pdfPath))
                {
                    if (!Directory.Exists(readyFileTrack))
                        Directory.CreateDirectory(readyFileTrack);

                    //перенос файла в папку CompleteFile
                    File.Copy(appFolder + "\\LaTexFullWork.pdf", readyFileTrack + file);

                    Label1.Text = "<a href='/Files/" + User.Identity.Name +
                                  "/LaTexFullWork.pdf'> Получить ваш документ</a>";

                    var newFile = new ReadyFile() { Name = file, UrlFile = url, User = Context.User.Identity.Name, Date = DateTime.Now };
                    using (var DB = new ApplicationDbContext())
                    {
                        DB.ReadyFiles.Add(newFile);
                        DB.SaveChanges();
                    }
                }


                //Удаление директории пользователя после загрузки
                // Thread.Sleep(10000000);
                // Delete(sender, e);
            }
            else
            {
                InProgramUploadStatusLabel.Text = "Для продолжения необходим документ с названием LaTexFullWork.tex!";
            }

        }

        protected void Delete(object sender, EventArgs e)
        {
            try
            {
                string appPath = Request.PhysicalApplicationPath + "Files\\" + User.Identity.Name.ToString();
                Directory.Delete(appPath, true); //удаление папки

                using (var DB = new ApplicationDbContext())//удаление записей из базы
                {
                    var query = from SourceFile in DB.SourceFiles
                                where SourceFile.User == User.Identity.Name
                                select SourceFile;

                    foreach (SourceFile file in query)
                    {
                        DB.SourceFiles.Remove(file);
                    }
                    DB.SaveChanges();
                }
            }
            catch (Exception ex) { Label1.Text = ex.Message; }
        }
        public IEnumerable<SourceFile> Files() //выборка файлов конкретного юзера
        {
            using (var DB = new ApplicationDbContext())
            {
                var query = from SourceFile in DB.SourceFiles
                            where SourceFile.User == User.Identity.Name
                            select SourceFile;

                foreach (var file in query)
                {
                    yield return file;
                }
            }

        }
    }
}