using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFGravarDadosMySQL
{
    public partial class Form1 : Form
    {

        MySqlConnection Conexao;
        string data_source = "datasource=localhost;username=root;password=;database=db_agenda";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try //tentar conexão
            {


                Conexao = new MySqlConnection(data_source);

                string sql = "INSERT INTO contato (nome,email,telefone)" +
                    "VALUES" +
                    "(' " + txtNome.Text + "', '" + txtEmail.Text + "', '" + txtTelefone.Text + "')";

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                comando.ExecuteReader();

                MessageBox.Show("Cadastro inserido com sucesso meu nobre");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                Conexao.Close();
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                Conexao = new MySqlConnection(data_source);

                string q = "'%" + txt_buscar.Text + "%'";

                //SELECT * FROM CONTATO WHERE NOME %(ALGUM NOME)%
                string sql = "SELECT * FROM contato WHERE nome LIKE" + q + "OR email Like" + q;

                MySqlCommand comando = new MySqlCommand(sql, Conexao);

                Conexao.Open();
                //lê os dados
                MySqlDataReader reader = comando.ExecuteReader();

                //limpa a lista
                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row = {


                    reader.GetString(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                };
                    //criando uma linha
                    var linhaListView = new ListViewItem(row);
                    //adicionado a linha no lst_contatos
                    lst_contatos.Items.Add(linhaListView);

            }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {
                Conexao.Close();

            }
        }
    }
}
