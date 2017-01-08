using System.Collections.ObjectModel;

namespace ScrabbleEngine
{
    public delegate void CommunicationIdWasSetEvent(int id);
    public delegate void GameBegunEvent();
    public delegate void LogWasUpdatedEvent(string message);
    public delegate void MoveWasMadeEvent(ReadOnlyCollection<Player> players, Board board, bool gameIsTerminated);    
}
