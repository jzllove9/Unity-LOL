using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public delegate void WarningResult();
public class WarningModel
{
    public WarningResult result;
    public string value;

    public WarningModel(string value, WarningResult result = null)
    {
        this.result = result;
        this.value = value;
    }


}

