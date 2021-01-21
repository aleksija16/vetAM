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
    public partial class Glavna : Form
    {
        public GraphClient client;
        public Glavna(string ime, GraphClient cl)
        {
            InitializeComponent();
            client = cl;
            lblUlogovan.Text = ime;
        }

        private void btnZatvoriGlavnu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDodajZivortinju_Click(object sender, EventArgs e)
        {
            int god, idVlas;
            if (txtZivIme.Text == "" || txtZivVrsta.Text == "" || !Int32.TryParse(txtZivIdVlasnika.Text, out idVlas) || !Int32.TryParse(txtZivStarost.Text, out god))
            {
                MessageBox.Show("Unesite lepo podatke o zivotinji.");
                return;
            }
            Dictionary<string, object> queryDict2 = new Dictionary<string, object>();
            queryDict2.Add("idVlasnik", txtZivIdVlasnika.Text);
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Vlasnik) where n.idVlasnik = '" + txtZivIdVlasnika.Text + "' return n",
                                        queryDict2, CypherResultMode.Set);

            List<Vlasnik> vla = ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();

            if (vla.Count == 0)
            {
                MessageBox.Show("Vlasnik sa zadatim ID ne postoji.");
                return;
            }

            Zivotinja z = new Zivotinja();
            z.vrsta = txtZivVrsta.Text;
            z.ime = txtZivIme.Text;
            int starost;
            if (!Int32.TryParse(txtZivStarost.Text, out starost))
            {
                MessageBox.Show("Starost zivotinje je ceo broj (broj godina).");
                return;
            }
            z.starost = starost;
            z.opis = txtZivOpis.Text;
            z.pregledi = new List<Pregled>();
            z.idZivotinja = Int32.Parse(nadjiMaxIdZivotinja()) + 1;
            z.vlasnik = vla[0];
            if (vla[0].zivotinje == null)
                vla[0].zivotinje = new List<Zivotinja>();
            vla[0].zivotinje.Add(z);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Zivotinja {idZivotinja:'" + z.idZivotinja + "', vrsta:'" + z.vrsta + "', ime:'" + z.ime
                                                    + "', starost:'" + z.starost + "', opis:'" + z.opis
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();



            Dictionary<string, object> queryDict1 = new Dictionary<string, object>();

            string s = "MATCH (a:Zivotinja),(b:Vlasnik) WHERE a.idZivotinja = '" + z.idZivotinja + "' AND b.idVlasnik = '" + txtZivIdVlasnika.Text +
                                                       "' CREATE (a) -[r:PRIPADA]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict1, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query);

            MessageBox.Show("Uspesno ste dodali novu zivotinju. Id: " + z.idZivotinja.ToString());
        }

        private string nadjiMaxIdZivotinja()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Zivotinja) and exists(n.idZivotinja) return max(n.idZivotinja)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {}

        private void btnDodajVlasnika_Click(object sender, EventArgs e)
        {
            if (txtVlasnikIme.Text == "" || txtVlasnikPrezime.Text == "")
            {
                MessageBox.Show("Unesite lepo podatke o vlasniku.");
                return;
            }
            Vlasnik v = new Vlasnik();
            v.ime = txtVlasnikIme.Text;
            v.prezime = txtVlasnikPrezime.Text;
            v.telefon = txtVlasnikTelefon.Text;
            v.mail = txtVlasnikMail.Text;
            v.zivotinje = new List<Zivotinja>();
            v.idVlasnik = Int32.Parse(nadjiMaxIdVlasnik()) + 1;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Vlasnik {idVlasnik:'" + v.idVlasnik + "', ime:'" + v.ime + "', prezime:'" + v.prezime
                                                    + "', telefon:'" + v.telefon + "', mail:'" + v.mail
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();
            MessageBox.Show("Uspesno ste dodali novog vlasnika. Id: " + v.idVlasnik.ToString());
        }

        private string nadjiMaxIdVlasnik()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Vlasnik) and exists(n.idVlasnik) return max(n.idVlasnik)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDodajVeterinara_Click(object sender, EventArgs e)
        {
            if (txtVeterinarIme.Text == "" || txtVeterinarPrezime.Text == "" || txtVeterinarUsername.Text == "" || txtVeterinarPassword.Text == "")
            {
                MessageBox.Show("Unesite lepo podatke o veterinaru.");
                return;
            }
            Veterinar v = new Veterinar();
            v.ime = txtVeterinarIme.Text;
            v.prezime = txtVeterinarPrezime.Text;
            v.telefon = txtVeterinarTelefon.Text;
            v.mail = txtVeterinarMail.Text;
            v.username = txtVeterinarUsername.Text;
            v.password = txtVeterinarPassword.Text;
            v.adresa = txtVeterinarAdresa.Text;
            v.titula = txtVeterinarTitula.Text;
            v.pregledi = new List<Pregled>();
            v.idVeterinar = Int32.Parse(nadjiMaxIdVeterinar()) + 1;

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            var query = new Neo4jClient.Cypher.CypherQuery("CREATE (n:Veterinar {idVeterinar:'" + v.idVeterinar + "', ime:'" + v.ime + "', prezime:'" + v.prezime
                                                    + "', username: '" + v.username + "', password: '" + v.password + "', adresa: '" + v.adresa + "', titula: '" + v.titula +
                                                    "', telefon:'" + v.telefon + "', mail:'" + v.mail
                                                    + "'}) return n",
                                                    queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();
            MessageBox.Show("Uspesno ste dodali novog veterinara. Id: " + v.idVeterinar.ToString());
        }

        private string nadjiMaxIdVeterinar()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Veterinar) and exists(n.idVeterinar) return max(n.idVeterinar)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void btnNadjiVlasnika_Click(object sender, EventArgs e)
        {
            if (txtNadjiVlasnika.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + txtNadjiVlasnika.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("vlasnik", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Vlasnik) and (exists(n.ime) and n.ime =~ {vlasnik}) " +
                "OR (exists(n.prezime) and n.prezime =~ {vlasnik}) OR (exists(n.idVlasnik) and n.idVlasnik =~ {vlasnik}) OR (exists(n.telefon) and n.telefon =~ {vlasnik}) OR (exists(n.mail) and n.mail =~ {vlasnik}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Vlasnik> vlasnici = ((IRawGraphClient)client).ExecuteGetCypherResults<Vlasnik>(query).ToList();
            if (vlasnici.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan vlasnik.");
                return;
            }
            string odgovor = "";
            foreach (Vlasnik v in vlasnici)
            {
                odgovor += "Id: " + v.idVlasnik + "; Ime: " + v.ime + " " + v.prezime + "; Tel: " + v.telefon + "; Mail: " + v.mail + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void btnNadjiZivotinje_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(txtNadjiZivotinje.Text, out id) || id < 1)
            {
                MessageBox.Show("Id vlasnika mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("vlasnik", txtNadjiZivotinje.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:PRIPADA]->(m) " +
                "where exists(m.idVlasnik) and m.idVlasnik = {vlasnik} return n",
                                                            queryDict, CypherResultMode.Set);

            List<Zivotinja> zivotinje = ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();
            if (zivotinje.Count == 0)
            {
                MessageBox.Show("Nije pronadjena nijedna zivotinja.");
                return;
            }
            string odgovor = "";
            foreach (Zivotinja z in zivotinje)
            {
                odgovor += "Id: " + z.idZivotinja + "; Vrsta: " + z.vrsta + "; Ime: " + z.ime + "; Starost: " + z.starost + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void btnNadjiVeterinara_Click(object sender, EventArgs e)
        {
            if (txtNadjiVeterinara.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + txtNadjiVeterinara.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("veterinar", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Veterinar) and (exists(n.ime) and n.ime =~ {veterinar}) " +
                "OR (exists(n.prezime) and n.prezime =~ {veterinar}) OR (exists(n.idVeterinar) and n.idVeterinar =~ {veterinar}) OR (exists(n.username) and n.username =~ {veterinar}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Veterinar> vet = ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();
            if (vet.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan veterinar.");
                return;
            }
            string odgovor = "";
            foreach (Veterinar v in vet)
            {
                odgovor += "Id: " + v.idVeterinar + "; Ime: " + v.ime + " " + v.prezime + "; Tel: " + v.telefon + "; Mail: " + v.mail + 
                    "; Username: " + v.username + "; Titula: " + v.titula + "; Adresa: " + v.adresa + "\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void btnNadjiZivotinju_Click(object sender, EventArgs e)
        {
            if (txtNadjiZivotinju.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + txtNadjiZivotinju.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("zivotinja", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Zivotinja) and (exists(n.ime) and n.ime =~ {zivotinja}) " +
                "OR (exists(n.vrsta) and n.vrsta =~ {zivotinja}) OR (exists(n.idZivotinja) and n.idZivotinja =~ {zivotinja}) OR (exists(n.starost) and n.starost =~ {zivotinja}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Zivotinja> ziv = ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();
            if (ziv.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedna zivotinja.");
                return;
            }
            string odgovor = "";
            foreach (Zivotinja z in ziv)
            {
                odgovor += "Id: " + z.idZivotinja + "; Vrsta: " + z.vrsta + "; Ime: " + z.ime + "; Starost: " + z.starost + "; Opis: " + z.opis + "; Mikrocip: " + z.mikrocip + "\n";
            }
            MessageBox.Show(odgovor);
        }

        private void Glavna_Load(object sender, EventArgs e)
        {

        }

        private void btnPregledSacuvaj_Click(object sender, EventArgs e)
        {
            int idVeterinara, idZivotinje;
            double cena;
            if (txtPregledDijagnoza.Text == "" || txtPregledTerapija.Text == "" || !Double.TryParse(txtPregledCena.Text, out cena) || !Int32.TryParse(txtPregledIdZivotinje.Text, out idZivotinje) || !Int32.TryParse(txtPregledIdVeterinara.Text, out idVeterinara))
            {
                MessageBox.Show("Unesite lepo podatke o pregledu.");
                return;
            }
            Dictionary<string, object> queryDict10 = new Dictionary<string, object>();
            var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Veterinar) where n.idVeterinar = '" + txtPregledIdVeterinara.Text + "' return n",
                                        queryDict10, CypherResultMode.Set);

            List<Veterinar> vet = ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();

            if (vet.Count == 0)
            {
                MessageBox.Show("Veterinar sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Zivotinja) where n.idZivotinja = '" + txtPregledIdZivotinje.Text + "' return n",
                            queryDict10, CypherResultMode.Set);

            List<Zivotinja> ziv = ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();

            if (ziv.Count == 0)
            {
                MessageBox.Show("Zivotinja sa zadatim ID ne postoji.");
                return;
            }

            Pregled p = new Pregled();
            p.cenaPregleda = cena;
            p.dijagnoza = txtPregledDijagnoza.Text;
            p.terapija = txtPregledTerapija.Text;
            p.datum = dtpPregledDatum.Value.ToString();
            p.idPregled = Int32.Parse(nadjiMaxIdPregleda()) + 1;

            if (vet[0].pregledi == null)
                vet[0].pregledi = new List<Pregled>();
            vet[0].pregledi.Add(p);

            if (ziv[0].pregledi == null)
                ziv[0].pregledi = new List<Pregled>();
            ziv[0].pregledi.Add(p);

            Dictionary<string, object> queryDict = new Dictionary<string, object>();

            string s = "CREATE (n:Pregled {idPregled:'" + p.idPregled + "', dijagnoza:'" + p.dijagnoza + "', terapija:'" + p.terapija
                                                     + "', datum: '" + p.datum + "', cenaPregleda: '" + p.cenaPregleda
                                                    + "'}) return n";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query).ToList();

            s = "MATCH (a:Pregled),(b:Zivotinja) WHERE a.idPregled = '" + p.idPregled + "' AND b.idZivotinja = '" + idZivotinje +
                                                       "' CREATE (a) -[r:IZVRSEN_NAD]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query);

            s = "MATCH (a:Pregled),(b:Veterinar) WHERE a.idPregled = '" + p.idPregled + "' AND b.idVeterinar = '" + idVeterinara +
                                           "' CREATE (a) -[r:IZVRSEN_OD_STRANE]->(b) RETURN a";
            query = new Neo4jClient.Cypher.CypherQuery(s, queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query);

            MessageBox.Show("Uspesno ste dodali novi pregled. Id: " + p.idPregled.ToString());
        }

        private string nadjiMaxIdPregleda()
        {
            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Pregled) and exists(n.idPregled) return max(n.idPregled)",
                                                            new Dictionary<string, object>(), CypherResultMode.Set);
            var maxId = ((IRawGraphClient)client).ExecuteGetCypherResults<string>(query).ToList().FirstOrDefault();
            if (maxId == null)
                maxId = 0.ToString();
            return maxId;
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void txtNadjiVeterinara_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNadjiPregled_Click(object sender, EventArgs e)
        {
            if (txtNadjiPregled.Text == "")
            {
                MessageBox.Show("Upisite tekst za pretragu u textbox.");
                return;
            }
            string podatak = ".*" + txtNadjiPregled.Text + ".*";

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("pregled", podatak);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Pregled) and (exists(n.idPregled) and n.idPregled =~ {pregled}) " +
                "OR (exists(n.cenaPregleda) and n.cenaPregleda =~ {pregled}) OR (exists(n.dijagnoza) and n.dijagnoza =~ {pregled}) OR (exists(n.terapija) and n.terapija =~ {pregled}) OR (exists(n.datum) and n.datum =~ {pregled}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Pregled> pre = ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query).ToList();
            if (pre.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan pregled.");
                return;
            }
            string odgovor = "";
            foreach (Pregled p in pre)
            {
                odgovor += "Id: " + p.idPregled + "; Datum: " + p.datum + "; Cena: " + p.cenaPregleda + ";\n Dijagnoza: " + p.dijagnoza + ";\n Terapija: " + p.terapija + "\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void btnPregledObrisi_Click(object sender, EventArgs e)
        {
            if (txtPregledObrisiId.Text == "")
            {
                MessageBox.Show("Upisite id pregleda koji zelite da obrisete.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("pregled", txtPregledObrisiId.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Pregled) and (exists(n.idPregled) and n.idPregled = {pregled}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Pregled> pre = ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query).ToList();
            if (pre.Count == 0)
            {
                MessageBox.Show("Pregled sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Pregled) WHERE n.idPregled = '" + txtPregledObrisiId.Text + "' DETACH DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste obrisali zeljeni pregled. Id: " + txtPregledObrisiId.Text);
        }

        private void btnObrisiSve_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Da li ste sigurni da zelite da obrisete sve podatke iz baze?",
                         "Brisanje svih podataka",
                         MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Dictionary<string, object> queryDict = new Dictionary<string, object>();

                var query = new Neo4jClient.Cypher.CypherQuery("MATCH (n) DETACH DELETE n",
                                                                queryDict, CypherResultMode.Set);

                ((IRawGraphClient)client).ExecuteCypher(query);
                MessageBox.Show("Uspesno ste obrisali sve podatke iz baze!");
            }
            else
                return;
        }

        private void btnObrisiVeterinara_Click(object sender, EventArgs e)
        {
            if (txtObrisiVeterinaraId.Text == "")
            {
                MessageBox.Show("Upisite id veterinara koga zelite da obrisete.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("vet", txtObrisiVeterinaraId.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("start n=node(*) where (n:Veterinar) and (exists(n.idVeterinar) and n.idVeterinar = {vet}) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Veterinar> pre = ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();
            if (pre.Count == 0)
            {
                MessageBox.Show("Veterinar sa zadatim ID ne postoji.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("MATCH (n:Veterinar) WHERE n.idVeterinar = '" + txtObrisiVeterinaraId.Text + "' DETACH DELETE n",
                                                            queryDict, CypherResultMode.Set);

            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste obrisali zeljenog veterinara. Id: " + txtObrisiVeterinaraId.Text);
        }

        private void btnNadjiPreglede_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(txtNadjiPreglede.Text, out id) || id < 1)
            {
                MessageBox.Show("Id zivotinje mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("z", txtNadjiPreglede.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:IZVRSEN_NAD]->(m) " +
                "where exists(m.idZivotinja) and m.idZivotinja = {z} return n ORDER BY n.idPregled",
                                                            queryDict, CypherResultMode.Set);

            List<Pregled> pregledi = ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query).ToList();
            if (pregledi.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan pregled za zeljenu zivotinju.");
                return;
            }
            string odgovor = "";
            foreach (Pregled p in pregledi)
            {
                odgovor += "Id: " + p.idPregled + "; Datum: " + p.datum + "; Cena: " + p.cenaPregleda + "; \nDijagnoza: " + p.dijagnoza + ";\nTerapija: " + p.terapija + ";\n\n";
            }
            MessageBox.Show(odgovor);
        }

        private void btnSviPreglediVet_Click(object sender, EventArgs e)
        {
            int id;
            if (!Int32.TryParse(txtSviPreglediVet.Text, out id) || id < 1)
            {
                MessageBox.Show("Id veterinara mora biti ceo pozitivan broj.");
                return;
            }
            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("v", txtSviPreglediVet.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n)-[r:IZVRSEN_OD_STRANE]->(m) " +
                "where exists(m.idVeterinar) and m.idVeterinar = {v} return n ORDER BY n.idPregled",
                                                            queryDict, CypherResultMode.Set);

            List<Pregled> pregledi = ((IRawGraphClient)client).ExecuteGetCypherResults<Pregled>(query).ToList();
            if (pregledi.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan pregled za zeljenog veterinara.");
                return;
            }
            double uk = 0;
            string odgovor = "";
            foreach (Pregled p in pregledi)
            {
                odgovor += "Id: " + p.idPregled + "; Datum: " + p.datum + "; Cena: " + p.cenaPregleda + ";\n";
                uk += p.cenaPregleda;
            }
            odgovor += "\nUkupno je izvrseno: " + pregledi.Count + " pregled/-a. Ukupni pazar: " + uk.ToString() + ".";
            MessageBox.Show(odgovor);
        }

        private void btnAzuriranjeNovaLozinka_Click(object sender, EventArgs e)
        {
            if (txtAzuriranjeIdVet.Text == "" || txtNewPassword.Text == "")
            {
                MessageBox.Show("Lepo popunite podatke za izmenu.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("veterinar", txtAzuriranjeIdVet.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n:Veterinar) where n.idVeterinar = {veterinar} return n",
                                                            queryDict, CypherResultMode.Set);

            List<Veterinar> vet = ((IRawGraphClient)client).ExecuteGetCypherResults<Veterinar>(query).ToList();
            if (vet.Count == 0)
            {
                MessageBox.Show("Nije pronadjen nijedan veterinar.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("match (n:Veterinar) where n.idVeterinar = {veterinar} SET n.password = '" + txtNewPassword.Text + "'",
                                                            queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno ste sacuvali izmene.\nUsername: " + vet[0].username + "\nNew password: " + txtNewPassword.Text);
        }

        private void button_Click(object sender, EventArgs e) //Ugradnja cipa
        {
            if (txtCipIdZivotinje.Text == "" || txtCipOznaka.Text == "")
            {
                MessageBox.Show("Lepo popunite podatke za ugradnju cipa.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("ziv", txtCipIdZivotinje.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n:Zivotinja) where n.idZivotinja = {ziv} return n",
                                                            queryDict, CypherResultMode.Set);

            List<Zivotinja> zl = ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();
            if (zl.Count == 0)
            {
                MessageBox.Show("Nije pronadjena nijedna zivotinja sa zadatim ID.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("match (n:Zivotinja) where n.idZivotinja = {ziv} SET n.mikrocip = '" + txtCipOznaka.Text + "'",
                                                            queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesna ugradnja cipa.\nID zivotinje: " + zl[0].idZivotinja + "\nOznaka cipa: " + txtCipOznaka.Text);
        }

        private void btnUkloniCip_Click(object sender, EventArgs e)
        {
            if (txtUklanjanjeIdZivotinje.Text == "")
            {
                MessageBox.Show("Lepo popunite podatke za uklanjanje cipa.");
                return;
            }

            Dictionary<string, object> queryDict = new Dictionary<string, object>();
            queryDict.Add("ziv", txtUklanjanjeIdZivotinje.Text);

            var query = new Neo4jClient.Cypher.CypherQuery("match (n:Zivotinja) where n.idZivotinja = {ziv} and exists(n.mikrocip) return n",
                                                            queryDict, CypherResultMode.Set);

            List<Zivotinja> zl = ((IRawGraphClient)client).ExecuteGetCypherResults<Zivotinja>(query).ToList();
            if (zl.Count == 0)
            {
                MessageBox.Show("ID zivotinje nije dobar ili zivotinja nema ugradjen mikrocip.");
                return;
            }

            query = new Neo4jClient.Cypher.CypherQuery("match (n:Zivotinja) where n.idZivotinja = {ziv} REMOVE n.mikrocip",
                                                            queryDict, CypherResultMode.Set);
            ((IRawGraphClient)client).ExecuteCypher(query);

            MessageBox.Show("Uspesno uklanjanje cipa.\nID zivotinje: " + zl[0].idZivotinja);
        }

        private void btnOdjava_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(prijaviSeForma);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            this.Close();
        }

        private void prijaviSeForma()
        {
            Application.Run(new Form1());
        }
    }
}
