package hu.bme.sch.sokoban.game.map;

public class WrongLineFormat extends Exception {
    private String fieldName;

    public WrongLineFormat(String fieldName) {
        super("syntax error: " + fieldName);
        this.fieldName = fieldName;
    }

    public String getFieldName() {
        return fieldName;
    }
}
