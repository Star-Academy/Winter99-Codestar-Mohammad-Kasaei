import java.util.Map;

public class Main {

    public static void main(String[] args) {
        final String path = "EnglishData";
        final InvertedIndex invertedIndex = new InvertedIndex();
        final Map<Document, String> docs = TextFileReader.readAllFileInFolder(path);
        invertedIndex.addDocuments(docs);
        QueryEngine queryEngine = new QueryEngine(invertedIndex);
        final CommandLine cmd = new CommandLine(queryEngine::advancedSearch);
        cmd.start();
    }
}
