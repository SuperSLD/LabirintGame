using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabirintGame.Classes {

    class WebSocketConnection {
        //public const string SERVER_WS_URI = "ws://192.168.1.55:8080/labirint";
        //public const string SERVER_WS_URI = "ws://localhost:8080/labirint";
        public const string SERVER_WS_URI = "ws://10.6.193.20:8080/labirint";

        private static ClientWebSocket socket;
        private static CancellationToken token;

        /// <summary>
        /// Подключение к серверу.
        /// </summary>
        public static void Connect() {
            socket = new ClientWebSocket();
            token = new CancellationToken();

            socket.ConnectAsync(new Uri(SERVER_WS_URI), token);
        }

        /// <summary>
        /// Отправка строки на сервер.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task SendString(String data) {
            if (socket.State == WebSocketState.Open) {
                var encoded = Encoding.UTF8.GetBytes(data);
                var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);
                return socket.SendAsync(buffer, WebSocketMessageType.Text, true, new CancellationToken());
            } else {
                return null;
            }
        }

        /// <summary>
        /// Получение строки с сервера.
        /// </summary>
        /// <returns></returns>
        public static async Task<string> ReceiveMessage() {
            if (socket.State == WebSocketState.Open) {
                byte[] buffer = new byte[1024 * 2];
                var segment = new ArraySegment<byte>(buffer);
                var message = new StringBuilder();
                WebSocketReceiveResult result;
                do {
                    result = await socket.ReceiveAsync(segment, token).ConfigureAwait(false);
                    message.Append(Encoding.UTF8.GetString(segment.Array, 0, result.Count));
                } while (!token.IsCancellationRequested && !result.EndOfMessage);
                return message.ToString();
            } else {
                return null;
            }
        }

        /// <summary>
        /// Закрытие подключения.
        /// </summary>
        public static void Close() {
            try {
                socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, token);
            } catch (Exception ex) { }
        }
    }
}
