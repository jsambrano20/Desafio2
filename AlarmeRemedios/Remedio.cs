using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlarmeRemedios
{
    class Remedio
    {

        private string nome1, hr1;
        public string Nome { get => nome1; set => nome1 = value; }
        public string Hora { get => hr1; set => hr1 = value; }

        public bool grRemedio()
        {
            Banco banco = new Banco();
            SqlConnection cn = banco.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;

            command.CommandText = "insert into remedio values(@nome, @horario); ";
            command.Parameters.Add("@nome", SqlDbType.VarChar);
            command.Parameters.Add("@horario", SqlDbType.DateTime);

            command.Parameters[0].Value = nome1;
            command.Parameters[1].Value = hr1;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {

                tran.Rollback();
                MessageBox.Show("" + ex);
                return false;
            }


        }
        public bool excluirRemedio()
        {
            Banco bd = new Banco();

            SqlConnection cn = bd.abrirConexao();
            SqlTransaction tran = cn.BeginTransaction();
            SqlCommand command = new SqlCommand();

            command.Connection = cn;
            command.Transaction = tran;
            command.CommandType = CommandType.Text;
            command.CommandText = "delete from remedio where nome = @nome";
            command.Parameters.Add("@nome", SqlDbType.VarChar);


            command.Parameters[0].Value = nome1;

            try
            {
                command.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                return false;
            }
            finally
            {
                bd.fecharConexao();
            }
        }



    }
}
