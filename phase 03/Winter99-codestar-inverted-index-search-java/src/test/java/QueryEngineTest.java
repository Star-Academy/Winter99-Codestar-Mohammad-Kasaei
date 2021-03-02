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

    @Test
    public void andTest() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("Car", "buy")
                ),
                new TreeSet<>(
                        Arrays.asList(repairBuyCar, buyCar)
                )
        );
    }

    @Test
    public void orTest() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("+repair", "+buy")
                ),
                new TreeSet<>(
                        Arrays.asList(repairBuyCar, buyCar, repairCar, buyDishwasher, repairDishwasher)
                )
        );
    }

    @Test
    public void andNotComplexTest() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("+car", "+dishwasher", "-buy", "-repair")
                ),
                new TreeSet<>()
        );
    }

    @Test
    public void andNotTest() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("+car", "+dishwasher", "-buy")
                ),
                new TreeSet<>(
                        Arrays.asList(repairCar, repairDishwasher)
                )
        );
    }

    @Test
    public void andOr1Test() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("+dishwasher", "repair", "buy")
                ),
                new TreeSet<>()
        );
    }

    @Test
    public void andOr2Test() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("+car", "+dishwasher", "buy")
                ),
                new TreeSet<>(
                        Arrays.asList(repairBuyCar, buyCar, buyDishwasher)
                )
        );
    }

    @Test
    public void andComplexTest() {
        runEngineTest(
                new ArrayList<>(
                        Arrays.asList("car", "dishwasher")
                ),
                new TreeSet<>()
        );
    }


    public void runEngineTest(ArrayList<String> listOfWords, Set<Document> expected) {
        final Set<Document> gotDocs = engine.advancedSearch(listOfWords);
        assertEquals(expected, gotDocs);
    }

}