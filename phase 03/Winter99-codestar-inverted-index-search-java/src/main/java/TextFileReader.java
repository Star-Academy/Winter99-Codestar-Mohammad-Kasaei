import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class TextFileReader {

    final String path;

    public TextFileReader(String path) {
        this.path = path;
    }

    public Map<Document, String> readAllFileInFolder() {
        final Map<Document, String> result = new HashMap<>();
        for (File f : listOfFileInFolder()) {
            result.put(new Document(f.getName()), readTextFile(f));
        }
        return result;
    }

    private File[] listOfFileInFolder() {
        final File folder = new File(path);
        return folder.listFiles();
    }

    private String readTextFile(File file) {
        final FileReader fileReader;
        BufferedReader reader = null;
        try {
            fileReader = new FileReader(file);
            reader = new BufferedReader(fileReader);
            String line;
            final StringBuilder sb = new StringBuilder();
            while ((line = reader.readLine()) != null) {
                sb.append(line).append('\n');
            }
            return sb.toString();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            if (reader != null) {
                try {
                    reader.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        return "";
    }
}
