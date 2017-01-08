namespace ScrabbleEngine
{
    internal static class Settings
    {
        internal const char separator = ' ';
        internal const string emptyString = "";

        internal const string logGameHadBegun = "Game had begun!";
        internal const string logNoneActionMoveMessageFormat = "{0}: passed the turn.";
        internal const string logExchangeActionMoveMessageFormat = "{0}: exchanged {1} tiles.";
        internal const string logLayActionMoveMessageFormat = "{0}: scored {1} point laying these words: {2}.";
        internal const string logGameOverWithWinnerMessageFormat = "Game Over: {0} won the game!";

        internal const string logMoveFailureNameMessageFormat = "{0}: failed to move (not on move).";
        internal const string logMoveFailureIdMessageFormat = "{0}: failed to move (invalid id).";
        internal const string logMoveFailureCodeMessageFormat = "{0}: failed to move (invalid code).";
        internal const string logMoveFailureSizeMessageFormat = "{0}: failed to move (invalid move size).";
        internal const string logMoveFailureActionMessageFormat = "{0}: failed to move (non-consistent action).";
        internal const string logMoveFailureFirstMovePositionMessageFormat = "{0}: failed to move (first word must be situated in center).";
        internal const string logMoveFailureNoChangingMessageFormat = "{0}: failed to move (changing tiles is no longer possible).";
        internal const string logMoveFailureLayoutMessageFormat = "{0}: failed to move (tiles laid on non-empty squares).";
        internal const string logMoveFailureOrientationMessageFormat = "{0}: failed to move (tiles not in a same row or column).";
        internal const string logMoveFailureMainWordIncontinuousMessageFormat = "{0}: failed to move (main word incontinuous).";
        internal const string logMoveFailureInvalidWordMessageFormat = "{0}: failed to move (invalid word).";

        internal const string dictionaryPathString = "\\SOWPODS.sdf";
        internal const string wordTableNameString = "Words";
        internal const string wordColumnNameString = "Word";

        internal const int boardSize = 15;
        internal const int boardCenterCoor = 7;
        internal const int handSize = 7;
        internal const int minimalWordLength = 2;
        internal const int winningPoints = 50;

        private const char capAChar = 'A';

        internal static readonly LetterInfo[] letterInfos = new LetterInfo[26]
        {
            new LetterInfo('A', 9, 1),
            new LetterInfo('B', 2, 3),
            new LetterInfo('C', 2, 3),
            new LetterInfo('D', 4, 2),
            new LetterInfo('E', 12, 1),
            new LetterInfo('F', 2, 4),
            new LetterInfo('G', 3, 2),
            new LetterInfo('H', 2, 4),
            new LetterInfo('I', 9, 1),
            new LetterInfo('J', 1, 8),
            new LetterInfo('K', 1, 5),
            new LetterInfo('L', 4, 1),
            new LetterInfo('M', 2, 3),
            new LetterInfo('N', 6, 1),
            new LetterInfo('O', 8, 1),
            new LetterInfo('P', 2, 3),
            new LetterInfo('Q', 1, 10),
            new LetterInfo('R', 6, 1),
            new LetterInfo('S', 4, 1),
            new LetterInfo('T', 6, 1),
            new LetterInfo('U', 4, 1),
            new LetterInfo('V', 2, 4),
            new LetterInfo('W', 2, 4),
            new LetterInfo('X', 1, 8),
            new LetterInfo('Y', 2, 4),
            new LetterInfo('Z', 1, 10),
        };
        internal static readonly MultiplierInfo[] letterMultiplierInfos = new MultiplierInfo[36]
        {
            new MultiplierInfo(3, 0, 2),
            new MultiplierInfo(11, 0, 2),
            new MultiplierInfo(5, 1, 3),
            new MultiplierInfo(9, 1, 3),
            new MultiplierInfo(6, 2, 2),
            new MultiplierInfo(8, 2, 2),
            new MultiplierInfo(0, 3, 2),
            new MultiplierInfo(7, 3, 2),
            new MultiplierInfo(14, 3, 2),
            new MultiplierInfo(1, 5, 3),
            new MultiplierInfo(5, 5, 3),
            new MultiplierInfo(9, 5, 3),
            new MultiplierInfo(13, 5, 3),
            new MultiplierInfo(2, 6, 2),
            new MultiplierInfo(6, 6, 2),
            new MultiplierInfo(8, 6, 2),
            new MultiplierInfo(12, 6, 2),
            new MultiplierInfo(3, 7, 2),
            new MultiplierInfo(11, 7, 2),
            new MultiplierInfo(2, 8, 2),
            new MultiplierInfo(6, 8, 2),
            new MultiplierInfo(8, 8, 2),
            new MultiplierInfo(12, 8, 2),
            new MultiplierInfo(1, 9, 3),
            new MultiplierInfo(5, 9, 3),
            new MultiplierInfo(9, 9, 3),
            new MultiplierInfo(13, 9, 3),
            new MultiplierInfo(0, 11, 2),
            new MultiplierInfo(7, 11, 2),
            new MultiplierInfo(14, 11, 2),
            new MultiplierInfo(6, 12, 2),
            new MultiplierInfo(8, 12, 2),
            new MultiplierInfo(5, 13, 3),
            new MultiplierInfo(9, 13, 3),
            new MultiplierInfo(3, 14, 2),
            new MultiplierInfo(11, 14, 2)
        };
        internal static readonly MultiplierInfo[] wordMultiplierInfos = new MultiplierInfo[25]
        {
            new MultiplierInfo(0, 0, 3),
            new MultiplierInfo(7, 0, 3),
            new MultiplierInfo(14, 0, 3),
            new MultiplierInfo(1, 1, 2),
            new MultiplierInfo(13, 1, 2),
            new MultiplierInfo(2, 2, 2),
            new MultiplierInfo(12, 2, 2),
            new MultiplierInfo(3, 3, 2),
            new MultiplierInfo(11, 3, 2),
            new MultiplierInfo(4, 4, 2),
            new MultiplierInfo(10, 4, 2),
            new MultiplierInfo(0, 7, 3),
            new MultiplierInfo(7, 7, 2),
            new MultiplierInfo(14, 7, 3),
            new MultiplierInfo(4, 10, 2),
            new MultiplierInfo(10, 10, 2),
            new MultiplierInfo(3, 11, 2),
            new MultiplierInfo(11, 11, 2),
            new MultiplierInfo(2, 12, 2),
            new MultiplierInfo(12, 12, 2),
            new MultiplierInfo(1, 13, 2),
            new MultiplierInfo(13, 13, 2),
            new MultiplierInfo(0, 14, 3),
            new MultiplierInfo(7, 14, 3),
            new MultiplierInfo(14, 14, 3),
        };

        internal static int ValueOfLetter(char letter)
        {
            return Settings.letterInfos[letter - Settings.capAChar].Value;
        }
    }
}
