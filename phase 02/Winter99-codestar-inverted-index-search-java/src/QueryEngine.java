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
        HashSet<String> plusWords = new HashSet<>();
        HashSet<String> noSignWords = new HashSet<>();
        HashSet<String> minusWords = new HashSet<>();
        for (String w : words) {
            switch (w.charAt(0)) {
                case '+':
                    plusWords.add(w.substring(1));
                    break;
                case '-':
                    minusWords.add(w.substring(1));
                    break;
                default:
                    noSignWords.add(w);
            }
        }
        return query(plusWords, noSignWords, minusWords);
    }


    public Set<String> query(Set<String> plusWords,
                             Set<String> noSignWords,
                             Set<String> minusWords) {
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


    private Set<String> getUnionOfDocsContainingWords(Set<String> tokens) {
        final Set<String> result = new HashSet<>();
        for (String token : tokens) {
            result.addAll(index.query(token));
        }
        return result;
    }

    private Set<String> getIntersectionOfDocsContainingWords(Set<String> tokens) {
        Set<String> result = null;
        for (String word : tokens) {
            if (result == null) {
                result = new HashSet<>(index.query(word));
            } else {
                result.retainAll(index.query(word));
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
