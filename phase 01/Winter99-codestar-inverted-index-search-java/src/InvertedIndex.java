import java.util.*;
import java.util.function.Predicate;

public class InvertedIndex {
    private final Tokenizer tokenizer;
    private final Map<String, Set<String>> data;

    public InvertedIndex() {
        tokenizer = new Tokenizer();
        data = new HashMap<>();
    }

    public void addDocument(String docID, String content) {
        String[] tokens = tokenizer.getTokens(content);
        for (String tokenValue : tokens) {
            if (!data.containsKey(tokenValue)) {
                data.put(tokenValue, new HashSet<>());
            }
            data.get(tokenValue).add(docID);
        }
    }

    public void addDocuments(Map<String, String> docs) {
        for (Map.Entry<String, String> entry : docs.entrySet()) {
            addDocument(entry.getKey(), entry.getValue());
        }
    }

    private Set<String> query(String word) {
        word = tokenizer.normalizeToken(word);
        return queryToken(word);
    }

    private Set<String> queryToken(String word) {
        return data.getOrDefault(word, new HashSet<>());
    }

    public Set<String> query(String... words) {
        HashSet<String> plusWords = new HashSet<>();
        HashSet<String> noSignWords = new HashSet<>();
        HashSet<String> minusWords = new HashSet<>();
        for (String w : words) {
            w = tokenizer.normalizeToken(w);
            switch (w.charAt(0)) {
                case '+':
                    plusWords.add(w.substring(1));
                    break;
                case '-':
                    minusWords.add(w.substring(1));
                    break;
                default:
                    noSignWords.add(w);
                    break;
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
            ansDocs.removeIf(new Predicate<String>() {
                @Override
                public boolean test(String doc) {
                    return (!plusDocs.contains(doc)) || (!noSignDocs.contains(doc));
                }
            });
        }
        ansDocs.removeIf(new Predicate<String>() {
            @Override
            public boolean test(String doc) {
                return minusDocs.contains(doc);
            }
        });

        return ansDocs;
    }

    private Set<String> getUnionOfDocsContainingWords(Set<String> tokens) {
        final Set<String> result = new HashSet<>();
        for (String token : tokens) {
            result.addAll(queryToken(token));
        }
        return result;
    }

    private Set<String> getIntersectionOfDocsContainingWords(Set<String> tokens) {
        Set<String> result = null;
        for (String token : tokens) {
            if (result == null) {
                result = new HashSet<>(queryToken(token));
            } else {
                result.retainAll(queryToken(token));
                if (result.isEmpty())
                    break;
            }
        }
        return result;
    }
}
