using Verse;

namespace FriendlyArmchair
{
    public class Building_FriendlyCouch : Building_FriendlyThing
    {
        public override ThingDef target => ThingDef.Named("FriendlyCouch");
    }
}