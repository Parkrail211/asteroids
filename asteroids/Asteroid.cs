using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asteroids
{
    class Asteroid
    {
        public int x, y, speed;
        public bool direction;

        public Asteroid(int _x, int _y, int _speed, bool _direction)
        {
            x = _x;
            y = _y;
            speed = _speed;
            direction = _direction;
        }
    }

   
}
