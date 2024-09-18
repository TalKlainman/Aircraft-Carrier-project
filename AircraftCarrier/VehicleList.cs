using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AircraftCarrier
{
    using VEHICLE;
    using System;
    using System.Collections;
    using System.Drawing;
    [Serializable]
    public class VehicleList
    {
        protected SortedList vehicles;
        public VehicleList()
        {
            vehicles = new SortedList();
        }
        public int NextIndex
        {
            get
            {
                return vehicles.Count;
            }
        }

        public Vehicle this[int index]
        {
            get
            {
                if (index >= vehicles.Count || index<0)
                    return (Vehicle)null;
                else
                return (Vehicle)vehicles.GetByIndex(index);

            }
            set
            {
                if (index <= vehicles.Count)
                    vehicles[index] = value;
            }
        }

        public void remove(int element)
        {
            if(element>=0 && element<=vehicles.Count)
            {
                for (int i = element; i < vehicles.Count - 1; i++)
                    vehicles[i] = vehicles[i + 1];

                vehicles.RemoveAt(vehicles.Count - 1);
            }
        }

        public void DrawAll(Graphics g)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                ((Vehicle)vehicles[i]).Draw(g);
            }
        }
    }
}
