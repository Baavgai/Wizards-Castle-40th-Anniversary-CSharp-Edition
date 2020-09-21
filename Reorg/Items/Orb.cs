using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    class Orb : Item, IContent {
        private static Lazy<Orb> instance = new Lazy<Orb>(() => new Orb());
        public static Orb Instance => instance.Value;

        private Orb() : base("Orb") { }

        public void OnEntry(State state) => Game.DefaultItemMessage(state, this);

        public void Gaze(State state) {
            var effect = Util.RandPick(Effects.Value.Select(f => f(state)).Where(s => s != null));
            state.WriteLine($"\nYou gaze into the Orb and see {effect}");
        }

        private static Func<State, string> ThingAt(IHasName thing, string label) =>
            state => {
                var pick = state.Map.RandCellPos(thing);
                return !pick.HasValue ? null : $"{label} at ({pick.Value.pos}).";
            };


        private static readonly Lazy<List<Func<State, string>>> Effects = new Lazy<List<Func<State, string>>>(() =>
            new List<Func<State, string>>() {
                _ => "yourself in a bloody heap.",
                _ => "your mother telling you to clean your room.",
                _ => "a soap opera re-run.",
                _ => "yourself playing The Wizard's Castle.",
                _ => "your life drifting before your eyes.",
                _ => $"yourself in {Util.RandPick(new string[] { "fencing", "religion", "language", "alchemy" })} class.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} gazing back at you.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} eating the flesh from your corpse.",
                _ => $"a {Util.RandPick(MonsterFactory.All)} using your leg-bone as a tooth-pick.",
                s => $"yourself drinking from a pool and becomine a {Util.RandPick(Race.All.Where(x => x != s.Player.Race))}.",
                ThingAt(Content.Gold, "a pile of gold"),
                ThingAt(Content.Chest, "a chest"),
                ThingAt(Content.SinkHole, "a sinkhole"),
                ThingAt(Content.Warp, "a warp"),
                ThingAt(Content.Flares, "flares")
        });
    }
}

/*
                s => $"{randomTreasure} at ({treasureLocation[0] + 1}, {treasureLocation[1] + 1}, {treasureLocation[2] + 1}).",
                s => $"{randomMonster} at ({monsterLocation[0] + 1}, {monsterLocation[1] + 1}, {monsterLocation[2] + 1}).",
                s => $"{theMap[stairsLocation[0], stairsLocation[1], stairsLocation[2]]} at ({stairsLocation[0] + 1}, {stairsLocation[1] + 1}, {stairsLocation[2] + 1}).",
                s => $"a large mug of ale at ({rand.Next(0, theMap.GetLength(0)) + 1}, {rand.Next(0, theMap.GetLength(1)) + 1}, {rand.Next(0, theMap.GetLength(2)) + 1}) and you feel VERY thristy.",
                s => $"THE ORB OF ZOT AT ({orbOfZotLocation[0] + 1}, {orbOfZotLocation[1] + 1}, {orbOfZotLocation[2] + 1})!"
 * 
 *                 s => $"a chest at ({chestLocation[0] + 1}, {chestLocation[1] + 1}, {chestLocation[2] + 1}).",
                s => $"a sinkhole at ({sinkHoleLocation[0] + 1}, {sinkHoleLocation[1] + 1}, {sinkHoleLocation[2] + 1}).",
                s => $"a warp at ({warpLocation[0] + 1}, {warpLocation[1] + 1}, {warpLocation[2] + 1}).",
                s => $"flares at ({flaresLocation[0] + 1}, {flaresLocation[1] + 1}, {flaresLocation[2] + 1}).",
                s => $"{randomTreasure} at ({treasureLocation[0] + 1}, {treasureLocation[1] + 1}, {treasureLocation[2] + 1}).",
                s => $"{randomMonster} at ({monsterLocation[0] + 1}, {monsterLocation[1] + 1}, {monsterLocation[2] + 1}).",
                s => $"{theMap[stairsLocation[0], stairsLocation[1], stairsLocation[2]]} at ({stairsLocation[0] + 1}, {stairsLocation[1] + 1}, {stairsLocation[2] + 1}).",
                s => $"a large mug of ale at ({rand.Next(0, theMap.GetLength(0)) + 1}, {rand.Next(0, theMap.GetLength(1)) + 1}, {rand.Next(0, theMap.GetLength(2)) + 1}) and you feel VERY thristy.",
                s => $"THE ORB OF ZOT AT ({orbOfZotLocation[0] + 1}, {orbOfZotLocation[1] + 1}, {orbOfZotLocation[2] + 1})!"

 * map.RandCellPosContent<IMonster>().content.Inventory.Add(item);
 *                 $"a pile of gold at ({goldLocation[0] + 1}, {goldLocation[1] + 1}, {goldLocation[2] + 1}).",
                $"a chest at ({chestLocation[0] + 1}, {chestLocation[1] + 1}, {chestLocation[2] + 1}).",
                $"a sinkhole at ({sinkHoleLocation[0] + 1}, {sinkHoleLocation[1] + 1}, {sinkHoleLocation[2] + 1}).",
                $"a warp at ({warpLocation[0] + 1}, {warpLocation[1] + 1}, {warpLocation[2] + 1}).",
                $"flares at ({flaresLocation[0] + 1}, {flaresLocation[1] + 1}, {flaresLocation[2] + 1}).",
                $"{randomTreasure} at ({treasureLocation[0] + 1}, {treasureLocation[1] + 1}, {treasureLocation[2] + 1}).",
                $"{randomMonster} at ({monsterLocation[0] + 1}, {monsterLocation[1] + 1}, {monsterLocation[2] + 1}).",
                $"{theMap[stairsLocation[0], stairsLocation[1], stairsLocation[2]]} at ({stairsLocation[0] + 1}, {stairsLocation[1] + 1}, {stairsLocation[2] + 1}).",
                $"a large mug of ale at ({rand.Next(0, theMap.GetLength(0)) + 1}, {rand.Next(0, theMap.GetLength(1)) + 1}, {rand.Next(0, theMap.GetLength(2)) + 1}) and you feel VERY thristy.",
                $"THE ORB OF ZOT AT ({orbOfZotLocation[0] + 1}, {orbOfZotLocation[1] + 1}, {orbOfZotLocation[2] + 1})!"

 * */
