import java.util.*;

public class InvertedIndex {
    private final Tokenizer tokenizer;
    private final Map<String, Map<String, Set<Integer>>> data;

    public InvertedIndex() {
        tokenizer = new Tokenizer();
        data = new HashMap<>();
    }

    public void addDocument(String docID, String content) {
        Map<Integer, String> tokens = tokenizer.getTokens(content);
        for (Map.Entry<Integer, String> tokenPair : tokens.entrySet()) {
            final int index = tokenPair.getKey();
            final String tokenValue = tokenPair.getValue();
            if (!data.containsKey(tokenValue)) {
                data.put(tokenValue, new TreeMap<>());
            }
            Map<String, Set<Integer>> tokenObject = data.get(tokenValue);
            if (!tokenObject.containsKey(docID)) {
                tokenObject.put(docID, new TreeSet<>());
            }
            tokenObject.get(docID).add(index);
        }
    }

    public void addDocuments(Map<String, String> docs) {
        for (Map.Entry<String, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
        System.out.println(data);
    }

    public Map<String, Set<Integer>> query(String q) {
        q = tokenizer.normalizeToken(q);
        return data.get(q);
    }
}
