import java.util.Objects;

public class Token {
    private final String value;

    /**
     * Create the token and validates the characters
     *
     * @param value source string to create the token based on
     */
    public Token(String value) {
        this.value = Tokenizer.normalizeString(value);
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

    @Override
    public String toString() {
        return value;
    }
}
