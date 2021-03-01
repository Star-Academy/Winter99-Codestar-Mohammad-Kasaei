import java.util.Map;

public class Main {

    public static void main(String[] args) {
        final String path = "EnglishData";
        final InvertedIndex invertedIndex = new InvertedIndex();
        final TextFileReader fileReader = new TextFileReader(path);
        final Map<Document, String> docs = fileReader.readAllFileInFolder();
        invertedIndex.addDocuments(docs);
        final QueryEngine queryEngine = new QueryEngine(invertedIndex);
        final CommandLine cmd = new CommandLine(queryEngine::advancedSearch);
        cmd.start();
    }
}
