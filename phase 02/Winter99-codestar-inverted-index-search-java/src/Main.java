import java.util.Map;

public class Main {

    private static QueryEngine queryEngine;
    private static final String path = "EnglishData";

    public static void main(String[] args) {
        final TextFileReader reader = new TextFileReader();
        final InvertedIndex invertedIndex = new InvertedIndex();
        final Map<Document, String> docs = reader.readAllFileInFolder(path);
        invertedIndex.addDocuments(docs);
        queryEngine = new QueryEngine(invertedIndex);
        final CommandLine cmd = new CommandLine(
                words -> queryEngine.advancedSearch(words)
        );
        cmd.start();
    }
}
