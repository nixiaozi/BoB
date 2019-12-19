using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class ToolbarButton
    {
        ICommand _command;

        public ToolbarButton(ICommand command,string commandText)
        {
            _command = command;
        }

        public void Click()
        {
            _command.ToDo();
        }

    }
}
