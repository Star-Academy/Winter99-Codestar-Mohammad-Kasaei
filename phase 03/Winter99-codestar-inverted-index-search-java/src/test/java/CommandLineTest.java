import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import java.io.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.TreeSet;

import static org.junit.Assert.assertEquals;

public class CommandLineTest {

    private File inFile;
    private File outFile;
    private InputStream inputStream;
    private PrintStream printStream;

    @SuppressWarnings("ResultOfMethodCallIgnored")
    @Before
    public void setUp() throws Exception {
        inFile = new File("in");
        outFile = new File("out");
        inFile.createNewFile();
        outFile.createNewFile();
        inputStream = new FileInputStream(inFile);
        printStream = new PrintStream(outFile);
    }

    @SuppressWarnings("ResultOfMethodCallIgnored")
    @After
    public void tearDown() throws Exception {
        inputStream.close();
        printStream.close();
        inFile.delete();
        outFile.delete();
    }

    private void writeToInputFile(String text) {
        try {
            final PrintStream stream = new PrintStream(inFile);
            stream.print(text);
            stream.close();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }

    @Test
    public void oneQueryTest() {
        ArrayList<ArrayList<String>> expectedQueries = new ArrayList<>(
                Collections.singletonList(new ArrayList<>(
                        Arrays.asList(
                                "hello",
                                "Mohammad",
                                "Me"
                        )
                ))
        );
        runSearch("hello\nMohammad\nMe\n---\ny", expectedQueries);
    }

    @Test
    public void twoQueriesTest() {
        final ArrayList<ArrayList<String>> expectedQueries = new ArrayList<>(
                Arrays.asList(
                        new ArrayList<>(Arrays.asList("hello", "Mohammad", "Me")),
                        new ArrayList<>(Arrays.asList("Ali", "Reza"))
                )
        );
        runSearch("hello\nMohammad\nMe\n---\nn\nAli\nReza\n---\ny", expectedQueries);
    }

    @Test
    public void emptyTest() {
        runSearch("---\nnothing\ny", new ArrayList<>(Collections.singletonList(new ArrayList<>())));
    }

    public void runSearch(String inputString, ArrayList<ArrayList<String>> expectedQueries) {
        writeToInputFile(inputString);
        final ArrayList<ArrayList<String>> gotQueries = new ArrayList<>();
        CommandLine commandLine = new CommandLine(
                words -> {
                    gotQueries.add(words);
                    return new TreeSet<>();
                }, CommandLine.DEFAULT_TERMINATOR, inputStream, printStream);
        commandLine.start();
        assertEquals(gotQueries, expectedQueries);
    }
}