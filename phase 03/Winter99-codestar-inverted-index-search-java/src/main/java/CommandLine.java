import java.io.InputStream;
import java.io.PrintStream;
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
    private final InputStream inputStream;
    private final PrintStream printStream;

    public CommandLine(Events eventsHandler, String terminationString, InputStream inputStream, PrintStream printStream) {
        this.eventsHandler = eventsHandler;
        this.terminationString = terminationString;
        this.inputStream = inputStream;
        this.printStream = printStream;
    }

    public CommandLine(Events eventsHandler) {
        this(eventsHandler, DEFAULT_TERMINATOR, System.in, System.out);
    }

    public void start() {
        final Scanner scanner = new Scanner(inputStream);
        boolean terminate = false;

        while (!terminate) {
            printStream.printf(
                    "Enter list of words to find in sample english database (finish input by %s (3 dashes))\n",
                    terminationString);
            final ArrayList<String> listOfWords = getListOfWords(scanner);
            printStream.println(eventsHandler.onNewSearchQuery(listOfWords));
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
            printStream.print("\nTerminate (y) or Continue query (n) : ");
            String input = scanner.next().toLowerCase();
            if (input.equals("y")) {
                return true;
            } else if (input.equals("n")) {
                return false;
            } else {
                printStream.println("No choice selected.");
            }
        }
    }
}
