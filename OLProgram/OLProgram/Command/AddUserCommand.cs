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

        // TODO: Users tilføjes til "_users" (dvs BaseVM.Users) men gemmes kun i hukommelsen
        // -> Hvis programmet uventet lukker ned vil disse ændringer ikke blive gemt.

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
