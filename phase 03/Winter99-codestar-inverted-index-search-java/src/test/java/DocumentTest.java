import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotEquals;

public class DocumentTest {

    private final String docID1 = "Document ID 1";
    private final String docID2 = "Document ID 2";
    private final Document doc1 = new Document(docID1);
    private final Document doc1Copy = new Document(docID1);
    private final Document doc2 = new Document(docID2);
    private final Document doc2Copy = new Document(docID2);

    @Test
    public void getId() {
        assertEquals(docID1, doc1.getId());
    }

    @SuppressWarnings("AssertBetweenInconvertibleTypes")
    @Test
    public void testEquals() {
        assertNotEquals(null, doc1);
        assertNotEquals(docID1, doc1);
        assertNotEquals(doc1.toString(), doc1);
        assertEquals(doc1, doc1);
        assertEquals(new Document(docID1), doc1);
        assertNotEquals(doc1, doc2);
    }

    @Test
    public void testHashCode() {
        assertEquals(doc1.hashCode(), doc1Copy.hashCode());
        assertEquals(doc2.hashCode(), doc2Copy.hashCode());
        assertNotEquals(doc1.hashCode(), doc2.hashCode());
    }

    @Test
    public void compareTo() {
        assertEquals(doc1.compareTo(doc2), docID1.compareTo(docID2));
        assertEquals(doc2.compareTo(doc1), docID2.compareTo(docID1));
    }
}