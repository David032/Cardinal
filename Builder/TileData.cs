using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cardinal.Builder
{
    public class TileData : MonoBehaviour
    {
        public int xPos;
        public int yPos;

        public GameObject construct;
        public bool isStructureBuildable = true;
        public bool isTerrainBuildable = false;
        public bool containsResource = false;
        public bool isUsable = true;

        [SerializeField]
        TileData _NorthTile;
        [SerializeField]
        TileData _EastTile;
        [SerializeField]
        TileData _SouthTile;
        [SerializeField]
        TileData _WestTile;


        public void UpdateAdjacentTiles()
        {
            var tiles = CardinalBuilder.Instance.GetTiles();
            int size = CardinalBuilder.Instance.AreaSize;
            //North
            if (yPos != size - 1)
            {
                var NorthTile = tiles.Where
                    (x => x.xPos == xPos && x.yPos == yPos + 1).FirstOrDefault();
                if (NorthTile.construct != null)
                {
                    _NorthTile = NorthTile;
                }
            }
            //East
            if (xPos != size - 1)
            {
                var EastTile = tiles.Where
                    (x => x.xPos == xPos + 1 && x.yPos == yPos).FirstOrDefault();
                if (EastTile.construct != null)
                {
                    _EastTile = EastTile;
                }
            }
            //South
            if (yPos != 0)
            {
                var SouthTile = tiles.Where
                    (x => x.xPos == xPos && x.yPos == yPos - 1).FirstOrDefault();
                if (SouthTile.construct != null)
                {
                    _SouthTile = SouthTile;
                }
            }
            //West
            if (xPos != 0)
            {
                var WestTile = tiles.Where
                    (x => x.xPos == xPos - 1 && x.yPos == yPos).FirstOrDefault();
                if (WestTile.construct != null)
                {
                    _WestTile = WestTile;
                }
            }
        }
    }
}

