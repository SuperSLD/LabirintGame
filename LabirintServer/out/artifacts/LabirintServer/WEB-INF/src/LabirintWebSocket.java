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

    public static Thread testSend = new Thread(new Runnable() {
        @Override
        public void run() {
            while (true) {
                try {
                    for (Session sess : sessions) {
                        int l = 400;
                        sess.getBasicRemote().sendText("xyn&"+ (1000 * 2) +
                                "&" + (1000 * 399) + "&" + 4 + "&" + 0);
                        Thread.sleep(1000);
                    }
                } catch (Exception e) {
                    //e.printStackTrace();
                }
            }
        }
    });

    private static ArrayList<Session> sessions = new ArrayList<>();
    private int USER_ID = new Random().nextInt(Integer.MAX_VALUE);
    private static int SEED = new Random().nextInt(Integer.MAX_VALUE);

    @OnOpen
    public void onOpen(Session session) {
        sessions.add(session);
        log.add("<b>new user connect</b> " + USER_ID);
        session.setMaxIdleTimeout(1000*60*60*24);
        System.out.println("new user connect " + USER_ID);

        if (!testSend.isAlive()) testSend.start();
    }
    @OnClose
    public void onClose(Session session) {
        log.add("<b>user disconnect</b> " + USER_ID);
        System.out.println("user disconnect " + USER_ID);
        sessions.remove(session);
    }

    @OnMessage
    public void onMessage(String message, Session session) {
        System.out.println(message);
        log.add("<b>message</b> " + message);
        try {
            String[] mes = message.split("<!>");
            if (mes[0].equals("getseed")) {
                session.getBasicRemote().sendText("seed&" + Integer.toString(SEED));
            } else if (mes[0].equals("sendxyn")) {
                for (Session sess : sessions) {
                    if (!sess.equals(session))
                        sess.getBasicRemote().sendText("xyn&"+ mes[1] +
                                "&" + mes[2] + "&" + mes[3] + "&" + USER_ID);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    @OnError
    public void onError(Throwable t) {

    }
}
