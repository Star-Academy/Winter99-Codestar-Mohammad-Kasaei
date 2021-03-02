import org.junit.Before;
import org.junit.Test;

import java.util.*;

import static org.junit.Assert.*;


public class InvertedIndexTest {

    private InvertedIndex index;
    private final Document doc1 = new Document("Doc1");
    private final Document doc2 = new Document("Doc2");
    private final Document doc3 = new Document("Doc3");
    private final Document doc3_ = new Document("Doc3_");
    private final Document doc4 = new Document("Doc4");

    @Before
    public void setup() {
        final Map<Document, String> docsData = new HashMap<Document, String>() {{
            put(doc1, "this is simple doc one 1 and also contains some more info about every thing in the worlds");
            put(doc2, "this is simple doc one 2 and have nothing about reality");
            put(doc3, "this is simple doc one 3 and is super imaginary to be used in earth");
            put(doc3_, "this is simple doc one after doc 3 and is super imaginary to be used in earth(But is spam)");
            put(doc4, "how is going these days :>>>>)))");
        }};
        index = new InvertedIndex();
        index.addDocuments(docsData);
    }

    @Test
    public void searchIs() {
        runSearch("is", new HashSet<>(Arrays.asList(doc1, doc2, doc3, doc3_, doc4)));
    }

    @Test
    public void searchSimple() {
        runSearch("simple", new HashSet<>(Arrays.asList(doc1, doc2, doc3, doc3_)));
    }

    @Test
    public void searchDays() {
        runSearch("days", new HashSet<>(Collections.singletonList(doc4)));
    }

    public void runSearch(String keyword, Set<Document> expected) {
        assertNotNull(index);
        final Set<Document> found = index.search(new Token(keyword));
        assertEquals(found, expected);
    }
}