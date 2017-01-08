namespace ScrabbleEngine
{
    public interface IServer
    {
        // event LogWasUpdatedEvent LogWasUpdated;
        // event GameBegunEvent GameBegun;
        // event MoveWasMadeEvent MoveWasMade;

        void AddPlayer(string name, int code, EventProxy proxy);
        void PerformMove(Move move);
    }
}
