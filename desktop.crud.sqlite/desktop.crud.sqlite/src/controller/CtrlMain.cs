using desktop.crud.sqlite.src.db;
using desktop.crud.sqlite.src.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * author		Antonio Membrides Espinosa
 * update       13/08/2019
 * version    	1.0
 */
namespace desktop.crud.sqlite.src.ctrl
{
  /*  class CtrlMain
    {
        private FormMain viewmain;
        private FormAdd viewadd;

        public CtrlMain()
        {
            KsManager.self().setPath("C:/Users/jose.rod/dev/demo.ms.net/desktop.crud.sqlite/resource/data/storage.db");
        }

        public FormMain getViewMain()
        {
            return this.viewmain;
        }

        public void setViewMain(FormMain view)
        {
            this.viewmain = view;
            this.viewmain.tsmiSave.Click += new System.EventHandler((object sender, EventArgs e) => this.onSave());
            this.viewmain.tsmiExit.Click += new System.EventHandler((object sender, EventArgs e) => this.onExit());
            this.viewmain.tsmiLoad.Click += new System.EventHandler((object sender, EventArgs e) => this.onLoad());
            this.viewmain.tsmiAdd.Click += new System.EventHandler((object sender, EventArgs e) => this.onShowAdd());
            this.viewmain.tsmiDel.Click += new System.EventHandler((object sender, EventArgs e) => this.onDel());
            this.viewmain.tsmiSave.Click += new System.EventHandler((object sender, EventArgs e) => this.onSave());
            this.viewmain.tsmiDetail.Click += new System.EventHandler((object sender, EventArgs e) => this.onDetail());
            this.onLoad();
        }

        public void setViewAdd(FormAdd view) {
            this.viewadd = view;
            this.viewadd.btnOk.Click += new System.EventHandler(this.onAddOk);
            this.viewadd.btnCancel.Click += new System.EventHandler((object sender, EventArgs e) => this.onHide());
        }

        private void onDel()
        {
            if (KsManager.self().connect())
            {
                KsManager.self().exec(string.Format(
                    "DELETE FROM {0} WHERE id = '{1}';",
                    this.getCurrentPerson().table,
                    this.getCurrentPerson().id
                ));
                this.onLoad();
                this.viewadd.Hide();
            }
            else
            {
                Console.Write("SQLite connections error... ");
            }
        }

        private void onDetail()
        {
            this.viewadd.action = "detail";
            this.viewadd.tbDni.Text = this.getCurrentPerson().dni.ToString();
            this.viewadd.tbFirstname.Text = this.getCurrentPerson().firstname;
            this.viewadd.tbLastname.Text = this.getCurrentPerson().lastname;
            this.viewadd.tbAge.Text = this.getCurrentPerson().age;
            this.viewadd.tbUser.Text = this.getCurrentPerson().user;
            this.viewadd.Show();
        }

        private void onHide() {
            this.viewadd.Hide();
        }

        private void onAddOk(object sender, EventArgs e)
        {
            if (this.viewadd.action == "add") {
                if (KsManager.self().connect())
                {
                    KsManager.self().exec(string.Format(
                        "INSERT INTO person (dni, firstname, lastname, age, user) VALUES ({0},'{1}','{2}',{3},'{4}');",
                        this.viewadd.tbDni.Text,
                        this.viewadd.tbFirstname.Text,
                        this.viewadd.tbLastname.Text,
                        this.viewadd.tbAge.Text,
                        this.viewadd.tbUser.Text
                    ));
                    this.onLoad();
                }
                else
                {
                    Console.Write("SQLite connections error... ");
                }
            }
            this.viewadd.Hide();
        }

        private void onShowAdd()
        {
            this.viewadd.action = "add";
            this.viewadd.Show();
        }

        private void onSave()
        {
            int index = this.viewmain.dgvmain.CurrentRow.Index;
            KsManager.self().exec(string.Format(
                "UPDATE {6}  SET dni = '{0}', firstname = '{1}', lastname = '{2}', age = '{3}', user = '{4}' WHERE id = '{5}';",
                this.getCurrentPerson().dni,
                this.getCurrentPerson().firstname,
                this.getCurrentPerson().lastname,
                this.getCurrentPerson().age,
                this.getCurrentPerson().user,
                this.getCurrentPerson().id,
                this.getCurrentPerson().table
            ));
        }

        private void onLoad()
        {
            if (KsManager.self().connect())
            {
                this.viewmain.dgvmain.DataSource = KsManager.self().getAt("person");
                this.viewmain.dgvmain.Update();
                this.viewmain.dgvmain.Refresh();
            }
            else
            {
                Console.Write("SQLite connections error... ");
            }
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

        private PersonModel getCurrentPerson() {
            int index = this.viewmain.dgvmain.CurrentRow.Index;
            PersonModel person = new PersonModel(
                Convert.ToInt32(this.viewmain.dgvmain.Rows[index].Cells[0].Value.ToString()),
                Convert.ToInt64(this.viewmain.dgvmain.Rows[index].Cells[1].Value.ToString()),
                this.viewmain.dgvmain.Rows[index].Cells[2].Value.ToString(),
                this.viewmain.dgvmain.Rows[index].Cells[3].Value.ToString(),
                this.viewmain.dgvmain.Rows[index].Cells[4].Value.ToString(),
                this.viewmain.dgvmain.Rows[index].Cells[5].Value.ToString()
            );
            return person;
        }
    }*/
}
