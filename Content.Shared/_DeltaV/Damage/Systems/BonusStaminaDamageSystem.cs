using Content.Shared._DeltaV.Damage.Components;
using Content.Shared.Damage.Events;

namespace Content.Shared._DeltaV.Damage.Systems;

public sealed partial class BonusStaminaDamageSystem : EntitySystem
{
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<BonusStaminaDamageComponent, StaminaMeleeHitEvent>(OnStamHit);
    }

    private void OnStamHit(Entity<BonusStaminaDamageComponent> ent, ref StaminaMeleeHitEvent args)
    {
        args.Multiplier *= ent.Comp.Multiplier;
    }
}
