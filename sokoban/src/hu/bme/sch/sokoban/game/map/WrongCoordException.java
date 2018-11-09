package hu.bme.sch.sokoban.game.map;

public class WrongCoordException extends Exception {
    private String fieldName;

    public WrongCoordException(String fieldName) {
        super("syntax error: " + fieldName);
        this.fieldName = fieldName;
    }

    public String getFieldName() {
        return fieldName;
    }
}
