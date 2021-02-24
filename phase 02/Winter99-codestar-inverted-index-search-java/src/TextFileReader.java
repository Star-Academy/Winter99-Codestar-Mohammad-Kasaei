import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class TextFileReader {

    /**
     * @param folderPath source folder
     * @return a map object that connects every document with raw string of content
     */
    public Map<Document, String> readAllFileInFolder(String folderPath) {
        Map<Document, String> result = new HashMap<>();
        for (File f : listOfFileInFolder(folderPath)) {
            result.put(new Document(f.getPath(), f.getName()), readTextFile(f));
        }
        return result;
    }

    /**
     * @param folderPath source folder
     * @return all files in that folder
     */
    private File[] listOfFileInFolder(String folderPath) {
        File folder = new File(folderPath);
        return folder.listFiles();
    }

    /**
     * Reads any text file and returns its content as string
     *
     * @param file source file to read its content
     * @return string of raw content of the file
     */
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
