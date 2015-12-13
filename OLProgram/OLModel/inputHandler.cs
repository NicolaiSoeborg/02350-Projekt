using System.ComponentModel;

namespace OLModel
{
    public class InputHandler : INotifyPropertyChanged
    {
        private string input;

        public event PropertyChangedEventHandler PropertyChanged;

        public InputHandler()
        {
        }

        public InputHandler(string value)
        {
            this.input = value;
        }

        public string InputGetSetter 
        {
            get { return input; }
            set
            {
                input = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged("inputGetSetter");
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string input)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(input));
            }
        }
    }
}
