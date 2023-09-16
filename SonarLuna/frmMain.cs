using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SonarLuna
{
    public partial class frmMain : Form
    {
        public System.Windows.Forms.Timer temporizador = new System.Windows.Forms.Timer();

        int largura = 360, altura = 360, guia = 180;

        int graus;
        int centerX, centerY;
        int x, y;

        int tx, ty, lim = 20;

        Bitmap bmp;
        Pen p, pL, ponteiro;
        Graphics g, entidade;

        int a = 190;
        int b = 45;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(largura + 1, altura + 1);

            panel1.BackColor = Color.Black;

            centerX = largura / 2;
            centerY = altura / 2;

            graus = 0;

            temporizador.Interval = 30; //ms, demora aproximadamente 11/12 segundos pra dar uma volta
            temporizador.Tick += new EventHandler(timer_Elapsed);
            temporizador.Start();
            Entidade();
        }
        private void Entidade()
        {
            a = a + 5;
            b = b + 5;
            entidade = Graphics.FromImage(bmp);
            pL = new Pen(Color.Red, 1f);
            entidade.DrawEllipse(pL, a,b,10,10 );
            
        }
        private void timer_Elapsed(object sender, EventArgs e)
        {
            p = new Pen(Color.LimeGreen, 1f);

            ponteiro = new Pen(Color.LimeGreen, 4f);

            g = Graphics.FromImage(bmp);

            int vrau = (graus - lim) % 360;

            if (graus >= 0 && graus <= 180)
            {
                x = centerX + (int)(guia * Math.Sin(Math.PI * graus / 180));
                y = centerY - (int)(guia * Math.Cos(Math.PI * graus / 180));
            }
            else
            {
                x = centerX - (int)(guia * -Math.Sin(Math.PI * graus / 180));
                y = centerY - (int)(guia * Math.Cos(Math.PI * graus / 180));
            }
            if (vrau >= 0 && vrau <= 180)
            {
                tx = centerX + (int)(guia * Math.Sin(Math.PI * vrau / 180));
                ty = centerY - (int)(guia * Math.Cos(Math.PI * vrau / 180));
            }
            else
            {
                tx = centerX - (int)(guia * -Math.Sin(Math.PI * vrau / 180));
                ty = centerY - (int)(guia * Math.Cos(Math.PI * vrau / 180));
            }

            //circulos do sonar
            g.DrawEllipse(p, 0, 0, largura, altura);
            g.DrawEllipse(p, 90, 90, largura - 180, altura - 180);
            g.DrawEllipse(p, 45, 45, largura - 90, altura - 90);
            g.DrawEllipse(p, 135, 135, largura - 270, altura - 270);

            //linhas verticais e horizontais
            g.DrawLine(p, new Point(centerX, 0), new Point(centerX, altura));
            g.DrawLine(p, new Point(0, centerY), new Point(largura, centerY));
         
            //Ponteiro
            g.DrawLine(new Pen(Color.Black, 4f), new Point(centerX, centerY), new Point(tx, ty));
            g.DrawLine(ponteiro, new Point(centerX, centerY), new Point(x, y));
          
            //carregar a picturebox
            pictureBox1.Image = bmp;
            g.Dispose();
            p.Dispose();

            //atualiza radar
            graus++;           
            if (graus == 360)
            {
                graus = 0;
                //Chamo a entidade q foi encontrada pelo radar
                Entidade();
            }

        }
    }
}