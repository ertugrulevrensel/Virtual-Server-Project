using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ns1;

namespace sanal_sunucu
{
    public partial class Form1 : Form
    {
        List<Thread> Th = new List<Thread>();
        List<ns1.BunifuCircleProgressbar> pb = new List<ns1.BunifuCircleProgressbar>();
        Button btn = new Button();
        static object Kontrol = new object();

        int MainVeri = 0;
        int cek = 0;
        List<int> ChildVeri = new List<int>();
        int syc = -1;
        int thcont = 0;
        int qwedsa = 2;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Thread thrA = new Thread(MainThread);
            thrA.Start();
        }

        private void MainThread()
        {
            ns1.BunifuCircleProgressbar MainProgress = new ns1.BunifuCircleProgressbar();
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                this.Invoke((MethodInvoker)delegate ()
                {
                    panel2.Controls.Add(MainProgress);
                });
            }

            MainProgress.MaxValue = 10000;
            MainProgress.Height = 120;
            MainProgress.Width = 120;

            Thread thr1 = new Thread(IstekKabulMain);
            Thread thr2 = new Thread(IstekCevapMain);
            thr1.Start();
            thr2.Start();
            MainProgress.BackColor = Color.Black;



            MainProgress.Refresh();
            Thread thrB = new Thread(childthread);
            Thread thrC = new Thread(childthread);

            label4.Text = qwedsa.ToString();

            ChildVeri.Add(0);
            ChildVeri.Add(0);
            thrB.Start();
            Thread.Sleep(100);
            thrC.Start();



            while (true)
            {
                MainProgress.Value = MainVeri;
                MainProgress.Refresh();
            }
        }

        private void childthread()
        {
            syc++;
            Thread.CurrentThread.Name = syc.ToString();

            ns1.BunifuCircleProgressbar ChildProgress = new ns1.BunifuCircleProgressbar();
            if (this.InvokeRequired) //Forma gelen talebin farklı bir iş parçacığından gelip gelmediği kontrol ediliyor.
            {
                //Eğer farklı bir iş parçacığından talep gelmişse aşağıdaki Invoke metoduyla işlem gerçekleştiriliyor.
                this.Invoke((MethodInvoker)delegate ()
                {
                    flowLayoutPanel2.Controls.Add(ChildProgress);
                });
            }
            ChildProgress.MaxValue = 5000;
            ChildProgress.Height = 130;
            ChildProgress.Width = 130;
            Thread thr3 = new Thread(IstekKabulChild);
            Thread thr4 = new Thread(IstekCevapChild);
            thr3.Name = Thread.CurrentThread.Name;
            thr3.Start();
            thr4.Name = Thread.CurrentThread.Name;
            thr4.Start();
            while (true)
            {
                Thread.Sleep(100);

                ChildProgress.Value = ChildVeri[Int32.Parse(Thread.CurrentThread.Name)];
                ChildProgress.Refresh();
                if (((ChildProgress.Value * 100) / 5000) > 70)
                {
                    int knt = ChildProgress.Value / 2;
                    Th.Add(new Thread(childthread));
                    ChildVeri.Add(knt);
                    ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] -= knt;
                    Th[thcont].Start();
                    thcont++;
                    qwedsa++;

                    label4.Text = qwedsa.ToString();
                    label4.Refresh();
                }
                else if (ChildProgress.Value == 0)
                {
                    if (qwedsa > 2)
                    {
                        flowLayoutPanel2.Controls.Remove(ChildProgress);
                        thr3.Abort();
                        thr4.Abort();
                        qwedsa--;
                        label4.Text = qwedsa.ToString();
                        label4.Refresh();

                        Thread.Sleep(Timeout.Infinite);


                    }

                }
            }


        }

        private void IstekKabulMain()
        {
            Random rnd = new Random();
            while (true)
            {
                MainVeri += rnd.Next(1, 100);
                Thread.Sleep(20);
            }
        }
        private void IstekCevapMain()
        {
            Random rnd = new Random();
            while (true)
            {
                MainVeri -= rnd.Next(1, 50);
                if (MainVeri < 0)
                    MainVeri = 0;
                Thread.Sleep(50);
            }
        }
        public void IstekKabulChild()
        {
            Random rnd = new Random();
            while (true)
            {
                cek = rnd.Next(1, 50);
                MainVeri -= cek;
                if (MainVeri < 0)
                {
                    MainVeri += cek;
                    ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] += MainVeri;
                    MainVeri = 0;
                }
                else
                    ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] += cek;
                Thread.Sleep(30);
            }
        }
        private void IstekCevapChild()
        {

            Random rnd = new Random();
            while (true)
            {
                ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] -= rnd.Next(1, 50);
                if (ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] < 0)
                    ChildVeri[Int32.Parse(Thread.CurrentThread.Name)] = 0;
                Thread.Sleep(50);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}