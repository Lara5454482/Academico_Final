using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academico_Final.DAO
{
    public class ProjetoDAO
    {
        private string LinhaConexao = "Server=LS05MPF;Database=AULA_DS;User Id=sa;Password=admsasql;";
        private SqlConnection Conexao;
        public ProjetoDAO()
        {
            Conexao = new SqlConnection(LinhaConexao);
        }
        public void Inserir(ProjetoEntidade curso)
        {
            Conexao.Open();
            string query = "Insert into Cursos ( Nome, Turno, Sigla, Ativo) Values (@nome, @turno, @sigla, @ativo) ";
            SqlCommand comando = new SqlCommand(query, Conexao);
            SqlParameter parametro1 = new SqlParameter("@nome", curso.Nome);
            SqlParameter parametro2 = new SqlParameter("@turno", curso.Turno);
            SqlParameter parametro3 = new SqlParameter("@sigla", curso.Turno);
            SqlParameter parametro4 = new SqlParameter("@ativo", curso.Ativo);
            comando.Parameters.Add(parametro1);
            comando.Parameters.Add(parametro2);
            comando.Parameters.Add(parametro3);
            comando.Parameters.Add(parametro4);
            comando.ExecuteNonQuery();
            Conexao.Close();

        }

        public DataTable PreencherComboBox()
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT Id, Nome, Turno, Sigla, Ativo FROM Cursos";

            using (SqlConnection connection = new SqlConnection(LinhaConexao))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                try
                {
                    // Preenche o DataTable com os dados da consulta
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    // Lida com erros, se necessário
                    throw new Exception("Erro ao acessar os dados: " + ex.Message);
                }
            }

            return dataTable;
        }


        public DataTable ObterCurso()
        {
            DataTable dt = new DataTable();
            Conexao.Open();
            string query = "SELECT Id, Nome, Turno, Sigla, Ativo FROM Cursos Order by Id desc";
            SqlCommand comando = new SqlCommand(query, Conexao);

            SqlDataReader Leitura = comando.ExecuteReader();

            foreach (var atributos in typeof(ProjetoEntidade).GetProperties())
            {
                dt.Columns.Add(atributos.Name);
            }

            if (Leitura.HasRows)
            {
                while (Leitura.Read())
                {
                    ProjetoEntidade p = new ProjetoEntidade();
                    p.Id = Convert.ToInt32(Leitura[0]);
                    p.Nome = Leitura[1].ToString();
                    p.Turno = Leitura[2].ToString();
                    p.Sigla = Leitura[3].ToString();
                    p.Ativo = Convert.ToBoolean(Leitura[4]);
                    dt.Rows.Add(p.Linha());
                }
            }

            Conexao.Close();
            return dt;
        }
        public DataTable Pesquisar(string pesquisa)
        {
            DataTable dt = new DataTable();
            Conexao.Open();
            string query = "";
            if (string.IsNullOrEmpty(pesquisa))
            {
                query = "SELECT Id, Nome, Turno, Sigla, Ativo FROM Cursos Order by Id desc";
            }
            else
            {
                query = "SELECT Id, Nome, Turno, Sigla, Ativo FROM Cursos Where Nome like '%" + pesquisa + "%' Order by Id desc";
            }

            SqlCommand comando = new SqlCommand(query, Conexao);

            SqlDataReader Leitura = comando.ExecuteReader();

            foreach (var atributos in typeof(ProjetoEntidade).GetProperties())
            {
                dt.Columns.Add(atributos.Name);
            }

            if (Leitura.HasRows)
            {
                while (Leitura.Read())
                {
                    ProjetoEntidade p = new ProjetoEntidade();
                    p.Id = Convert.ToInt32(Leitura[0]);
                    p.Nome = Leitura[1].ToString();
                    p.Turno = Leitura[2].ToString();
                    p.Sigla = Leitura[3].ToString();
                    p.Ativo = Convert.ToBoolean(Leitura[4]);
                    dt.Rows.Add(p.Linha());
                }
            }

            Conexao.Close();
            return dt;
        }

    }
}

