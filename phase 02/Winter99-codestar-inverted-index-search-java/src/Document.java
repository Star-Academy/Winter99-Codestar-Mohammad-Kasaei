import java.util.Objects;

public class Document implements Comparable<Document> {
    private final String fullPath;
    private final String name;

    public Document(String fullPath, String name) {
        this.fullPath = fullPath;
        this.name = name;
    }

    public String getFullPath() {
        return fullPath;
    }

    public String getName() {
        return name;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Document document = (Document) o;
        return Objects.equals(fullPath, document.fullPath) && Objects.equals(name, document.name);
    }

    @Override
    public int hashCode() {
        return Objects.hash(fullPath, name);
    }

    @Override
    public int compareTo(Document o) {
        return fullPath.compareTo(o.fullPath);
    }

    @Override
    public String toString() {
        return String.format("{Document name : %s , path : %s}", getName(), getFullPath());
    }
}
