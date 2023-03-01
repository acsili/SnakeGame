using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGameWF
{
    public partial class Main : Form
    {
        private int randomFoodX, randomFoodY;
        private PictureBox food;
        private PictureBox[] snake = new PictureBox[500];
        private Label labelScore;
        private Label labelRecord;
        private int directionSnakeX = 1, directionSnakeY = 0;
        private int widthWindow = 700;
        private int heightWindow = 600;
        private int sizeHead = 20;
        private int score = 0;
        private int record = 0;

        private int XStart = 1; 
        private int YStart = 1;

        public Main()
        {
            InitializeComponent();
            this.Width = widthWindow;
            this.Height = heightWindow;

            labelScore = new Label();
            createLabelScore();

            labelRecord = new Label();
            createLabelRecord();

            snake[0] = new PictureBox();
            createSnakeHead();

            food = new PictureBox();
            craeteFood();

            timer1.Tick += new EventHandler(picUpdate);
            timer1.Interval = 100;
            timer1.Start();

            this.KeyDown += new KeyEventHandler(keyDown);
        }
        private void createLabelScore()
        {
            labelScore.Text = "Счёт: 0";
            labelScore.Location = new Point(widthWindow - 90, 10);
            labelScore.Font = new Font("Tobota", 12);
            this.Controls.Add(labelScore);
        }
        private void createSnakeHead()
        {
            snake[0].Location = new Point(XStart, YStart);
            snake[0].Size = new Size(sizeHead - 1, sizeHead - 1);
            snake[0].BackColor = Color.Blue;
            this.Controls.Add(snake[0]);
        }
        private void createLabelRecord()
        {
            labelRecord.Text = "Рекорд: 0";
            labelRecord.Location = new Point(widthWindow - 90, 30);
            labelRecord.Font = new Font("Tobota", 12);
            this.Controls.Add(labelRecord);
        }
        private void craeteFood()
        {
            food.BackColor = Color.Red;
            food.Size = new Size(sizeHead, sizeHead);
            Random r = new Random();
            randomFoodX = r.Next(0, heightWindow - sizeHead);
            int tempI = randomFoodX % sizeHead;
            randomFoodX -= tempI;
            randomFoodY = r.Next(0, heightWindow - sizeHead);
            int tempJ = randomFoodY % sizeHead;
            randomFoodY -= tempJ;
            randomFoodX++;
            randomFoodY++;
            food.Location = new Point(randomFoodX, randomFoodY);
            this.Controls.Add(food);
        }

        private void touchBoard()
        {
            if (snake[0].Location.X < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                recordScore(score);
                score = 0;
                labelScore.Text = "Счёт: " + score;
                directionSnakeX = 1;
            }
            if (snake[0].Location.X > heightWindow)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                recordScore(score);
                score = 0;
                labelScore.Text = "Счёт: " + score;
                directionSnakeX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                recordScore(score);
                score = 0;
                labelScore.Text = "Счёт: " + score;
                directionSnakeY = 1;
            }
            if (snake[0].Location.Y > heightWindow)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                recordScore(score);
                score = 0;
                labelScore.Text = "Счёт: " + score;
                directionSnakeY = -1;
            }
        }

        private void recordScore(int score) 
        {
            if (record < score) record = score;
            labelRecord.Text = "Рекорд: " + score;
        }

        private void eatTail()
        {
            for (int i = 1; i < score; i++)
            {
                if (snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score = score - (score - i + 1);
                    labelScore.Text = "Счёт: " + score;
                }
            }
        }

        private void eatFood()
        {
            if (snake[0].Location.X == randomFoodX && snake[0].Location.Y == randomFoodY)
            {
                labelScore.Text = "Счёт: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * directionSnakeX, snake[score - 1].Location.Y - 40 * directionSnakeY);
                snake[score].Size = new Size(sizeHead - 1, sizeHead - 1);
                snake[score].BackColor = Color.Blue;
                this.Controls.Add(snake[score]);
                craeteFood();
            }
        }


        private void moveSnake()
        {
            for (int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + directionSnakeX * (sizeHead), snake[0].Location.Y + directionSnakeY * (sizeHead));
            eatTail();
        }

        private void keyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right)
            {
                directionSnakeX = 1;
                directionSnakeY = 0;
            }
            if (e.KeyCode == Keys.Left)
            {
                directionSnakeX = -1;
                directionSnakeY = 0;
            }
            if (e.KeyCode == Keys.Up)
            {
                directionSnakeY = -1;
                directionSnakeX = 0;
            }
            if (e.KeyCode == Keys.Down)
            {
                directionSnakeY = 1;
                directionSnakeX = 0;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, widthWindow - 100, heightWindow);
        }

        private void picUpdate(Object myObject, EventArgs eventsArgs)
        {
            touchBoard();
            eatFood();
            moveSnake();
        }


    }
}
