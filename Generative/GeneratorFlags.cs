using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Generative
{
    #region DungeonFlags
    public enum SizeOfDungeon
    {
        Small = 16,
        Medium = 32,
        Large = 64
    }
    public enum TypeOfDungeon
    {
        Linear,
        Branching,
        Special
    }
    public enum RoomFlags
    {
        StartingRoom,
        BossRoom,
        SpecialRoom,
        PuzzleRoom
    }
    public enum Heading
    {
        North,
        East,
        South,
        West
    }
    public enum ResourceAvailability //How many to activate out of the available number
    {
        None, //None
        Sparse, //Quarter
        Regular, //Half
        Abundant, // 3/4s
        Overflowing //All
    }
    public enum MarkerType
    {
        Loot,
        Enemy,
        Resource
    }

    public enum BuildState
    {
        Empty,
        Building,
        Built
    }
    #endregion

    #region FieldFlags
    public enum FieldLocationMix
    {
        AllDangerous,
        SomeDangerous,
        EvenMix,
        MostSafe,
        TotallySafe
    }

    public enum FieldNodeSize
    {
        Tiny = 5,
        Small = 10,
        Medium = 20,
        Large = 30,
        Huge = 50
    }
    #endregion
}
