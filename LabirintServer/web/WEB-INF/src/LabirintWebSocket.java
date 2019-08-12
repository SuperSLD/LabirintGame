import com.sun.prism.paint.Color;

import javax.websocket.*;
import javax.websocket.server.ServerEndpoint;
import javax.xml.crypto.Data;
import java.io.IOException;
import java.util.ArrayList;

@ServerEndpoint("/labirint")
public class LabirintWebSocket {
    public static ArrayList<String> log = new ArrayList<>();

    private int USER_ID;

    @OnOpen
    public void onOpen(Session session) {
        log.add("new user connect");
        session.setMaxIdleTimeout(1000*60*60*24);
        System.out.println("new user connect");
    }
    @OnClose
    public void onClose(Session session) {
        log.add("user disconnect");
    }

    @OnMessage
    public void onMessage(String message, Session session) {
        System.out.println(message);
        log.add(message);
    }

    @OnError
    public void onError(Throwable t) {

    }
}
