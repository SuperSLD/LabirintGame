import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class ServletLog extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        String html = "";
        for (String string : LabirintWebSocket.log) {
            html += string + "<br>";
        }

        req.setAttribute("log", html);
        req.getRequestDispatcher("index.jsp").forward(req, resp);
    }
}
