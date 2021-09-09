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

namespace Cadastro
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }
        private MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "cadastro";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        private void atualizarGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM cliente WHERE ativocliente = 1 ";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dgcadastro.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dgcadastro.Rows[0].Clone();//FAZ UM CAST E CLONA A LINHA DA TABELA
                    row.Cells[0].Value = reader.GetInt32(0);//ID
                    row.Cells[1].Value = reader.GetString(1);//NOME
                    row.Cells[2].Value = reader.GetString(2);//ANO NASC
                    row.Cells[3].Value = reader.GetString(3);//PREFERÊNCIAS
                    row.Cells[4].Value = reader.GetString(4);//NÍVEL FIDELIDADE
                    dgcadastro.Rows.Add(row);//ADICIONO A LINHA NA TABELA

                }

                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }

        }

        private void btlimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()

        {
            tbId.Clear();
            tbNome.Clear();
            tbAno.Clear();
            tbPref.Clear();
            tbNivel.Clear();
            
        }

        private void btcadastrar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();

                comandoMySql.CommandText = "INSERT INTO cliente (idCliente,nomeCliente,dataNasc,clientePef,nivelFid) " +
                    "VALUES('" + tbId.Text + "', '" + tbNome.Text + "','" + Convert.ToInt16(tbAno.Text) + "', '" + tbPref.Text + "', '" + tbNivel.Text + "')";
                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close();
                MessageBox.Show("Inserido com sucesso");
                atualizarGrid();
                limparCampos();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}