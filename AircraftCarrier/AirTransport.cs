using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTransport
{
    using System.Drawing;
    using VEHICLE;
    [Serializable]
    public class AirTransport : Vehicle
    {
        private int maxSpeed;
        private int weight;
        private bool isArmed;

        public AirTransport()
            : this(100,100,6000,250,60,50,false)
        {}

        public AirTransport(float xp, float yp, int weightp, int speedp, int widthp, int lenp,bool status)
        {
            X = xp;
            Y = yp;
            Weight = weightp;
            MaxSpeed = speedp;
            Width = widthp;
            Length = lenp;
            IsArmed = status;
        }

        public int MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                maxSpeed = value;
            }
        }

        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public bool IsArmed
        {
            get
            {
                return isArmed;
            }
            set
            {
                isArmed = value;
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\\tank.jpg"), new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y), 100, 100));
        }
        public override bool isInside(int xp, int yp)
        {
            return Math.Abs(xp - X) <= Width / 2 && Math.Abs(yp - Y) <= Length / 2;
        }
        public override void Fire(Graphics g)
        {
            //g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\Tank_explosion.jpg"), new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y + 120), 20, 20));
        }

        public override bool inRange(float xp, float yp)
        {
            return true;
        }
    }
}
