public class Tokenizer {

    private static final String INVALID_TOKENS_REGEX = "\\W+";

    /**
     * Extract content of the file to tokens
     *
     * @param content String of text in the file to be tokenized
     * @return list of all valid tokens (Just alpha numeric chars are valid in tokens)
     */
    public String[] getTokens(String content) {
        content = normalizeContent(content);
        return content.split(INVALID_TOKENS_REGEX);
    }

    /**
     * Converts token to its normal form (lower case chars and also remove non alphanumerics)
     *
     * @param word input string
     * @return normalized valid token
     */
    public String normalizeToken(String word) {
        word = word.toLowerCase();
        word = word.replaceAll(INVALID_TOKENS_REGEX, "");
        return word;
    }

    /**
     * Converts token to its normal form (lower case chars)
     *
     * @param content input string
     * @return normalized content
     */
    private String normalizeContent(String content) {
        content = content.toLowerCase();
        return content;
    }
}
