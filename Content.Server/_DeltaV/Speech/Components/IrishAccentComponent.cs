using Content.Server._DeltaV.Speech.EntitySystems;

namespace Content.Server._DeltaV.Speech.Components;

// Takes the ES and assigns the system and component to each other
[RegisterComponent]
[Access(typeof(IrishAccentSystem))]
public sealed partial class IrishAccentComponent : Component
{ }
