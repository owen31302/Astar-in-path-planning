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
            
            xypoint startNode = new xypoint();
            startNode.x = 1;
            startNode.y = 2;

            xypoint goalNode = new xypoint();
            goalNode.x = 3;
            goalNode.y = 5;
            String words = helperfunction.AstarSearch(startNode, goalNode);

            Console.WriteLine(words);

        }
    }

    public class helperfunction {

        static public String AstarSearch(xypoint startNode, xypoint goalNode) {

            String returnString = "fail";

            //TODO need to be fixed
            node[,] map = buildMap(8, 8);

            List<node> open = new List<node>();
            List<node> closed = new List<node>();

            // this is the initial node
            map[startNode.x, startNode.y].costToGoal = PathCostEstimation(startNode, goalNode);
            map[startNode.x, startNode.y].totalCost = map[startNode.x, startNode.y].costToGoal;

            open.Add(map[startNode.x, startNode.y]);

            while (open.Count>0) {
                int index = lowestTotalCost(open);
                node Node = pop(open, index);

                if (Node.selfNodexy.x == goalNode.x && Node.selfNodexy.y == goalNode.y)
                {
                    Console.WriteLine("Goal("+goalNode.x+","+goalNode.y+")");
                    xypoint print = Node.parentNodexy;
                    while (print.x != 0 && print.y != 0) {
                        Console.WriteLine("("+print.x+","+ print.y+")");
                        print = map[print.x, print.y].parentNodexy;
                    }
                    returnString = "success";
                    break;
                }
                else {
                    List<node> SuccessorNodes = successorNodes(Node, map);
                    foreach (node NewNode in SuccessorNodes) {
                        float NewCost = Node.costFromStart + TraverseCost(NewNode.selfNodexy, Node.selfNodexy);


                        if (((open.IndexOf(NewNode) != -1) || (closed.IndexOf(NewNode) != -1)) &&
                            NewNode.costFromStart <= NewCost)
                        {
                            continue;
                        }
                        else {
                            map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].parentNodexy = Node.selfNodexy;
                            map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].costFromStart = NewCost;
                            map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].costToGoal = PathCostEstimation(NewNode.selfNodexy, goalNode);
                            map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].totalCost = 
                                map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].costFromStart + 
                                map[NewNode.selfNodexy.x, NewNode.selfNodexy.y].costToGoal;
                            if (closed.IndexOf(NewNode) != -1) {
                                closed.Remove(NewNode);
                            }
                            if (open.IndexOf(NewNode) != -1)
                            {
                                open.Remove(NewNode);
                                open.Add(map[NewNode.selfNodexy.x, NewNode.selfNodexy.y]);
                            }
                            else {
                                open.Add(NewNode);
                            }
                        }
                    }
                }
                closed.Add(Node);
            }
            return returnString;
        }

        static private List<node> successorNodes(node startNode, node[,] map) {
            List<node> successorNodes = new List<node>();
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (map[startNode.selfNodexy.x + i, startNode.selfNodexy.y + j].selfNodexy != startNode.parentNodexy &&
                        map[startNode.selfNodexy.x + i, startNode.selfNodexy.y + j].selfNodexy != startNode.selfNodexy) {
                        successorNodes.Add(map[startNode.selfNodexy.x + i, startNode.selfNodexy.y + j]);
                    }
                }
            }
            return successorNodes;
        }

        static private float TraverseCost(xypoint startNode, xypoint endNode) {
            return PathCostEstimation(startNode, endNode);
        }

        static private float PathCostEstimation(xypoint startNode, xypoint goalNode) {
            return (float)Math.Sqrt(Math.Pow(startNode.x - goalNode.x, 2) + Math.Pow(startNode.y - goalNode.y, 2));
        }

        static private int lowestTotalCost(List<node> list) {
            float cost = float.MaxValue;
            int i = 0;
            for (int j=0; j<list.Count; j++) {
                if (list[j].totalCost < cost) {
                    i = j;
                    cost = list[j].totalCost;
                }
            }
            return i;
        }

        static private node pop(List<node> list, int index) {
            node returnNode = null;
            if (index == -1){
                returnNode = list[list.Count-1];
                list.RemoveAt(list.Count - 1);
            }
            else {
                returnNode = list[index];
                list.RemoveAt(index);
            }
            return returnNode;
        }

        static private node[,] buildMap(int row, int col)
        {
            node[,] map = new node[row, col];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new node();
                    map[x, y].selfNodexy.x = x;
                    map[x, y].selfNodexy.y = y;
                }
            }
            return map;
        }
    } 

    

    public class xypoint
    {
        public int x;
        public int y;
        public xypoint()
        {
            this.x = 0;
            this.y = 0;
        }
    }

    public class node {
        public xypoint selfNodexy;
        public float costFromStart;
        public float costToGoal;
        public float totalCost;
        public xypoint parentNodexy;

        public node() {
            this.selfNodexy = new xypoint();
            this.costFromStart = 0;
            this.costToGoal = 0;
            this.totalCost = this.costFromStart + this.costToGoal;
            this.parentNodexy = new xypoint();
        }
    } 
}
