import java.util.Locale;

public class Tokenizer {
    private static final String INVALID_TOKENS_REGEX = "\\W+";

    public static String normalizeString(String rawString) {
        return rawString
                .toLowerCase(Locale.ROOT)
                .replaceAll(INVALID_TOKENS_REGEX, "");
    }

    public static Token[] contentToTokens(String content) {
        content = content.toLowerCase(Locale.ROOT);
        String[] words = content.split(INVALID_TOKENS_REGEX);
        Token[] tokens = new Token[words.length];
        for (int i = 0; i < words.length; i++) {
            tokens[i] = new Token(words[i]);
        }
        return tokens;
    }
}
