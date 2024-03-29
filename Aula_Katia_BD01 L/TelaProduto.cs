﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
            dvgDados.DataSource = null;
            DataTable dados = con.Retorna("select prod_codigo, prod_nome, " + "prod_descricao, cat_descricao, prod_valor " +
                "from tb_produto inner join tb_categoria on prod_categoria=cat_id ");
            dvgDados.DataSource = dados;
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
            if (con.Executar(sql))
            {
                MessageBox.Show("Excluído com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao excluir!");
            }
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

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            DataTable dados = con.Retorna("select * from tb_produto " + "where prod_codigo=" + txtCodigo.Text);
            txtNome.Text = dados.Rows[0]["prod_nome"].ToString();
            txtDescricao.Text = dados.Rows[0]["prod_descricao"].ToString();
            cbxCategoria.SelectedValue = Convert.ToInt32(
            dados.Rows[0]["prod_categoria"]);
            txtValor.Text = dados.Rows[0]["prod_valor"].ToString();
        }

        private void dvgDados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigo = Convert.ToInt32(dvgDados["prod_codigo",e.RowIndex].Value);
            DataTable dados = con.Retorna("select * from tb_produto " +
            "where prod_codigo=" + codigo);
            txtCodigo.Text = codigo.ToString();
            txtNome.Text = dados.Rows[0]["prod_nome"].ToString();
            txtDescricao.Text = dados.Rows[0]["prod_descricao"].ToString();
            cbxCategoria.SelectedValue = Convert.ToInt32(dados.Rows[0]["prod_categoria"]);
            txtValor.Text = dados.Rows[0]["prod_valor"].ToString();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            var doc = new Document(PageSize.A4);
            doc.SetMargins(40, 40, 40, 40);
            string nomeArquivo = @"C:\Users\curso.ads2\Documents\docProduto.pdf";
            PdfWriter.GetInstance(doc, new FileStream(nomeArquivo, FileMode.Create));
            doc.Open();
            var fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            var fonteTitulo = new iTextSharp.text.Font(fonteBase,32,iTextSharp.text.Font.BOLD);
            var titulo = new Paragraph("RELATÓRIO DE PRODUTOS\n\n", fonteTitulo);
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);


            var tabela = new PdfPTable(5);
            tabela.DefaultCell.BorderWidth = 0;
            tabela.WidthPercentage = 100;
            var fonteCell = new iTextSharp.text.Font(fonteBase, 14, iTextSharp.text.Font.BOLD, BaseColor, BLACK);
            var cell1 = new PdfPCell(new Phrase("Código", fonteCell));
            tabela.AddCell(cell1);
            var cell2 = new PdfPCell(new Phrase("Código", fonteCell));
            tabela.AddCell(cell2);
            var cell3 = new PdfPCell(new Phrase("Código", fonteCell));
            tabela.AddCell(cell3);
            var cell4 = new PdfPCell(new Phrase("Código", fonteCell));
            tabela.AddCell(cell4);
            var cell5 = new PdfPCell(new Phrase("Código", fonteCell));
            tabela.AddCell(cell5);

            DataTable dados = con.Retorna(
                "select * from tb_produto inner join tb_categoria on prod_categoria=cat_id"
                );

            for (int i = 0; i < dados.Rows.Count; i++)
            {

                var campo1 = new PdfPCell(new Phrase(dados.Rows[i]["prod_id"].ToString(), fonteCell));
                tabela.AddCell(campo1);
                var campo2 = new PdfPCell(new Phrase(dados.Rows[i]["prod_nome"].ToString(), fonteCell));
                tabela.AddCell(campo2);
                var campo3 = new PdfPCell(new Phrase(dados.Rows[i]["prod_descricao"].ToString(), fonteCell));
                tabela.AddCell(campo3);
                var campo4 = new PdfPCell(new Phrase(dados.Rows[i]["cat_descricao"].ToString(), fonteCell));
                tabela.AddCell(campo4);
                var campo5 = new PdfPCell(new Phrase(dados.Rows[i]["prod_valor"].ToString(), fonteCell));
                tabela.AddCell(campo5);

            }
            doc.Add( tabela );
            doc.Close();

          //  doc.Close();  //
         //   var caminhoPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nomeArquivo);  //
         //   if (File.Exists(caminhoPdf))  //
         //   {  //
          //      Process.Start(new ProcessStartInfo()  //
           //         {//
              //        Arguments=$"c start {caminhoPdf}",  //
               //       FileName="cmd.exe",  //
                 //     CreateNoWindow = true  //
              //      });  //
         //   }  //

        }
    }
}
