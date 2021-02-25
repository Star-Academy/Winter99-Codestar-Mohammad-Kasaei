import java.util.ArrayList;
import java.util.Map;
import java.util.Scanner;

public class Main {

    private static QueryEngine queryEngine;
    private static final String path = "EnglishData";
    private static final String TERMINATION_STRING = "---";

    public static void main(String[] args) {
        initializeEngine();
        startCLI();
    }

    /**
     * This method starts the Command line interface to interact with the user
     * prints some messages and read responses
     */
    private static void startCLI() {
        final Scanner scanner = new Scanner(System.in);
        boolean terminate = false;

        while (!terminate) {
            System.out.printf("Enter list of words to find in sample english database (finish input by %s (3 dashes))\n", TERMINATION_STRING);
            ArrayList<String> listOfWords = getListOfWords(scanner);
            System.out.println(queryEngine.query(listOfWords));
            terminate = askToTerminate(scanner);
        }
        scanner.close();
    }


    /**
     * Ask the users whether it was the last query or no
     *
     * @param scanner scanner for the input stream to the user
     * @return true if the program must terminate and no more query is asked otherwise false
     */
    private static boolean askToTerminate(Scanner scanner) {
        while (true) {
            System.out.print("\nTerminate (y) or Continue query (n) : ");
            String input = scanner.next().toLowerCase();
            if (input.equals("y")) {
                return true;
            } else if (input.equals("n")) {
                return false;
            } else {
                System.out.println("No choice selected.");
            }
        }
    }

    /**
     * Read a list word until {@code TERMINATION_STRING} from the user input terminal
     *
     * @param scanner scanner for the input stream to the user
     * @return list of all words users requested to query ( not including the stop phrase )
     */
    private static ArrayList<String> getListOfWords(Scanner scanner) {
        final ArrayList<String> words = new ArrayList<>();
        while (true) {
            String input = scanner.next();
            if (input.equals(TERMINATION_STRING)) {
                break;
            }
            words.add(input);
        }
        return words;
    }

    /**
     * Basic initialization for the inverted index class and query engine
     * to perform for the rest of the program
     */
    private static void initializeEngine() {
        final TextFileReader reader = new TextFileReader();
        final InvertedIndex invertedIndex = new InvertedIndex();

        final Map<Document, String> docs = reader.readAllFileInFolder(path);
        invertedIndex.addDocuments(docs);

        queryEngine = new QueryEngine(invertedIndex);
    }
}
