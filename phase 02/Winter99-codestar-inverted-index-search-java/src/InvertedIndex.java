import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class InvertedIndex {
    private final Map<Token, Set<String>> data;

    public InvertedIndex() {
        data = new HashMap<>();
    }

    public void addDocument(String docID, String content) {
        Token[] tokens = Token.textToTokens(content);
        for (Token t : tokens) {
            if (!data.containsKey(t)) {
                data.put(t, new HashSet<>());
            }
            data.get(t).add(docID);
        }
    }

    public void addDocuments(Map<String, String> docs) {
        for (Map.Entry<String, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
    }

    public Set<String> query(Token token) {
        return data.getOrDefault(token, new HashSet<>());
    }
}
