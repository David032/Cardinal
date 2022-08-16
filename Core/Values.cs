using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal
{
    public enum GameState
    {
        Hub,
        Field,
        Dungeon,
        Loading
    }

    //Need much better values for abilities
    public enum PhysicalAbility
    {
        Weak = -1,
        Normal = 0,
        Strong = 1,
    }
    public enum MentalAbility
    {
        Dumb = -1,
        Normal = 0,
        Smart = 1,
    }
    public enum BehaviourTypes
    {
        Attacker,
        Socialiser,
        Explorer,
        Achiever,
        Mandatory,
        Optional,
        Lawful,
        Chaotic,
        Neutral,
        Good,
        Evil
    }
    public enum HexadTypes
    {
        Philanthropists,
        Socialisers,
        FreeSpirits,
        Achievers,
        Players,
        Disruptors
    }
    public enum Categories
    {
        Good,
        Evil,
        Lawful,
        Chaotic,
        Neutral,
        Rich,
        Poor,
        Helpful,
        Cruel,
        Cat,
        Dog,
        Spider,
        Horse,
        Trade,
        Change,
        StatusQuo,
        Kind,
        Charitable,
        Crafts,
        HardWorker,
        Lazy,
        Skilled,
        Unskilled,
        Danger,
        Dragon,
        Heroic,
        Villanous,
        NotImportant,
        Important,
        Death,
        Life,
        Town,
        City,
        Royalty,
        Peasents,
        Crime,
        Murder,
        Theft,
        Magic,
        Melee,
        Ranged,
        Order,
        Chaos,
        Funny,
        Boring
    }
    public enum Factions
    {
        TownA,
        TownB,
        VillageA,
        City,
        None
    }
    public enum RoomType
    {
        MainRoom,
        SecondaryRoom,
        PuzzleRoom,
        BossRoom,
        SpecialRoom
    }
    public enum Priority
    {
        Low,
        Medium,
        High
    }
    public enum NodeType
    {
        Chest,
        Resource
    }
    public enum InventoryChange
    {
        Gain,
        Loss
    }
    public enum PlayerPerformance
    {
        Poor,
        BelowAverage,
        Average,
        AboveAverage,
        Exceptional
    }

    public enum Heading
    {
        North,
        East,
        South,
        West,
    }

    #region Adjustor Message Elements
    public enum ResponseWindow
    {
        NextDungeonGeneration,
        NextRespawn,
        NextReturnToHubArea,
        Immediate
    }
    public enum ResponseSubject
    {
        Player,
        NPC,
        Enemies,
        Boss,
        Dungeon,
    }
    public enum ResponseAction
    {
        Completed,
        Failed,
        Killed,
        Entered,
        Left,
        Achieved,
        Increase,
        Decrease,
        Died,
        Exploring,
        IsNotExploring
    }
    public enum ResponseValue
    {
        KillDeathRatio,
        DungeonProgression,
        PlayerPerformance,
        DungeonsCompleted,
        Philanthropist,
        Socialiser,
        FreeSpirit,
        Achiever,
        Disruptor,
        Player,
    }
    public enum ResponseLocation
    {
        CurrentDungeon,
        HubArea,
        BossRoom,
        StartingRoom,
    }
    public enum ResponseGoal
    {
        Endeavour,
        Job,
        Quest,
        Achievement
    }
    public enum ResponseModifier
    {
        Exclusivley,
        All,
        None,
    }
    #endregion
}



