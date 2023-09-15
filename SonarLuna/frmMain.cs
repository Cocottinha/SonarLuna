using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SonarLuna
{
    public partial class frmMain : Form
    {
        public System.Windows.Forms.Timer temporizador = new System.Windows.Forms.Timer();

        int largura = 300, altura = 300, guia = 150;

        int graus;
        int centerX, centerY;
        int x, y;

        int tx, ty, lim = 20;

        Bitmap bmp;
        Pen p;
        Graphics g;

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

            temporizador.Interval = 30; //ms
            temporizador.Tick += new EventHandler(timer_Elapsed);
            temporizador.Start();
        }
        private void timer_Elapsed(object sender, EventArgs e)
        {
            p = new Pen(Color.LimeGreen, 1f);

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

            //circulo do sonar
            g.DrawEllipse(p, 0, 0, largura, altura);
            g.DrawEllipse(p, 80, 80, largura - 160, altura - 160);

            //linhas verticais e horizontais
            g.DrawLine(p, new Point(centerX, 0), new Point(centerX, altura));
            g.DrawLine(p, new Point(0, centerY), new Point(largura, centerY));

            //Ponteiro
            g.DrawLine(new Pen(Color.Black, 2f), new Point(centerX, centerY), new Point(tx, ty));
            g.DrawLine(p, new Point(centerX, centerY), new Point(x, y));

            //carregar a picturebox
            pictureBox1.Image = bmp;
            g.Dispose();
            p.Dispose();

            //atualiza radar
            graus++;
            if (graus == 360)
            {
                graus = 0;
            }
        }
    }
}