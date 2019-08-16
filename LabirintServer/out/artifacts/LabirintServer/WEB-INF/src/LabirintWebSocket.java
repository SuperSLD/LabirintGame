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

    private static ArrayList<Session> sessions = new ArrayList<>();
    private int USER_ID = new Random().nextInt(Integer.MAX_VALUE);
    private static int SEED = new Random().nextInt(Integer.MAX_VALUE);

    private static ArrayList<Dot> flags = new ArrayList<>();
    private static ArrayList<Dot> flagbox = new ArrayList<>();

    @OnOpen
    public void onOpen(Session session) {
        sessions.add(session);
        log.add("<b>new user connect</b> " + USER_ID);
        System.out.println("new user connect " + USER_ID);

    }
    @OnClose
    public void onClose(Session session) {
        log.add("<b>user disconnect</b> " + USER_ID);
        System.out.println("user disconnect " + USER_ID);
        sessions.remove(session);
    }

    @OnMessage
    public void onMessage(String message, Session session) {
        try {
            String[] mes = message.split("<!>");
            if (mes[0].equals("getseed")) {
                session.getBasicRemote().sendText("fff&0");
                session.getBasicRemote().sendText("seed&" + Integer.toString(SEED));
                System.out.println("send seed: " + SEED);
                log.add("send seed: " + SEED);
            } else if (mes[0].equals("sendxyn")) {
                for (Session sess : sessions) {
                    if (!sess.equals(session))
                        sess.getBasicRemote().sendText("xyn&"+ mes[1] +
                                "&" + mes[2] + "&" + mes[3] + "&" + USER_ID);
                }
            } else if (mes[0].equals("sendflag")) {
                log.add("<b>send flag</b> " + message.replaceAll("<", "/")
                        .replaceAll(">", "/"));
                System.out.println("send flag : "  + message);
                for (Session sess : sessions) {
                    if (!sess.equals(session))
                        sess.getBasicRemote().sendText("addflag&"+ mes[1] +
                                "&" + mes[2]);
                }
                flags.add(new Dot(Integer.parseInt(mes[1]), Integer.parseInt(mes[2])));
            } else if (mes[0].equals("sendflagbox")) {
                for (Session sess : sessions) {
                    if (!sess.equals(session))
                        sess.getBasicRemote().sendText("deleteflagbox&"+ mes[1] +
                                "&" + mes[2]);
                }
                flags.add(new Dot(Integer.parseInt(mes[1]), Integer.parseInt(mes[2])));
            } else if (mes[0].equals("sendobjectinfo")) {
                for (Dot dot : flags) {
                    session.getBasicRemote().sendText("addflag&"+ dot.x +
                            "&" + dot.y);
                }
                for (Dot dot : flagbox) {
                    session.getBasicRemote().sendText("addflag&"+ dot.x +
                            "&" + dot.y);
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

class Dot {
    public int x;
    public int y;

    public Dot(int x, int y) {
       this.x = x;
       this.y = y;
    }
}
