import java.util.Map;

public class Main {

    public static void main(String[] args) {
        final QueryEngine queryEngine = new QueryEngine(initializeInvertedIndex());
        final CommandLine cmd = new CommandLine(
                queryEngine::advancedSearch,
                CommandLine.DEFAULT_TERMINATOR,
                System.in,
                System.out);
        cmd.start();
    }

    private static InvertedIndex initializeInvertedIndex() {
        final String path = "EnglishData";
        final InvertedIndex invertedIndex = new InvertedIndex();
        final TextFileReader fileReader = new TextFileReader(path);
        final Map<Document, String> docs = fileReader.readAllFileInFolder();
        invertedIndex.addDocuments(docs);
        return invertedIndex;
    }
}
