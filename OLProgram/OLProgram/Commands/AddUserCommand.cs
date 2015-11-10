using OLProgram.OLModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OLProgram.Commands
{
    public class AddUserCommand : IUndoRedoCommand
    {
        private ObservableCollection<User> users;

        private User user;

        public AddUserCommand(ObservableCollection<User> _users, User _user)
        {
            users = _users;
            user = _user;
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
