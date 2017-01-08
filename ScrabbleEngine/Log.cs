using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ScrabbleEngine
{
    public sealed class Log
    {
        private List<string> messages;

        internal Log()
        {
            messages = new List<string>();
            WriteGameHadBegun();
        }

        internal ReadOnlyCollection<string> GetMessages
        {
            get
            {
                return new ReadOnlyCollection<string>(messages);
            }
        }

        public string GetLastMessage()
        {
            return messages[messages.Count - 1];
        }

        internal void WriteGameOverWithWinner(String playerName)
        {
            messages.Add(String.Format(Settings.logGameOverWithWinnerMessageFormat, playerName));
        }

        internal void WriteNoneActionMove(String playerName)
        {
            messages.Add(String.Format(Settings.logNoneActionMoveMessageFormat, playerName));
        }

        internal void WriteExchangeActionMove(String playerName, int numberOfExchangedTiles)
        {
            messages.Add(String.Format(Settings.logExchangeActionMoveMessageFormat, playerName, numberOfExchangedTiles));
        }

        internal void WriteLayActionMove(String playerName, List<Word> words, int points)
        {
            StringBuilder wordsMessage = new StringBuilder();
            foreach (Word newWord in words)
            {
                wordsMessage.Append(newWord.ToString() + Settings.separator);
            }
            messages.Add(String.Format(Settings.logLayActionMoveMessageFormat, playerName, points, wordsMessage.ToString()));
        }

        internal void WriteMoveFailure(String playerName, String failureMessageFormat)
        {
            messages.Add(String.Format(failureMessageFormat, playerName));
        }

        internal void WriteGameHadBegun()
        {
            messages.Add(Settings.logGameHadBegun);
        }
    }
}
