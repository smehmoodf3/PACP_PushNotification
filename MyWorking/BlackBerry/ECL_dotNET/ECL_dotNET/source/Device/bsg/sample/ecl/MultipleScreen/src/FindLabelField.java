package src;

import net.rim.device.api.ui.Graphics;
import net.rim.device.api.ui.component.EditField;

public class FindEditField extends EditField {

    public FindLabelField() {
        super("Find: ","");
    }

    public void paint(Graphics g) {
        super.paint(g);
        int xOffset = getFont().getAdvance(getText()) + 2;
        g.fillRect(xOffset,0,5,getHeight());
    }
}
