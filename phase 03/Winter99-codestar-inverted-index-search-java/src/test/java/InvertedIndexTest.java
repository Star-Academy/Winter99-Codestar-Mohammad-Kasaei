import org.junit.Before;
import org.junit.Test;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

import static org.junit.jupiter.api.Assertions.*;


public class InvertedIndexTest {

    private InvertedIndex index;
    private final Document doc1 = new Document("Doc1");
    private final Document doc2 = new Document("Doc2");
    private final Document doc3 = new Document("Doc3");
    private final Document doc3_ = new Document("Doc3_");
    private final Document doc4 = new Document("Doc4");

    @Before
    public void setup() {
        final Map<Document, String> docsData = new HashMap<>();
        docsData.put(doc1,
                "this is simple doc one 1 and also contains some more info about every thing in the worlds");
        docsData.put(doc2,
                "this is simple doc one 2 and have nothing about reality");
        docsData.put(doc3,
                "this is simple doc one 3 and is super imaginary to be used in earth");
        docsData.put(doc3_,
                "this is simple doc one after doc 3 and is super imaginary to be used in earth(But is spam)");
        docsData.put(doc4,
                "how is going these days :>>>>)))");

        index = new InvertedIndex();
        index.addDocuments(docsData);
    }

    @Test
    public void searchIs() {
        assertNotNull(index);
        Set<Document> expected = new HashSet<>();
        expected.add(doc1);
        expected.add(doc2);
        expected.add(doc3);
        expected.add(doc3_);
        expected.add(doc4);
        Set<Document> found = index.search(new Token("is"));
        assertEqualsDocsSets(found, expected);
    }

    @Test
    public void searchSimple() {
        assertNotNull(index);
        Set<Document> expected = new HashSet<>();
        expected.add(doc1);
        expected.add(doc2);
        expected.add(doc3);
        expected.add(doc3_);
        Set<Document> found = index.search(new Token("simple"));
        assertEqualsDocsSets(found, expected);
    }

    @Test
    public void searchDays() {
        assertNotNull(index);
        Set<Document> expected = new HashSet<>();
        expected.add(doc4);
        Set<Document> found = index.search(new Token("days"));
        assertEqualsDocsSets(found, expected);
    }

    public void assertEqualsDocsSets(Set<Document> docs1, Set<Document> docs2) {
        assertEquals(docs1.size(), docs2.size());
        for (Document d : docs1) {
            assertTrue(docs2.contains(d));
        }
    }
}