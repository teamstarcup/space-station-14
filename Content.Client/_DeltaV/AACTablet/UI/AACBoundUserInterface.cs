using Content.Client.Chat.TypingIndicator;
using Content.Shared._DeltaV.AACTablet;
using Content.Shared._DeltaV.QuickPhrase;
using Content.Shared.Chat.TypingIndicator;
using Robust.Shared.Prototypes;
using Robust.Client.UserInterface;

namespace Content.Client._DeltaV.AACTablet.UI;

public sealed class AACBoundUserInterface : BoundUserInterface
{
    [ViewVariables]
    private AACWindow? _window;

    private static readonly ProtoId<TypingIndicatorPrototype> AACTypingIndicator = "aac";

    private TypingIndicatorSystem? _typing;

    public AACBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
    {
    }

    protected override void Open()
    {
        base.Open();
        _window?.Close();
        _window = this.CreateWindow<AACWindow>();
        _window.PhraseButtonPressed += OnPhraseButtonPressed;
        _window.Typing += OnTyping;
        _window.SubmitPressed += OnSubmit;
    }

    private void OnPhraseButtonPressed(List<ProtoId<QuickPhrasePrototype>> phraseId)
    {
        SendMessage(new AACTabletSendPhraseMessage(phraseId));
    }

    private void OnTyping()
    {
        _typing ??= EntMan.System<TypingIndicatorSystem>();
        _typing?.ClientAlternateTyping(AACTypingIndicator);
    }

    private void OnSubmit()
    {
        _typing ??= EntMan.System<TypingIndicatorSystem>();
        _typing?.ClientSubmittedChatText();
    }
}
