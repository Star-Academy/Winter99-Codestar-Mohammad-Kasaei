import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class InvertedIndex {
    private final Map<Token, Set<Document>> data;

    public InvertedIndex() {
        data = new HashMap<>();
    }

    public void addDocument(Document doc, String content) {
        Token[] tokens = Tokenizer.contentToTokens(content);
        for (Token t : tokens) {
            if (!data.containsKey(t)) {
                data.put(t, new HashSet<>());
            }
            data.get(t).add(doc);
        }
    }

    public void addDocuments(Map<Document, String> docs) {
        for (Map.Entry<Document, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
    }

    public Set<Document> search(Token token) {
        return data.getOrDefault(token, new HashSet<>());
    }
}
