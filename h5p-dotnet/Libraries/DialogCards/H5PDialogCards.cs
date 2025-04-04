using Newtonsoft.Json;

namespace H5pDotNet.Libraries.DialogCards;

/// <summary>
///     The Dialogcards library for H5p.
/// </summary>
public class H5PDialogCards : IH5PContent
{
    /// <summary>
    ///     The Heading that will be displayed outside of the dialog cards themselves.
    ///     Supports HTML Tags: p, br, strong, em, code.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; }

    /// <summary>
    ///     The Task description that will be displayed outside of the dialog cards themselves.
    ///     Supports HTML Tags: p, br, strong, em, code.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     The dialog cards themselves.
    /// </summary>
    [JsonProperty("dialogs")]
    public List<H5PDialogCard> Dialogs { get; set; } = new();

    /// <summary>
    ///     Card behaviour.
    /// </summary>
    [JsonProperty("behaviour")]
    public Behaviour CardBehaviour { get; set; } = new();

    /// <summary>
    ///     Mode of presenting the dialog cards,
    ///     Can be "normal", or "repetition".
    /// </summary>
    [JsonProperty("mode")]
    public string Mode { get; set; } = "normal";

    // Below are the default translations for English for various settings.
    [JsonProperty("next")] public string TransNext { get; set; } = "Next";

    [JsonProperty("confirmStartingOver")] public ConfirmStartingOver TransConfirmOptions { get; set; } = new();

    [JsonProperty("answer")] public string TransAnswer { get; set; } = "Turn";

    [JsonProperty("prev")] public string TransPrev { get; set; } = "Previous";

    [JsonProperty("retry")] public string TransRetry { get; set; } = "Try again";

    [JsonProperty("progressText")] public string TransProgressText { get; set; } = "Card @card of @total";


    [JsonProperty("cardFrontLabel")] public string TransCardFrontLabel { get; set; } = "Card front";

    [JsonProperty("cardBackLabel")] public string TransCardBackLabel { get; set; } = "Card back";

    [JsonProperty("tipButtonLabel")] public string TransTipButtonLabel { get; set; } = "Show tip";

    [JsonProperty("audioNotSupported")]
    public string TransAudioNotSupported { get; set; } = "Your browser does not support this audio";

    [JsonProperty("correctAnswer")] public string TransCorrectAnswer { get; set; } = "I got it right!";

    [JsonProperty("incorrectAnswer")] public string TransIncorrectAnswer { get; set; } = "I got it wrong";

    [JsonProperty("round")] public string TransRound { get; set; } = "Round @round";

    [JsonProperty("cardsLeft")] public string TransCardsLeft { get; set; } = "Cards left: @number";

    [JsonProperty("nextRound")] public string TransNextRound { get; set; } = "Proceed to round @round";

    [JsonProperty("startOver")] public string TransStartOver { get; set; } = "Start over";

    [JsonProperty("showSummary")] public string TransShowSummary { get; set; } = "Next";

    [JsonProperty("summary")] public string TransSummary { get; set; } = "Summary";

    [JsonProperty("summaryCardsRight")] public string TransSummaryCardsRight { get; set; } = "Cards you got right:";

    [JsonProperty("summaryCardsWrong")] public string TransSummaryCardsWrong { get; set; } = "Cards you got wrong:";

    [JsonProperty("summaryCardsNotShown")]
    public string TransSummaryCardsNotShown { get; set; } = "Cards in pool not shown:";

    [JsonProperty("summaryOverallScore")] public string TransSummaryOverallScore { get; set; } = "Overall Score";

    [JsonProperty("summaryCardsCompleted")]
    public string TransSummaryCardsCompleted { get; set; } = "Cards you have completed learning:";

    [JsonProperty("summaryCompletedRounds")]
    public string TransSummaryCompletedRounds { get; set; } = "Completed rounds:";

    [JsonProperty("summaryAllDone")]
    public string TransSummaryAllDone { get; set; } =
        "Well done! You got all @cards cards correct @max times in a row!";

    public class Behaviour
    {
        /// <summary>
        ///     Scale the text to fit inside the card
        ///     Setting this option to false will make the card adapt its size to the size of the text
        /// </summary>
        [JsonProperty("scaleTextNotCard")]
        public bool ScaleTextNotCard { get; set; } = true;

        /// <summary>
        ///     Enable the "Retry" button.
        /// </summary>
        [JsonProperty("enableRetry")]
        public bool EnableRetry { get; set; } = true;

        /// <summary>
        ///     This option will only allow you to move forward with Dialog Cards
        /// </summary>
        [JsonProperty("disableBackwardsNavigation")]
        public bool DisableBackwardsNavigation { get; set; }

        /// <summary>
        ///     Enable to randomize the order of cards on display.
        ///     Only compatible with "normal" mode.
        /// </summary>
        [JsonProperty("randomCards")]
        public bool RandomCards { get; set; }

        /// <summary>
        ///     Maximum proficiency level.
        ///     Only compatible with "repetition" mode.
        ///     Min = 3
        ///     Max = 7
        ///     Default = 5.
        /// </summary>
        [JsonProperty("maxProficiency")]
        public int MaxProficiency { get; set; } = 5;

        /// <summary>
        ///     If activated, learners can decide to indicate that they know a card without turning it.
        ///     Only compatible with "repetition" mode.
        /// </summary>
        [JsonProperty("quickProgression")]
        public bool QuickProgression { get; set; }
    }

    public class ConfirmStartingOver
    {
        [JsonProperty("header")] public string Header { get; set; } = "Start over?";

        [JsonProperty("body")]
        public string Body { get; set; } = "All progress will be lost. Are you sure you want to start over?";

        [JsonProperty("cancelLabel")] public string CancelLabel { get; set; } = "Cancel";

        [JsonProperty("confirmLabel")] public string ConfirmLabel { get; set; } = "Start over";
    }
}