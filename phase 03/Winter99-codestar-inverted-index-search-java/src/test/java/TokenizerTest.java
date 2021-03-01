import org.junit.Test;

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
        Token[] tokens = Tokenizer.contentToTokens("");
        assertEquals(0, tokens.length);
    }

    @Test
    public void contentToTokens1() {
        Token[] tokens = Tokenizer.contentToTokens("Hello I am mohammad");
        assertEquals(4, tokens.length);
        assertEquals(tokens[0].toString(), "hello");
        assertEquals(tokens[1].toString(), "i");
        assertEquals(tokens[2].toString(), "am");
        assertEquals(tokens[3].toString(), "mohammad");
    }

    @Test
    public void contentToTokens2() {
        Token[] tokens = Tokenizer.contentToTokens("Hello)(((* I^ am mohammad??))(((****");
        assertEquals(4, tokens.length);
        assertEquals(tokens[0].toString(), "hello");
        assertEquals(tokens[1].toString(), "i");
        assertEquals(tokens[2].toString(), "am");
        assertEquals(tokens[3].toString(), "mohammad");
    }
}