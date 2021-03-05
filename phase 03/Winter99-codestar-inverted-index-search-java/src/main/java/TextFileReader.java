import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;

public class TextFileReader {

    final String path;

    public TextFileReader(String path) {
        this.path = path;
    }

    public Map<Document, String> readAllFileInFolder() {
        final Map<Document, String> result = new HashMap<>();
        if (pathExists()) {
            for (File f : listOfFileInFolder()) {
                result.put(new Document(f.getName()), readTextFile(f));
            }
        }
        return result;
    }

    private boolean pathExists() {
        return new File(path).exists();
    }

    private File[] listOfFileInFolder() {
        final File folder = new File(path);
        return folder.listFiles();
    }

    public static String readTextFile(File file) {
        try (Scanner scanner = new Scanner(file)) {
            final StringBuilder sb = new StringBuilder();
            while (scanner.hasNextLine()) {
                if (sb.length() > 0) {
                    sb.append('\n');
                }
                sb.append(scanner.nextLine());
            }
            return sb.toString();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return "";
    }
}
