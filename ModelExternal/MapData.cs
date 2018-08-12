using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSBombmanServer
{
    public class MapData
    {
        [JsonProperty("turn")]
        public int Turn { get; set; }
        [JsonProperty("walls")]
        public List<int[]> Walls { get; set; }
        [JsonProperty("blocks")]
        public List<int[]> Blocks { get; set; }
        [JsonProperty("players")]
        public List<Player> Players { get; set; }
        [JsonProperty("bombs")]
        public List<Bomb> Bombs { get; set; }
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
        [JsonProperty("fires")]
        public List<int[]> Fires { get; set; }

        public MapData(int turn,
                       List<Position> walls,
                       List<Block> blocks,
                       List<Player> players,
                       List<Bomb> bombs,
                       List<Item> items,
                       List<Position> fires)
        {
            this.Turn = turn;
            this.Walls = walls.Select(p => new int[] { p.x, p.y }).ToList();
            this.Blocks = blocks.Select(b => new int[] { b.pos.x, b.pos.y }).ToList();
            this.Players = players;
            this.Bombs = bombs;
            this.Items = items;
            this.Fires = fires.Select(p => new int[] { p.x, p.y }).ToList();
        }

        [JsonConstructor]
        public MapData(int turn,
               List<int[]> walls,
               List<int[]> blocks,
               List<Player> players,
               List<Bomb> bombs,
               List<Item> items,
               List<int[]> fires)
        {
            this.Turn = turn;
            this.Walls = walls;
            this.Blocks = blocks;
            this.Players = players;
            this.Bombs = bombs;
            this.Items = items;
            this.Fires = fires;
        }

    }

}
