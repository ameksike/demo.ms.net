//using KsRFS.src.service;
using System;
using Ks.RFS;
namespace KsRFS.src.controller
{
    class Main : Controller
    {
        private Form1 viewMain;
        private Service server;

        public Main() {
            this.viewMain = null;
            this.server = new Service();
            this.server.log += this.onError;
        }

        public override void setView(Object view)
        {
            switch (view) {
                case Form1 obj:
                    this.viewMain = obj;
                    obj.tsmConnect.Click += new System.EventHandler((object sender, EventArgs e) => this.onConnect());
                    obj.tsmDownload.Click += new System.EventHandler((object sender, EventArgs e) => this.onDownload());
                    obj.tsmExit.Click += new System.EventHandler((object sender, EventArgs e) => this.onExit());
                    obj.tsmList.Click += new System.EventHandler((object sender, EventArgs e) => this.onList());
                    obj.lsbSource.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((object sender, System.Windows.Forms.MouseEventArgs e) => this.onDownload());
                    break;
            }
        }

        private void onConnect()
        {

        }

        private void onSelect()
        {
            
        }

        private void onRead(DirInfo line)
        {
            this.viewMain.lsbSource.Items.Add(line.name + (line.isdir ? "/" : ""));
        }

        private void onList()
        {
            this.updateSetting();
            string path = this.viewMain.tbxSource.Text;
            this.server.list(path, (DirInfo line) => this.onRead(line));
        }

        private void onDownload()
        {
            this.updateSetting();
            string path = this.viewMain.tbxPlace.Text;
            string sour = this.viewMain.tbxSource.Text;
            string file = this.viewMain.lsbSource.SelectedItem.ToString();
            this.server.download(file, sour, path);
        }

        private void updateSetting() {
            this.server.host = String.IsNullOrEmpty(this.viewMain.tbxHost.Text) ? this.server.host : this.viewMain.tbxHost.Text;
            this.server.port = String.IsNullOrEmpty(this.viewMain.tbxPort.Text) ? this.server.port : this.viewMain.tbxPort.Text; 
            this.server.user = String.IsNullOrEmpty(this.viewMain.tbxUser.Text) ? this.server.user : this.viewMain.tbxUser.Text; 
            this.server.pass = String.IsNullOrEmpty(this.viewMain.tbxPass.Text) ? this.server.pass : this.viewMain.tbxPass.Text; 
            this.server.protocol = String.IsNullOrEmpty(this.viewMain.cmbProtocol.Text) ? this.server.protocol : this.viewMain.cmbProtocol.Text;
        }

        private void onExit()
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }

        public void onError(string msg) {
            this.viewMain.lbxLog.Items.Add(msg);
        }

    }
}
