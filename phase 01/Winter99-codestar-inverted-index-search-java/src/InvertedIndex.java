import java.util.*;

public class InvertedIndex {
    private final Tokenizer tokenizer;
    private final Map<String, Set<String>> data;

    public InvertedIndex() {
        tokenizer = new Tokenizer();
        data = new HashMap<>();
    }

    public void addDocument(String docID, String content) {
        String[] tokens = tokenizer.getTokens(content);
        for (String tokenValue : tokens) {
            if (!data.containsKey(tokenValue)) {
                data.put(tokenValue, new HashSet<>());
            }
            data.get(tokenValue).add(docID);
        }
    }

    public void addDocuments(Map<String, String> docs) {
        for (Map.Entry<String, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
    }

    public Set<String> query(String word) {
        word = tokenizer.normalizeToken(word);
        return data.get(word);
    }
}
