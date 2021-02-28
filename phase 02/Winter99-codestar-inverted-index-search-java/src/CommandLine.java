import java.util.ArrayList;
import java.util.Scanner;
import java.util.Set;

public class CommandLine {
    private static final String DEFAULT_TERMINATOR = "---";

    public interface Events {
        Set<Document> onNewSearchQuery(ArrayList<String> words);
    }

    private final Events eventsHandler;
    private final String terminationString;

    public CommandLine(Events eventsHandler, String terminationString) {
        this.eventsHandler = eventsHandler;
        this.terminationString = terminationString;
    }

    public CommandLine(Events eventsHandler) {
        this(eventsHandler, DEFAULT_TERMINATOR);
    }

    public void start() {
        final Scanner scanner = new Scanner(System.in);
        boolean terminate = false;

        while (!terminate) {
            System.out.printf(
                    "Enter list of words to find in sample english database (finish input by %s (3 dashes))\n",
                    terminationString);
            final ArrayList<String> listOfWords = getListOfWords(scanner);
            System.out.println(eventsHandler.onNewSearchQuery(listOfWords));
            terminate = askToTerminate(scanner);
        }
        scanner.close();
    }

    private ArrayList<String> getListOfWords(Scanner scanner) {
        final ArrayList<String> words = new ArrayList<>();
        while (true) {
            String input = scanner.next();
            if (input.equals(terminationString)) {
                break;
            }
            words.add(input);
        }
        return words;
    }

    private boolean askToTerminate(Scanner scanner) {
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
}
