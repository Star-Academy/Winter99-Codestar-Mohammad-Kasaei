import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotEquals;

public class TokenTest {

    private static final String tokenString = "My_TOKEN:)";
    private static final String expectedTokenString = "my_token";
    private static final Token token = new Token(tokenString);

    @Test
    public void equalityNull() {
        assertNotEquals(null, token);
    }

    @SuppressWarnings("AssertBetweenInconvertibleTypes")
    @Test
    public void equalityString() {
        assertNotEquals("MyToken", token);
    }

    @Test
    public void equalityNewObject() {
        assertEquals(token, new Token(tokenString));
    }


    @Test
    public void hashCodeNewObjectSimilar() {
        assertEquals(new Token(expectedTokenString).hashCode(), token.hashCode());
    }

    @Test
    public void testToString() {
        assertEquals(expectedTokenString, token.toString());
    }
}