namespace ScrabbleClient
{
    internal static class Settings
    {
        internal const string clientURI = "client.Rem";
        internal const int defaultServerPort = 4523;
        internal const int portLowerBound = 2000;
        internal const int portUpperBound = 50000;
        internal const int nameLengthLowerBound = 2;
        internal const int nameLengthUpperBound = 10;
        internal const int squareGraphicSize = 30;
        internal const int squareGapGraphicSize = 1;

        internal const string portKeyword = "port";
        internal const string nameKeyword = "name";
        internal const string tcpProtocolString = "tcp://";
        internal const string emptyString = "";
        internal const char spaceChar = ' ';
        internal const char columnChar = ':';
        internal const string newLineString = "\r\n";
        internal const char ipPortUriSeparator = ':';
        internal const string serverURITail = "/server.Rem";

        internal const string scrabbleClientString = "Scrabble Client";
        internal const string connectedAndWaitingString = ": Connected, waiting for game to begin!";
        internal const string invalidNameMessage = "Your name must be 2 - 10 letters long!";
        internal const string invalidIPMessage = "Invalid IP adress provided!";
        internal const string invalidPortMessage = "Invalid port provided. It must be from interval 2000 - 50000!";
    }
}
