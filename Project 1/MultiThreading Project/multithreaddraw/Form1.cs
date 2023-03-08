namespace multithreaddraw
{
    public partial class Form1 : Form
    {
        Random rd;
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(() =>
            {
                for(int i=0; i<100; i++)
                {
                    int x = rd.Next(0, this.Width); 
                    int y = rd.Next(50,this.Height);
                    Rectangle dim = new Rectangle(x, y, 20, 20);
                    Pen p = new Pen(Brushes.DarkRed,1);
                    this.CreateGraphics().DrawEllipse(p,dim);
                    Thread.Sleep(200);
                }
            });
            thread1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rd = new Random();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread2 = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = rd.Next(0, this.Width);
                    int y = rd.Next(50, this.Height);
                    Pen p = new Pen(Brushes.Green, 1);
                    Rectangle dim = new Rectangle(x, y, 20, 20);
                    this.CreateGraphics().DrawRectangle(p, dim);
                    Thread.Sleep(200);
                }
            });
            thread2.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread3 = new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = rd.Next(0, this.Width);
                    int y = rd.Next(50, this.Height);
                    Pen p = new Pen(Brushes.Black, 1);
                    Rectangle dim = new Rectangle(x, y, 20, 20);
                    this.CreateGraphics().DrawPie(p, dim,350,300);
                    Thread.Sleep(200);
                }
            });
            thread3.Start();
        }
    }
}
