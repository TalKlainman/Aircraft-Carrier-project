using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftCarrier
{
    using System.Drawing;
    using VEHICLE;
    using AirTransport;
    [Serializable]
    public class F16:AirTransport
    {
        private int type;
        private int releaseYear;
        public static int Id = 300;

        public F16()
            : this(100, 100, 6000, 250, 60, 50, true,0,2010,Id++)
        { }

        public F16(float xp, float yp, int weightp, int speedp, int widthp, int lenp, bool status,int type,int year,int id)
        {
            X = xp;
            Y = yp;
            Weight = weightp;
            MaxSpeed = speedp;
            Width = widthp;
            Length = lenp;
            IsArmed = status;
            Type = type;
            ReleaseYear = year;
            ID = id;
            Id++;
        }

        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public int ReleaseYear
        {
            get
            {
                return releaseYear;
            }
            set
            {
                releaseYear = value;
            }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\plane.jpg"), new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y), 100, 100));
        }

        public override bool isInside(int xp, int yp)
        {
            return (xp - X) <= 100 && (xp - X)>=0 && (yp - Y) <= 100&& (yp - Y)>=0;
            //return Math.Abs(xp - X) <= Width / 2 && Math.Abs(yp - Y) <= Length / 2;
        }

        public override bool inRange(float xp, float yp)
        {
            return (xp - X) <= 300 && (xp - X) >= 70 && (yp - Y)  <= 240 && (yp - Y) >=0;
        }

        public override void Fire(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\PlaneExploasion.png"), new Rectangle(Convert.ToInt32(this.X + 150), Convert.ToInt32(this.Y+90), 150, 150));
        }

    }
}
