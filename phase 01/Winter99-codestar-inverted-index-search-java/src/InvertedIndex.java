import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class InvertedIndex {
    private final Tokenizer tokenizer;
    private final Map<String, Set<String>> data;

    public InvertedIndex(Tokenizer tokenizer) {
        this.tokenizer = tokenizer;
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

    public Set<String> query(String str) {
        return data.getOrDefault(tokenizer.normalizeToken(str), new HashSet<>());
    }
}
