import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class TextFileReader {

    public static Map<Document, String> readAllFileInFolder(String folderPath) {
        final Map<Document, String> result = new HashMap<>();
        for (File f : listOfFileInFolder(folderPath)) {
            result.put(new Document(f.getName()), readTextFile(f));
        }
        return result;
    }

    private static File[] listOfFileInFolder(String folderPath) {
        final File folder = new File(folderPath);
        return folder.listFiles();
    }

    public static String readTextFile(File file) {
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
