using Neo4jClient;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VetAM.DomainModel;

namespace VetAM
{
    public partial class Form1 : Form
    {
        string ime;
        Thread th;
        private GraphClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "ajmk");
            try
            {
                client.Connect();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void btnZatvori_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtIme_Click(object sender, EventArgs e)
        {
            if (txtIme.Text == "korisničko ime")
                txtIme.Text = "";
            animacijaPrijave(0);
        }

        private void txtLozinka_Click(object sender, EventArgs e)
        {
            if (txtLozinka.Text == "Lozinka")
                txtLozinka.Text = "";
            animacijaPrijave(1);
        }

        private void animacijaPrijave(int vrednost)
        {
            if (vrednost == 0)
            {
                pnlIme.BackColor = txtIme.ForeColor = System.Drawing.Color.FromArgb(78, 184, 206);
                pnlLozinka.BackColor = txtLozinka.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                pnlIme.BackColor = txtIme.ForeColor = System.Drawing.Color.White;
                pnlLozinka.BackColor = txtLozinka.ForeColor = System.Drawing.Color.FromArgb(78, 184, 206);
            }
        }

        private void txtIme_TextChanged(object sender, EventArgs e)
        {
            if (txtIme.Text == "korisničko ime")
                txtIme.Text = "";
            animacijaPrijave(0);
        }

        private void txtLozinka_TextChanged(object sender, EventArgs e)
        {
            if (txtLozinka.Text == "Lozinka")
                txtLozinka.Text = "";
            animacijaPrijave(1);
        }

        private void txtLozinka_Leave(object sender, EventArgs e)
        {
            txtLozinka.Text = txtLozinka.Text.ToLower();
        }

        private void txtIme_Leave(object sender, EventArgs e)
        {
            txtLozinka.Text = txtLozinka.Text.ToLower();
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            //queryDict.Add("username", txtIme.Text);
            //queryDict.Add("password", txtLozinka.Text);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Veterinar) where n.username = '" + txtIme.Text + "' AND n.password = '"+ txtLozinka.Text + "' return n",
                                        queryDict, CypherResultMode.Set);

            List<Veterinar> vet = ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();

            if (vet.Count == 0)
            {
                lblGreska.Visible = true;
                return;
            }
            else
            {
                ime = vet[0].username;
                th = new Thread(openNewForm);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                this.Close();
            }
        }

        private void openNewForm()
        {
            Application.Run(new Glavna(ime, client));
        }
    }
}
