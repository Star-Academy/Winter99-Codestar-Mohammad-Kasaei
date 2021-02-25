import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class TextFileReader {

    public Map<Document, String> readAllFileInFolder(String folderPath) {
        Map<Document, String> result = new HashMap<>();
        for (File f : listOfFileInFolder(folderPath)) {
            result.put(new Document(f.getPath(), f.getName()), readTextFile(f));
        }
        return result;
    }

    private File[] listOfFileInFolder(String folderPath) {
        File folder = new File(folderPath);
        return folder.listFiles();
    }

    public String readTextFile(File file) {
        FileReader fileReader;
        BufferedReader reader = null;
        try {
            fileReader = new FileReader(file);
            reader = new BufferedReader(fileReader);
            String line;
            StringBuilder sb = new StringBuilder();
            while ((line = reader.readLine()) != null) {
                sb.append(line);
                sb.append("\n");
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
