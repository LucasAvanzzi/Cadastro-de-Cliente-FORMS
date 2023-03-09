using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aula_Katia_BD01_L
{
    public partial class TelaCliente : Form
    {
        public TelaCliente()
        {
            InitializeComponent();
        }

        Conexao con = new Conexao();

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {

                string sql = "insert into tb_cliente values (null, " +
                txtNome.Text + "','" + mtxtCelular.Text + "','" +
                mtxtCPF.Text + "', null);";

                if (con.Executar(sql))
                {
                    MessageBox.Show("Cadastrado com sucesso!");
                }
                else
                {
                    MessageBox.Show("Não cadastrado!");
                }
            }
            else
            {
                string sql = "update tb_cliente set cli_cpf'" + mtxtCPF.Text +
                    "',cli_nome='" + txtNome.Text + "', cli_celular='" +
                    mtxtCelular.Text + "'where cli_id=" + txtID.Text;

                if (con.Executar(sql))
                {
                    MessageBox.Show("Atualizado com sucesso");
                }
                else
                {
                    MessageBox.Show("Não cadastrado!");
                }
            }
        }
    }

}