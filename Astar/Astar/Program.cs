using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astar
{
    class Program
    {
        static void Main(string[] args)
        {
            node[,] map = helperfunction.buildMap(8, 8);


        }
    }

    public class helperfunction {

        static public node[,] buildMap(int row, int col)
        {
            node[,] map = new node[row, col];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new node();
                }
            }
            return map;
        }

    } 

    

    public class xypoint
    {
        public float x;
        public float y;
        public xypoint()
        {
            this.x = 0;
            this.y = 0;
        }
    }

    public class node {
        public float costFromStart;
        public float costToGoal;
        public float totalCost;
        public xypoint parentNodexy;

        public node() {
            this.costFromStart = 0;
            this.costToGoal = 0;
            this.totalCost = this.costFromStart + this.costToGoal;
            this.parentNodexy = new xypoint();
        }
    } 
}
