import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;
import java.util.TreeSet;

public class QueryEngine {
    private final InvertedIndex index;

    public QueryEngine(InvertedIndex index) {
        this.index = index;
    }

    public Set<String> query(ArrayList<String> words) {
        HashSet<Token> plusWords = new HashSet<>();
        HashSet<Token> noSignWords = new HashSet<>();
        HashSet<Token> minusWords = new HashSet<>();
        for (String w : words) {
            switch (w.charAt(0)) {
                case '+':
                    plusWords.add(new Token(w.substring(1)));
                    break;
                case '-':
                    minusWords.add(new Token(w.substring(1)));
                    break;
                default:
                    noSignWords.add(new Token(w));
            }
        }
        return query(plusWords, noSignWords, minusWords);
    }


    public Set<String> query(Set<Token> plusWords,
                             Set<Token> noSignWords,
                             Set<Token> minusWords) {
        final Set<String> plusDocs = getUnionOfDocsContainingWords(plusWords);
        final Set<String> noSignDocs = getIntersectionOfDocsContainingWords(noSignWords);
        final Set<String> minusDocs = getUnionOfDocsContainingWords(minusWords);
        Set<String> ansDocs = new HashSet<>();
        if (plusDocs.isEmpty()) {
            ansDocs.addAll(noSignDocs);
        } else if (noSignDocs.isEmpty()) {
            ansDocs.addAll(plusDocs);
        } else {
            ansDocs.addAll(plusDocs);
            ansDocs.addAll(noSignDocs);
            ansDocs.removeIf(doc -> (!plusDocs.contains(doc)) || (!noSignDocs.contains(doc)));
        }
        ansDocs.removeIf(minusDocs::contains);

        return ansDocs;
    }


    private Set<String> getUnionOfDocsContainingWords(Set<Token> tokens) {
        final Set<String> result = new HashSet<>();
        for (Token token : tokens) {
            result.addAll(index.query(token));
        }
        return result;
    }

    private Set<String> getIntersectionOfDocsContainingWords(Set<Token> tokens) {
        Set<String> result = null;
        for (Token token : tokens) {
            if (result == null) {
                result = new HashSet<>(index.query(token));
            } else {
                result.retainAll(index.query(token));
                if (result.isEmpty())
                    break;
            }
        }
        if (result == null) {
            return new TreeSet<>();
        } else {
            return result;
        }
    }
}
