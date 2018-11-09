package hu.bme.sch.sokoban.game.map;

public class WrongDirectionFormat extends Exception {
    private String fieldName;

    public WrongDirectionFormat(String fieldName) {
        super("syntax error: " + fieldName);
        this.fieldName = fieldName;
    }

    public String getFieldName() {
        return fieldName;
    }
}
