using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Builder
{
    public class TileData : MonoBehaviour
    {
        public GameObject construct;
        public bool isStructureBuildable = true;
        public bool isTerrainBuildable = false;
        public bool containsResource = false;

        public Dictionary<Heading, TileData> adjacentTiles = new();
    }
}

