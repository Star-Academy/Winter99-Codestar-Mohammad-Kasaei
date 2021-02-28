import java.util.Objects;

public class Document implements Comparable<Document> {
    private final String id;

    public Document(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Document document = (Document) o;
        return Objects.equals(id, document.id);
    }

    @Override
    public int hashCode() {
        return Objects.hash(id);
    }

    @Override
    public int compareTo(Document object) {
        return id.compareTo(object.id);
    }

    @Override
    public String toString() {
        return String.format("{Document id : %s}", getId());
    }
}
