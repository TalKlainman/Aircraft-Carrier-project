using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using VEHICLE;

namespace AircraftCarrier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VehicleList vehicles = new VehicleList();
        int curIndex = -1;
        int selectedIndex = -1;
        bool moveRight, moveLeft, moveUp, moveDown,fire;
        int speed = 12;

        public void clear()
        {
            TypeComboBox.Text = "";
            WeightBox.Text = "";
            SpeedBox.Text = "";
            WidthBox.Text = "";
            LengthBox.Text = "";
            TypeBox.Text = "";
            YeartextBox.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int weight = Convert.ToInt32(WeightBox.Text);
            int speed = Convert.ToInt32(SpeedBox.Text);
            int width = Convert.ToInt32(WidthBox.Text);
            int len = Convert.ToInt32(LengthBox.Text);
            int wheel;
            int year;
            int type = Convert.ToInt32(TypeBox.Text);
            bool status;

            switch (TypeComboBox.Text)
            {
                case "Tank":
                    wheel = 10;
                    vehicles[vehicles.NextIndex] = new Tank(0, 0, weight, speed, width, len, wheel, type, Tank.Id);
                    break;

                case "Hummer":
                    wheel = 4;
                    year = Convert.ToInt32(YeartextBox.Text);
                    vehicles[vehicles.NextIndex] = new Hummer(0, 0, weight, speed, width, len, wheel, type, year, Hummer.Id);
                    break;

                case "F16":
                    year = Convert.ToInt32(YeartextBox.Text);
                    status = checkBox1.Checked == true ? true : false;
                    vehicles[vehicles.NextIndex] = new F16(0, 0, weight, speed, width, len, status, type, year, F16.Id);
                    break;

                default:
                    break;
            }
            pictureBox1.Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //!!!!
                    formatter.Serialize(stream, vehicles);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();// + "..\\myModels";
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                vehicles = (VehicleList)binaryFormatter.Deserialize(stream);
                pictureBox1.Invalidate();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\background.jpg"), new Rectangle(0, 0, 1128, 730));
            vehicles.DrawAll(g);
            if (fire)
            {
                if(selectedIndex>=0 && selectedIndex<vehicles.NextIndex)
                {
                    vehicles[selectedIndex].Fire(e.Graphics);
                    pictureBox1.Invalidate();
                    return;
                } 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            label9.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeComboBox.Text == "Tank")
            {
                YearLabel.Visible = false;
                YeartextBox.Visible = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                label8.Visible = false;
            }
            else if (TypeComboBox.Text == "Hummer")
            {
                YearLabel.Visible = true;
                YeartextBox.Visible = true;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                label8.Visible = false;
            }

            else
            {
                YearLabel.Visible = true;
                YeartextBox.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                label8.Visible = true;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int weight = 0, speed = 0, width = 200, len = 200, wheel, year = 0;
            bool status;
            try
            {
                weight = Convert.ToInt32(WeightBox.Text);
            }
            catch
            { }
            try
            {
                speed = Convert.ToInt32(SpeedBox.Text);
            }
            catch { }
            try
            {
                width = Convert.ToInt32(WidthBox.Text);
            }
            catch { }
            try
            {
                len = Convert.ToInt32(LengthBox.Text);
            }
            catch { }
            
            Point coordinates = e.Location;
            curIndex = -1;

            for (int i = 0; i <vehicles.NextIndex; i++)
            {
                if (vehicles[i].isInside(coordinates.X, coordinates.Y))
                {
                    curIndex = i;
                    selectedIndex = i;
                    string s = e.Button.ToString();
                    if (s == "Right") //if Right button pressed - Remove
                    {
                        vehicles.remove(curIndex);
                        curIndex = -1;
                        pictureBox1.Invalidate();
                        return;
                    }
                    if (s =="Left")
                    {
                        Vehicle a = vehicles[i];
                        if (vehicles[i].ID >= 100 && vehicles[i].ID < 200)
                        {
                            Tank b = Cast(a, typeof(Tank));
                            TypeComboBox.Text = Convert.ToString("Tank");
                            WeightBox.Text = Convert.ToString(b.Weight);
                            SpeedBox.Text = Convert.ToString(b.MaxSpeed);
                            WidthBox.Text = Convert.ToString(b.Width);
                            LengthBox.Text = Convert.ToString(b.Length);
                            TypeBox.Text = Convert.ToString(b.Type);

                        }
                        else if (vehicles[i].ID >= 200 && vehicles[i].ID < 300)
                        {
                            Hummer b = Cast(a, typeof(Hummer));
                            TypeComboBox.Text = Convert.ToString("Hummer");
                            WeightBox.Text = Convert.ToString(b.Weight);
                            SpeedBox.Text = Convert.ToString(b.MaxSpeed);
                            WidthBox.Text = Convert.ToString(b.Width);
                            LengthBox.Text = Convert.ToString(b.Length);
                            TypeBox.Text = Convert.ToString(b.Type);
                            YeartextBox.Text = Convert.ToString(b.ReleaseYear);
                        }
                        else
                        {
                            F16 b = Cast(a, typeof(F16));
                            TypeComboBox.Text = Convert.ToString("F16");
                            WeightBox.Text = Convert.ToString(b.Weight);
                            SpeedBox.Text = Convert.ToString(b.MaxSpeed);
                            WidthBox.Text = Convert.ToString(b.Width);
                            LengthBox.Text = Convert.ToString(b.Length);
                            TypeBox.Text = Convert.ToString(b.Type);
                            YeartextBox.Text = Convert.ToString(b.ReleaseYear);
                            if (Convert.ToBoolean(b.IsArmed) == true)
                                checkBox1.Checked = true;
                            else
                                checkBox2.Checked = true;
                        }
                        return;
                    }
                    break;
                }  
            }
            
            switch (TypeComboBox.Text)
            {
                case "Tank":
                    wheel = 10;
                    vehicles[vehicles.NextIndex] = new Tank(e.X, e.Y, weight, speed, width, len, wheel, 0, Tank.Id);
                    break;

                case "Hummer":
                    wheel = 4;
                    try
                    {
                        year = Convert.ToInt32(YeartextBox.Text);
                    }
                    catch
                    { }
                    vehicles[vehicles.NextIndex] = new Hummer(e.X, e.Y, weight, speed, width, len, wheel, 1, year, Hummer.Id);
                    break;

                case "F16":
                    try
                    {
                        year = Convert.ToInt32(YeartextBox.Text);
                    }
                    catch { }
                    status = checkBox1.Checked == true ? true : false;
                    vehicles[vehicles.NextIndex] = new F16(e.X, e.Y, weight, speed, width, len, status, 2, year, F16.Id);
                    break;

                default:
                    break;
            }
            clear();
            curIndex = vehicles.NextIndex - 1;
            pictureBox1.Invalidate();
            pictureBox1.Focus();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (curIndex >= 0)
            {
                Vehicle c = (Vehicle)vehicles[curIndex];
                c.X = e.X;
                c.Y = e.Y;
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            curIndex = -1;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                moveUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                moveDown = true;
            }
            if(e.KeyCode==Keys.Space)
            {
                fire = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                moveRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                moveUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                moveDown = false;
            }
            if (e.KeyCode == Keys.Space)
            {
               fire = false;
            }
        }

        private void moveTimerEvent(object sender, EventArgs e)
        {
            if(moveLeft)
            {
                if (selectedIndex >= 0)
                {
                    Vehicle c = (Vehicle)vehicles[selectedIndex];
                    c.X -= speed;
                    pictureBox1.Invalidate();
                }
            }
            if(moveRight)
            {
                if (selectedIndex >= 0)
                {
                    Vehicle c = (Vehicle)vehicles[selectedIndex];
                    c.X += speed;
                    pictureBox1.Invalidate();
                }
            }
            if(moveUp)
            {
                if (selectedIndex >= 0)
                {
                    Vehicle c = (Vehicle)vehicles[selectedIndex];
                    c.Y -= speed;
                    pictureBox1.Invalidate();
                }
            }
            if(moveDown)
            {
                if (selectedIndex >= 0)
                {
                    Vehicle c = (Vehicle)vehicles[selectedIndex];
                    c.Y += speed;
                    pictureBox1.Invalidate();
                }
            }

            if(fire && selectedIndex >= 0 && selectedIndex<vehicles.NextIndex)
            {
                for (int i = 0; i < vehicles.NextIndex; i++)
                {
                    if (vehicles[selectedIndex].inRange(vehicles[i].X, vehicles[i].Y))
                    {
                        vehicles.remove(i);
                        selectedIndex--;
                        if (selectedIndex == -1)
                            selectedIndex = 0;
                        break;
                    }
                }
                pictureBox1.Invalidate();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Vehicle v = vehicles[selectedIndex];
            if (vehicles[selectedIndex].ID >= 100 && vehicles[selectedIndex].ID < 200)
            {
                Tank b = Cast(v, typeof(Tank));
                b.Weight = Convert.ToInt32(WeightBox.Text);
                b.MaxSpeed = Convert.ToInt32(SpeedBox.Text);
                b.Width = Convert.ToInt32(WidthBox.Text);
                b.Length = Convert.ToInt32(LengthBox.Text);
                b.Type = Convert.ToInt32(TypeBox.Text);
            }
            else if (vehicles[selectedIndex].ID >= 200 && vehicles[selectedIndex].ID < 300)
            {
                Hummer b = Cast(v, typeof(Hummer));
                b.Weight = Convert.ToInt32(WeightBox.Text);
                b.MaxSpeed = Convert.ToInt32(SpeedBox.Text);
                b.Width = Convert.ToInt32(WidthBox.Text);
                b.Length = Convert.ToInt32(LengthBox.Text);
                b.Type = Convert.ToInt32(TypeBox.Text);
                b.ReleaseYear = Convert.ToInt32(YeartextBox.Text);           
            }
            else
            {
                F16 b = Cast(v, typeof(F16));
                b.Weight = Convert.ToInt32(WeightBox.Text);
                b.MaxSpeed = Convert.ToInt32(SpeedBox.Text);
                b.Width = Convert.ToInt32(WidthBox.Text);
                b.Length = Convert.ToInt32(LengthBox.Text);
                b.Type = Convert.ToInt32(TypeBox.Text);
                b.ReleaseYear = Convert.ToInt32(YeartextBox.Text);
                if (checkBox1.Checked == true)
                    b.IsArmed = true;
                else
                    b.IsArmed = false;
            }

            vehicles[selectedIndex] = v;
            curIndex = -1;
            //selectedIndex = -1;
            clear();
        }
    }
}   
