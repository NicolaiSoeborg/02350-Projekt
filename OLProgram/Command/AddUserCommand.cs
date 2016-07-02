using OLModel;
using System.Collections.ObjectModel;

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
            _users.Add(_user);
        }

        public void UnExecute()
        {
            _users.Remove(_user);
        }
    }
}
