using Verse;

namespace FriendlyArmchair
{
    public class Building_FriendlyArmchair : Building_FriendlyThing
    {
        public override ThingDef target => ThingDef.Named("FriendlyArmchair");
    }
}