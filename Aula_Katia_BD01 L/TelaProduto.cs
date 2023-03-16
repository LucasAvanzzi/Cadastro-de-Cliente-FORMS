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
    public partial class TelaProduto : Form
    {
        public TelaProduto()
        {
            InitializeComponent();
        }


        Conexao con = new Conexao();

        private void CarregaCategoria()
        {
            cbxCategoria.DataSource = null;
            cbxCategoria.DataSource = con.Retorna(
                "select * from tb_categoria"
                );
            cbxCategoria.DisplayMember = "cat_descricao";
            cbxCategoria.ValueMember = "cat_id";
        }

        private void CarregaTabela()
        {
            dgvDados.Source = null;
            DataTable dados = con.Retorna(
                "select * from tb_categoria"
                );
            dgvDados.DataSource = dados;
        }

        private void TelaProduto_Load(object sender, EventArgs e)
        {
            CarregaCategoria();
            CarregaTabela();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = "delete from tb_produto where prod_id=" +
                txtCodigo.Text;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            string sql = "update tb_produto set prod_name='"+
                txtNome.Text + "', prod_descricao='" + txtDescricao.Text +
                "', prod_categoria=" + cbxCategoria.SelectedValue +
                ", prod_valor" + txtValor.Text + "where prod_codigo="+
                txtCodigo.Text;
            if(con.Executar(sql))
            {
                MessageBox.Show("Atualizado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao atualizar!");
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string sql="insert into tb_produto values (null, '" +
                txtNome.Text + "', '" + txtDescricao.Text + "', " +
                cbxCategoria.SelectedValue + ", " + txtValor.Text + ")";
            if (con.Executar(sql))
            {
                MessageBox.Show("Cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao cadastrar!");
            }
        }
    }
}
