using RimWorld;
using UnityEngine;
using Verse;

namespace FriendlyArmchair
{
    public abstract class Building_FriendlyThing : Building_Sarcophagus
    {
        public abstract ThingDef target { get; }

        public override bool CanOpen => false;

        public override void Destroy(DestroyMode mode = DestroyMode.Vanish)
        {
            innerContainer.Clear();
            base.Destroy(mode);
        }

        public override void Notify_HauledTo(Pawn hauler, Thing thing, int count)
        {
            base.Notify_HauledTo(hauler, thing, count);
            IntVec3 position = Position;
            Rot4 rotation = Rotation;
            Map map = Map;

            Building_FriendlyThing friendlyArmchair = (Building_FriendlyThing)ThingMaker.MakeThing(target);

            friendlyArmchair.SetFactionDirect(Faction);
            friendlyArmchair.HitPoints = HitPoints;
            friendlyArmchair.GetComp<CompQuality>().SetQuality(GetComp<CompQuality>().Quality, ArtGenerationContext.Colony);
            friendlyArmchair.innerContainer.TryAddOrTransfer(thing);

            CompArt comp = friendlyArmchair.GetComp<CompArt>();
            if (comp != null && !comp.Active && hauler.RaceProps.Humanlike)
            {
                comp.JustCreatedBy(hauler);
                comp.InitializeArt(friendlyArmchair.Corpse.InnerPawn);
            }

            Destroy(DestroyMode.Vanish);
            GenPlace.TryPlaceThing(friendlyArmchair, position, map, ThingPlaceMode.Direct);

            friendlyArmchair.Rotation = rotation;
            if (friendlyArmchair.Corpse.InnerPawn.story.favoriteColor != null)
                friendlyArmchair.SetColor((Color)friendlyArmchair.Corpse.InnerPawn.story.favoriteColor);
        }
    }
}