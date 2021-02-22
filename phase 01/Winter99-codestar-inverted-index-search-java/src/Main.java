import java.util.Map;

public class Main {
    public static void main(String[] args){
        TextFileReader reader = new TextFileReader();
        Map<String , String> result = reader.readAllFileInFolder("data");
        System.out.println(result);
    }
}