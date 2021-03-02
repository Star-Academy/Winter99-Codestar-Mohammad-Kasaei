import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Set;
import java.util.TreeSet;

import static org.junit.Assert.assertEquals;
import static org.mockito.Mockito.when;

public class QueryEngineTest {

    @Mock
    InvertedIndex invertedIndex;

    private QueryEngine engine;
    private final Token carToken = new Token("car");
    private final Token repairToken = new Token("repair");
    private final Token buyToken = new Token("buy");
    private final Token dishwasherToken = new Token("dishwasher");

    private final Document repairBuyCar = new Document("Repair Buy Car");
    private final Document buyCar = new Document("Buy Car");
    private final Document repairCar = new Document("Repair Car");
    private final Document repairDishwasher = new Document("Repair Dishwasher");
    private final Document buyDishwasher = new Document("BUY Dishwasher");

    @Before
    public void setUp() {
        MockitoAnnotations.initMocks(this);
        engine = new QueryEngine(invertedIndex);

        when(invertedIndex.search(carToken))
                .thenReturn(new TreeSet<>(Arrays.asList(repairBuyCar, buyCar, repairCar)));
        when(invertedIndex.search(repairToken))
                .thenReturn(new TreeSet<>(Arrays.asList(repairBuyCar, repairCar, repairDishwasher)));
        when(invertedIndex.search(buyToken))
                .thenReturn(new TreeSet<>(Arrays.asList(repairBuyCar, buyCar, buyDishwasher)));
        when(invertedIndex.search(dishwasherToken))
                .thenReturn(new TreeSet<>(Arrays.asList(repairDishwasher, buyDishwasher)));
    }

    @After
    public void tearDown() {
    }

    @Test
    public void andTest() {
        final Set<Document> expectedDocs = new TreeSet<>(
                Arrays.asList(repairBuyCar, buyCar)
        );
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("Car", "buy")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(gotDocs, expectedDocs);
    }

    @Test
    public void orTest() {
        final Set<Document> expectedDocs = new TreeSet<>(
                Arrays.asList(repairBuyCar, buyCar, repairCar, buyDishwasher, repairDishwasher)
        );
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("+repair", "+buy")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(gotDocs, expectedDocs);
    }

    @Test
    public void complex1Test() {
        final Set<Document> expectedDocs = new TreeSet<>();
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("+dishwasher", "repair", "buy")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expectedDocs, gotDocs);
    }

    @Test
    public void complex2Test() {
        final Set<Document> expectedDocs = new TreeSet<>();
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("+car", "+dishwasher", "-buy", "-repair")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expectedDocs, gotDocs);
    }

    @Test
    public void complex3Test() {
        final Set<Document> expectedDocs = new TreeSet<>(
                Arrays.asList(repairCar, repairDishwasher)
        );
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("+car", "+dishwasher", "-buy")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expectedDocs, gotDocs);
    }

    @Test
    public void complex4Test() {
        final Set<Document> expectedDocs = new TreeSet<>(
                Arrays.asList(repairBuyCar, buyCar, buyDishwasher)
        );
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("+car", "+dishwasher", "buy")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expectedDocs, gotDocs);
    }

    @Test
    public void complex5Test() {
        final Set<Document> expectedDocs = new TreeSet<>();
        final ArrayList<String> listOfWords = new ArrayList<>(
                Arrays.asList("car", "dishwasher")
        );
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expectedDocs, gotDocs);
    }

}