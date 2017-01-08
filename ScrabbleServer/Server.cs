using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using ScrabbleEngine;

namespace ScrabbleServer
{
    public sealed class Server : MarshalByRefObject, IServer
    {
        private ObjRef internalRef;
        private readonly int port;
        private readonly TcpServerChannel serverChannel;
        private readonly Hashtable serverProperties = new Hashtable();
        private readonly List<Player> players;
        private bool isWaitingForPlayers;
        private Game game;

        private event LogWasUpdatedEvent LogWasUpdated;
        private event GameBegunEvent GameBegun;
        private event MoveWasMadeEvent MoveWasMade;

        public Server()
        {
            port = Settings.defaultServerPort;
            serverProperties[Settings.portKeyword] = port;
            serverProperties[Settings.nameKeyword] = Settings.serverURI;
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            serverChannel = new TcpServerChannel(serverProperties, serverProv);
            players = new List<Player>();
            isWaitingForPlayers = false;
        }

        public Server(int port)
        {
            this.port = port;
            serverProperties = new Hashtable(); ;
            serverProperties[Settings.portKeyword] = this.port;
            serverProperties[Settings.nameKeyword] = Settings.serverURI;
            BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
            serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            serverChannel = new TcpServerChannel(serverProperties, serverProv);
            players = new List<Player>();
            isWaitingForPlayers = false;
        }
        
        public void AddPlayer(string name, int code, EventProxy proxy)
        {
            if (!isWaitingForPlayers || players.Count > Settings.maxPlayersCount)
            {
                return;    
            }

            int playerId = players.Count;
            players.Add(new Player(playerId, name, code));
            proxy.SetCommunicationId(playerId);
            LogWasUpdated += proxy.UpdateLog;
            GameBegun += proxy.BeginGame;
            MoveWasMade += proxy.UpdateAfterMove;

            Console.WriteLine(String.Format(Settings.playerConnectedMessageFormat, name));
        }

        public void PerformMove(Move move)
        {
            bool wasMoveValid = game.PerformMove(move);
            UpdateClientLogs();
            if (wasMoveValid)
            {
                UpdateClientGameInformation();
            }
        }
        
        internal void Start()
        {
            if (isWaitingForPlayers)
            {
                return;
            }

            ChannelServices.RegisterChannel(serverChannel, false);
            internalRef = RemotingServices.Marshal(this, serverProperties["name"].ToString());
            isWaitingForPlayers = true;
            Console.WriteLine(Settings.serverStartedMessage);
        }

        internal void Stop()
        {
            if (!isWaitingForPlayers)
            {
                return;
            }

            RemotingServices.Unmarshal(internalRef);
            ChannelServices.UnregisterChannel(serverChannel);

            Console.WriteLine(Settings.serverStoppedMessage);
        }

        internal void BeginGame()
        {
            if (players.Count == 0)
            {
                Console.WriteLine(Settings.nonePlayersConnectedMessage);
                return;
            }

            isWaitingForPlayers = false;
            game = new Game(players);

            Delegate[] dels = GameBegun.GetInvocationList();
            foreach (Delegate del in dels)
            {
                ((GameBegunEvent)del).Invoke();
            }

            UpdateClientLogs();
            UpdateClientGameInformation();
        }

        private void UpdateClientLogs()
        {
            Delegate[] dels = LogWasUpdated.GetInvocationList();
            foreach (Delegate del in dels)
            {
                ((LogWasUpdatedEvent)del).Invoke(game.Log.GetLastMessage());
            }
        }

        private void UpdateClientGameInformation()
        {
            Delegate[] dels = MoveWasMade.GetInvocationList();
            foreach (Delegate del in dels)
            {
                ((MoveWasMadeEvent)del).Invoke(new ReadOnlyCollection<Player>(game.Players), game.Board, game.IsTerminated);
            }
        }
    }
}
