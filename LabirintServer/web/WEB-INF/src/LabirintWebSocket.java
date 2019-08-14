import com.sun.prism.paint.Color;

import javax.websocket.*;
import javax.websocket.server.ServerEndpoint;
import javax.xml.crypto.Data;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Random;

@ServerEndpoint("/labirint")
public class LabirintWebSocket {
    public static ArrayList<String> log = new ArrayList<>();

    private int USER_ID = new Random().nextInt(Integer.MAX_VALUE);
    private static int SEED = new Random().nextInt(Integer.MAX_VALUE);

    @OnOpen
    public void onOpen(Session session) {
        log.add("<b>new user connect</b> " + USER_ID);
        session.setMaxIdleTimeout(1000*60*60*24);
        System.out.println("new user connect " + USER_ID);
    }
    @OnClose
    public void onClose(Session session) {
        log.add("<b>user disconnect</b> " + USER_ID);
        System.out.println("user disconnect " + USER_ID);
    }

    @OnMessage
    public void onMessage(String message, Session session) {
        System.out.println(message);
        log.add("<b>message</b> " + message);
        log.add(message);
    }

    @OnError
    public void onError(Throwable t) {

    }
}
