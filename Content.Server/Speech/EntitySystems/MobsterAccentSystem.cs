using System.Linq;
using System.Text.RegularExpressions;
using Content.Server.Speech.Components;
using Robust.Shared.Random;

namespace Content.Server.Speech.EntitySystems;

public sealed class MobsterAccentSystem : EntitySystem
{
    private static readonly Regex RegexIng = new(@"(?<=\w\w)(in)g(?!\w)", RegexOptions.IgnoreCase);
    private static readonly Regex RegexLowerOr = new(@"(?<=\w)o[Rr](?=\w)");
    private static readonly Regex RegexUpperOr = new(@"(?<=\w)O[Rr](?=\w)");
    private static readonly Regex RegexLowerAr = new(@"(?<=\w)a[Rr](?=\w)");
    private static readonly Regex RegexUpperAr = new(@"(?<=\w)A[Rr](?=\w)");

    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly ReplacementAccentSystem _replacement = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<MobsterAccentComponent, AccentGetEvent>(OnAccentGet);
    }

    public string Accentuate(string message, MobsterAccentComponent component)
    {
        // Order:
        // Do text manipulations first
        // Then prefix/suffix funnyies

        // direct word replacements
        var msg = _replacement.ApplyReplacements(message, "mobster");

        // thinking -> thinkin'
        // king -> king
        //Uses captures groups to make sure the captialization of IN is kept
        msg = RegexIng.Replace(msg, "$1'");

        // or -> uh and ar -> ah in the middle of words (fuhget, tahget)
        msg = RegexLowerOr.Replace(msg, "uh");
        msg = RegexUpperOr.Replace(msg, "UH");
        msg = RegexLowerAr.Replace(msg, "ah");
        msg = RegexUpperAr.Replace(msg, "AH");

        // Sanitize capital again, in case we substituted a word that should be capitalized
        msg = msg[0].ToString().ToUpper() + msg.Remove(0, 1);

        return msg;
    }

    private void OnAccentGet(EntityUid uid, MobsterAccentComponent component, AccentGetEvent args)
    {
        args.Message = Accentuate(args.Message, component);
    }
}
