import java.util.Locale;
import java.util.Objects;

public class Token {
    private static final String INVALID_TOKENS_REGEX = "\\W+";

    /**
     * The main string of token
     */
    private final String value;

    /**
     * Create the token and validates the characters
     *
     * @param value source string to create the token based on
     */
    public Token(String value) {
        value = value.toLowerCase(Locale.ROOT);
        value = value.replaceAll(INVALID_TOKENS_REGEX, "");
        this.value = value;
    }

    /**
     * @param content everything in the document to be converted to tokens
     * @return array of tokens in the {@code content}
     */
    public static Token[] textToTokens(String content) {
        content = normalizeContent(content);
        String[] words = content.split(INVALID_TOKENS_REGEX);
        Token[] tokens = new Token[words.length];
        for (int i = 0; i < words.length; i++) {
            tokens[i] = new Token(words[i]);
        }
        return tokens;
    }

    /**
     * Converts all the file to lower case
     *
     * @param content everything in the file to be reverse indexed
     * @return normalized content of the file
     */
    private static String normalizeContent(String content) {
        return content.toLowerCase();
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Token token = (Token) o;
        return Objects.equals(value, token.value);
    }

    @Override
    public int hashCode() {
        return Objects.hash(value);
    }
}
