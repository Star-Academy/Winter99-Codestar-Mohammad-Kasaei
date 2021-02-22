import java.util.HashMap;
import java.util.Map;

public class Tokenizer {

    public Map<Integer, String> getTokens(String content) {
        String[] rawTokens = content.split("\\W+");
        Map<Integer, String> tokenIndexes = new HashMap<>();
        for (int i = 0; i < rawTokens.length; i++) {
            String normalized = normalizeToken(rawTokens[i]);
            tokenIndexes.put(i, normalized);
        }
        return tokenIndexes;
    }

    public String normalizeToken(String word) {
        word = word.toLowerCase();
        return word;
    }
}
