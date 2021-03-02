import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import java.io.*;
import java.util.ArrayList;
import java.util.Set;
import java.util.TreeSet;

import static org.junit.Assert.*;

public class CommandLineTest {

    private File inFile;
    private File outFile;
    private InputStream inputStream;
    private PrintStream printStream;

    @Before
    public void setUp() throws Exception {
        inFile = new File("in");
        outFile = new File("out");
        if (!inFile.exists()) {
            inFile.createNewFile();
        }
        if (!outFile.exists()) {
            outFile.createNewFile();
        }
        inputStream = new FileInputStream(inFile);
        printStream = new PrintStream(outFile);
    }

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
        ArrayList<ArrayList<String>> expectedQueries = new ArrayList<>();
        ArrayList<String> expectedQuery = new ArrayList<>();
        expectedQuery.add("hello");
        expectedQuery.add("Mohammad");
        expectedQuery.add("Me");
        expectedQueries.add(expectedQuery);
        writeToInputFile("hello\nMohammad\nMe\n---\ny");
        ArrayList<ArrayList<String>> queries = new ArrayList<>();
        CommandLine commandLine = new CommandLine(new CommandLine.Events() {
            @Override
            public Set<Document> onNewSearchQuery(ArrayList<String> words) {
                queries.add(words);
                return new TreeSet<>();
            }
        }, CommandLine.DEFAULT_TERMINATOR, inputStream, printStream);
        commandLine.start();
        assertEquals(expectedQueries.size(), queries.size());
        assertEquals(expectedQueries, queries);
    }

    @Test
    public void twoQueriesTest() {
        final ArrayList<ArrayList<String>> expectedQueries = new ArrayList<>();
        ArrayList<String> q = new ArrayList<>();
        q.add("hello");
        q.add("Mohammad");
        q.add("Me");
        expectedQueries.add(q);
        q = new ArrayList<>();
        q.add("Ali");
        q.add("Reza");
        expectedQueries.add(q);

        writeToInputFile("hello\nMohammad\nMe\n---\nn\nAli\nReza\n---\ny");
        ArrayList<ArrayList<String>> queries = new ArrayList<>();
        CommandLine commandLine = new CommandLine(new CommandLine.Events() {
            @Override
            public Set<Document> onNewSearchQuery(ArrayList<String> words) {
                queries.add(words);
                return new TreeSet<>();
            }
        }, CommandLine.DEFAULT_TERMINATOR, inputStream, printStream);
        commandLine.start();
        assertEquals(expectedQueries.size(), queries.size());
        assertEquals(expectedQueries, queries);
    }

    @Test
    public void emptyTest() {
        writeToInputFile("---\nnothing\ny");
        ArrayList<ArrayList<String>> queries = new ArrayList<>();
        CommandLine commandLine = new CommandLine(new CommandLine.Events() {
            @Override
            public Set<Document> onNewSearchQuery(ArrayList<String> words) {
                queries.add(words);
                return new TreeSet<>();
            }
        }, CommandLine.DEFAULT_TERMINATOR, inputStream, printStream);
        commandLine.start();
        assertEquals(1, queries.size());
        assertEquals(0, queries.get(0).size());
    }
}