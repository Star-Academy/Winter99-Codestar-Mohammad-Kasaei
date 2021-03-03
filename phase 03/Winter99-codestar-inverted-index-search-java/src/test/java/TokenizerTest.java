import org.junit.Test;

import static org.junit.Assert.assertArrayEquals;
import static org.junit.Assert.assertEquals;

public class TokenizerTest {
    @Test
    public void normalizeStringAllInvalid() {
        assertEquals("", Tokenizer.normalizeString("---"));
    }

    @Test
    public void normalizeStringMixCaseString() {
        assertEquals("lowercaseme", Tokenizer.normalizeString("LowerCaseMe"));
    }

    @Test
    public void normalizeStringMixInvalidChars() {
        assertEquals("remove_not_alpha_numeric_chars_", Tokenizer.normalizeString("remove_not_alpha?_numeric_chars^&*()_+"));
    }

    @Test
    public void contentToTokensEmptyString() {
        final Token[] tokens = Tokenizer.contentToTokens("");
        assertArrayEquals(new Token[0], tokens);
    }

    @Test
    public void contentToTokens1() {
        final Token[] tokens = Tokenizer.contentToTokens("Hello I am mohammad");
        final Token[] expected = new Token[]{
                new Token("hello"),
                new Token("i"),
                new Token("am"),
                new Token("mohammad")
        };
        assertArrayEquals(expected, tokens);
    }

    @Test
    public void contentToTokens2() {
        final Token[] tokens = Tokenizer.contentToTokens("Hello)(((* I^ am mohammad??))(((****");
        final Token[] expected = new Token[]{
                new Token("hello"),
                new Token("i"),
                new Token("am"),
                new Token("mohammad"),
        };
        assertArrayEquals(expected, tokens);
    }
}