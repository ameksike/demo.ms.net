using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
/*
 * author		Antonio Membrides Espinosa
 * email        tonykssa@gmail.com
 * update       27/08/2019
 * version    	1.0
 * dependencies System.Net, SSH.NET 
 */
namespace Ks.RFS
{
    public delegate void CallbackOnList(DirInfo data);
    public delegate void CallbackOnError(string data);

    public class DirInfo
    {
        public string name;
        public string path;
        public string full;
        public string ext;
        public bool isdir;
    }

    public abstract class Driver
    {
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string protocol { get; set; }

        public CallbackOnError log;
        public abstract bool connect(string path = "");
        public abstract bool disconnect();
        public abstract bool exists(string search);
        public abstract bool download(string file, string pathIn, string pathOut);
        public abstract bool download(string pathIn, string pathOut);
        public abstract bool upload(string pathIn, string pathOut);
        public abstract bool upload(string file, string pathIn, string pathOut);
        public abstract void list(string path, CallbackOnList callback = null);
    }

    public class DriverFTP : Driver
    {
        protected FtpWebRequest request;

        public DriverFTP()
        {
            this.host = "localhost";
            this.port = "21";
            this.user = "anonymous";
            this.pass = "";
            this.protocol = "ftp";
            this.log = null;
        }

        public override bool connect(string path = "")
        {
            try
            {
                this.request = (FtpWebRequest)WebRequest.Create(this.url(path));
                request.Credentials = new NetworkCredential(this.user, this.pass);
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
                return false;
            }
            return true;
        }

        public override bool disconnect()
        {
            return true;
        }

        public override bool download(string file, string pathIn, string pathOut)
        {
            return this.download(pathIn + file, pathOut + "\\" + file);
        }

        public override bool download(string pathIn, string pathOut)
        {
            try
            {
                if (this.connect(pathIn))
                {
                    this.request.Method = WebRequestMethods.Ftp.DownloadFile;
                    this.request.UseBinary = true;
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    Stream responsestream = response.GetResponseStream();
                    this.copyStream(responsestream, pathOut);
                }
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
                return false;
            }
            return true;
        }

        public override bool exists(string search)
        {
            bool tmp = true;
            try
            {
                if (this.connect(search))
                {
                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        tmp = false;
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        tmp = true;
                    }
                }
            }

            return tmp;
        }

        public override void list(string path, CallbackOnList callback = null)
        {
            StreamReader reader = null;
            try
            {
                if (this.connect(path))
                {
                    request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    reader = new StreamReader(request.GetResponse().GetResponseStream());
                }
            }
            catch (Exception error)
            {
                reader = null;
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
            }
            if (reader != null && callback != null)
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string name = line.Substring(line.LastIndexOf(" ") + 1);


                    if (callback != null)
                    {
                        callback(new DirInfo()
                        {
                            name = name,
                            full = path + name,
                            ext = System.IO.Path.GetExtension(name),
                            //  isdir = this.exists(path + name), //... lenta
                            isdir = (System.IO.Path.GetExtension(name) == string.Empty),  //... rapida no segura
                            path = path
                        });
                    }
                }
            }
        }

        public override bool upload(string pathIn, string pathOut)
        {
            throw new NotImplementedException();
        }

        public override bool upload(string file, string pathIn, string pathOut)
        {
            throw new NotImplementedException();
        }

        public virtual string url(string path = "")
        {
            return "ftp://" + this.host + ":" + this.port + ((path == "" ? "" : "/" + path));
        }

        protected void copyStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
        }
    }

    public class DriverFTPS : DriverFTP
    {
        public override string url(string path = "")
        {
            return "ftp://" + this.host + ((path == "" ? "" : "/" + path));
        }

        public override bool connect(string path)
        {
            try
            {
                Uri target = new Uri(this.url(path));
                this.request = (FtpWebRequest)WebRequest.Create(target);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(this.user, this.pass);
                request.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
                return false;
            }
            return true;
        }

        public void setServerCertificateValidation()
        {
            RemoteCertificateValidationCallback orgCallback = ServicePointManager.ServerCertificateValidationCallback;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(onValidateServerCertificate);
                ServicePointManager.Expect100Continue = true;
            }
            finally
            {
                ServicePointManager.ServerCertificateValidationCallback = orgCallback;
            }
        }

        public bool onValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }

    public class DriverSFTP : Driver
    {
        protected Renci.SshNet.SftpClient client;

        public override bool connect(string path = "")
        {
            try
            {
                this.client = new Renci.SshNet.SftpClient(this.host, this.user, this.pass);
                this.client.Connect();
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
                return false;
            }
            return true;
        }

        public override bool disconnect()
        {
            if (this.client != null)
            {
                this.client.Disconnect();
                return true;
            }
            return false;
        }

        public override bool download(string file, string pathIn, string pathOut)
        {
            return this.download(pathIn + file, pathOut + "\\" + file);
        }

        public override bool download(string pathIn, string pathOut)
        {
            try
            {
                if (this.connect())
                {
                    using (var file = File.OpenWrite(pathOut))
                    {
                        this.client.DownloadFile(pathIn, file);
                    }
                    this.disconnect();
                }
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
                return false;
            }
            return true;
        }

        public override bool exists(string search)
        {
            throw new NotImplementedException();
        }

        public override void list(string path, CallbackOnList callback = null)
        {
            try
            {
                if (this.connect())
                {
                    this.client.ChangeDirectory(path);
                    foreach (var entry in client.ListDirectory("."))
                    {
                        if (callback != null)
                        {
                            if (entry.Name != "." && entry.Name != "..")
                            {
                                callback(new DirInfo()
                                {
                                    name = entry.Name,
                                    full = entry.FullName,
                                    isdir = entry.IsDirectory,
                                    ext = System.IO.Path.GetExtension(entry.Name),
                                    path = path
                                });
                            }
                        }
                    }
                    this.disconnect();
                }
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
            }
        }

        public override bool upload(string pathIn, string pathOut)
        {
            throw new NotImplementedException();
        }

        public override bool upload(string file, string pathIn, string pathOut)
        {
            throw new NotImplementedException();
        }
    }

    public class Service
    {
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string protocol { get; set; }

        public CallbackOnError log;

        public Service()
        {
            this.host = "localhost";
            this.port = "21";
            this.user = "anonymous";
            this.pass = "";
            this.protocol = "ftp";
            this.log = null;
        }

        public bool upload(string file, string pathIn, string pathOut)
        {
            var driver = this.getDriver();
            if (driver != null)
            {
                return driver.upload(file, pathIn, pathOut);
            }
            return false;
        }

        public bool download(string file, string pathIn, string pathOut)
        {
            var driver = this.getDriver();
            if (driver != null)
            {
                return driver.download(file, pathIn, pathOut);
            }
            return false;
        }

        public void list(string path, CallbackOnList callback = null)
        {
            var driver = this.getDriver();
            if (driver != null)
            {
                driver.list(path, callback);
            }
        }

        protected Driver getDriver()
        {
            string classname = "Ks.RFS.Driver" + this.protocol.ToUpper();
            Type cntype = Type.GetType(classname);
            try
            {
                var obj = Activator.CreateInstance(cntype) as Driver;
                if (obj != null)
                {
                    obj.protocol = this.protocol;
                    obj.host = this.host;
                    obj.port = this.port;
                    obj.user = this.user;
                    obj.pass = this.pass;
                    obj.log = this.log;
                }
                return obj;
            }
            catch (Exception error)
            {
                if (this.log != null)
                {
                    this.log(error.Message.ToString());
                }
            }
            return null;
        }
    }
}
