using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AlarmeRemedios
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        readonly SoundPlayer soundPlayer = new SoundPlayer();

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Remedio re = new Remedio();

            re.Nome = txtNome.Text;
            re.Hora = dateTimePicker1.Value.ToString();
            re.grRemedio();

            Banco bd = new Banco();
            string sql = "select * from remedio";
            DataTable dt = new DataTable();

            dt = bd.executarConsultaG(sql);

            dataGridView1.DataSource = dt;

            timer.Start();
            MessageBox.Show("Alarme está agendado", "START");
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Remedio re = new Remedio();

            re.Nome = txtNome.Text;

            DateTime atualTime = DateTime.Now;
            DateTime time = dateTimePicker1.Value;
            if (atualTime.Hour == time.Hour && atualTime.Minute == time.Minute && atualTime.Second == time.Second)
            {
                timer.Stop();
                try
                {
                    soundPlayer.SoundLocation = "Alarm Clock Sound.wav";
                    soundPlayer.PlayLooping();
                    MessageBox.Show("Horário do Rémedio " + re.Nome);
                }
                catch
                {
                    MessageBox.Show("Insira um horário valido");
                }
            }
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            timer.Stop();
            soundPlayer.Stop();
            MessageBox.Show("Alarme interrompido", "STOP");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Remedio re = new Remedio();
            re.Nome = txtNome.Text;
            re.excluirRemedio();
            MessageBox.Show("Excluido");

            Banco bd = new Banco();
            string sql = "select * from remedio";
            DataTable dt = new DataTable();

            dt = bd.executarConsultaG(sql);

            dataGridView1.DataSource = dt;

        }
    }
}
