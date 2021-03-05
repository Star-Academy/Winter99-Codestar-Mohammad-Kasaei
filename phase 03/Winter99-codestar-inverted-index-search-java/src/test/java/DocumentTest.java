import org.junit.Test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertNotEquals;

public class DocumentTest {

    private final String docID1 = "Document ID 1";
    private final String docID2 = "Document ID 2";
    private final Document doc1 = new Document(docID1);
    private final Document doc2 = new Document(docID2);
    private final Document doc2Copy = new Document(docID2);

    @Test
    public void getId() {
        assertEquals(docID1, doc1.getId());
    }

    @Test
    public void EqualityNull() {
        assertNotEquals(null, doc1);
    }

    @SuppressWarnings("AssertBetweenInconvertibleTypes")
    @Test
    public void EqualityDataType() {
        assertNotEquals(docID1, doc1);
    }

    @Test
    public void EqualityIdentical() {
        assertEquals(doc1, doc1);
    }

    @Test
    public void EqualityNewObject() {
        assertEquals(new Document(docID1), doc1);
    }

    @Test
    public void EqualityDifferentObjects() {
        assertNotEquals(doc1, doc2);
    }

    @Test
    public void HashCodeDifferentDataTypes() {
        assertNotEquals(doc1.hashCode(), doc2.hashCode());
    }

    @Test
    public void HashCodeDifferentIdenticalObjects() {
        assertEquals(doc2.hashCode(), doc2Copy.hashCode());
    }

    @Test
    public void compareTo() {
        assertEquals(doc1.compareTo(doc2), docID1.compareTo(docID2));
        assertEquals(doc2.compareTo(doc1), docID2.compareTo(docID1));
    }
}