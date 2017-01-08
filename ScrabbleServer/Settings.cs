namespace ScrabbleServer
{
    internal static class Settings
    {
        internal const int maxPlayersCount = 4;
        internal const int defaultServerPort = 4523;
        internal const string serverURI = "server.Rem";

        internal const string portKeyword = "port";
        internal const string nameKeyword = "name";

        internal const string startCommandKeyword = "start";
        internal const string beginCommandKeyword = "begin";
        internal const string stopCommandKeyword = "stop";

        internal const string welcomeMessage = "SCRABBLE SERVER BY JAKUB KUDELA (C) 2011";
        internal const string portRequestMessage = "Please enter the port number you would like to use ('0' for default port):";
        internal const string commandRequestMessage = "Enter command:";
        internal const string serverStartedMessage = "Server started!";
        internal const string serverStoppedMessage = "Server stopped!";
        internal const string playerConnectedMessageFormat = "Player connected: {0}.";
        internal const string nonePlayersConnectedMessage = "None players connected so far!";
    }
}
