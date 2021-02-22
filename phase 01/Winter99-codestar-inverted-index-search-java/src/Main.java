import java.util.Map;

public class Main {
    public static void main(String[] args) {
//        final String path = "EnglishData";
        final String path = "SimpleData";

        TextFileReader reader = new TextFileReader();
        Map<String, String> docs = reader.readAllFileInFolder(path);
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.addDocuments(docs);
    }
}