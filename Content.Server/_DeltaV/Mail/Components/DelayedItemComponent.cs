namespace Content.Server._DeltaV.Mail.Components
{
    /// <summary>
    /// A placeholder for another entity, spawned when dropped or placed in someone's hands.
    /// Useful for storing instant effect entities, e.g. smoke, in the mail.
    /// </summary>
    [RegisterComponent]
    public sealed partial class DelayedItemComponent : Component
    {
        /// <summary>
        /// The entity to replace this when opened or dropped.
        /// </summary>
        [DataField]
        public string Item = "None";
    }
}
