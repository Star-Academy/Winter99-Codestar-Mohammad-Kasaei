import java.util.ArrayList;
import java.util.Map;
import java.util.Scanner;

public class Main {
    public static void main(String[] args) {
        final String path = "EnglishData";
//        final String path = "SimpleData";

        TextFileReader reader = new TextFileReader();
        Tokenizer tokenizer = new Tokenizer();
        InvertedIndex invertedIndex = new InvertedIndex(tokenizer);

        Map<String, String> docs = reader.readAllFileInFolder(path);
        invertedIndex.addDocuments(docs);

        QueryEngine queryEngine = new QueryEngine(invertedIndex);

        Scanner scanner = new Scanner(System.in);
        ArrayList<String> words = new ArrayList<>();
        boolean terminate = false;

        while (!terminate) {
            System.out.println("Enter list of words to find in sample english database (finish input by --- (3 dash))");
            while (true) {
                String input = scanner.next();
                if (input.equals("---")) {
                    System.out.println(queryEngine.query(words));
                    break;
                }
                words.add(input);
            }
            while (true) {
                System.out.print("\nTerminate (y) or Continue query (n) : ");
                String input = scanner.next().toLowerCase();
                if (input.equals("y")) {
                    terminate = true;
                    break;
                } else if (input.equals("n")) {
                    break;
                } else {
                    System.out.println("No choice selected.");
                }
            }
        }
    }
}