import java.util.Map;

public class Main {
    public static void main(String[] args) {
//        final String path = "EnglishData";
        final String path = "SimpleData";

        TextFileReader reader = new TextFileReader();
        Tokenizer tokenizer = new Tokenizer();
        InvertedIndex invertedIndex = new InvertedIndex(tokenizer);

        Map<String, String> docs = reader.readAllFileInFolder(path);
        invertedIndex.addDocuments(docs);

        QueryEngine queryEngine = new QueryEngine(invertedIndex);

        System.out.println("Simple word is in : " + queryEngine.query("-two"));
    }
}