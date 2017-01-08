using System;
using System.Collections.ObjectModel;

namespace ScrabbleEngine
{
    public class EventProxy : MarshalByRefObject
    {
        public event CommunicationIdWasSetEvent communicationIdWasSet;
        public event GameBegunEvent gameBegun;
        public event LogWasUpdatedEvent logWasUpdated;
        public event MoveWasMadeEvent moveWasMade;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public void SetCommunicationId(int id)
        {
            if (communicationIdWasSet != null)
            {
                communicationIdWasSet(id);
            }
        }

        public void BeginGame()
        {
            if (logWasUpdated != null)
            {
                gameBegun();
            }
        }

        public void UpdateLog(string message)
        {
            if (logWasUpdated != null)
            {
                logWasUpdated(message);
            }
        } 

        public void UpdateAfterMove(ReadOnlyCollection<Player> players, Board board, bool gameIsTerminated)
        {
            if (moveWasMade != null)
            {
                moveWasMade(players, board, gameIsTerminated);
            }   
        }
    }
}
