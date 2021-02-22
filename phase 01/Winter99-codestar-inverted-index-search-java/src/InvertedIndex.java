import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import java.util.TreeSet;

public class InvertedIndex {
    private final Tokenizer tokenizer;
    private final Map<String, Map<String, Set<Integer>>> data;

    public InvertedIndex() {
        tokenizer = new Tokenizer();
        data = new HashMap<>();
    }

    public void addDocument(String docID, String content) {
        Map<Integer, String> tokens = tokenizer.getTokens(content);
        if (tokens.size() > 0 && !this.data.containsKey(docID)) {
            data.put(docID, new HashMap<>());
        }
        Map<String, Set<Integer>> docRecord = data.get(docID);
        for (Map.Entry<Integer, String> tokenPair : tokens.entrySet()) {
            int index = tokenPair.getKey();
            String tokenValue = tokenPair.getValue();
            if (!docRecord.containsKey(tokenValue)) {
                docRecord.put(tokenValue, new TreeSet<>());
            }
            docRecord.get(tokenValue).add(index);
        }
    }

    public void addDocuments(Map<String, String> docs) {
        for (Map.Entry<String, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
        System.out.println(data);
    }
}
