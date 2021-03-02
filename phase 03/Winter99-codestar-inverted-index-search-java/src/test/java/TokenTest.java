import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotEquals;

public class TokenTest {

    @SuppressWarnings("AssertBetweenInconvertibleTypes")
    @Test
    public void testEquals() {
        final String tokenString = "My_TOKEN:)";
        final Token token = new Token(tokenString);
        assertNotEquals(null, token);
        assertNotEquals("MyToken", token);
        assertEquals(token, new Token(tokenString));
        assertEquals(token, new Token("my_token"));
    }

    @Test
    public void testHashCode() {
        final Token token = new Token("(((----??tokenMe:)");
        final Token token2 = new Token(token.toString());
        assertEquals(token2.hashCode(), token.hashCode());
    }

    @Test
    public void testToString() {
        final Token token = new Token("(((----??tokenMe:)");
        assertEquals("tokenme", token.toString());
    }
}