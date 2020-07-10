using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama_W
{
    public partial class frmDama : Form
    {
        public struct Board
        {
            public int x, y;
            public int color;
            public int stato;
            public int giocatore_numero;
        }

        public Board[,] board = new Board[8, 8];
        public frmDama()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                if ((i % 2) == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        board[i, j].x = j;
                        board[i, j].y = i;
                        board[i, j].giocatore_numero = 0;
                        board[i, j].stato = 0;

                        if ((j % 2) == 0)
                        {
                            board[i, j].color = 0;

                        }
                        else
                        {
                            board[i, j].color = 1;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < 8; j++)
                    {

                        board[i, j].x = j;
                        board[i, j].y = i;
                        board[i, j].giocatore_numero = 0;
                        board[i, j].stato = 0;

                        if ((j % 2) != 0)
                        {
                            board[i, j].color = 0;
                        }
                        else
                        {
                            board[i, j].color = 1;
                        }

                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, i].color == 1)
                    {
                        if (i == 0 || i == 1)
                        {
                            board[j, i].giocatore_numero = 1;  // primo giocatore
                            board[j, i].stato = 1;             // pedina (1), dama (2)
                        }
                    }

                    if (board[j, i].color == 1)
                    {
                        if (i == 6 || i == 7)
                        {
                            board[j, i].giocatore_numero = 2; // secondo giocatore
                            board[j, i].stato = 1;            // pedina (1), dama (2)
                        }
                    }
                }
            }
            create_dama(x, y, 1);

        }

        private int x = 0, y = 0, yTmp = 0, xTmp = 0, Pedina;
        private int giocatoreTmp = 0;
        private bool pflag_move = false;
        private bool pFlag_move_end = false;
        private List<KeyValuePair<int, int>> pPosKeys = new List<KeyValuePair<int, int>>();

        private void FrmDama_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Right:
                    {
                        if (x < 7)
                        {
                            x++;
                        }

                    }
                    break;
                case Keys.Left:
                    {
                        if (x > 0)
                        {
                            x--;
                        }

                    }
                    break;
                case Keys.Down:
                    {
                        if (y < 7)
                        {
                            y++;
                        }

                    }
                    break;
                case Keys.Up:
                    {
                        if (y > 0)
                        {
                            y--;
                        }

                    }
                    break;
                case Keys.Enter:
                    {

                        if (pFlag_move_end == false)
                        {
                            if (board[x, y].color == 1)
                            {
                                if (board[x, y].giocatore_numero != 0)
                                {
                                    if (board[x, y].stato == 1 || board[x, y].stato == 2)
                                    {
                                        string sLog = string.Format("Pedina Selezionata G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                        txtLog.Text += sLog;
                                        pflag_move = true;

                                        yTmp = y;
                                        xTmp = x;
                                        Pedina = board[x, y].stato;
                                        giocatoreTmp = board[x, y].giocatore_numero;
                                    }
                                }
                            }
                            else
                            {
                                board[x, y].stato = 1;

                            }
                        }
                        else
                        {
                            // Se sono le caselle nere
                            if (board[x, y].color == 1)
                            {
                                // Se la posizione di arrivo è vuota 
                                if (board[x, y].stato == 0)
                                {
                                    // Se il giocatore è 1
                                    if (giocatoreTmp == 1)
                                    {
                                        // Se stiamo muovendo una pedina è la posizione di arrivo y > yTmp e y < yTmp + 2
                                        if (Pedina == 1)
                                        {
                                            if (y == yTmp + 1)
                                            {
                                                string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                txtLog.Text += sLog;

                                                board[xTmp, yTmp].stato = 0;
                                                board[xTmp, yTmp].giocatore_numero = 0;

                                                board[x, y].stato = 1;
                                                board[x, y].giocatore_numero = giocatoreTmp;

                                                if (y == 7)
                                                {
                                                    board[x, y].stato = 2;
                                                }
                                            }

                                            if (y == yTmp + 2)
                                            {

                                                if (x < xTmp && board[xTmp - 1, yTmp + 1].stato == 1 && board[xTmp - 1, yTmp + 1].giocatore_numero != giocatoreTmp)
                                                {
                                                    string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                    txtLog.Text += sLog;

                                                    board[xTmp, yTmp].stato = 0;
                                                    board[xTmp, yTmp].giocatore_numero = 0;

                                                    board[xTmp - 1, yTmp + 1].stato = 0;
                                                    board[xTmp - 1, yTmp + 1].giocatore_numero = 0;

                                                    board[x, y].stato = 1;
                                                    board[x, y].giocatore_numero = giocatoreTmp;

                                                    if (y == 7)
                                                    {
                                                        board[x, y].stato = 2;
                                                    }


                                                }

                                                if (x > xTmp && board[xTmp + 1, yTmp + 1].stato == 1 && board[xTmp + 1, yTmp + 1].giocatore_numero != giocatoreTmp)
                                                {
                                                    string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                    txtLog.Text += sLog;

                                                    board[xTmp, yTmp].stato = 0;
                                                    board[xTmp, yTmp].giocatore_numero = 0;

                                                    board[xTmp + 1, yTmp + 1].stato = 0;
                                                    board[xTmp + 1, yTmp + 1].giocatore_numero = 0;

                                                    board[x, y].stato = 1;
                                                    board[x, y].giocatore_numero = giocatoreTmp;

                                                    if (y == 7)
                                                    {
                                                        board[x, y].stato = 2;
                                                    }
                                                }
                                            }
                                        }

                                        if (Pedina == 2)
                                        {
                                            if (y == yTmp + 1 || y == yTmp - 1)
                                            {
                                                string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                txtLog.Text += sLog;

                                                board[xTmp, yTmp].stato = 0;
                                                board[xTmp, yTmp].giocatore_numero = 0;

                                                board[x, y].stato = Pedina;
                                                board[x, y].giocatore_numero = giocatoreTmp;
                                            }

                                            if (y == yTmp + 2 || y == yTmp - 2)
                                            {
                                                int yTmpS = 0;
                                                if (y > yTmp)
                                                {
                                                    if (yTmp == 0)
                                                        yTmpS = 0;
                                                    else
                                                        yTmpS = yTmp + 1;

                                                    if (x < xTmp && board[xTmp - 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp - 1, yTmpS].stato = 0;
                                                        board[xTmp - 1, yTmpS].giocatore_numero = 0;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }

                                                    if (x > xTmp && board[xTmp + 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp + 1, yTmpS].stato = 0;
                                                        board[xTmp + 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }
                                                }

                                                if (y < yTmp)
                                                {
                                                    if (yTmp == 0)
                                                        yTmpS = 0;
                                                    else
                                                        yTmpS = yTmp - 1;

                                                    if (x < xTmp && board[xTmp - 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp - 1, yTmpS].stato = 0;
                                                        board[xTmp - 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }

                                                    if (x > xTmp && board[xTmp + 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp + 1, yTmpS].stato = 0;
                                                        board[xTmp + 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (giocatoreTmp == 2)
                                    {
                                        if (Pedina == 1)
                                        {
                                            if (y == yTmp - 1)
                                            {
                                                string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                txtLog.Text += sLog;

                                                board[xTmp, yTmp].stato = 0;
                                                board[xTmp, yTmp].giocatore_numero = 0;

                                                board[x, y].stato = 1;
                                                board[x, y].giocatore_numero = giocatoreTmp;

                                                if (y == 0)
                                                {
                                                    board[x, y].stato = 2;
                                                }
                                            }

                                            if (y == yTmp - 2)
                                            {
                                                if (x < xTmp && board[xTmp - 1, yTmp - 1].stato == 1 && board[xTmp - 1, yTmp - 1].giocatore_numero != giocatoreTmp)
                                                {
                                                    string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                    txtLog.Text += sLog;

                                                    board[xTmp, yTmp].stato = 0;
                                                    board[xTmp, yTmp].giocatore_numero = 0;

                                                    board[xTmp - 1, yTmp - 1].stato = 0;
                                                    board[xTmp - 1, yTmp - 1].giocatore_numero = 0;

                                                    board[x, y].stato = 1;
                                                    board[x, y].giocatore_numero = giocatoreTmp;

                                                    if (y == 0)
                                                    {
                                                        board[x, y].stato = 2;
                                                    }

                                                }

                                                if (x > xTmp && board[xTmp + 1, yTmp - 1].stato == 1 && board[xTmp + 1, yTmp - 1].giocatore_numero != giocatoreTmp)
                                                {
                                                    string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                    txtLog.Text += sLog;

                                                    board[xTmp, yTmp].stato = 0;
                                                    board[xTmp, yTmp].giocatore_numero = 0;

                                                    board[xTmp + 1, yTmp - 1].stato = 0;
                                                    board[xTmp + 1, yTmp - 1].giocatore_numero = 0;

                                                    board[x, y].stato = 1;
                                                    board[x, y].giocatore_numero = giocatoreTmp;

                                                    if (y == 0)
                                                    {
                                                        board[x, y].stato = 2;
                                                    }
                                                }
                                            }
                                        }

                                        if (Pedina == 2)
                                        {
                                            if (y == yTmp + 1 || y == yTmp - 1)
                                            {
                                                string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                txtLog.Text += sLog;

                                                board[xTmp, yTmp].stato = 0;
                                                board[xTmp, yTmp].giocatore_numero = 0;

                                                board[x, y].stato = Pedina;
                                                board[x, y].giocatore_numero = giocatoreTmp;
                                            }

                                            if (y == yTmp + 2 || y == yTmp - 2)
                                            {
                                                int yTmpS = 0;
                                                if (y > yTmp)
                                                {
                                                    if (yTmp < 0)
                                                        yTmpS = 0;
                                                    else
                                                        yTmpS = yTmp + 1;

                                                    if (x < xTmp && board[xTmp - 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp - 1, yTmpS].stato = 0;
                                                        board[xTmp - 1, yTmpS].giocatore_numero = 0;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }

                                                    if (x > xTmp && board[xTmp + 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp + 1, yTmpS].stato = 0;
                                                        board[xTmp + 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }
                                                }

                                                if (y < yTmp)
                                                {
                                                    if (yTmp < 0)
                                                        yTmpS = 0;
                                                    else
                                                        yTmpS = yTmp - 1;

                                                    if (x < xTmp && board[xTmp + 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp + 1, yTmpS].stato = 0;
                                                        board[xTmp + 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }

                                                    if (x > xTmp && board[xTmp + 1, yTmpS].giocatore_numero != giocatoreTmp)
                                                    {
                                                        string sLog = string.Format("Mossa Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                                        txtLog.Text += sLog;

                                                        board[xTmp, yTmp].stato = 0;
                                                        board[xTmp, yTmp].giocatore_numero = 0;

                                                        board[xTmp + 1, yTmpS].stato = 0;
                                                        board[xTmp + 1, yTmpS].giocatore_numero = 0;

                                                        board[x, y].stato = 2;
                                                        board[x, y].giocatore_numero = giocatoreTmp;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string sLog = string.Format("Mossa non Corretta! G={0} X={1} Y={2} S{3}<1=Pedina 2=Dama>\r\n", board[x, y].giocatore_numero, x, y, board[x, y].stato);
                                        txtLog.Text += sLog;
                                    }

                                    pPosKeys.Clear();
                                    pflag_move = false;
                                    pFlag_move_end = false;
                                }
                            }
                        }
                        break;
                    }
            }

            create_dama(x, y, 0);

            for (int pos = 0; pos < pPosKeys.Count(); pos++)
            {
                KeyValuePair<int, int> pkey_temp = pPosKeys[pos];
                Graphics g = this.CreateGraphics();
                using (Pen selPen = new Pen(Color.Red, 3.0f))
                {
                    g.DrawRectangle(selPen, 10 + (pkey_temp.Key * 50), 30 + (pkey_temp.Value * 50), 50, 50);
                }
            }

            /* Esegui Mossa */
            if (pPosKeys.Count() == 2)
            {
                /* Check se la mossa è lecita */
            }
        }

        private void FrmDama_Load(object sender, EventArgs e)
        {
        }

        private void FrmDama_Paint(object sender, PaintEventArgs e)
        {

            create_dama(x, y, 1);
        }

        private void FrmDama_Activated(object sender, EventArgs e)
        {

        }

        private void FrmDama_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void create_dama(int x, int y, int init)
        {
            Graphics g = this.CreateGraphics();
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            for (int j = 0; j < 8; j++)
            {
                if ((j % 2) == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((i % 2) == 0)
                        {
                            using (Brush brush = new SolidBrush(Color.LightYellow))
                            {
                                g.FillRectangle(brush, 10 + (i * 50), 30 + (j * 50), 50, 50);

                            }
                        }
                        else
                        {
                            using (Brush brush = new SolidBrush(Color.Black))
                            {
                                g.FillRectangle(brush, 10 + (i * 50), 30 + (j * 50), 50, 50);

                                if (board[i, j].stato == 1 && board[i, j].giocatore_numero == 1)
                                {
                                    /* disegnare in base alla matrice modificata */
                                    //if (j == 0 || j == 1)
                                    //{
                                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                                    g.FillEllipse(myBrush, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                    //}
                                }

                                if (board[i, j].stato == 2 && board[i, j].giocatore_numero == 1)
                                {
                                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkRed);
                                    g.FillEllipse(myBrush, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                }

                                if (board[i, j].stato == 1 && board[i, j].giocatore_numero == 2)
                                {

                                    //if (j == 6 || j == 7)
                                    //{
                                    System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                                    g.FillEllipse(myBrush1, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                    //}
                                }

                                if (board[i, j].stato == 2 && board[i, j].giocatore_numero == 2)
                                {
                                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                                    g.FillEllipse(myBrush, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                }
                                /* disegnare in base alla matrice modificata */
                            }
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if ((i % 2) != 0)
                        {
                            using (Brush brush = new SolidBrush(Color.LightYellow))
                            {
                                g.FillRectangle(brush, 10 + (i * 50), 30 + (j * 50), 50, 50);
                            }
                        }
                        else
                        {
                            using (Brush brush = new SolidBrush(Color.Black))
                            {
                                g.FillRectangle(brush, 10 + (i * 50), 30 + (j * 50), 50, 50);

                                if (board[i, j].stato == 1 && board[i, j].giocatore_numero == 1)
                                {
                                    /* disegnare in base alla matrice modificata */
                                    //if (j == 0 || j == 1)
                                    //{
                                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                                    g.FillEllipse(myBrush, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                }

                                if (board[i, j].stato == 2 && board[i, j].giocatore_numero == 1)
                                {
                                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkRed);
                                    g.FillEllipse(myBrush, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                }
                                //}
                                if (board[i, j].stato == 1 && board[i, j].giocatore_numero == 2)
                                {

                                    //if (j == 6 || j == 7)
                                    //{
                                    System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                                    g.FillEllipse(myBrush1, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                    //}
                                }

                                if (board[i, j].stato == 2 && board[i, j].giocatore_numero == 2)
                                {

                                    System.Drawing.SolidBrush myBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
                                    g.FillEllipse(myBrush1, new Rectangle(10 + (i * 50), 30 + (j * 50), 50, 50));
                                }
                                /* disegnare in base alla matrice modificata */
                            }
                        }
                    }
                }

                using (Pen selPen = new Pen(Color.Blue, 5.0f))
                {
                    g.DrawRectangle(selPen, 10 + (x * 50), 30 + (y * 50), 50, 50);
                }

                if (pflag_move)
                {
                    using (Pen selPen = new Pen(Color.Red, 3.0f))
                    {
                        g.DrawRectangle(selPen, 10 + (x * 50), 30 + (y * 50), 50, 50);
                        pPosKeys.Add(new KeyValuePair<int, int>(x, y));
                        pflag_move = false;
                        pFlag_move_end = true;

                    }
                }
            }
        }
    }
}

