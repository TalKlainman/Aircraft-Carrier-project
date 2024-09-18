using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum TankType:int
{
    Light=0 ,
    Heavy=1   
}

namespace AircraftCarrier
{
    using System.Drawing;
    using VEHICLE;
    using GROUND;
    [Serializable]

    public class Tank : GROUND
    {
        private int type;
        public static int Id = 100;


        public Tank()
            :this(200, 200, 10000, 90, 50, 50, 10,0,Id++)
        { }
        public Tank(float xp, float yp, int weightp, int speedp, int widthp, int lenp, int wheelNump,int status,int id)
        {
            X = xp;
            Y = yp;
            Weight = weightp;
            MaxSpeed = speedp;
            Width = widthp;
            Length = lenp;
            WheelNum = wheelNump;
            Type = status;
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

        public override void Draw(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\tank.jpg"), new Rectangle(Convert.ToInt32(this.X), Convert.ToInt32(this.Y), 100, 100));
        }

        public override bool isInside(int xp, int yp)
        {
            // return Math.Abs(xp - X) <= Width / 2 && Math.Abs(yp - Y) <= Length / 2;
            return (xp - X) <= 100 && (xp - X) >= 0 && (yp - Y) <= 100 && (yp - Y) >= 0;
        }
        public override void Fire(Graphics g)
        {
            g.DrawImage(Bitmap.FromFile("C:\\Users\\talok\\Desktop\\aircraft new\\AircraftCarrier\\AircraftCarrier\\Images\\Tank_explosion.png"), new Rectangle(Convert.ToInt32(this.X+5), Convert.ToInt32(this.Y-93), 90, 90));
        }

        public override bool inRange(float xp, float yp)
        {
            bool res = (xp - X) +20 <= 100 && (xp - X) +20  >= -75 && (yp - Y)+170  <= 90 && (yp - Y) +170 >= 0;
            return res;
        }
    }
}
