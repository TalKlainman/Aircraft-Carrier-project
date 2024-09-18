using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace VEHICLE
{
    using System;
    using System.Collections;
    [Serializable]
    public abstract class Vehicle
    {
        float x;
        float y;
        int width;
        int length;
        int id;

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public abstract bool isInside(int xp, int yp);
        public abstract void Draw(Graphics g);
        public abstract void Fire(Graphics g);
        public abstract bool inRange(float xp, float yp);
    }
}
