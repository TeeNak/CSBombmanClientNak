using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSBombmanClientNak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSBombmanServer;
using CSBombmanClientNak.ModelInternal;

namespace CSBombmanClientNak.Tests
{
    [TestClass()]
    public class ActionDeciderTests
    {
        [TestMethod()]
        public void NextMoveTest()
        {
            var s = "{\"turn\":11,\"walls\":[[0,0],[0,1],[0,2],[0,3],[0,4],[0,5],[0,6],[0,7],[0,8],[0,9],[0,10],[0,11],[0,12],[0,13],[0,14],[1,0],[1,14],[2,0],[2,2],[2,4],[2,6],[2,8],[2,10],[2,12],[2,14],[3,0],[3,14],[4,0],[4,2],[4,4],[4,6],[4,8],[4,10],[4,12],[4,14],[5,0],[5,14],[6,0],[6,2],[6,4],[6,6],[6,8],[6,10],[6,12],[6,14],[7,0],[7,14],[8,0],[8,2],[8,4],[8,6],[8,8],[8,10],[8,12],[8,14],[9,0],[9,14],[10,0],[10,2],[10,4],[10,6],[10,8],[10,10],[10,12],[10,14],[11,0],[11,14],[12,0],[12,2],[12,4],[12,6],[12,8],[12,10],[12,12],[12,14],[13,0],[13,14],[14,0],[14,1],[14,2],[14,3],[14,4],[14,5],[14,6],[14,7],[14,8],[14,9],[14,10],[14,11],[14,12],[14,13],[14,14]],\"blocks\":[[13,5],[9,10],[6,1],[2,7],[5,9],[13,6],[11,12],[1,7],[7,2],[9,4],[7,10],[12,5],[3,1],[11,13],[7,11],[5,2],[1,8],[4,9],[6,11],[9,12],[5,3],[3,9],[11,6],[4,3],[10,13],[7,4],[3,2],[9,13],[5,11],[10,7],[8,13],[13,8],[1,9],[7,5],[4,11],[3,3],[6,5],[9,6],[5,12],[2,3],[13,9],[1,10],[9,7],[7,13],[5,5],[3,11],[8,7],[11,8],[7,7],[3,12],[1,3],[12,9],[11,9],[1,4],[12,3],[10,9],[3,6],[9,1],[6,7],[11,2],[8,1],[13,11],[9,9],[12,11],[11,3],[7,1],[5,7],[10,3],[13,4],[9,3],[11,11],[3,8],[8,3],[10,5],[2,9],[13,7],[5,4],[4,5],[5,13],[13,10],[3,5],[7,8],[1,5],[9,2],[5,8],[7,9],[10,11],[1,6],[5,1],[4,1]],\"players\":[{\"name\":\"敵\",\"pos\":{\"x\":1,\"y\":1},\"power\":2,\"setBombLimit\":2,\"ch\":\"敵\",\"isAlive\":true,\"setBombCount\":0,\"totalSetBombCount\":0,\"id\":0},{\"name\":\"泰\",\"pos\":{\"x\":1,\"y\":13},\"power\":2,\"setBombLimit\":2,\"ch\":\"泰\",\"isAlive\":true,\"setBombCount\":1,\"totalSetBombCount\":1,\"id\":1},{\"name\":\"敵\",\"pos\":{\"x\":10,\"y\":1},\"power\":2,\"setBombLimit\":2,\"ch\":\"敵\",\"isAlive\":true,\"setBombCount\":0,\"totalSetBombCount\":0,\"id\":2},{\"name\":\"あなた\",\"pos\":{\"x\":13,\"y\":13},\"power\":2,\"setBombLimit\":2,\"ch\":\"あ\",\"isAlive\":true,\"setBombCount\":0,\"totalSetBombCount\":0,\"id\":3}],\"bombs\":[{\"pos\":{\"x\":1,\"y\":11},\"timer\":2,\"power\":2}],\"items\":[],\"fires\":[]}";

            var map = Utils.JsonToObject<MapData>(s);

            var internalMap = new InternalMapData(map);

            var moveDecider = new ActionDecider();

            Action m = moveDecider.NextMove(internalMap);

            Assert.AreEqual(MOVE.RIGHT, m.Move);
        }
    }
}