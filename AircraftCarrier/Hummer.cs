using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftCarrier
{    
    using System.Drawing;
    using VEHICLE;
    using GROUND;
    [Serializable]
    public class Hummer:GROUND
    {
        public static int Id = 200;
        private int type;
        private int releaseYear;

        public Hummer()
            : this(200, 200, 10000, 90, 50, 50, 10, 0,2000,Id++)
        { }
        public Hummer(float xp, float yp, int weightp, int speedp, int widthp, int lenp, int wheelNump, int status,int year,int id)
        {
            X = xp;
            Y = yp;
            Weight = weightp;
            MaxSpeed = speedp;
            Width = widthp;
            Length = lenp;
            WheelNum = wheelNump;
            Type = status;
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
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\hummer.png"), new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y+18), 100, 100));
        }

        public override bool isInside(int xp, int yp)
        {
            // return Math.Abs(xp - X) <= Width / 2 && Math.Abs(yp - Y) <= Length / 2;

            return (xp - X) <= 100 && (xp - X) >= 0 && (yp - Y) <= 100 && (yp - Y) >= 0;
        }

        public override void Fire(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\HummerExploasion.png"), new Rectangle(Convert.ToInt32(this.X + 80), Convert.ToInt32(this.Y -12), 80, 80));
        }

        public override bool inRange(float xp, float yp)
        {
            return (xp - X)-20<= 180 && (xp - X)+1>= -80 && (yp - Y)+1 <= 0 && (yp - Y)-1  >= -89;
        }

    }
}
