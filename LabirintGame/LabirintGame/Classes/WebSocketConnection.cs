using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabirintGame.Classes {

    class WebSocketConnection {
        public const string SERVER_WS_URI = "ws://192.168.1.55:8080/labirint";

        private static ClientWebSocket socket;
        private static CancellationTokenSource cts;

        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        public static void Connect() {
            socket = new ClientWebSocket();
            cts = new CancellationTokenSource();

            socket.ConnectAsync(new Uri(SERVER_WS_URI), cts.Token);
        }

        /// <summary>
        /// Закрытие подключения.
        /// </summary>
        public static void Close() {
            socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cts.Token);
        }
    }
}
