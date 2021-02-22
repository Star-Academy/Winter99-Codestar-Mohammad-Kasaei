public class Tokenizer {

    public String[] getTokens(String content) {
        content = normalizeContent(content);
        return content.split("\\W+");
    }

    public String normalizeToken(String word) {
        word = word.toLowerCase();
        return word;
    }


    public String normalizeContent(String content) {
        content = content.toLowerCase();
        return content;
    }
}
