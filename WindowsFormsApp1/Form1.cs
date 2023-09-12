using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Rectangle> rectangles = new List<Rectangle>();
        private int currentOrder = 0;

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += MainForm_MouseDown; // Связываем событие формы с методом
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Запоминаем начальные координаты
                Point startPoint = e.Location;

                // Создаем новый статик
                Rectangle rect = new Rectangle
                {
                    Location = startPoint,
                    Size = new Size(100, 100), //Тут менять размер появление квадрата
                    Order = currentOrder++
                };

                rectangles.Add(rect);

                // Добавляем статик на форму
                Label label = new Label
                {
                    Location = startPoint,
                    Size = rect.Size,
                    Text = rect.Order.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightBlue
                };

                label.MouseDown += Label_MouseDown;
                this.Controls.Add(label);
            }
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Находим статик с наибольшим порядковым номером
                Label label = sender as Label;

                int maxOrder = -1;
                Rectangle selectedRect = null;

                foreach (var rect in rectangles)
                {
                    if (rect.Order > maxOrder)
                    {
                        maxOrder = rect.Order;
                        selectedRect = rect;
                    }
                }

                if (selectedRect != null)
                {
                    MessageBox.Show($"Площадь: {selectedRect.Size.Width * selectedRect.Size.Height}\nКоординаты: X={selectedRect.Location.X}, Y={selectedRect.Location.Y}", "Информация о статике");
                }
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                // Находим статик с наименьшим порядковым номером и удаляем его
                Label label = sender as Label;

                int minOrder = int.MaxValue;
                Rectangle selectedRect = null;

                foreach (var rect in rectangles)
                {
                    if (rect.Order < minOrder)
                    {
                        minOrder = rect.Order;
                        selectedRect = rect;
                    }
                }

                if (selectedRect != null)
                {
                    rectangles.Remove(selectedRect);
                    this.Controls.Remove(label);
                }
            }
        }
    }

    class Rectangle
    {
        public Point Location { get; set; }
        public Size Size { get; set; }
        public int Order { get; set; }
    }
}
