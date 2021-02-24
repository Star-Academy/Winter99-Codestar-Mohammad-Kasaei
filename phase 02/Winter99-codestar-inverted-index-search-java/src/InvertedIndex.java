import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class InvertedIndex {
    /**
     * The whole inverted index structure to save all the docs related to one
     * token and search quickly
     */
    private final Map<Token, Set<Document>> data;

    public InvertedIndex() {
        data = new HashMap<>();
    }


    /**
     * Add one document to the inverted index
     *
     * @param doc     documents to add to inverted index
     * @param content content of the documented to be added
     */
    public void addDocument(Document doc, String content) {
        Token[] tokens = Token.textToTokens(content);
        for (Token t : tokens) {
            if (!data.containsKey(t)) {
                data.put(t, new HashSet<>());
            }
            data.get(t).add(doc);
        }
    }

    /**
     * Add multiple docs to the inverted index
     *
     * @param docs documents to add to inverted index
     */
    public void addDocuments(Map<Document, String> docs) {
        for (Map.Entry<Document, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
    }

    /**
     * Query one token in whole documents
     *
     * @param token a token to search the docs
     * @return returns the set of all documents containing the token
     */
    public Set<Document> query(Token token) {
        return data.getOrDefault(token, new HashSet<>());
    }
}
