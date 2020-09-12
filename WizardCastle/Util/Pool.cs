using System;
using System.Linq;
using System.Collections.Generic;

namespace WizardCastle {
    internal static partial class Util {

        private static readonly Lazy<IList<Func<Player, string>>> PoolEvents = new Lazy<IList<Func<Player, string>>>(() => {
            string PoolMsg(string poolEvent) => $"\nYou drink from the pool and {poolEvent}";
            return new List<Func<Player, string>>() {
                p => {
                    // p.Dexterity += Rand.Next(1, 3);
                    p.IncDexterity(Rand.Next(1, 3));
                    return PoolMsg("you feel nimbler.");
                },
                p => {
                    // p.Dexterity -= Rand.Next(1, 3);
                    p.DecDexterity(Rand.Next(1, 3));
                    return PoolMsg("you feel clumsier.");
                },
                p => {
                    p.IncIntelligence(Rand.Next(1, 3));
                    return PoolMsg("you feel smarter.");
                },
                p => {
                    p.DecIntelligence(Rand.Next(1, 3));
                    return PoolMsg("you feel dumber.");
                },
                p => {
                    p.IncStrength(Rand.Next(1, 3));
                    return PoolMsg("you feel stronger.");
                },
                p => {
                    p.DecStrength(Rand.Next(1, 3));
                    return PoolMsg("you feel weaker.");
                },
                p => {
                    var xs = GameCollections.Races.Select(x => x.RaceName).Where(x => x != p.race).ToList();
                    p.race = xs[Rand.Next(0, xs.Count)];
                    return PoolMsg($"you turn into a {p.race}.");
                },
                p => {
                    p.sex = p.sex == "Male" ? "FeMale" : "Male";
                    return PoolMsg($"you are now a {p.sex} {p.race}.");
                },
            };
        });


        public static string PoolEvent(Player player) =>
            RandPick(PoolEvents.Value)(player);
    }
}
