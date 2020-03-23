using System;
using System.Collections.Generic;
using System.Text;

namespace BoB.BaseModule.Test.AdaptersandDecorators
{
    public class ToolbarButton
    {
        ICommand _command;
        String _commandText;

        public string CommandText
        {
            get { return _commandText; }
        }

        public ToolbarButton(ICommand command,string commandText)
        {
            _command = command;
            _commandText = commandText;
        }

        public void Click()
        {
            _command.ToDo();
        }

    }
}
