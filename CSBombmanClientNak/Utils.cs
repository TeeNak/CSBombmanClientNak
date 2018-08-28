using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSBombmanServer
{
    public class Utils
    {
        public const string VERSION = "0.4.6";
        public const int INIT_FIRE_POWER = 2;
        public const int INIT_BOMB_LIMIT = 2;

        public const string UP = "UP";
        public const string DOWN = "DOWN";
        public const string LEFT = "LEFT";
        public const string RIGHT = "RIGHT";

        public static readonly string[] DEFAULT_MAP = new string[] {
        "■■■■■■■■■■■■■■■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■　■　■　■　■　■　■　■",
        "■　　　　　　　　　　　　　■",
        "■■■■■■■■■■■■■■■"};
        public static readonly int HEIGHT = DEFAULT_MAP.Length;
        public static readonly int WIDTH = DEFAULT_MAP[0].Length;
        public const int ITEM_COUNT = 20;

        public const double DEFAULT_SLEEP_TIME = 500;

        public static readonly char[][] MAP_ARRAY = new char[][]{
            DEFAULT_MAP[0].ToCharArray(),
            DEFAULT_MAP[1].ToCharArray(),
            DEFAULT_MAP[2].ToCharArray(),
            DEFAULT_MAP[3].ToCharArray(),
            DEFAULT_MAP[4].ToCharArray(),
            DEFAULT_MAP[5].ToCharArray(),
            DEFAULT_MAP[6].ToCharArray(),
            DEFAULT_MAP[7].ToCharArray(),
            DEFAULT_MAP[8].ToCharArray(),
            DEFAULT_MAP[9].ToCharArray(),
            DEFAULT_MAP[10].ToCharArray(),
            DEFAULT_MAP[11].ToCharArray(),
            DEFAULT_MAP[12].ToCharArray(),
            DEFAULT_MAP[13].ToCharArray(),
            DEFAULT_MAP[14].ToCharArray()};

        public static readonly Position[] NEAR_INIT_POSITIONS = new Position[]{
            new Position(1,1), new Position(1,2), new Position(2,1),
            new Position(1, HEIGHT-2),new Position(1, HEIGHT-3),new Position(2, HEIGHT-2),//左下
            new Position(WIDTH-2,1),new Position(WIDTH-2,2),new Position(WIDTH-3,1),//右上
            new Position(WIDTH-2, HEIGHT-2),new Position(WIDTH-2, HEIGHT-3),new Position(WIDTH-3, HEIGHT-2)};

        public static readonly Position[] INIT_POSITIONS = new Position[]{
            new Position(1,1), new Position(1, HEIGHT-2), new Position(WIDTH-2,1), new Position(WIDTH-2, HEIGHT-2)};

        public static string ObjectToJson(object v)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(v);
        }

        public static T JsonToObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static readonly int[][] FALLING_WALL = {
            new int[] { 1, 1 },
            new int[] { 2, 1 },
            new int[] { 3, 1 },
            new int[] { 4, 1 },
            new int[] { 5, 1 },
            new int[] { 6, 1 },
            new int[] { 7, 1 },
            new int[] { 8, 1 },
            new int[] { 9, 1 },
            new int[] { 10, 1 },
            new int[] { 11, 1 },
            new int[] { 12, 1 },
            new int[] { 13, 1 },
            new int[] { 13, 2 },
            new int[] { 13, 3 },
            new int[] { 13, 4 },
            new int[] { 13, 5 },
            new int[] { 13, 6 },
            new int[] { 13, 7 },
            new int[] { 13, 8 },
            new int[] { 13, 9 },
            new int[] { 13, 10 },
            new int[] { 13, 11 },
            new int[] { 13, 12 },
            new int[] { 13, 13 },
            new int[] { 12, 13 },
            new int[] { 11, 13 },
            new int[] { 10, 13 },
            new int[] { 9, 13 },
            new int[] { 8, 13 },
            new int[] { 7, 13 },
            new int[] { 6, 13 },
            new int[] { 5, 13 },
            new int[] { 4, 13 },
            new int[] { 3, 13 },
            new int[] { 2, 13 },
            new int[] { 1, 13 },
            new int[] { 1, 12 },
            new int[] { 1, 11 },
            new int[] { 1, 10 },
            new int[] { 1, 9 },
            new int[] { 1, 8 },
            new int[] { 1, 7 },
            new int[] { 1, 6 },
            new int[] { 1, 5 },
            new int[] { 1, 4 },
            new int[] { 1, 3 },
            new int[] { 1, 2 },
            new int[] { 2, 2 },
            new int[] { 3, 2 },
            new int[] { 4, 2 },
            new int[] { 5, 2 },
            new int[] { 6, 2 },
            new int[] { 7, 2 },
            new int[] { 8, 2 },
            new int[] { 9, 2 },
            new int[] { 10, 2 },
            new int[] { 11, 2 },
            new int[] { 12, 2 },
            new int[] { 12, 3 },
            new int[] { 12, 4 },
            new int[] { 12, 5 },
            new int[] { 12, 6 },
            new int[] { 12, 7 },
            new int[] { 12, 8 },
            new int[] { 12, 9 },
            new int[] { 12, 10 },
            new int[] { 12, 11 },
            new int[] { 12, 12 },
            new int[] { 11, 12 },
            new int[] { 10, 12 },
            new int[] { 9, 12 },
            new int[] { 8, 12 },
            new int[] { 7, 12 },
            new int[] { 6, 12 },
            new int[] { 5, 12 },
            new int[] { 4, 12 },
            new int[] { 3, 12 },
            new int[] { 2, 12 },
            new int[] { 2, 11 },
            new int[] { 2, 10 },
            new int[] { 2, 9 },
            new int[] { 2, 8 },
            new int[] { 2, 7 },
            new int[] { 2, 6 },
            new int[] { 2, 5 },
            new int[] { 2, 4 },
            new int[] { 2, 3 },
            new int[] { 3, 3 },
            new int[] { 4, 3 },
            new int[] { 5, 3 },
            new int[] { 6, 3 },
            new int[] { 7, 3 },
            new int[] { 8, 3 },
            new int[] { 9, 3 },
            new int[] { 10, 3 },
            new int[] { 11, 3 },
            new int[] { 11, 4 },
            new int[] { 11, 5 },
            new int[] { 11, 6 },
            new int[] { 11, 7 },
            new int[] { 11, 8 },
            new int[] { 11, 9 },
            new int[] { 11, 10 },
            new int[] { 11, 11 },
            new int[] { 10, 11 },
            new int[] { 9, 11 },
            new int[] { 8, 11 },
            new int[] { 7, 11 },
            new int[] { 6, 11 },
            new int[] { 5, 11 },
            new int[] { 4, 11 },
            new int[] { 3, 11 },
            new int[] { 3, 10 },
            new int[] { 3, 9 },
            new int[] { 3, 8 },
            new int[] { 3, 7 },
            new int[] { 3, 6 },
            new int[] { 3, 5 },
            new int[] { 3, 4 },
            new int[] { 4, 4 },
            new int[] { 5, 4 },
            new int[] { 6, 4 },
            new int[] { 7, 4 },
            new int[] { 8, 4 },
            new int[] { 9, 4 },
            new int[] { 10, 4 },
            new int[] { 10, 5 },
            new int[] { 10, 6 },
            new int[] { 10, 7 },
            new int[] { 10, 8 },
            new int[] { 10, 9 },
            new int[] { 10, 10 },
            new int[] { 9, 10 },
            new int[] { 8, 10 },
            new int[] { 7, 10 },
            new int[] { 6, 10 },
            new int[] { 5, 10 },
            new int[] { 4, 10 },
            new int[] { 4, 9 },
            new int[] { 4, 8 },
            new int[] { 4, 7 },
            new int[] { 4, 6 },
            new int[] { 4, 5 }};


        // {{1, 1}, {2, 1}, {3, 1}, {4, 1}, {5, 1}, {6, 1}, {7, 1}, {8, 1}, {9, 1}, {10, 1}, {11, 1}, 
        // {12, 1}, {13, 1}, {13, 2}, {13, 3}, {13, 4}, {13, 5}, {13, 6}, {13, 7}, {13, 8}, {13, 9}, 
        // {13, 10}, {13, 11}, {13, 12}, {13, 13}, {12, 13}, {11, 13}, {10, 13}, {9, 13}, {8, 13}, 
        // {7, 13}, {6, 13}, {5, 13}, {4, 13}, {3, 13}, {2, 13}, {1, 13}, {1, 12}, {1, 11}, {1, 10}, 
        // {1, 9}, {1, 8}, {1, 7}, {1, 6}, {1, 5}, {1, 4}, {1, 3}, {1, 2}, {2, 2}, {3, 2}, {4, 2}, 
        // {5, 2}, {6, 2}, {7, 2}, {8, 2}, {9, 2}, {10, 2}, {11, 2}, {12, 2}, {12, 3}, {12, 4}, {12, 5},
        // {12, 6}, {12, 7}, {12, 8}, {12, 9}, {12, 10}, {12, 11}, {12, 12}, {11, 12}, {10, 12},
        // {9, 12}, {8, 12}, {7, 12}, {6, 12}, {5, 12}, {4, 12}, {3, 12}, {2, 12}, {2, 11}, {2, 10},
        // {2, 9}, {2, 8}, {2, 7}, {2, 6}, {2, 5}, {2, 4}, {2, 3}, {3, 3}, {4, 3}, {5, 3}, {6, 3}, 
        // {7, 3}, {8, 3}, {9, 3}, {10, 3}, {11, 3}, {11, 4}, {11, 5}, {11, 6}, {11, 7}, {11, 8}, 
        // {11, 9}, {11, 10}, {11, 11}, {10, 11}, {9, 11}, {8, 11}, {7, 11}, {6, 11}, {5, 11}, {4, 11},
        // {3, 11}, {3, 10}, {3, 9}, {3, 8}, {3, 7}, {3, 6}, {3, 5}, {3, 4}, {4, 4}, {5, 4}, {6, 4},
        // {7, 4}, {8, 4}, {9, 4}, {10, 4}, {10, 5}, {10, 6}, {10, 7}, {10, 8}, {10, 9}, {10, 10}, 
        // {9, 10}, {8, 10}, {7, 10}, {6, 10}, {5, 10}, {4, 10}, {4, 9}, {4, 8}, {4, 7}, {4, 6}, {4, 5}};


        public static bool IsNearInitPosition(Position pos)
        {
            return NEAR_INIT_POSITIONS.Contains(pos);
        }

    }
}
