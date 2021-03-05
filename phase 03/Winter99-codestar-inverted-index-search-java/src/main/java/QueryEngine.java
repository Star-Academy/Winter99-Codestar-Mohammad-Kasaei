import java.util.ArrayList;
import java.util.HashSet;
import java.util.Set;
import java.util.TreeSet;

public class QueryEngine {
    private final InvertedIndex index;

    public QueryEngine(InvertedIndex index) {
        this.index = index;
    }

    /**
     * @param words list of words to query (words can have + or - tag prefixes or no prefix)
     * @return the result of advanced search mentioned in the docs
     */
    public Set<Document> advancedSearch(ArrayList<String> words) {
        final Set<Token> plusWords = new HashSet<>();
        final Set<Token> noSignWords = new HashSet<>();
        final Set<Token> minusWords = new HashSet<>();
        for (String w : words) {
            String tokenString;
            Set<Token> tokenSet;
            switch (w.charAt(0)) {
                case '+':
                    tokenString = w.substring(1);
                    tokenSet = plusWords;
                    break;
                case '-':
                    tokenString = w.substring(1);
                    tokenSet = minusWords;
                    break;
                default:
                    tokenString = w;
                    tokenSet = noSignWords;
            }
            tokenSet.add(new Token(tokenString));
        }
        return advancedSearch(plusWords, noSignWords, minusWords);
    }


    /**
     * Performs advanced query mentioned in the project documents
     *
     * @param plusWords   list of words with + tag
     * @param noSignWords list of words with no sign
     * @param minusWords  list of words with - tag
     * @return the result is the set of documents the advanced search mechanism as mentioned in phase 1 documentations
     */
    public Set<Document> advancedSearch(Set<Token> plusWords,
                                        Set<Token> noSignWords,
                                        Set<Token> minusWords) {
        final Set<Document> plusDocs = getUnionOfDocsContainingWords(plusWords);
        final Set<Document> noSignDocs = getIntersectionOfDocsContainingWords(noSignWords);
        final Set<Document> minusDocs = getUnionOfDocsContainingWords(minusWords);
        final Set<Document> ansDocs = new HashSet<>();
        if (plusDocs.isEmpty()) {
            ansDocs.addAll(noSignDocs);
        } else if (noSignDocs.isEmpty()) {
            ansDocs.addAll(plusDocs);
        } else {
            ansDocs.addAll(plusDocs);
            ansDocs.retainAll(noSignDocs);
        }
        ansDocs.removeIf(minusDocs::contains);

        return ansDocs;
    }


    /**
     * @param tokens list of tokens
     * @return set of all docs containing at least one of the tokens in <code>token</code>
     */
    private Set<Document> getUnionOfDocsContainingWords(Set<Token> tokens) {
        final Set<Document> result = new HashSet<>();
        for (Token token : tokens) {
            result.addAll(index.search(token));
        }
        return result;
    }

    /**
     * @param tokens set of tokens
     * @return set of all documents containing every token in {@code tokens}
     */
    private Set<Document> getIntersectionOfDocsContainingWords(Set<Token> tokens) {
        final Set<Document> result = new TreeSet<>();
        for (Token token : tokens) {
            if (result.isEmpty()) {
                result.addAll(index.search(token));
            } else {
                result.retainAll(index.search(token));
                if (result.isEmpty())
                    break;
            }
        }
        return result;
    }
}
