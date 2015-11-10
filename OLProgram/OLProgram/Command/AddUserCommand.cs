using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Command
{
    public class AddUserCommand : IUndoRedoCommand
    {
        private ObservableCollection<User> _users;
        private User _user;

        public AddUserCommand(ObservableCollection<User> users, User user)
        {
            _users = users;
            _user = user;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void UnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
